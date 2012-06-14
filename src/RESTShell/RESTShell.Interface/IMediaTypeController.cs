using System.Net.Http;
using Tavis;

namespace RESTShell.Interface {
    public interface IMediaTypeController {

        void Handle(HttpResponseMessage response);  // Called when shell navigates to this controller
        void Deactivate();
        void ReturnFromChild(object returnValue); // Called after a child controller is closed

        HypermediaContent Content { get; }

    }
}