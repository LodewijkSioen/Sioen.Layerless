<%@ Page Title="List of Users" Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.User.UserList" MasterPageFile="~/Pages/Default.Master" %>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Repeater runat="server" ItemType="Sioen.Layerless.Logic.Entities.User" SelectMethod="ListUsers">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate><li><a href="<%#GetRouteUrl("UserAction", new{action="edit", id=Item.Id}) %>"><%# Item.UserName %></a></li></ItemTemplate>
            <FooterTemplate>
                <li><a href="<%#GetRouteUrl("UserAction", new {action="new"})%>">Add new user</a></li>
            </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>