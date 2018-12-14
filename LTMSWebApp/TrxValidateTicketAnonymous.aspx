<%@ Page Title="" Language="C#" MasterPageFile="~/StartPage.Master" AutoEventWireup="true" CodeBehind="TrxValidateTicketAnonymous.aspx.cs" Inherits="LTMSWebApp.TrxValidateTicketAnonymous" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="tm-section-4" class="row tm-section">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5 col-xl-6 tm-contact-left">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-left">
               <asp:Panel ID="pnlDataEntry" runat="server" Visible="true">
                <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryType" Width="100%" runat="server"  class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryName" Width="100%" runat="server" class="form-control"  ></asp:DropDownList></td> 
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawDate" Width="80%" runat="server"  class="form-control"></asp:TextBox>
                            </td>                                
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%; padding-left:70px;">OR</td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw No<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:TextBox ID="txtDrawNo" Width="80%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery/Ticket No<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:TextBox ID="txtLotteryNo" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;" >
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" CommandName="Save" OnClick="btnView_Click"  />                                
                                <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                       
                    </table>
                   
                </asp:Panel>
                
            </div>

        </div>
        <div class="tm-white-curve-right col-xs-12 col-sm-6 col-md-6 col-lg-7 col-xl-6">
            <div class="tm-white-curve-right-circle"></div>
            <div class="tm-white-curve-right-rec"></div>
            <div class="tm-white-curve-text">
                <h2 class="tm-section-header green-text">
                    <asp:Label ID="lblValidTicket" runat="server" Text=""></asp:Label>
                </h2>
                 <%-- <a class="nav-link" href="ResourcePages/Instruction.pdf" target="_blank">Download the Instruction</a>--%>
              <%--  <h3 class="tm-section-subheader green-text">Lottery Type</h3>
                <address>Select Lottery Type i.e Daily, Weekly, Monthly, Yearly       </address>
                <h3 class="tm-section-subheader green-text">Lottery Name</h3>
                <address>Select Lottery Name </address>--%>
            </div>
        </div>
    </section>
</asp:Content>
