using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tavis;
using Tavis.Tools;

namespace Microblog.Tests {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class InteractionTests {
       

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetRootDoc() {
            var agent = new RestAgent();

            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/")});
            
            var content = agent.CurrentContent;

            //Assert.AreEqual("application/xhtml+xml",content.Headers.ContentType);
            
            Assert.IsNotNull(content);
        }

        [TestMethod]
        public void GetALink() {
            var agent = new RestAgent();
            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/") });
           
            var content = agent.CurrentContent;

            var indexLink = content.GetLink<Index>();
            var usersAllLink = content.GetLink<UsersAll>();
            var userAddLink = content.GetLink<UserAdd>();
            var messagesSearchForm = content.GetLink<MessagesSearchForm>();
            
            //Assert.AreEqual("application/xhtml+xml",content.Headers.ContentType);
            
            Assert.IsNotNull(indexLink);
            Assert.IsNotNull(messagesSearchForm);
            //Assert.IsNotNull(usersAllLink);
            Assert.IsNotNull(userAddLink);
        }

        [TestMethod]
        public void RegisterAUser() {
            var agent = new RestAgent();
            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/") });
            
    
            var content = agent.CurrentContent;

            var userAddLink = content.GetLink<UserAdd>();
            agent.NavigateTo(userAddLink);
            
            content = agent.CurrentContent;
            var userAddForm = content.GetLink<UserAddForm>();
            userAddForm.User = "darrel";
            userAddForm.Password = "foome";
            userAddForm.Email = "darrel@tavis.ca";
            userAddForm.Name = "Darrel Miller";

            agent.NavigateTo(userAddForm);
            var finalContent = agent.CurrentContent;  // Force request to happen.

            Assert.AreEqual(HttpStatusCode.OK, agent.CurrentStatusCode);
            
        }


        [TestMethod]
        public void PostMessage() {
            var agent = new RestAgent();
            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/") });
            
            agent.SetBasicCredentials("darrel","foome");
            var content = agent.CurrentContent;

            var messagePostForm = content.GetLink<MessagePostForm>();
            messagePostForm.Message = "Here's a message";
            agent.NavigateTo(messagePostForm);
            
            var finalcontent = agent.CurrentContent;

            Assert.AreEqual(HttpStatusCode.OK, agent.CurrentStatusCode);

        }

        [TestMethod]
        public void SearchMessages() {
            var httpClient = new HttpClient();
            
            var agent = new RestAgent(httpClient);
            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/") });
            
            
            agent.SetBasicCredentials("darrel", "foome");
            var content = agent.CurrentContent;

            var messagePostForm = content.GetLink<MessagesSearchForm>();
            messagePostForm.Search = "yuppie";
            agent.NavigateTo(messagePostForm);

            var finalcontent = agent.CurrentContent;

            Assert.AreEqual(HttpStatusCode.OK, agent.CurrentStatusCode);

        }

        [TestMethod]
        public void RetweetYuppieMessages() {
          
            var agent = new RestAgent();
            agent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml")); 
            MicroblogSemanticsProvider.RegisterSemantics(agent.SemanticsRegistry);

            agent.NavigateTo(new Link() { Target = new Uri("http://127.0.0.1:8080/") });


            agent.SetBasicCredentials("darrel", "foome");
            var content = agent.CurrentContent;

            var messagePostForm = content.GetLink<MessagesSearchForm>();
            messagePostForm.Search = "yuppie";
            agent.NavigateTo(messagePostForm);

            var shareFormLinks = agent.CurrentContent.GetLinks<MessageShareForm>();
            foreach (var messageShareForm in shareFormLinks) {
                agent.NavigateTo(messageShareForm);    
            }
            

            Assert.AreEqual(HttpStatusCode.OK, agent.CurrentStatusCode);

        }

    }
}
