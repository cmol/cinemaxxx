<%@ Page Language="C#" Inherits="cinemaXXX.admin_advanced" MasterPageFile="~/masterpage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div>
		<asp:PlaceHolder id="PlaceHolderNavigation" runat="server"></asp:PlaceHolder>
</div>
<div>
	<asp:PlaceHolder id="PlaceHolderSelect" runat="server"></asp:PlaceHolder>
	<asp:PlaceHolder id="PlaceHolderEdit" runat="server"></asp:PlaceHolder>
	<div id="advAdminButtons" runat="server">
		<asp:Button runat="server" Text="Update" OnClick="advUpdate"></asp:Button>
		<asp:Button runat="server" Text="Insert" OnClick="advInsert"></asp:Button>
	</div>
</div>
</asp:Content>