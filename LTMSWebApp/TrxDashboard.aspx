<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxDashboard.aspx.cs" Inherits="LTMSWebApp.TrxDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="calctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Dasboard</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Dasboard"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
              <asp:Panel ID="pnlDataEntry" runat="server" Visible="true" style="width: 950px; margin: auto;margin-top: 5px;" >
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 100%;" colspan="4">
                                <div style="width: 100%; margin: auto; border: 1px solid #277a91;  padding: 5px; margin-top: 15px;">
                                    <table border="0">
                                        <tr>
                                            <td style="width: 25%; text-align: right;">Deposit Amount on DSL A/c</td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtDepositeAmountLD" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                                           <%-- <td style="width: 25%; text-align: right;">Deposit (Amount SPL)</td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtDepositeAmountSPL" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>--%>
                                        </tr>
                                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                                    </table>
                                </div>
                               <asp:Button ID="PrintOrder" runat="server" Text="Button" OnClick="PrintOrder_Click" />
                            </td>
                        </tr>

                       
                       
                    </table>
                </asp:Panel>

            </div>
        </div>
    </div>
</asp:Content>

