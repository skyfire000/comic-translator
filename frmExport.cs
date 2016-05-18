using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace Overlay
{
	public class frmExport : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code
		public System.Windows.Forms.ProgressBar progressBar;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Project project;
		private System.Windows.Forms.Button btnCancel;
		private string folder;
		private object quality;

		public frmExport(Project project, string folder, object quality)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.project = project;
			this.folder = folder;
			this.quality = quality;
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
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(8, 12);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(328, 23);
			this.progressBar.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(244, 44);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmExport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(342, 75);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.btnCancel,
																																	this.progressBar});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmExport";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Exporting Pictures...";
			this.Load += new System.EventHandler(this.frmExport_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private Thread thread = null;
		
		private void frmExport_Load(object sender, System.EventArgs e)
		{
			ThreadStart ts = new ThreadStart(threadHandle);
			thread = new Thread(ts);
			thread.Start();
		}

		public void threadHandle()
		{
			progressBar.Maximum = project.Scans.Count;
			for (int i = 0; i < project.Scans.Count; i++) 
			{
				Scan sc = (Scan)project.Scans[i];
				string filename = sc.Filename;
				if (filename == "") filename = "no_image.jpg";
				sc.Export(folder + "\\" + filename, false, quality);
				progressBar.Value = i;
			}
			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			if (thread != null) 
				thread.Abort();
			Close();
		}
	}
}
