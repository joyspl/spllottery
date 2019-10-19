<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxBankGuranteeLedger.aspx.cs" Inherits="LTMSWebApp.TrxBankGuranteeLedger" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="calctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Report</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Bank Gurantee Ledger"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                       
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">To Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;">
                                        <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;">
                                        <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" />
                                    </td>

                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                            Width="1800px" >
                            <Columns>                              
                             
                                <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Lottery Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Government Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGovermentOrder" runat="server" Text='<%# Bind("GovermentOrder") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Draw No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Draw Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="BG No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositId" runat="server" Text='<%# Bind("DepositId") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Deposit Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositAmount" runat="server" Text='<%# Bind("DepositAmount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit Date">
                                    <ItemTemplate>
                                         <asp:Label ID="lblDepositDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DepositDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Total Ticket Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalTicketAmount" runat="server" Text='<%# Bind("TotalTicketAmount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Balence Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotBalenceAmount" runat="server" Text='<%# Bind("TotBalenceAmount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                                        
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate><asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

