<%@ Page Title="Edit User" Language="C#" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.Account.UserForm" MasterPageFile="~/Pages/Default.Master" %>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:FormView ID="Form" ItemType="Sioen.Layerless.Web.Pages.User.UserModel" SelectMethod="SelectUser" InsertMethod="InsertUser" UpdateMethod="UpdateUser" DeleteMethod="DeleteUser" DataKeyNames="Id" runat="server">
            <ItemTemplate>
                <a href="<%# GetRouteUrl("UserAction", new {action="edit", id = Item.Id} )%>"><%#Item.UserName %></a>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Label AssociatedControlID="NameTextBox" runat="server" />
                <asp:TextBox ID="NameTextBox" Text="<%#BindItem.UserName%>" runat="server" /><br />
                <asp:Button CommandName="Insert" Text="Insert User" runat="server" />
                
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Label ID="Label1" AssociatedControlID="NameTextBox" runat="server" />
                <asp:TextBox ID="NameTextBox" Text="<%#BindItem.UserName %>" runat="server" /><br />
                <asp:Button CommandName="Update" Text="Update User" runat="server" />
                <asp:Button CommandName="Delete" Text="Delete User" runat="server" />
            </EditItemTemplate>
        </asp:FormView>    
    </div>
</asp:Content>