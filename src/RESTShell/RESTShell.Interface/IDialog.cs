using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RESTShell.Interface {
    // Used to display content in a distinct window modally, aka a Dialog
    public interface IDialog {
        void Show(object parent);
    }
}
