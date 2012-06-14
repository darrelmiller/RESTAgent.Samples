using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RESTShell.Interface;

namespace RESTShell.Renderers {
    public partial class HtmlView : UserControl, IView   {
        private bool _Wait;    
        public HtmlView() {
            InitializeComponent();
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);            
        }

        public void Navigate(Uri url) {
			while (_Wait) {
                Application.DoEvents();
            }

            _Wait = true;
            webBrowser1.Navigate(url);
        }



        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            _Wait = false;
        }
    }
}
