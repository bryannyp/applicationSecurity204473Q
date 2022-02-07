<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="_204473Q_assignment_2_AS.registration" MasterPageFile="~/masterpage.Master"%>


<asp:content ContentPlaceHolderID="headContent" ID="registrationHead" runat="server">
    <title>Register page</title>
         <script type="text/javascript">
             function validate() {
                 //Password validation
                 var strpassword = document.getElementById("password_txt").value;
                 if (strpassword.length == 0) {
                     document.getElementById("lbl_passwordchecker").innerHTML = "";
                     document.getElementById("lbl_passwordchecker").style.color = "Red";
                     
                 }
                 else if (strpassword.length < 12) {
                     document.getElementById("lbl_passwordchecker").innerHTML = "Password Length must be at least 12 characters";
                     document.getElementById("lbl_passwordchecker").style.color = "Red";
                 }
                 else if (strpassword.search(/[0-9+]/) == -1) {
                     document.getElementById("lbl_passwordchecker").innerHTML = "Must have at least 1 number";
                     document.getElementById("lbl_passwordchecker").style.color = "red";
                     return ("no_number");
                 }
                 else if (strpassword.search(/[A-Z+]/) == -1) {
                     document.getElementById("lbl_passwordchecker").innerHTML = "Must have at least 1 capital";
                     document.getElementById("lbl_passwordchecker").style.color = "red";
                     return ("no_cap");
                 }
                 else if (strpassword.search(/[\W+]/) == -1) {
                     document.getElementById("lbl_passwordchecker").innerHTML = "Must have at least 1 special character";
                     document.getElementById("lbl_passwordchecker").style.color = "red";
                     return ("no_symbol");
                 }

                 else {
                     document.getElementById("lbl_passwordchecker").innerHTML = "Eggcellent";
                     document.getElementById("lbl_passwordchecker").style.color = "Blue";

                 }
             }
         </script>
            
</asp:content>
<asp:Content ID="registrationContent" ContentPlaceHolderID="mainContent" runat="server">
     <div>
            <%-- first name --%>
            <asp:Label ID="firstnamelbl" runat="server" Text="First Name:"></asp:Label>
            <asp:TextBox ID="firstname_txt" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldFirstName" runat="server"
            ControlToValidate="firstname_txt"
            ErrorMessage="First name is a required field."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />



            <%-- last name --%>
            <asp:Label ID="lastnamelbl" runat="server" Text="Last Name:"></asp:Label>
            <asp:TextBox ID="lastname_txt" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldLastName" runat="server"
            ControlToValidate="lastname_txt"
            ErrorMessage="Last name is a required field."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />



            <%-- credit card --%>
            <asp:Label ID="creditCardlbl" runat="server" Text="Credit Card Info:"></asp:Label>
            <asp:TextBox ID="creditcard_txt" runat="server" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldCreditCard" runat="server"
            ControlToValidate="creditcard_txt"
            ErrorMessage="Credit Card information is required."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />
            <asp:RegularExpressionValidator ID="RegexnumberOnlyCC"
            ControlToValidate="creditcard_txt" runat="server"
            ErrorMessage="Only Numbers allowed"
            ValidationExpression="\d+"
            ForeColor="Red">
            </asp:RegularExpressionValidator>
            <br />



            <%-- email --%>
            <asp:Label ID="emaillbl" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="email_txt" runat="server" TextMode="Email" MaxLength="255"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldEmail" runat="server"
            ControlToValidate="email_txt"
            ErrorMessage="Email is required."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />
                   <asp:Label ID="email_Error" runat="server"></asp:Label>
            <br />
            <%-- password --%>
            <asp:Label ID="passwordlbl" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="password_txt" runat="server" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldPassword" runat="server"
            ControlToValidate="password_txt"
            ErrorMessage="Password is required."
            ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />
            <asp:RegularExpressionValidator ID="regexForpassword"
            ControlToValidate="password_txt" runat="server"
            ErrorMessage="needs minimum 12 characters, at least one uppercase letter, one lowercase letter, one number and one special character"
            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$"
            ForeColor="Red">
            </asp:RegularExpressionValidator>
           
            <br />
            <asp:Label ID="lbl_passwordchecker" runat="server"></asp:Label>
            <br />

            <%-- date --%>
            <asp:Label ID="lbl_dateChecker" runat="server"></asp:Label>
            <br />
            <asp:Label ID="dateofBirthlbl" runat="server" Text="date of birth:"></asp:Label>
            <input type="date" id="dateofBirth_cal" runat="server" />

            <%-- pic upload --%>
            <asp:Label ID="lbl_picchecker" runat="server"></asp:Label>
            <br />
            <asp:Label ID="photolbl" runat="server" Text="photo:"></asp:Label>
            <asp:FileUpload ID="photoUpload_fu" runat="server" accept=".png,.jpg,.jpeg,.gif"/>
            <asp:RequiredFieldValidator ID="photoUploadValidator" ControlToValidate="photoUpload_fu" runat="server" ForeColor="Red" ErrorMessage="Photo is required!">
            </asp:RequiredFieldValidator>
            <br />
            <asp:validationsummary
            id="ErrorSummary"
            ForeColor="Red"
            runat="server"/>
            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click1"/>
            <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
        </div>
            <script>
                grecaptcha.ready(function () {
                    grecaptcha.execute('6Lc4-V4eAAAAAO4ffnWiGVIqyMiIJZUMdzxmpjSe', { action: 'register' }).then(function (token) {
                        document.getElementById("g-recaptcha-response").value = token;
                    });
                });
            </script>
</asp:Content>
       

