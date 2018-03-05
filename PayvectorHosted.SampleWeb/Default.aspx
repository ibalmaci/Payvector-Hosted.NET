<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayvectorHosted.SampleWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hosted payment form integration – initial post sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" Width="150px" Height="75px"
            OnClick="btnPayNow_Click" />
    </div>
    </form>
</body>
</html>
