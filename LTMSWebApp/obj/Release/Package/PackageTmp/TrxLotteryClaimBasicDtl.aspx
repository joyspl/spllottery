<%@ Page Title="" Language="C#" MasterPageFile="~/StartPage.Master" AutoEventWireup="true" CodeBehind="TrxLotteryClaimBasicDtl.aspx.cs" Inherits="LTMSWebApp.TrxLotteryClaimBasicDtl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="tm-section-4" class="row tm-section">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5 col-xl-6 tm-contact-left">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-left">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="true">
                    <table border="0" class="divDataEntry">
                         <tr>
                            <td style="width: 50%; text-align: right;">Claim Type<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:DropDownList ID="ddlClaimType" Width="100%" runat="server"  class="form-control"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryType" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged" class="form-control"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:DropDownList ID="ddlLotteryName" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw No<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawNo" Width="100%" runat="server" MaxLength="3" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;"><font color="White">OR</font></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                <aspctr:CalendarExtender ID="CalExtenderEndDate" runat="server" TargetControlID="txtDrawDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery/Ticket No<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtLotteryNo" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Captcha</td>
                            <td style="width: 50%;">
                                <asp:Image ID="imgCaptcha" runat="server" />
                                <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-primary btn-sm" Text="Refresh" BorderWidth="0" OnClick="btnRefresh_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Enter above captcha code<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtCaptcha" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;">
                                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" BorderWidth="0" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnBack" runat="server" class="btn btn-primary" Text="Back" BorderWidth="0" OnClick="btnBack_Click"  />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlValidTicket" runat="server" Visible="false">
                    <table border="0" class="divDataEntry" style="width: 450px;">
                        <tr>
                            <td style="width: 100%; text-align: left;" colspan="2"> <asp:Label ID="lblPrize" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left;" colspan="2">Do you Have Original Ticket?</td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">
                                <asp:Button ID="btnYes" runat="server" class="btn btn-primary" Text="Yes" BorderWidth="0" OnClick="btnYes_Click" />
                            </td>
                            <td style="width: 50%;">
                                <asp:Button ID="btnNo" runat="server" class="btn btn-primary" Text="No" OnClientClick="alert('Sorry better luck next time :(');" BorderWidth="0" OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlLogin" runat="server" Visible="false">
                    <table border="0" class="divDataEntry" style="width: 600px;">
                        <tr>
                            <td style="width: 150px; text-align: right;">Email Id<font color="red">*</font></td>
                            <td style="width: 250px;">
                                <asp:TextBox ID="txtEmailId" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 200px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align: right;">Mobile No.<font color="red">*</font></td>
                            <td style="width: 250px;">
                                <asp:TextBox ID="txtMobileNo" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 200px;">
                                <asp:Button ID="btnSendOtp" runat="server" CssClass="btn btn-primary btn-sm" Text="Send" BorderWidth="0" OnClick="btnSendOtp_Click" /></td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align: right;">Enter OTP<font color="red">*</font></td>
                            <td style="width: 250px;">
                                <asp:TextBox ID="txtOTP" Width="60%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 200px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 150px; text-align: right;">&nbsp;</td>
                            <td style="width: 250px;">
                                <asp:Button ID="btnLogin" runat="server" class="btn btn-primary" Text="Submit" BorderWidth="0" OnClick="btnLogin_Click" /></td>
                            <td style="width: 200px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblMsg" ForeColor="Yellow" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
                
            </div>

        </div>
        <div class="tm-white-curve-right col-xs-12 col-sm-6 col-md-6 col-lg-7 col-xl-6">
            <div class="tm-white-curve-right-circle"></div>
            <div class="tm-white-curve-right-rec"></div>
            <div class="tm-white-curve-text">
                <h2 class="tm-section-header green-text">Instruction</h2>
                  <a class="nav-link" href="ResourcePages/Instruction.pdf" target="_blank">Download the Instruction</a>
              <%--  <h3 class="tm-section-subheader green-text">Lottery Type</h3>
                <address>Select Lottery Type i.e Daily, Weekly, Monthly, Yearly       </address>
                <h3 class="tm-section-subheader green-text">Lottery Name</h3>
                <address>Select Lottery Name </address>--%>
            </div>
        </div>
    </section>
    
</asp:Content>
