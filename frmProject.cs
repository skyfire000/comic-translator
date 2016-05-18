using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Overlay
{
	/// <summary>
	/// Summary description for frmProject.
	/// </summary>
	public class frmProject : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbUrl;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;

		public frmProject()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmProject));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tbName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(160, 356);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(244, 356);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																																							this.tabPage1});
			this.tabControl1.Location = new System.Drawing.Point(4, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(332, 344);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																																					 this.tbName,
																																					 this.label3,
																																					 this.tbDescription,
																																					 this.label2,
																																					 this.tbUrl,
																																					 this.label1});
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(324, 318);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(8, 24);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(308, 20);
			this.tbName.TabIndex = 13;
			this.tbName.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Name:";
			// 
			// tbDescription
			// 
			this.tbDescription.AcceptsReturn = true;
			this.tbDescription.AcceptsTab = true;
			this.tbDescription.Location = new System.Drawing.Point(8, 104);
			this.tbDescription.Multiline = true;
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbDescription.Size = new System.Drawing.Size(308, 204);
			this.tbDescription.TabIndex = 11;
			this.tbDescription.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Description:";
			// 
			// tbUrl
			// 
			this.tbUrl.Location = new System.Drawing.Point(8, 64);
			this.tbUrl.Name = "tbUrl";
			this.tbUrl.Size = new System.Drawing.Size(308, 20);
			this.tbUrl.TabIndex = 9;
			this.tbUrl.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Scans URL:";
			// 
			// frmProject
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(338, 387);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.tabControl1,
																																	this.btnCancel,
																																	this.btnOk});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmProject";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Project Properties";
			this.Load += new System.EventHandler(this.frmProject_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmProject_Load(object sender, System.EventArgs e)
		{
			Project project = Project.Current;
			tbName.Text = project.Name;
			tbUrl.Text = project.ScansUrl;
			tbDescription.Text = project.Description;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Project project = Project.Current;
			project.Name = tbName.Text;
			project.ScansUrl = tbUrl.Text;
			project.Description = tbDescription.Text;
			this.Close();
		}
	}

}
