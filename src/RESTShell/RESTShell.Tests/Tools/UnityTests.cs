using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMShopClient.Tests.Tools {

    [TestClass]
    public class UnityTests {

        [TestMethod]
        public void ResolveExistingInstance() {

            Foo.Container = new UnityContainer();
            var bar = new Bar();

            Foo.Container.RegisterInstance<Bar>(bar, new ExternallyControlledLifetimeManager());

            var foo = new Foo();

            Assert.AreSame(bar, foo.Bar);
        }


        [TestMethod]
        public void ResolveMultipleInstances() {

            var container = new UnityContainer();


            container.RegisterType<IFruit,Apple>("a");
            container.RegisterType<IFruit, Pear>("b");

            var ss = container.ResolveAll<IFruit>();

            Assert.AreEqual(2,ss.Count());
        }

    

    }

    public interface IFruit {
        
    } 
    public class Apple : IFruit {
        
    }

    public class Pear : IFruit {

    }

    public class Foo {
        public static UnityContainer Container { get; set; }
        public Bar Bar { get; set; }

        //public Foo(Bar bar) {
        //    Bar = bar;
        //}

        public Foo() {
            Bar = (Bar) Foo.Container.Resolve(typeof (Bar));
        }
    }

    public class Bar {


    }

}
