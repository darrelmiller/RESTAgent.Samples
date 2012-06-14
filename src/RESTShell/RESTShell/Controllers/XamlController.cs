using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Tavis;
using RESTShell.Interface;
using RESTShell.Interfaces;
using RESTShell.Renderers;

namespace RESTShell.Controllers {
    public class XamlController : IContentController {
        private readonly IShell _Shell;
        private readonly XamlView _View;
        public XamlController(IShell shell) {
            _Shell = shell;
            _View = new XamlView();
        }

        public void Display(Link source) {
            throw new NotImplementedException();

            //_Shell.ShowView(_View);
        }


		public void UpdateContent(HypermediaContent content)
		{
			var xamlContent = content;
			//_View.LoadXaml(xamlContent.XamlDoc);
            
			throw new NotImplementedException();
		}
    }
}
