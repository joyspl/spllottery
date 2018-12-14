<%@ Page Title="" Language="C#" MasterPageFile="~/StartPage.Master" AutoEventWireup="true" CodeBehind="TrxQrValidation.aspx.cs" Inherits="LTMSWebApp.TrxQrValidation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="tm-section-4" class="row tm-section">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5 col-xl-6 tm-contact-left">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-left">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="true">
                    <table border="0" class="divDataEntry">                        
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type</td>
                            <td style="width: 50%;"><asp:TextBox ID="txtLotteryType" Width="100%" ReadOnly="true" runat="server"  class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Name</td>
                           <td style="width: 50%;"><asp:TextBox ID="txtLotteryName" Width="100%" ReadOnly="true" runat="server"  class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw No</td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawNo" Width="100%"  ReadOnly="true" runat="server"  class="form-control"></asp:TextBox></td>
                        </tr>                      
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw Date</td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawDate" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>                       
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery/Ticket No</td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtLotteryNo" Width="100%"  ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>   
                        <tr>
                            <td style="width: 50%; background-color:white; text-align:center;" colspan="2" >
                                <asp:Label ID="lblMessage" runat="server" Text="" ></asp:Label>
                            </td>
                        </tr>                      
                    </table>
                </asp:Panel>           
            </div>
        </div>        
    </section>
</asp:Content>
