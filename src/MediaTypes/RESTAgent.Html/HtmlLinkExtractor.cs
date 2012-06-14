using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Tavis;


namespace RESTAgent.Html {
    public class HtmlLinkExtractor : ILinkExtractor {

        public Type SupportedType { get { return typeof (HtmlDocument); } }


        public Link GetLink(Func<string,Link> factory, object content, string relation, string anchor)
		{
			return GetLinks(factory, content, relation, anchor).FirstOrDefault();
		}


        public IEnumerable<Link> GetLinks(Func<string, Link> factory, object content, string relation = null, string anchor = null)
		{
			if (anchor != null) throw new ArgumentException("Anchor is not supported at this time");
			
			return GetLinks((HtmlDocument)content, factory).Where(l => relation == null || l.Relation == relation);
		}


        private static IEnumerable<Link> GetLinks(HtmlDocument doc, Func<string, Link> factory)
		{
			return (doc.DocumentNode.SelectNodes("//a[@href]") ?? Enumerable.Empty<HtmlNode>())
				.Where(l => !string.IsNullOrEmpty(l.InnerText))
				.Select(l => CreateLink(l, factory))
				.ToList()
			;
		}

        private static Link CreateLink(HtmlNode linkElement, Func<string, Link> factory)
		{
			var relation = linkElement.Attributes["rel"] != null ? linkElement.Attributes["rel"].Value : string.Empty;

			var link = factory(relation);
			link.Context = new Uri(linkElement.Attributes["id"] != null ? "#" + linkElement.Attributes["id"].Value : string.Empty, UriKind.RelativeOrAbsolute);

			var target = linkElement.Attributes["href"].Value;
			link.Target = new Uri(target, UriKind.RelativeOrAbsolute);

			return link;
		}
    }
}
