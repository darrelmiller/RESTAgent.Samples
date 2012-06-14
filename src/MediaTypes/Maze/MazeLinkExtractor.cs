using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tavis;


namespace Maze
{
	public class MazeLinkExtractor : ILinkExtractor
	{
		

		public Link GetLink(Func<string,Link> factory, object content, string relation, string anchor = null)
		{
			return GetLinks(factory, content, relation, anchor).FirstOrDefault();
		}


        public IEnumerable<Link> GetLinks(Func<string, Link> factory, object content, string relation = null, string anchor = null)
		{
			if (anchor != null) throw new ArgumentException("Anchor is not supported");

			return GetLinks((XmlDocument)content, factory).Where(l => relation == null || l.Relation == relation);
		}

	    public Type SupportedType
	    {
            get { return typeof (XmlDocument); } 
	    }


	    private static IEnumerable<Link> GetLinks(XmlDocument doc, Func<string, Link> factory)
		{
			return doc.SelectNodes("//link[@href]")
				.Cast<XmlElement>()
				.Select(e => CreateLink(e, factory))
				.ToList()
			;
		}


        private static Link CreateLink(XmlElement linkElement, Func<string, Link> factory)
		{
			var relname = linkElement.GetAttribute("rel");

			var link = factory(relname);
			link.Target = new Uri(linkElement.GetAttribute("href"), UriKind.RelativeOrAbsolute);

			return link;
		}
	}
}
