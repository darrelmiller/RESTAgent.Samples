using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HttpContrib;
using HttpContrib.Tools;
using Microsoft.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tavis.Http;
using TMShopClient.Tests.Fakes;

namespace TMShopClient.Tests {
    [TestClass]
    public class ClientStateTests {

        [TestMethod]
        public void CreateClientState() {
            //Arrange
            
            var handlerRegistry = new MefHandlerRegistry(new TypeCatalog());
            
            //Act
            var clientState = new RestAgent(handlerRegistry);

            //Assert
            Assert.IsNotNull(clientState);
        }

        [Ignore]
        [TestMethod]
        public void NavigateSomewhere() {
            //Arrange
            
            
            var clientState = new RestAgent(new HttpClient(),new MefHandlerRegistry(new TypeCatalog()));
            //Act
            clientState.NavigateTo(new Link());
            //Assert
            Assert.IsNotNull(clientState.CurrentContent);

        }


        [TestMethod]
        public void EmptyTest() {
            //Arrange
                        
            //Act

            //Assert
            
        }


        [TestMethod]
        public void CanTimerBeRestarted() {
            //Arrange
            var timer = new Timer();
            timer.Interval = 1000*3;
            //Act
            timer.Start();
            timer.Start();
            //Assert
            Assert.IsNotNull(timer);
        }

    }
}
