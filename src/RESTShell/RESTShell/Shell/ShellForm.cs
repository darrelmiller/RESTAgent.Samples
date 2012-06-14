using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RESTShell.Interface;
using RESTShell.Tools.Interfaces;

namespace RESTShell.Shell {
    [Export(typeof(IShellView))]
     partial class ShellForm : Form, IShellView {

        private Control _View;
        
        public ShellForm() {
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e) {
            
        }

        public void ShowView(IView view) {
            if (_View != null) {
                Controls.Remove(_View);
                _View = null;
            }
            _View = view as Control;
            Controls.Add(_View);
            _View.Dock = DockStyle.Fill;
            _View.Focus();
        }


        


    }
}
