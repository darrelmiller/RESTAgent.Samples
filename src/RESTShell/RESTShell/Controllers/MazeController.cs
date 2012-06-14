using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Maze;
using RESTShell.Interface;
using RESTShell.Interfaces;
using RESTShell.Views;
using Tavis;

namespace RESTShell.Controllers {
    public class MazeController : IContentController {
        private readonly IShell _Shell;
        private readonly PlainTextView _View;

        public MazeController(IShell shell) {
            _Shell = shell;
            _View = new PlainTextView();
        }

        public void Display(Link source) {
			_Shell.ShowView(_View);
        }


		public void UpdateContent(HypermediaContent content)
		{
			var mazeContent = (XmlDocument)content.Value;
			_View.SetText(XDocument.Load(new XmlNodeReader(mazeContent)).ToString());
		}
    }
}
