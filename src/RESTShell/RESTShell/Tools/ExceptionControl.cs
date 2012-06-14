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
	public sealed class ExceptionControl : System.Windows.Forms.UserControl
	{
		private delegate void TabRenderer();

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label exceptionLabel;
		private bool hasGeneralBeenCalled = false;
		private bool hasOtherInformationBeenCalled = false;
		private bool hasStackTraceBeenCalled = false;
		private TabRenderer[] renderers;
		private Label exceptionDetails;
		private TreeView exceptionHierarchyTree;
		private TabPage specificTabPage;
		private ListView otherInformationList;
		private ColumnHeader nameHeader;
		private ColumnHeader descriptionHeader;
		private TabPage stackTabPage;
		private ListView stackTraceList;
		private ColumnHeader methodHeader;
		private TabPage generalTabPage;
		private TextBox helpLinkValueText;
		private TextBox exceptionSourceValueText;
		private TextBox exceptionTargetMethodValueText;
		private TextBox exceptionMessageValueText;
		private Label helpLinkLabel;
		private Label exceptionTargetMethodLabel;
		private Label exceptionSourceLabel;
		private Label exceptionMessageLabel;
		private TabControl informationTab;
		private Exception selectedExceptionFromTree;
		private TextBox assemblyValueText;
		private Label assemblyLabel;
		private List<Exception> exceptions;

		//  TODO (3/26/2004): Remember: OSFeature.IsPresent(OSFeature.Themes) 
		public ExceptionControl() : base()
		{
			InitializeComponent();
			this.renderers = new TabRenderer[]
				{new TabRenderer(this.DisplayGeneralInformation),
				new TabRenderer(this.DisplayStackTrace),
				new TabRenderer(this.DisplayOtherInformation)};
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
			this.exceptionLabel = new System.Windows.Forms.Label();
			this.exceptionDetails = new System.Windows.Forms.Label();
			this.exceptionHierarchyTree = new System.Windows.Forms.TreeView();
			this.specificTabPage = new System.Windows.Forms.TabPage();
			this.otherInformationList = new System.Windows.Forms.ListView();
			this.nameHeader = new System.Windows.Forms.ColumnHeader();
			this.descriptionHeader = new System.Windows.Forms.ColumnHeader();
			this.stackTabPage = new System.Windows.Forms.TabPage();
			this.stackTraceList = new System.Windows.Forms.ListView();
			this.methodHeader = new System.Windows.Forms.ColumnHeader();
			this.generalTabPage = new System.Windows.Forms.TabPage();
			this.assemblyValueText = new System.Windows.Forms.TextBox();
			this.assemblyLabel = new System.Windows.Forms.Label();
			this.helpLinkValueText = new System.Windows.Forms.TextBox();
			this.exceptionSourceValueText = new System.Windows.Forms.TextBox();
			this.exceptionTargetMethodValueText = new System.Windows.Forms.TextBox();
			this.exceptionMessageValueText = new System.Windows.Forms.TextBox();
			this.helpLinkLabel = new System.Windows.Forms.Label();
			this.exceptionTargetMethodLabel = new System.Windows.Forms.Label();
			this.exceptionSourceLabel = new System.Windows.Forms.Label();
			this.exceptionMessageLabel = new System.Windows.Forms.Label();
			this.informationTab = new System.Windows.Forms.TabControl();
			this.specificTabPage.SuspendLayout();
			this.stackTabPage.SuspendLayout();
			this.generalTabPage.SuspendLayout();
			this.informationTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// exceptionLabel
			// 
			this.exceptionLabel.Location = new System.Drawing.Point(0, 8);
			this.exceptionLabel.Name = "exceptionLabel";
			this.exceptionLabel.Size = new System.Drawing.Size(62, 16);
			this.exceptionLabel.TabIndex = 0;
			this.exceptionLabel.Text = "&Exceptions:";
			// 
			// exceptionDetails
			// 
			this.exceptionDetails.Location = new System.Drawing.Point(255, 9);
			this.exceptionDetails.Name = "exceptionDetails";
			this.exceptionDetails.Size = new System.Drawing.Size(44, 15);
			this.exceptionDetails.TabIndex = 4;
			this.exceptionDetails.Text = "&Details:";
			// 
			// exceptionHierarchyTree
			// 
			this.exceptionHierarchyTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.exceptionHierarchyTree.Location = new System.Drawing.Point(0, 28);
			this.exceptionHierarchyTree.Name = "exceptionHierarchyTree";
			this.exceptionHierarchyTree.Size = new System.Drawing.Size(249, 321);
			this.exceptionHierarchyTree.TabIndex = 3;
			this.exceptionHierarchyTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnExceptionHierarchyTreeAfterSelect);
			// 
			// specificTabPage
			// 
			this.specificTabPage.Controls.Add(this.otherInformationList);
			this.specificTabPage.Location = new System.Drawing.Point(4, 22);
			this.specificTabPage.Name = "specificTabPage";
			this.specificTabPage.Size = new System.Drawing.Size(349, 295);
			this.specificTabPage.TabIndex = 3;
			this.specificTabPage.Text = "Other Information";
			// 
			// otherInformationList
			// 
			this.otherInformationList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.otherInformationList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.descriptionHeader});
			this.otherInformationList.Location = new System.Drawing.Point(8, 8);
			this.otherInformationList.Name = "otherInformationList";
			this.otherInformationList.Size = new System.Drawing.Size(333, 281);
			this.otherInformationList.TabIndex = 0;
			this.otherInformationList.View = System.Windows.Forms.View.Details;
			// 
			// nameHeader
			// 
			this.nameHeader.Text = "Name";
			this.nameHeader.Width = 120;
			// 
			// descriptionHeader
			// 
			this.descriptionHeader.Text = "Description";
			this.descriptionHeader.Width = 220;
			// 
			// stackTabPage
			// 
			this.stackTabPage.Controls.Add(this.stackTraceList);
			this.stackTabPage.Location = new System.Drawing.Point(4, 22);
			this.stackTabPage.Name = "stackTabPage";
			this.stackTabPage.Size = new System.Drawing.Size(349, 295);
			this.stackTabPage.TabIndex = 2;
			this.stackTabPage.Text = "Stack Trace";
			// 
			// stackTraceList
			// 
			this.stackTraceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.stackTraceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.methodHeader});
			this.stackTraceList.Location = new System.Drawing.Point(8, 8);
			this.stackTraceList.Name = "stackTraceList";
			this.stackTraceList.Size = new System.Drawing.Size(333, 281);
			this.stackTraceList.TabIndex = 1;
			this.stackTraceList.View = System.Windows.Forms.View.Details;
			// 
			// methodHeader
			// 
			this.methodHeader.Text = "Method";
			this.methodHeader.Width = 400;
			// 
			// generalTabPage
			// 
			this.generalTabPage.Controls.Add(this.assemblyValueText);
			this.generalTabPage.Controls.Add(this.assemblyLabel);
			this.generalTabPage.Controls.Add(this.helpLinkValueText);
			this.generalTabPage.Controls.Add(this.exceptionSourceValueText);
			this.generalTabPage.Controls.Add(this.exceptionTargetMethodValueText);
			this.generalTabPage.Controls.Add(this.exceptionMessageValueText);
			this.generalTabPage.Controls.Add(this.helpLinkLabel);
			this.generalTabPage.Controls.Add(this.exceptionTargetMethodLabel);
			this.generalTabPage.Controls.Add(this.exceptionSourceLabel);
			this.generalTabPage.Controls.Add(this.exceptionMessageLabel);
			this.generalTabPage.Location = new System.Drawing.Point(4, 22);
			this.generalTabPage.Name = "generalTabPage";
			this.generalTabPage.Size = new System.Drawing.Size(349, 295);
			this.generalTabPage.TabIndex = 0;
			this.generalTabPage.Text = "General Information";
			// 
			// assemblyValueText
			// 
			this.assemblyValueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.assemblyValueText.Location = new System.Drawing.Point(96, 272);
			this.assemblyValueText.Name = "assemblyValueText";
			this.assemblyValueText.ReadOnly = true;
			this.assemblyValueText.Size = new System.Drawing.Size(245, 20);
			this.assemblyValueText.TabIndex = 9;
			this.assemblyValueText.TabStop = false;
			// 
			// assemblyLabel
			// 
			this.assemblyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.assemblyLabel.Location = new System.Drawing.Point(8, 272);
			this.assemblyLabel.Name = "assemblyLabel";
			this.assemblyLabel.Size = new System.Drawing.Size(88, 16);
			this.assemblyLabel.TabIndex = 8;
			this.assemblyLabel.Text = "Assembly:";
			this.assemblyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// helpLinkValueText
			// 
			this.helpLinkValueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.helpLinkValueText.Location = new System.Drawing.Point(96, 248);
			this.helpLinkValueText.Name = "helpLinkValueText";
			this.helpLinkValueText.ReadOnly = true;
			this.helpLinkValueText.Size = new System.Drawing.Size(245, 20);
			this.helpLinkValueText.TabIndex = 7;
			this.helpLinkValueText.TabStop = false;
			// 
			// exceptionSourceValueText
			// 
			this.exceptionSourceValueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionSourceValueText.Location = new System.Drawing.Point(96, 200);
			this.exceptionSourceValueText.Name = "exceptionSourceValueText";
			this.exceptionSourceValueText.ReadOnly = true;
			this.exceptionSourceValueText.Size = new System.Drawing.Size(245, 20);
			this.exceptionSourceValueText.TabIndex = 3;
			this.exceptionSourceValueText.TabStop = false;
			// 
			// exceptionTargetMethodValueText
			// 
			this.exceptionTargetMethodValueText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionTargetMethodValueText.Location = new System.Drawing.Point(96, 224);
			this.exceptionTargetMethodValueText.Name = "exceptionTargetMethodValueText";
			this.exceptionTargetMethodValueText.ReadOnly = true;
			this.exceptionTargetMethodValueText.Size = new System.Drawing.Size(245, 20);
			this.exceptionTargetMethodValueText.TabIndex = 5;
			this.exceptionTargetMethodValueText.TabStop = false;
			// 
			// exceptionMessageValueText
			// 
			this.exceptionMessageValueText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionMessageValueText.Location = new System.Drawing.Point(96, 9);
			this.exceptionMessageValueText.Multiline = true;
			this.exceptionMessageValueText.Name = "exceptionMessageValueText";
			this.exceptionMessageValueText.ReadOnly = true;
			this.exceptionMessageValueText.Size = new System.Drawing.Size(245, 185);
			this.exceptionMessageValueText.TabIndex = 1;
			this.exceptionMessageValueText.TabStop = false;
			// 
			// helpLinkLabel
			// 
			this.helpLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.helpLinkLabel.Location = new System.Drawing.Point(8, 248);
			this.helpLinkLabel.Name = "helpLinkLabel";
			this.helpLinkLabel.Size = new System.Drawing.Size(88, 16);
			this.helpLinkLabel.TabIndex = 6;
			this.helpLinkLabel.Text = "Help Link:";
			this.helpLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionTargetMethodLabel
			// 
			this.exceptionTargetMethodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exceptionTargetMethodLabel.Location = new System.Drawing.Point(8, 224);
			this.exceptionTargetMethodLabel.Name = "exceptionTargetMethodLabel";
			this.exceptionTargetMethodLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionTargetMethodLabel.TabIndex = 4;
			this.exceptionTargetMethodLabel.Text = "Target Method:";
			this.exceptionTargetMethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionSourceLabel
			// 
			this.exceptionSourceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exceptionSourceLabel.Location = new System.Drawing.Point(8, 200);
			this.exceptionSourceLabel.Name = "exceptionSourceLabel";
			this.exceptionSourceLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionSourceLabel.TabIndex = 2;
			this.exceptionSourceLabel.Text = "Source:";
			this.exceptionSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionMessageLabel
			// 
			this.exceptionMessageLabel.Location = new System.Drawing.Point(8, 9);
			this.exceptionMessageLabel.Name = "exceptionMessageLabel";
			this.exceptionMessageLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionMessageLabel.TabIndex = 0;
			this.exceptionMessageLabel.Text = "Message:";
			this.exceptionMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// informationTab
			// 
			this.informationTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.informationTab.Controls.Add(this.generalTabPage);
			this.informationTab.Controls.Add(this.stackTabPage);
			this.informationTab.Controls.Add(this.specificTabPage);
			this.informationTab.Location = new System.Drawing.Point(256, 28);
			this.informationTab.Name = "informationTab";
			this.informationTab.SelectedIndex = 0;
			this.informationTab.Size = new System.Drawing.Size(357, 321);
			this.informationTab.TabIndex = 5;
			this.informationTab.SelectedIndexChanged += new System.EventHandler(this.OnInformationTabSelectedIndexChanged);
			// 
			// ExceptionControl
			// 
			this.Controls.Add(this.exceptionHierarchyTree);
			this.Controls.Add(this.exceptionDetails);
			this.Controls.Add(this.informationTab);
			this.Controls.Add(this.exceptionLabel);
			this.Name = "ExceptionControl";
			this.Size = new System.Drawing.Size(613, 349);
			this.Load += new System.EventHandler(this.OnExceptionDialogLoad);
			this.specificTabPage.ResumeLayout(false);
			this.stackTabPage.ResumeLayout(false);
			this.generalTabPage.ResumeLayout(false);
			this.generalTabPage.PerformLayout();
			this.informationTab.ResumeLayout(false);
			this.ResumeLayout(false);

        }
		#endregion

		private void DisplayGeneralInformation()
		{
			if(this.hasGeneralBeenCalled == false)
			{
				this.exceptionMessageValueText.Text = this.selectedExceptionFromTree.Message;
				this.exceptionSourceValueText.Text = this.selectedExceptionFromTree.Source;
                if (this.selectedExceptionFromTree.TargetSite != null) {
                    this.exceptionTargetMethodValueText.Text = this.selectedExceptionFromTree.TargetSite.ToString();
                }
                else {
                    this.exceptionTargetMethodValueText.Text = String.Empty;
                }
				this.helpLinkValueText.Text = this.selectedExceptionFromTree.HelpLink;
				this.assemblyValueText.Text = this.selectedExceptionFromTree.GetType().Assembly.GetName().Name;
				this.hasGeneralBeenCalled = true;
			}
		}

		private void DisplayInformationTab(int selectedTab)
		{
			try
			{
				TabRenderer renderTab = this.renderers[selectedTab];

				if(renderTab != null)
				{
					renderTab();
				}
			}
			catch
			{
				//  Ignore - display as much as possible.
				//  TODO (3/26/2004): Add log4net logging.
			}
		}

		private void DisplayOtherInformation()
		{
			if(this.hasOtherInformationBeenCalled == false)
			{
				Hashtable customInformation = this.GetCustomExceptionInfo(this.selectedExceptionFromTree);
				IDictionaryEnumerator customEnumerator = customInformation.GetEnumerator();
			
				this.otherInformationList.Items.Clear();
				this.otherInformationList.BeginUpdate();

				ListViewItem listItem;

				while(customEnumerator.MoveNext())
				{
					listItem = new ListViewItem(customEnumerator.Key.ToString());
					
					if(customEnumerator.Value != null)
					{
						listItem.SubItems.Add(customEnumerator.Value.ToString());
					}
					
					this.otherInformationList.Items.Add(listItem);
				}

				this.otherInformationList.EndUpdate();
				this.hasOtherInformationBeenCalled = true;
			}
		}

		private void DisplayStackTrace()
		{
			if(this.hasStackTraceBeenCalled == false)
			{
				StackTrace stackTrace = new StackTrace(selectedExceptionFromTree);
				
				for(int i = 0; i < stackTrace.FrameCount - 1; i++)
				{
					StackFrame stackFrame = stackTrace.GetFrame(i);
					stackTraceList.Items.Add(stackFrame.GetMethod().ToString());
				}
			
				this.hasStackTraceBeenCalled = true;
			}
		}

		private Hashtable GetCustomExceptionInfo(Exception exception)
		{
			Hashtable customInformation = new Hashtable();
            Type exceptionType = typeof(System.Exception);

            foreach(PropertyInfo propertyInfo in exception.GetType().GetProperties())
			{
				if(exceptionType.GetProperty(propertyInfo.Name) == null)
				{
					customInformation.Add(propertyInfo.Name, propertyInfo.GetValue(exception, null));
				}
			}

			return customInformation;
		}

		private void OnCopyButtonClick(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(this.ToString(), true);
		}

		private void OnExceptionDialogLoad(object sender, System.EventArgs e)
		{
            this.UpdateUI();
		}

		private void OnInformationTabSelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DisplayInformationTab(informationTab.SelectedIndex);
		}

		private void OnExceptionHierarchyTreeAfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this.exceptionHierarchyTree.SelectedNode != null)
			{
				this.selectedExceptionFromTree = this.exceptionHierarchyTree.SelectedNode.Tag as Exception;
				this.UpdateExceptionTab();
			}
		}

		private void UpdateExceptionTree()
		{
			try
			{
				this.exceptionHierarchyTree.BeginUpdate();
				this.exceptionHierarchyTree.Nodes.Clear();

				if (this.exceptions != null && this.exceptions.Count > 0)
				{
					foreach (Exception exception in this.exceptions)
					{
						Exception innerException = exception;
						TreeNode parentNode = null, childNode = null;

						while (innerException != null)
						{
							childNode = new TreeNode(innerException.GetType().FullName);
							childNode.Tag = innerException;

							if (parentNode != null)
							{
								parentNode.Nodes.Add(childNode);
							}
							else
							{
								this.exceptionHierarchyTree.Nodes.Add(childNode);
							}

							parentNode = childNode;
							innerException = innerException.InnerException;
						}
					}

					this.selectedExceptionFromTree = this.exceptions[0];
				}
			}
			finally
			{
				this.exceptionHierarchyTree.EndUpdate();
			}
		}

		private void UpdateExceptionTab()
		{
			this.hasGeneralBeenCalled = false;
			this.hasOtherInformationBeenCalled = false;
			this.hasStackTraceBeenCalled = false;
			this.stackTraceList.Items.Clear();
			this.otherInformationList.Items.Clear();
			this.exceptionMessageValueText.Text = string.Empty;
			this.exceptionSourceValueText.Text = string.Empty;
			this.exceptionTargetMethodValueText.Text = string.Empty;
			this.helpLinkValueText.Text = string.Empty;

			if (this.selectedExceptionFromTree != null)
			{
				this.informationTab.Enabled = true;
				this.DisplayInformationTab(this.informationTab.SelectedIndex);
			}
			else
			{
				this.informationTab.Enabled = false;
			}
		}

        private void UpdateUI()
        {
			this.selectedExceptionFromTree = null;

			this.UpdateExceptionTree();
			this.UpdateExceptionTab();
        }

		public override string ToString()
		{
			StringBuilder formattedException = new StringBuilder();

			if (this.selectedExceptionFromTree != null)
			{
				formattedException.Append("EXCEPTION INFORMATION").Append(Environment.NewLine)
					.Append(Environment.NewLine)
					.Append("Date/Time: ").Append(DateTime.Now.ToString("F")).Append(Environment.NewLine)
					.Append("Type: ").Append(this.selectedExceptionFromTree.GetType().FullName).Append(Environment.NewLine)
					.Append("Message: ").Append(this.selectedExceptionFromTree.Message).Append(Environment.NewLine)
					.Append("Source: ").Append(this.selectedExceptionFromTree.Source).Append(Environment.NewLine)
					.Append("Target Method: ")
					.Append(this.selectedExceptionFromTree.TargetSite.ToString())
					.Append(Environment.NewLine).Append(Environment.NewLine)
					.Append("Call Stack:").Append(Environment.NewLine);

				StackTrace exceptionStack = new StackTrace(this.selectedExceptionFromTree);

				for(int i = 0; i < exceptionStack.FrameCount; i++)
				{
					StackFrame exceptionFrame = exceptionStack.GetFrame(i);

					formattedException.Append("\t").Append("Method Name: ").Append(exceptionFrame.GetMethod().ToString()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("File Name: ").Append(exceptionFrame.GetFileName()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Column: ").Append(exceptionFrame.GetFileColumnNumber()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Line: ").Append(exceptionFrame.GetFileLineNumber()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("CIL Offset: ").Append(exceptionFrame.GetILOffset()).Append(Environment.NewLine)
                        .Append("\t").Append("\t").Append("Native Offset: ").Append(exceptionFrame.GetNativeOffset()).Append(Environment.NewLine)
						.Append(Environment.NewLine);
				}

				formattedException.Append("Inner Exception(s)").Append(Environment.NewLine);

				Exception innerException = this.selectedExceptionFromTree.InnerException;

				while (innerException != null)
				{
					formattedException.Append("\t").Append("Exception: ")
						.Append(innerException.GetType().FullName).Append(Environment.NewLine);
					innerException = innerException.InnerException;
				}	

				formattedException.Append(Environment.NewLine).Append("Custom Properties")
					.Append(Environment.NewLine);

				Type exceptionType = typeof(Exception);

				foreach (PropertyInfo propertyInfo in this.selectedExceptionFromTree.GetType().GetProperties())
				{
					if(exceptionType.GetProperty(propertyInfo.Name) == null)
					{
						formattedException.Append("\t").Append(propertyInfo.Name).Append(": ")
							.Append(propertyInfo.GetValue(this.selectedExceptionFromTree, null))
							.Append(Environment.NewLine);
					}
				}
			}

			return formattedException.ToString();
		}

        public void CopyExceptionDataToClipboard()
        {
            Clipboard.SetDataObject(this.ToString(), true);
        }

        public List<Exception> Exceptions
        {
            get
            {
                return this.exceptions;
            }
            set
            {
                if(value != null)
                {
					this.exceptions = value;
					this.UpdateUI();
                }
            }
        }
	}
}
