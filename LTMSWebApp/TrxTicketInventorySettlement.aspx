<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxTicketInventorySettlement.aspx.cs" Inherits="LTMSWebApp.TrxTicketInventorySettlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <asp:HiddenField ID="hdAdjustmentAmount" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Ticket Inventory Adjustment</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Adjustment"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <div style="width: 100%;" runat="server">
                         <div style=" width:900px; margin:0 auto;" >
                        <table border="0" class="divDataEntry">
                            <tr>
                                <td style="width: 25%; text-align: right;">Requisition No<font color="red">*</font></td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtReqCode" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                                <td style="width: 25%; text-align: right;">Requisition Date<font color="red">*</font></td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtReqDate" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">Lottery Name<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtLotteryName" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">Lottery Type<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtLotteryType" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">Draw No<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtDrawNo" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtDrawDate" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>                               
                                <td style="text-align: right;">Total Sale</td>
                                <td>
                                    <asp:TextBox ID="txtTotalSale" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">Total Sent for Adjustment</td>
                                <td>
                                    <asp:TextBox ID="txtSentForAdusutment" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                                <td style="text-align: right;">Remaining Balence<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtRemainingBalence" ReadOnly="true" Width="100%" runat="server" BackColor="#6699ff" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </div> 
                        <div style="margin-top:10px;"></div>
                          <div class="gridDivCustom" id="dvTransaction"  style="width: 80%; margin: auto; height: 50vh;" >
                            <asp:GridView ID="GvTransaction" runat="server" CssClass="table table-bordered table-hover"
                               DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1550px"
                               HeaderStyle-Font-Size="12px" RowStyle-Font-Size="10px" >
                                <Columns>
                                  
                                     <asp:TemplateField HeaderText="Description" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>     
                                      <asp:TemplateField HeaderText="Document No" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Bind("DocumentNo") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>                      
                                    <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Convert.ToDateTime(Eval("Date")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>                                                                   
                                    <asp:TemplateField HeaderText="Amount" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Requisition Code" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requisition Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy")  %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="LotteryType" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Lottery Name" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DrawNo" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Draw Date" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy")  %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EntryType" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryType" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Sub Entry Type" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntrySubType" runat="server" Text='<%# Bind("EntrySubType") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>                                                                    
                                </Columns>
                                <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                            </asp:GridView>
                            
                        </div>
                        <div style="width:900px; margin:10px auto;">
                            <table>
                                <tr>
                                    <td style="text-align: right;">&nbsp;</td>
                                    <td colspan="3">
                                        <asp:Button ID="btnConfirm" runat="server" Text="Close Transaction" CssClass="btn btn-primary" CommandName="View" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 15%;">From Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 15%; text-align: right;">To Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%; text-align: right;">Status:</td>
                                    <td style="width: 15%;">
                                        <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="width: 15%;"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1750px"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" >
                            <Columns>
                                <asp:TemplateField HeaderText="View" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Search.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrintEntry" CommandName="PrintEntry" Text="Print" runat="server" ImageUrl="~/Content/Images/Pdf.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnStart" runat="server" Text='<%# Bind("FnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnEnd" runat="server" Text='<%# Bind("FnEnd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alphabet Series" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlphabetSeries" runat="server" Text='<%# Bind("AlphabetSeries") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnStart" runat="server" Text='<%# Bind("TnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnEnd" runat="server" Text='<%# Bind("TnEnd") %>'></asp:Label></ItemTemplate>
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

