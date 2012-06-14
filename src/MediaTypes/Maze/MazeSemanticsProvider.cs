using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RESTAgent.Maze;
using Tavis;

namespace Maze {
    public class MazeSemanticsProvider {
        public static void RegisterSemantics(SemanticsRegistry registry)
        {

			registry.RegisterLinkType<NorthLink>("north");
            registry.RegisterLinkType<SouthLink>("south");
            registry.RegisterLinkType<EastLink>("east");
            registry.RegisterLinkType<WestLink>("west");
            registry.RegisterLinkType<StartLink>("start");
            registry.RegisterLinkType<CurrentLink>("current");
            registry.RegisterLinkType<ExitLink>("exit");

            registry.RegisterFormatter(new XmlFormatter("application/vnd.amundsen.maze+xml"));
            registry.RegisterLinkExtractor(new MazeLinkExtractor());
        }
    }

    public class XmlFormatter : MediaTypeFormatter
    {
        public XmlFormatter(string mediaType)
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
        }
        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
        {
            return new TaskFactory<object>().StartNew(() =>
                                                          {
                                                              var doc = new XmlDocument();
                                                              doc.Load(stream);
                                                              return doc;
                                                          });
        }
    }
}
