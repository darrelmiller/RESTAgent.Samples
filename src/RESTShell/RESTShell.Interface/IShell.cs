using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RESTShell.Interface {
    public interface IShell {

        void ShowView(IView view);
        void ShowDialog(IDialog window);
    }
}