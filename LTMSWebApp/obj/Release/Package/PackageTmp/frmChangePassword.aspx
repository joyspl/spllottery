<%@ Page Title="" Language="C#" MasterPageFile="~/PasswordChange.Master" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="LTMSWebApp.frmChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PasswordContentPlaceholder" runat="server">
    <div class="card card-login mx-auto mt-5">
        <div class="card-header">Reset Password</div>
        <div class="card-body">
            <div class="text-center mt-4 mb-5">
                <h4>Change your password</h4>
                <p>Enter your email address and we will send you OTP to change your password.</p>
            </div>
            <table border="0" class="divDataEntry" style="width: 100%;">
                <tr>
                    <td style="width: 30%; text-align: right;">Email Id<font color="red">*</font></td>
                    <td style="width: 50%;">
                        <asp:TextBox ID="txtEmailId" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                    <td style="width: 20%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">Mobile No.<font color="red">*</font></td>
                    <td>
                        <asp:TextBox ID="txtMobileNo" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnSendOtp" runat="server" CssClass="btn btn-primary btn-sm" Text="Send" BorderWidth="0" OnClick="btnSendOtp_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Enter OTP<font color="red">*</font></td>
                    <td>
                        <asp:TextBox ID="txtOTP" Width="60%" runat="server" class="form-control"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-block" OnClick="btnLogin_Click" Text="Reset Password" BorderWidth="0" />
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
