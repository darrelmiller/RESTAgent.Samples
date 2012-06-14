using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;
using Tavis;
using Tavis.Actions;
using RESTShell.Missions;


namespace RESTShell.Shell {
    public class ShellModel {
        private RestAgent _RestAgent;

        public event Action OnContentChanging = delegate { };
        public event Action OnContentChanged = delegate { };

        public CompositionContainer Container { get; set; }
		public HypermediaContent CurrentContent { get; set; }

		public string CurrentMediaType { get;  set; }
		public Link CurrentLocation { get; set; }


        public RestAgent RestAgent {
            get { return _RestAgent; }
            set { _RestAgent = value;
                CurrentContent = _RestAgent.CurrentContent;
				CurrentMediaType = _RestAgent.CurrentContent.MediaType;
				CurrentLocation = _RestAgent.CurrentLocation;
            }
        }

		public IMission DefaultMission { get; set; }

		public void FireOnContentChanged()
		{
		    OnContentChanged();
		}

        public void FireOnContentChanging()
        {
            OnContentChanging();
        }


        internal void InitClientState() {

            var responseAction = new ShellResponseAction(this);
            _RestAgent.RegisterResponseAction(responseAction);


            //Assembly a = Assembly.GetExecutingAssembly();
            //_RestAgent.AddProductToUserAgent(new ProductHeaderValue("RESTShell", a.GetName().Version.ToString()));
			// _RestAgent.NavigateTo(new Link(RootUri));
        }
    }


    public class ShellResponseAction : ResponseAction
    {
        private readonly ShellModel _model;



        public ShellResponseAction(ShellModel model)

        {
            _model = model;
        }

        public override bool ShouldRespond(HttpResponseMessage responseMessage)
        {
            return responseMessage.StatusCode == HttpStatusCode.OK;
        }

        public override void HandleResponseComplete(HypermediaContent currentContent)
        {
            _model.CurrentContent = currentContent;
            _model.FireOnContentChanged();
        }

        public override void HandleResponseStart(Link link, HttpResponseMessage response)
        {

            _model.CurrentMediaType = response.Content.Headers.ContentType.MediaType;

            _model.CurrentLocation = link;

            _model.FireOnContentChanging();
        }
    }
}
