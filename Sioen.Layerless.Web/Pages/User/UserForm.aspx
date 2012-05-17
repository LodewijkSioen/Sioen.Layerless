<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.User.UserForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FormView ItemType="Sioen.Layerless.Logic.Entities.User" SelectMethod="" InsertMethod="" UpdateMethod="" DeleteMethod="" runat="server">
            <ItemTemplate>

            </ItemTemplate>
        </asp:FormView>    
    </div>
    </form>
</body>
</html>
