<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_204473Q_assignment_2_AS.home" MasterPageFile="~/masterpage.Master"%>
<asp:Content ID="homeHead" ContentPlaceHolderID="headContent" runat="server">
    <title>Home page</title>
</asp:Content>
<asp:Content ID="homeContent" ContentPlaceHolderID="mainContent" runat="server">
        <asp:Label ID="lbl_username" runat="server" Text="Label"></asp:Label>
           <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
           <script>
                grecaptcha.ready(function () {
                    grecaptcha.execute('6Lc4-V4eAAAAAO4ffnWiGVIqyMiIJZUMdzxmpjSe', { action: 'homepage' }).then(function (token) {
                        document.getElementById("g-recaptcha-response").value = token;
                    });
                });
           </script>
</asp:Content>

