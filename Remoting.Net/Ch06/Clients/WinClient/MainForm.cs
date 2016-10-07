using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using General;
using General.Client;

namespace WinClient
{
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ActionCall;
		private System.Windows.Forms.TextBox TextResults;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
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
				if (components != null) 
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
			this.ActionCall = new System.Windows.Forms.Button();
			this.TextResults = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ActionCall
			// 
			this.ActionCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ActionCall.Location = new System.Drawing.Point(456, 312);
			this.ActionCall.Name = "ActionCall";
			this.ActionCall.Size = new System.Drawing.Size(96, 24);
			this.ActionCall.TabIndex = 6;
			this.ActionCall.Text = "Call...";
			this.ActionCall.Click += new System.EventHandler(this.ActionCall_Click);
			// 
			// TextResults
			// 
			this.TextResults.AcceptsReturn = true;
			this.TextResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextResults.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TextResults.Location = new System.Drawing.Point(8, 8);
			this.TextResults.Multiline = true;
			this.TextResults.Name = "TextResults";
			this.TextResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextResults.Size = new System.Drawing.Size(544, 296);
			this.TextResults.TabIndex = 7;
			this.TextResults.Text = "";
			this.TextResults.WordWrap = false;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(562, 344);
			this.Controls.Add(this.TextResults);
			this.Controls.Add(this.ActionCall);
			this.Name = "MainForm";
			this.Text = "Windows Client";
			this.ResumeLayout(false);

		}
		#endregion

		private void ActionCall_Click(object sender, System.EventArgs e)
		{
			// Get the transparent proxy for the factory
			IRemoteFactory proxy = WinApplication.ServerProxy;
			Person p = proxy.GetPerson();
			
			TextResults.AppendText(
				string.Format("{0} {1}, {2}\r\n", p.Firstname, p.Lastname, p.Age));
		}


	}
}
