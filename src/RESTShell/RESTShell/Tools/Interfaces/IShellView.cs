using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using RESTShell.Interface;

namespace RESTShell.Tools.Interfaces {
    public interface IShellView {
        
        void Show();
        void ShowView(IView view);
        //void ClearView();
        
    }
}
