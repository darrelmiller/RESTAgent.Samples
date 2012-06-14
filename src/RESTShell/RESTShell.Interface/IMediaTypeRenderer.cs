using System.Net.Http;

namespace RESTShell.Interface {
   public interface IMediaTypeRenderer {

        IMediaTypeController RunController(HttpResponseMessage message);
    }
}
