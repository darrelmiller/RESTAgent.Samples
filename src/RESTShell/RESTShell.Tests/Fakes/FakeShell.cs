using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpContrib;
using Tavis.Http;
using TMShopClient.Interface;
using TMShopClient.Tools;
using TMShopClient.Tools.Interfaces;

namespace TMShopClient.Tests.Fakes {
    public class FakeShell : IShell {

        public bool ShowViewCalled = false;

        public FakeShell() {
                
        }
        public void NavigateTo(Link link) {
            throw new NotImplementedException();
        }

        public void ShowView(IView view) {
            ShowViewCalled = true;
        }

        public void ShowDialog(IDialog window) {
            throw new NotImplementedException();
        }

        public void ReturnToParent<TReturn>(TReturn returnValue) {
            throw new NotImplementedException();
        }
    }
}
