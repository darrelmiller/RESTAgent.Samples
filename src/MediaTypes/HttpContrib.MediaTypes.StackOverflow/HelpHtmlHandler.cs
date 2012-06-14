using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpContrib.MediaTypes.Standard;

namespace HttpContrib.MediaTypes.StackOverflow {
    public class HelpHtmlHandler : HtmlController {

        protected void ParseLinks() {
            _Links = new Dictionary<string, Link>();
            _Links.Add("users", new Link() { Relation = "users", Url = new Uri("/users", UriKind.RelativeOrAbsolute) });
        }
    }
}
