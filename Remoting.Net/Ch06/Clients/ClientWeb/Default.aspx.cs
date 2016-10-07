using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Runtime.Remoting;

using General;
using General.Client;

namespace ClientWeb
{
	public class MyPage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox ListResults;
		protected System.Web.UI.WebControls.Button ActionCall;
	
		private void Page_Load(object sender, System.EventArgs e)
		{

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ActionCall.Click += new System.EventHandler(this.ActionCall_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ActionCall_Click(object sender, System.EventArgs e)
		{
			IRemoteFactory proxy = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));
			Person p = proxy.GetPerson();
			
			ListResults.Items.Add(string.Format("{0} {1}, {2}",
				p.Firstname, p.Lastname, p.Age));
		}
	}
}
