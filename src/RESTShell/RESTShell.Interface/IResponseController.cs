using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace RESTShell.Interface {
    public interface IResponseController {


        void Handle(HttpResponseMessage response);  // Called when shell navigates to this controller
        void Deactivate();
        void ReturnFromChild(object returnValue); // Called after a child controller is closed

    }
}