<%@ Page Title="Edit User" Language="C#" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="Sioen.Layerless.Web.Pages.User.UserForm" MasterPageFile="~/Pages/Default.Master" %>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:FormView ID="Form" ItemType="Sioen.Layerless.Logic.Entities.User" SelectMethod="SelectUser" InsertMethod="InsertUser" UpdateMethod="UpdateUser" DeleteMethod="DeleteUser" runat="server">
            <ItemTemplate>
                <%#Item.UserName %>
            </ItemTemplate>
            <InsertItemTemplate>
            <asp:Label AssociatedControlID="NameTextBox" runat="server" />
            <asp:TextBox ID="NameTextBox" Text="<%#Item.UserName %>" runat="server" />
            <asp:Button CommandName="Insert" Text="Insert User" runat="server" /><br />
            </InsertItemTemplate>
            <EditItemTemplate>
            <asp:Label ID="Label1" AssociatedControlID="NameTextBox" runat="server" />
            <asp:TextBox ID="NameTextBox" Text="<%#Item.UserName %>" runat="server" /><br />
            <asp:Button CommandName="Update" Text="Insert User" runat="server" />
            <asp:Button CommandName="Delete" Text="Delete User" runat="server" />
            </EditItemTemplate>
        </asp:FormView>    
    </div>
</asp:Content>