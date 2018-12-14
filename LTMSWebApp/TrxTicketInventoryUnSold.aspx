<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxTicketInventoryUnSold.aspx.cs" Inherits="LTMSWebApp.TrxTicketInventoryUnSold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Unsold Ticket</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Upload Un Sold Ticket"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Requisition No<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtReqCode" Width="40%" ReadOnly="true" runat="server"  class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Requisition Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtReqDate" Width="40%" ReadOnly="true" runat="server"  class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtLotteryName" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtLotteryType" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw No<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawNo" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="width: 50%; text-align: right;">Quantity<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtQty" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>   
                         <tr>
                            <td style="width: 50%; text-align: right;">Un-Sold Percentage (%)<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtUnSoldPercentage" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>                       
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawDate" ReadOnly="true" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                <aspctr:CalendarExtender ID="caltDrawDate" runat="server" CssClass="MyCalendar" TargetControlID="txtDrawDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                            </td>
                        </tr>                      
                        
                         <tr>
                            <td style="width: 50%; text-align: right;">Upload File<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:FileUpload ID="fileUpldFromExcel" Width="340px" runat="server" />
                                <br />  <span style="color:red;"  >Only .xls/.xlsx file types are allowed!</span>
                                <br />  <span style="color:red;"  >Please clear all formats for all blank cells before uploading.</span>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: center;" colspan="2">OR</td>                            
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">No-Unsold ticket<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:CheckBox ID="chkNoFile" Enabled="true" Checked="true" runat="server" />
                            </td>                    
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;" colspan="2">
                                <asp:Button ID="btnConfirm" runat="server" Text="Upload/Save" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Upload?');" CommandName="Confirm" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;"><asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 10%; text-align: right;">To Date:</td><td style="width: 15%;"><asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td><td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;"><asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;"><asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;"><asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1750px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Download" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Download" runat="server" ImageUrl="~/Content/Images/Pdf.png" ToolTip="Download" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label>
                                        <asp:Label ID="lblTicketUnSoldUploadStatus" Visible="false" runat="server" Text='<%# Bind("TicketUnSoldUploadStatus") %>'></asp:Label>
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Lottery Name"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Count Of Un-Sold Ticket"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblCountOfUnSoldTicket" runat="server" Text='<%# Bind("CountOfUnSoldTicket") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Amount Of Un-Sold Ticket"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblAmountOfUnSoldTicket" runat="server" Text='<%# Bind("AmountOfUnSoldTicket") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="First Start No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblFnStart" runat="server" Text='<%# Bind("FnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="First End No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblFnEnd" runat="server" Text='<%# Bind("FnEnd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Alphabet Series" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblAlphabetSeries" runat="server" Text='<%# Bind("AlphabetSeries") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                       
                                 <asp:TemplateField HeaderText="Third Start No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblTnStart" runat="server" Text='<%# Bind("TnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Third End No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblTnEnd" runat="server" Text='<%# Bind("TnEnd") %>'></asp:Label></ItemTemplate>
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

