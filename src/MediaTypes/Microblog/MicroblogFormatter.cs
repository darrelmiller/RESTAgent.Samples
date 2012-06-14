using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Microblog {
    public class MicroblogFormatter : MediaTypeFormatter {

        public MicroblogFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xhtml"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xhtml+xml"));
        }



        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
        {
            return new TaskFactory<Object>().StartNew(() =>
                                                  {
                                                      if (contentHeaders.ContentType.MediaType == "text/html")
                                                      {
                                                          var html = new HtmlAgilityPack.HtmlDocument();
                                                          html.Load(stream);
                                                          return html;
                                                      }
                                                      else
                                                      {
                                                          var doc = XDocument.Load(stream);
                                                          return doc;
                                                      }
                                                  });
        }


        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}
