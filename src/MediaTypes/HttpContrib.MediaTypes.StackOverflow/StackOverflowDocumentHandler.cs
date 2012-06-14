using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;


namespace HttpContrib.MediaTypes.StackOverflow {
    public class StackOverflowDocumentHandler : IMediaTypeHandler {


        public IHypermediaContent GetContent(Uri requestUrl, HttpContent content) {
            throw new NotImplementedException();
        }

        public IMediaTypeController RunController(HttpResponseMessage message) {
            throw new NotImplementedException();
        }

  
    }
}
