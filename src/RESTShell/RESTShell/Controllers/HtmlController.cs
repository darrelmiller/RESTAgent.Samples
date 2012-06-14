using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using Tavis;
using RESTShell.Interface;
using RESTShell.Interfaces;
using RESTShell.Renderers;

namespace RESTShell.Controllers {
    public class HtmlController : IContentController {
        private readonly IShell _Shell;
        private readonly HtmlView _View;
        public HtmlController(IShell shell) {
            _Shell = shell;
            _View = new HtmlView();
        }

        public void Display(Link source) {
			_View.Navigate(source.Target);
			_Shell.ShowView(_View);

        }


		public void UpdateContent(HypermediaContent content)
		{
			// Ignore the content because the stupid webbrowser controll cannot accept content and a context url
			// Hopefully not a perf problem because the html page may be cached so a round trip will not occur.
		}
    }
}
