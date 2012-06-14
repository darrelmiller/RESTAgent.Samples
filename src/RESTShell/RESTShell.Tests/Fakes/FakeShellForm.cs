using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TMShopClient.Interface;
using TMShopClient.Tools.Interfaces;

namespace TMShopClient.Tests.Fakes {
    public class FakeShellForm : Form,IShellView {
        public void Show(int screenNo) {
            throw new NotImplementedException();
        }

        public void ShowView(IView view) {
            throw new NotImplementedException();
        }

    }
}
