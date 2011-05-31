<%@ Page Language="C#" Inherits="cinemaXXX.movie_prototype" MasterPageFile="~/masterpage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<asp:Literal ID="debugtext" runat="server"></asp:Literal>
<asp:PlaceHolder id="placeholder1" runat="server"></asp:PlaceHolder>
<asp:Button runat="server" Text="Button" OnClick="clickmig" id="clickmigbutton"></asp:Button>

</asp:Content>
