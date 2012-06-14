using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMShopClient.Interface;
using TMShopClient.Shell;
using TMShopClient.Tools.Interfaces;

namespace TMShopClient.Tests.Shell {

    [TestClass]
    public class ShellTests {

        [TestMethod]
        public void Iterate_over_stack() {

            var stack = new Stack<String>();

            stack.Push("three");
            stack.Push("blind");
            stack.Push("mice");

            var s = from st in stack
                    select st;

            Assert.AreEqual(3, s.Count());
        }



        [TestMethod]
        public void ManuallyRegisterShell() {
            var aggregateCatalog = new AggregateCatalog();

            var typeCatalog = new TypeCatalog(typeof (FakeController));
            aggregateCatalog.Catalogs.Add(typeCatalog);
            var assemblyCatalog = new AssemblyCatalog(Assembly.Load("TMShopClient"));
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            var container = new CompositionContainer(aggregateCatalog);

            var shell = new ShellController(new ShellModel());
            container.ComposeExportedValue<IShell>(shell);
            var controller = container.GetExportedValue<FakeController>();

            Assert.AreSame(controller.Shell, shell);
        }

        [Export(typeof(FakeController))]
        public class FakeController {

            public IShell Shell;

            [ImportingConstructor]
            public FakeController(IShell shell) {
                Shell = shell;

            }

        }




    }

//    public static class MefExtensions {

//        public static void ComposeExportedValue(this CompositionContainer container, string contractName, object exportedValue) {
//            if (container == null)
//                throw new ArgumentNullException("container");
//            if (exportedValue == null)
//                throw new ArgumentNullException("exportedValue");
//            CompositionBatch batch = new CompositionBatch();
//            var metadata = new Dictionary<string, object> {
//        { "ExportTypeIdentity", AttributedModelServices.GetTypeIdentity(exportedValue.GetType()) }
//    };
//            batch.AddExport(new Export(contractName, metadata, () => exportedValue));
//            container.Compose(batch);
//        }

//        public static void ComposeExportedValue(this CompositionContainer container, object exportedValue) {
//    if (container == null)
//        throw new ArgumentNullException("container");
//    if (exportedValue == null)
//        throw new ArgumentNullException("exportedValue");
//    ComposeExportedValue(container, AttributedModelServices.GetContractName(exportedValue.GetType(), exportedValue);
//}

  //  }
}
