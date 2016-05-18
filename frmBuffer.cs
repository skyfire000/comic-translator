using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Overlay
{
	/// <summary>
	/// Summary description for frmBuffer.
	/// </summary>
	public class frmBuffer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmBuffer()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmBuffer));
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(312, 381);
			this.textBox.TabIndex = 0;
			this.textBox.Text = "";
			// 
			// frmBuffer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 381);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.textBox});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBuffer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Text Buffer";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmBuffer_Closing);
			this.Load += new System.EventHandler(this.frmBuffer_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmBuffer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string s = textBox.Text.Replace("\r", "");
			frmMain.TextBuffer = s.Split(new char[1] {'\n'});
		}

		private void frmBuffer_Load(object sender, System.EventArgs e)
		{
			textBox.Text = "";
			for (int i = 0; i < frmMain.TextBuffer.Length; i++) 
			{
				textBox.Text += frmMain.TextBuffer[i];
				if (i < frmMain.TextBuffer.Length - 1) textBox.Text += "\n";
			}
		}
	}
}
