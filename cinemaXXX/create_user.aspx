<%@ Page Language="C#" Inherits="cinemaXXX.create_user" MasterPageFile="~/masterpage.master" %>
<%@ MasterType VirtualPath="~/masterpage.master" %>
<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder" ID="ContentPlaceHolderContent" runat="server">
	<h1>Create New User</h1>
	<p class="error"><asp:PlaceHolder id="errors" runat="server" /></p>
	<table>
		<tr>
			<td>First Name: </td>
			<td><asp:TextBox id="fname" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Last Name: </td>
			<td><asp:TextBox id="lname" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Phone: </td>
			<td><asp:TextBox id="phone" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>E-mail: </td>
			<td><asp:TextBox id="email" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Are you human?: </td>
			<td><asp:TextBox id="human" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Password: </td>
			<td><asp:TextBox id="passwd" runat="server" TextMode="password"></asp:TextBox></td>
		</tr>
		<tr>
			<td>Password again: </td>
			<td><asp:TextBox id="passwdAgain" runat="server" TextMode="password"></asp:TextBox></td>
		</tr>
		<tr>
			<td></td>
			<td><asp:Button runat="server" Text="Create User" OnClick="createUser"></asp:Button></td>
		</tr>
	</table>
</asp:Content>


