using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Tavis;

namespace RESTShell.Interfaces {
    public interface IContentController {

        void Display(Link source);
		void UpdateContent(HypermediaContent content);
    }
}
