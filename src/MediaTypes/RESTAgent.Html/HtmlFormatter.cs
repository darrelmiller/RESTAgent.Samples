using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace RESTAgent.Html {
    
    public class HtmlFormatter : MediaTypeFormatter {


        public HtmlFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }


        public override bool CanReadType(Type type)
        {
            return true;
        }
        public override bool CanWriteType(Type type)
        {
            return false;
        }

        
        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
        {
            return new TaskFactory<object>().StartNew(() =>
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load(stream);
                return htmlDocument;
            });

        }

        


      
    }
}
