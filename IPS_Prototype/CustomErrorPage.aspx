<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomErrorPage.aspx.cs" Inherits="IPS_Prototype.AccessRevoked" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#F6F6F6; margin:0px;">
    <form id="form1" runat="server">
        <div style="width:99.3%; height:50px; padding:5px;background-color:white; text-align:center; box-shadow:0 4px 2px -2px #EBECEA;">
            <img src="../Images/IPS_Logo_Long.PNG" />
        </div>
        <div style="float:right; margin-top:50px; margin-right:250px;">
                <img style="height:700px; width:430px;" src="../Images/AccessRevokedShocked.PNG" />
            </div>
        <div style="padding-top:120px; padding-left:250px;">
        <div style="float:left;">
            <p style="font-size:150px; font-weight:900; margin-top:0px; margin-bottom:0.1em; color:#616567;">Oops!</p>
            <p style="font-size:40px; font-weight:400; margin-bottom:0.1em; color:#616567;">It seems that the system has<br /> encountered an error.</p>
            
            <ul style="list-style:none; padding-left:0;">
                <li style="color:#616567; font-size:20px;">Use the bottom link to revert to Login Page:</li>
                <li><a style="color:#FF7377; font-size:20px; text-decoration:none; margin-top:-20px !important;" href="Login_test.aspx">Login</a></li>
            </ul>
            
        </div>
            
            </div>
        
    </form>
</body>
</html>
