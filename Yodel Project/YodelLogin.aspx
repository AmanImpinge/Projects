<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YodelLogin.aspx.cs"  ValidateRequest="false" Inherits="YodelLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <title></title> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtRequest" TextMode="MultiLine" runat="server"></asp:TextBox>
       
        <table>
            <tr>
                <td>
                    User Name
                </td>
            </tr>
            <tr>
                <td>
                    Password
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnLogin" runat="server" Text="Button" OnClick="BtnLogin_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
