<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.User.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>
