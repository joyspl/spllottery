<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxRequisition.aspx.cs" Inherits="LTMSWebApp.TrxRequisition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="calctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Requisition & Issue By Director</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Requisition By Director to Press"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:HiddenField ID="hdLastDrawDate" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 25%; text-align: right;">Requisition No<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtReqCode" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                            <td style="width: 25%; text-align: right;">Requisition Date<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtReqDate" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td>
                                <asp:DropDownList ID="ddlLotteryType" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged" class="form-control"></asp:DropDownList></td>
                            <td style="text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td>
                                <asp:DropDownList ID="ddlLotteryName" Width="100%" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryName_SelectedIndexChanged"></asp:DropDownList></td> 
                        </tr>

                        <tr>
                            <td style="text-align: right;">Paper Quality<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtPaperQuality" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Size Of Ticket<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtSizeOfTicket" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </td>  
                            
                        </tr>
                        <tr>
                            <td style="text-align: right;">Government Order<font color="red">*</font></td>
                            <td><asp:TextBox ID="txtGovermentOrder" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDrawDate" Width="100%" runat="server" onchange="javascript: Changed(this);" class="form-control"></asp:TextBox>
                                <calctr:CalendarExtender ID="CalExtenderStartDate" runat="server" TargetControlID="txtDrawDate" Format="dd-MMM-yyyy" Enabled="true"></calctr:CalendarExtender>
                            </td>                                
                        </tr>
                        <tr>
                            <td style="text-align: right;">Draw No<font color="red">*</font></td>
                            <td><asp:TextBox ID="txtDrawNo" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="text-align: right;">Quantity<font color="red">*</font></td>
                            <td><asp:TextBox ID="txtQty" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Delivery Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDelivaryDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                <calctr:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDelivaryDate" Format="dd-MMM-yyyy" Enabled="true"></calctr:CalendarExtender>
                            </td>        
                            <td style="text-align: right;">Slab Limit<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSlabLimit" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">&nbsp;</td>
                            <td colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Confirm?');" CommandName="Confirm" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add New Requisition" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />
                                </td>
                            </tr>
                        </table>
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
                            Width="1800px" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDeleteEntry" CommandName="DeleteEntry" Text="Delete" runat="server" ImageUrl="Content/Images/Delete.png" ToolTip="Delete" Height="15px" Width="15px" OnClientClick="return confirm('Are you sure you want to Delete the record?');" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
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
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("ModifiedLotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paper Quality">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaperQuality" runat="server" Text='<%# Bind("PaperQuality") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Size Of Ticket">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSizeOfTicket" runat="server" Text='<%# Bind("SizeOfTicket") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Press Delivery Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPressDeliveryDate" runat="server" Text='<%# Convert.ToDateTime(Eval("PressDeliveryDate")).ToString("dd-MMM-yyyy")  %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Government Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGovermentOrder" runat="server" Text='<%# Bind("GovermentOrder") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Draw No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Draw Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                                             
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate><asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Slab Limit">
                                    <ItemTemplate><asp:Label ID="lblSlabLimit" runat="server" Text='<%# Bind("SlabLimit") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distributor Rate">
                                    <ItemTemplate><asp:Label ID="lblSyndicateRate" runat="server" Text='<%# Bind("SyndicateRate") %>'></asp:Label></ItemTemplate>
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

