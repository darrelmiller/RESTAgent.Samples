//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Linq;
//using System.Text;
//using Microsoft.Http;
//using Microsoft.Http.Headers;
//using Tavis.Http;

//namespace TMShopClient.Tests.Fakes {

//    [Export(typeof(IHttpService))]
//    public class FakeHttpService : IHttpService {
//        public Link LoginLink {
//            get; set;
//        }

//        public HttpResponseMessage Follow(Link link) {
//            return new HttpResponseMessage() { Content = HttpContent.Create("Test","text/html")}; 
//        }

//        public HttpClient HttpClient {
//            get { throw new NotImplementedException(); }
//        }

//        public HttpContent GetContent(Link link) {
//            return HttpContent.CreateEmpty();
//        }

//        public void SetCredentials(string user, string password) {
//            throw new NotImplementedException();
//        }

//        public void SetCredentials(string token) {
//            throw new NotImplementedException();
//        }

//        public void AddProductToUserAgent(Product product) {
//            throw new NotImplementedException();
//        }

//        public void AddDefaultHeader(string header, string value) {
//            throw new NotImplementedException();
//        }
//    }
//}
