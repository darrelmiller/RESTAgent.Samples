using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Tavis;
using Tavis.Tools;

namespace Microblog {
    public class MicroblogLinkExtractor : ILinkExtractor {
        private static XmlNamespaceManager _NamespaceManager;

        public Type SupportedType {
            get {  return typeof(XDocument) ;   }
        }

        public Link GetLink(Func<string,Link> factory, object content, string relation, string anchor = null) {
            return GetLinks(factory, content, relation, anchor).FirstOrDefault();
        }


        public IEnumerable<Link> GetLinks(Func<string, Link> factory, object content, string relation = null, string anchor = null)
        {
            if (anchor != null) throw new ArgumentException("Anchor is not supported");
            if (content is XDocument) {
                return
                    GetLinks((XDocument) content, factory).Where(
                        l => relation == null || l.Relation.Split(' ').Contains(relation));
            } else {
                return null;
            }
        }


        private static IEnumerable<Link> GetLinks(XDocument doc, Func<string, Link> factory)
        {
            var links  = new List<Link>();

            var reader = doc.CreateReader();
            var xmlNameTable = reader.NameTable;
            _NamespaceManager = new XmlNamespaceManager(xmlNameTable);
            _NamespaceManager.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");

            var elements = doc.XPathSelectElements("//xhtml:link[@href]|//xhtml:a[@href]",_NamespaceManager);
            foreach(var linkElement in elements) {
                links.AddRange(CreateLinks(linkElement, factory));
            }

            var formElements = doc.XPathSelectElements("//xhtml:form[@action]", _NamespaceManager);
            foreach (var linkElement in formElements) {
                links.AddRange(CreateFormLinks(linkElement, factory));
            }
            return links;
        }

        private static IEnumerable<Link> CreateFormLinks(XElement linkElement, Func<string, Link> factory)
        {
            var links = new List<Link>();
            var relnames = linkElement.Attribute("class").Value.Split(' ');
            foreach (var relname in relnames) {
                var link = (FormLink)factory(relname+"-form");
                link.Target = new Uri(linkElement.Attribute("action").Value, UriKind.RelativeOrAbsolute);
                foreach (var inputElement in linkElement.Elements("input")) {
                    link.AddField(inputElement.Attribute("name").Value, inputElement.Attribute("value").Value);
                }
                link.BuildContent();
                links.Add(link);
            }
            return links;
        }

        private static IEnumerable<Link> CreateLinks(XElement linkElement, Func<string, Link> factory)
        {
            var links = new List<Link>();
            
            if (linkElement.Attribute("rel") != null) {
            var relnames = linkElement.Attribute("rel").Value.Split(' ');
            foreach (var relname in relnames) {
                var link = factory(relname);
                link.Target = new Uri(linkElement.Attribute("href").Value, UriKind.RelativeOrAbsolute);
                links.Add(link);
            }
            }
            return links;
        }
    }
}
