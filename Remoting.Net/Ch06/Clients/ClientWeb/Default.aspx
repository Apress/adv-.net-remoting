<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="ClientWeb.MyPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ASP.NET based client</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:ListBox id="ListResults" runat="server" Width="360px" Height="192px"></asp:ListBox><BR>
			<asp:Button id="ActionCall" runat="server" Text="Call Remote Server"></asp:Button>
		</form>
	</body>
</HTML>
