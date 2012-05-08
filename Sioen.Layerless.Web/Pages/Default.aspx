<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sioen.Layerless Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" OnCommand="OnCommand" CommandName="CreateDatabase" text="Create Database"/>
    </div>
    <div>
        <a href="<%= GetRouteUrl("UserList", null) %>">Users</a>
    </div>
    </form>
</body>
</html>
