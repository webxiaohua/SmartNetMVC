<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    Inherits="Smart.NetMVC2.PageView<TestWebApp.School>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    学校名称：
    <%= Model.Name %>
    <br />
    入学时间：<%=Model.Time %>
</asp:Content>
