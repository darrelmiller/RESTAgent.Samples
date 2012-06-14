using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Tavis;
using RESTShell.Interface;
using RESTShell.Interfaces;
using RESTShell.Views;

namespace RESTShell.Controllers {
    public class PlainTextController : IContentController {
        private readonly IShell _Shell;
        private readonly PlainTextView _View;

        public PlainTextController(IShell shell) {
            _Shell = shell;
            _View = new PlainTextView();
        }

        public void Display(Link source) {
            _Shell.ShowView(_View);
        }


		public void UpdateContent(HypermediaContent content)
		{
			var textContent = (string)content.Value;
			_View.SetText(textContent);
		}
    }
}
