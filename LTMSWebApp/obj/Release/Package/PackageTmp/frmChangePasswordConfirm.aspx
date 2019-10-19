<%@ Page Title="" Language="C#" MasterPageFile="~/PasswordChange.Master" AutoEventWireup="true" CodeBehind="frmChangePasswordConfirm.aspx.cs" Inherits="LTMSWebApp.frmChangePasswordConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PasswordContentPlaceholder" runat="server">
    <div class="card card-login mx-auto mt-5">
        <div class="card-header">Change Password</div>
        <div class="card-body">
            <div class="text-center mt-4 mb-5">
                  <asp:HiddenField ID="hdUniqueId" runat="server" />
                <p>Enter your new password.</p>
            </div>
            <table border="0" class="divDataEntry" style="width: 100%;">
                <tr>
                    <td style="width: 40%; text-align: right;">Email Id<font color="red">*</font></td>
                    <td style="width: 60%;"><asp:TextBox ID="txtEmailId" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">Mobile No<font color="red">*</font></td>
                    <td><asp:TextBox ID="txtMobileNo" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 40%; text-align: right;">User Id<font color="red">*</font></td>
                    <td style="width: 60%;"><asp:TextBox ID="txtUserId" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">New Password<font color="red">*</font></td>
                    <td><asp:TextBox ID="txtNewPassword" TextMode="Password" Width="60%" runat="server" class="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Confirm Password<font color="red">*</font></td>
                    <td><asp:TextBox ID="txtConformPassword" TextMode="Password"  Width="60%" runat="server" class="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-block" Text="Change Password" BorderWidth="0" OnClick="btnLogin_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;"><a class="d-block small" href="Default.aspx">Login Page</a></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
