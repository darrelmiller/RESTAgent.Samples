using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Microblog.DOM {
    public class MicroblogDocument {
        public static MicroblogDocument Load(XDocument document) {
            return new MicroblogDocument();    
        }


    }
}
