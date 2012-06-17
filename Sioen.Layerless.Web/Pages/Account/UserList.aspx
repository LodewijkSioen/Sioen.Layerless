<%@ Page Title="List of Users" Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.Account.UserList" MasterPageFile="~/Pages/Default.Master" %>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <ul>
        <asp:ListView id="ListOfUsers" runat="server" ItemType="Sioen.Layerless.Logic.Entities.User" SelectMethod="ListUsers">
            <ItemTemplate><li><a href="<%#GetRouteUrl("UserAction", new{action="view", id=Item.Id}) %>"><%# Item.UserName %></a></li></ItemTemplate>
        </asp:ListView>
            <li><a href="<%#GetRouteUrl("UserAction", new {action="new"})%>">Add new user</a></li>
        </ul>
        <asp:DataPager ID="pager" PagedControlID="ListOfUsers" PageSize="5" runat="server">
            <Fields>
                <asp:NumericPagerField ButtonType="Link" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:Content>