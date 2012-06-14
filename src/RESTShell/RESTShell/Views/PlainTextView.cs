using System.Windows.Forms;
using RESTShell.Interface;

namespace RESTShell.Views {
    public partial class PlainTextView : UserControl, IView  {
        public PlainTextView() {
            InitializeComponent();

            
        }



        public void SetText(string text) {
            TextBox.Text = text;
        }
    }
}
