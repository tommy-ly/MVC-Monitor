<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppSettings.aspx.cs" Inherits="MvcMonitor.AppSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal runat="server" ID="ltlOutput"></asp:Literal>
        <asp:Button runat="server" ID="btnTestSave" Text="Save" OnClick="btnTestSave_OnClick"/>
    </div>
    </form>
</body>
</html>
