using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using RESTShell.Controllers;
using RESTShell.Interface;
using RESTShell.Interfaces;
using RESTShell.Missions;
using RESTShell.Renderers;
using RESTShell.Tools.Interfaces;


namespace RESTShell.Shell {

    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IShell))]
    public class ShellController : IShell {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ShellForm _View;
        private readonly ShellModel _Model;
        private readonly Dictionary<string, IContentController> _Controllers = new Dictionary<string, IContentController>();
		
        public ShellController(ShellModel model) {
            _Model = model;
			_Model.OnContentChanging += Display;
			_Model.OnContentChanged += UpdateContent;
            _View = new ShellForm();

            
        }

        private void Display() {
            var ctrl = GetController(_Model.CurrentMediaType);
            ctrl.Display(_Model.CurrentLocation);
        }


		private void UpdateContent()
		{
			var ctrl = GetController(_Model.CurrentMediaType);
			ctrl.UpdateContent(_Model.CurrentContent);
		}


        private IContentController GetController(string mediaType) {
            if (!_Controllers.ContainsKey(mediaType)) {
                switch (mediaType) {
                    case "application/vnd.tavis.xaml+xml":
                        _Controllers.Add(mediaType,new XamlController(this) );
                        break;
                    case "application/vnd.amundsen.maze+xml":
                        _Controllers.Add(mediaType, new MazeController(this));
                        break;
                    case "text/plain":
                        _Controllers.Add(mediaType, new PlainTextController(this));
                        break;
                    case "text/html":
                        _Controllers.Add(mediaType,new HtmlController(this));
                        break;
                    
                }   
            }
            return _Controllers[mediaType];
        }

        //private IView GetView(HttpContent currentContent) {
        //    switch(currentContent.Headers.ContentType.MediaType) {
        //        case "application/vnd.tavis.xaml+xml":
        //            return new XamlView(currentContent);
        //        case "text/plain":
        //            return new PlainTextView(currentContent);
        //        case "text/html":
        //            return new HtmlView(currentContent);
        //        case "application/vnd.amundsen.maze+xml":
        //            return new PlainTextView(currentContent);
        //    }
        //    return null;
        //}

        public void Run() {
            
            _Model.InitClientState();
            _View.Show();

            if (_Model.DefaultMission != null) {
                _Model.DefaultMission.Go(_Model.RestAgent);
            }
            Display();

            Application.Run((Form)_View);
        }


        public void ShowView(IView view) {
            _View.ShowView(view);
        }

        public void ShowDialog(IDialog window) {
            window.Show(_View);
        }


        
    }
}
