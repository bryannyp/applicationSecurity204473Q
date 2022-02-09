<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="_204473Q_assignment_2_AS.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>My profile</title>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Label ID="lbl_headerTex" runat="server" Text="Profile Info:" Font-Bold="True"></asp:Label>
    <br />
    <asp:Label ID="Label6" runat="server" Text="First Name:"></asp:Label>
    <asp:Label ID="lbl_firstName" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label7" runat="server" Text="Last Name:"></asp:Label>
    <asp:Label ID="lblLastName" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="Credit Card:"></asp:Label>
    <asp:Label ID="lblCreditCard" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label9" runat="server" Text="Email:"></asp:Label>
    <asp:Label ID="lblEmail" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label10" runat="server" Text="Date of birth:"></asp:Label>
    <asp:Label ID="lblDateofBirth" runat="server"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="lbl_changePass" runat="server" Text="Change Password" Font-Bold="True"></asp:Label>
    <br />
    <asp:Label ID="lbl_oldpass" runat="server" Text="Old password"></asp:Label>
    <asp:TextBox ID="oldPassTxt" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator id="oldPassRequiredValidator" runat="server"
            ControlToValidate="oldPassTxt"
            ErrorMessage="old password is a required field."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
      <br />
               <asp:RegularExpressionValidator ID="regChecker1"
            ControlToValidate="oldPassTxt" runat="server"
            ErrorMessage="needs minimum 12 characters, at least one uppercase letter, one lowercase letter, one number and one special character"
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"
            ForeColor="Red">
            </asp:RegularExpressionValidator>
    <br />


    <asp:Label ID="lbl_newpass" runat="server" Text="New password:"></asp:Label>
    <asp:TextBox ID="newPassTxt" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator id="newPasswordRequiredValidator" runat="server"
            ControlToValidate="newPassTxt"
            ErrorMessage="new password is a required field."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
      <br />
               <asp:RegularExpressionValidator ID="regChecker2"
            ControlToValidate="newPassTxt" runat="server"
            ErrorMessage="needs minimum 12 characters, at least one uppercase letter, one lowercase letter, one number and one special character"
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"
            ForeColor="Red">
            </asp:RegularExpressionValidator>
    <br />


    <asp:Label ID="lbl_matchpass" runat="server" Text="Retype new password:"></asp:Label>
    <asp:TextBox ID="matchPasstxt" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator id="matchPassvalidator" runat="server"
            ControlToValidate="matchPasstxt"
            ErrorMessage="retype password is a required field."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
    <br />
               <asp:RegularExpressionValidator ID="regchecker3"
            ControlToValidate="matchPasstxt" runat="server"
            ErrorMessage="needs minimum 12 characters, at least one uppercase letter, one lowercase letter, one number and one special character"
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"
            ForeColor="Red">
            </asp:RegularExpressionValidator>
    <br />
      <asp:validationsummary
            id="ErrorSummary"
            ForeColor="Red"
            runat="server"/>
    <br />
    <asp:Button ID="passChangeBtn" runat="server" OnClick="passChangeBtn_Click" Text="Update" />
    <br />
</asp:Content>
