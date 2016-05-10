<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Smart.NetMVC2.PageView<TestWebApp.Models.LoginModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <% if (Model == null)
       {  %>
       请登录
    <% }
       else
       { %>
       登录成功：<%= Model.LoginName %>
    <% } %>
</body>
</html>
