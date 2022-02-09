<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_204473Q_assignment_2_AS.Login" MasterPageFile="~/masterpage.Master" %>

<asp:Content ID="loginHead" ContentPlaceHolderID="headContent" runat="server">
    <title>Login</title>
</asp:Content>
<asp:Content id="loginContent" ContentPlaceHolderID="mainContent" runat="server">
            <div>
            <asp:Label ID="lblUser" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="userTxt" runat="server" TextMode="Email"></asp:TextBox>
            <asp:requiredfieldvalidator id="RequiredFieldlogin" runat="server"
            ControlToValidate="userTxt"
            ErrorMessage="Email needed."
            ForeColor="Red">
            </asp:requiredfieldvalidator>
   
        <p>
            <asp:Label ID="lblPass" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox TextMode="Password" runat="server" id="passTxt"></asp:TextBox>
            <asp:requiredfieldvalidator id="RequiredFieldPass" runat="server"
            ControlToValidate="passTxt"
            ErrorMessage="Password needed."
            ForeColor="Red"></asp:requiredfieldvalidator>
        </p>
          </div>
        <p>
            <br />
            <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
            <br />
           <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
            <br />
            <asp:Button ID="submitBtn" runat="server" Text="Submit" OnClick="submitBtn_Click" />
        </p>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('SITE KEY', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
        </script>

        
</asp:Content>


