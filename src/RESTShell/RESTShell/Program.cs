using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Maze;
using RESTAgent.Html;
using Tavis;
using RESTShell.Interface;
using RESTShell.Missions;
using RESTShell.Shell;
using Tavis.Framework;
using System.Net.Http.Formatting;
using Tavis.Tools;

[assembly: log4net.Config.XmlConfigurator(Watch = false)]

namespace RESTShell {
     static class Program {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        /// 
        [STAThread]
        static void Main() {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            log.Info("Starting REST Shell");

            var container = new CompositionContainer(GetMefCatalog());

            ShellModel model = CreateMazeModel(container);
            //var model = CreateTestServerModel(container, registry);


            var shellController = new ShellController(model);
            container.ComposeExportedValue<IShell>(shellController);  // This allows response controllers to inject in the shell


            shellController.Run();

            
        }

        private static ShellModel CreateTestServerModel(CompositionContainer container)
        {
            var rootLink = new Link() {Target = new Uri("http://graphite:9000")};


            var restAgent = new RestAgent();
            restAgent.NavigateTo(rootLink);


            return new ShellModel {
                Container = container,
                RestAgent = restAgent
            };
        }

         private static ShellModel CreateMazeModel(CompositionContainer container) {
             EscapeMaze escapeMaze = CreateEscapeMazeMission();

             var mazeLink = new Link() {Target = new Uri("http://amundsen.com/examples/mazes/2d/five-by-five/")};
             

             var restAgent = new RestAgent();
			 MazeSemanticsProvider.RegisterSemantics(restAgent.SemanticsRegistry);
             restAgent.SemanticsRegistry.RegisterFormatter(new XmlFormatter("application/vnd.amundsen.maze+xml"));
             
             restAgent.SemanticsRegistry.RegisterFormatter(new HtmlFormatter());
             restAgent.SemanticsRegistry.RegisterLinkExtractor(new HtmlLinkExtractor());

             //restAgent.SetAcceptedMediaTypes(new List<MediaTypeWithQualityHeaderValue>(){ new MediaTypeWithQualityHeaderValue("application/vnd.amundsen.maze+xml")});
             restAgent.SetAcceptedMediaTypes(new List<MediaTypeWithQualityHeaderValue>() { new MediaTypeWithQualityHeaderValue("text/html") });

             restAgent.NavigateTo(mazeLink);
           

             return new ShellModel {
                                       Container = container,
                                       DefaultMission = escapeMaze,
                                       RestAgent = restAgent
                                   };
         }

         private static EscapeMaze CreateEscapeMazeMission() {
             
             
             return new EscapeMaze();
         }


         private static AggregateCatalog GetMefCatalog() {
            var aggregateCatalog = new AggregateCatalog();

            var typeCatalog = new TypeCatalog(typeof(IResponseController), typeof(IView));
            aggregateCatalog.Catalogs.Add(typeCatalog);

            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            // aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetAssembly(typeof(HtmlFormatter))));
            // aggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MazeSemanticsProvider).Assembly));
            //if (Directory.Exists("Plugins")) {
            //    var directoryCatalog = new DirectoryCatalog("Plugins");
            //    aggregateCatalog.Catalogs.Add(directoryCatalog);
            //}

            return aggregateCatalog;
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            HandleException(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            HandleException(e.ExceptionObject as Exception);
            ShutDownApplication();
        }

        private static void ShutDownApplication() {
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            System.Windows.Forms.Application.ThreadException -= Application_ThreadException;

            Environment.Exit(0);
        }

        private static void HandleException(Exception ex) {
            if (ex == null)
                return;
            log.Error("Handled Exception: " + ex.Message, ex);

            var exceptions = new List<Exception>();
            exceptions.Add(ex);
            var exceptionForm = new ExceptionDialog();
            exceptionForm.Exceptions = exceptions;
            exceptionForm.ShowDialog();

            //MessageBox.Show(BuildExceptionString(ex));

        }

    }
}
