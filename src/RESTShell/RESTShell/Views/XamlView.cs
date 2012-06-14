using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using RESTShell.Interface;

namespace RESTShell.Renderers {
    public partial class XamlView : UserControl, IView {
        public XamlView() {
            
            InitializeComponent();
        }

        public void LoadXaml(UIElement element) {
            elementHost1.Child = element;
        }
    }
}
