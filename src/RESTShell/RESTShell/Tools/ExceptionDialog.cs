using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Tavis.Framework
{
	public sealed class ExceptionDialog : System.Windows.Forms.Form
	{
		private delegate void TabRenderer();

		private enum TabToRender
		{
			General = 0,
			Stack = 1,
			Inner = 2,
			Other = 3
		}

		private const string UnknownException = "Unknown";

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button copyButton;
        private ExceptionControl exceptionControl;

		public ExceptionDialog() : base()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.exceptionControl = new ExceptionControl();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(615, 393);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(80, 24);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
			// 
			// copyButton
			// 
			this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.copyButton.Location = new System.Drawing.Point(529, 393);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(80, 24);
			this.copyButton.TabIndex = 3;
			this.copyButton.Text = "&Copy";
			this.copyButton.Click += new System.EventHandler(this.OnCopyButtonClick);
			// 
			// exceptionControl
			// 
			this.exceptionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionControl.Exceptions = null;
			this.exceptionControl.Location = new System.Drawing.Point(8, 8);
			this.exceptionControl.Name = "exceptionControl";
			this.exceptionControl.Size = new System.Drawing.Size(687, 379);
			this.exceptionControl.TabIndex = 5;
			// 
			// ExceptionDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(705, 422);
			this.Controls.Add(this.exceptionControl);
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.okButton);
			this.MinimizeBox = false;
			this.Name = "ExceptionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exception Information";
			this.ResumeLayout(false);

        }
		#endregion

		private void OnCopyButtonClick(object sender, System.EventArgs e)
		{
			this.exceptionControl.CopyExceptionDataToClipboard();
		}

        private void OnOkButtonClick(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<Exception> Exceptions
        {
            get
            {
                return this.exceptionControl.Exceptions;
            }
            set
            {
                this.exceptionControl.Exceptions = value;
            }
        }
	}
}
