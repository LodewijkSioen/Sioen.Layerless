<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.Home" MasterPageFile="~/Pages/Default.Master" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Test</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Button runat="server" OnCommand="Dispatcher.OnEvent" CommandName="CreateDatabase" text="Create Database"/>
    </div>
    <div>
        <asp:Button runat="server" OnCommand="Dispatcher.OnEvent" CommandName="TestBinding" text="test modelbinding" />
        <asp:DropDownList runat="server" ID="test" OnSelectedIndexChanged="Dispatcher.OnEvent" AutoPostBack="true">
            <asp:ListItem />
            <asp:ListItem Text="test" />
        </asp:DropDownList>
        <asp:HiddenField ID="hiddenId" Value="test" runat="server" />                
    </div>
    <div>
        <a href="<%= GetRouteUrl("UserList", null) %>">Users</a>
    </div>
</asp:Content>