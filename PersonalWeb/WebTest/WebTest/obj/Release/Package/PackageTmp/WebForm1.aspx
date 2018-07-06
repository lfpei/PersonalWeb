<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebTest.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:Image ID="Image1" runat="server" Height="91px" Width="108px" />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button" />
    </form>
</body>
</html>
