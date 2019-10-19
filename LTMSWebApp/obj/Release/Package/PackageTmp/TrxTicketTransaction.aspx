<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxTicketTransaction.aspx.cs" Inherits="LTMSWebApp.TrxTicketTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Transaction</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Ticket Transaction"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 25%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 25%;"><asp:TextBox ID="txtLotteryName" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 25%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 25%;"><asp:TextBox ID="txtLotteryType" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Draw No<font color="red">*</font></td>
                            <td style="">
                                <asp:TextBox ID="txtDrawNo" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                            <td style="">
                                <asp:TextBox ID="txtDrawDate" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr> 
                        <tr>
                            <td style="text-align: right;">Ticket Quantity<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtQuantity" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style=" text-align: right;">Ticket Amount<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtAmount" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>  
                        <tr>
                            <td style=" text-align: right;">Claim Quantity<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtClaimQuantity" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style=" text-align: right;">Claim Amount<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtClaimAmount" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr> 
                        <tr>
                            <td style=" text-align: right;">Un-Sold Quantity<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtUnSoldQty" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style=" text-align: right;">Un-Sold Amount<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtUnSoldAmount" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr> 
                         <tr>
                            <td style=" text-align: right;" colspan="3">Amount to be Adjust with Dealer<font color="red">*</font></td>
                            <td style=""><asp:TextBox ID="txtDealerAdjustAmount" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>        
                        <tr>
                            <td style=" text-align: right;" colspan="2">&nbsp;</td>
                            <td style="" colspan="2">
                                <asp:Button ID="btnConfirm" runat="server" Text="Update" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Upload?');" CommandName="Confirm" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel id="pnlDataDisplay" runat="server" style="position: relative;">
                    <asp:Panel id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;"><asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 10%; text-align: right;">To Date:</td><td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td><td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;"><asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;"><asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;"><asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" /></td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="800px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/ModifyTran.png" ToolTip="Edit" Height="20px" Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrintEntry" CommandName="PrintEntry" Text="Print" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>                           
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                    
                                 <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate><asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate><asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                         
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="120px">
                                    <ItemTemplate><asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Lottery Name"  ItemStyle-Width="250px">
                                    <ItemTemplate><asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Claimed ticket Count"  ItemStyle-Width="250px">
                                    <ItemTemplate><asp:Label ID="lblClaimedCount" runat="server" Text='<%# Bind("ClaimedCount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Un-Sold ticket Count"  ItemStyle-Width="250px">
                                    <ItemTemplate><asp:Label ID="lblUnSoldCount" runat="server" Text='<%# Bind("CountOfUnSoldTicket") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>           
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </asp:Panel>

                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

