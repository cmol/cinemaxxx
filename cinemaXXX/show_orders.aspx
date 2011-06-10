<%@ Page Language="C#" Inherits="cinemaXXX.show_orders" MasterPageFile="~/masterpage.master" %>
<%@ MasterType VirtualPath="~/masterpage.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder" ID="ContentPlaceHolderContent" runat="server">
	<h1>Your Orders</h1>
	<asp:PlaceHolder id="orderList" runat="server" />
</asp:Content>


