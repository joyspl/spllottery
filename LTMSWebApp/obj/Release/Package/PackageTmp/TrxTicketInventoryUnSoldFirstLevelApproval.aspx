<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxTicketInventoryUnSoldFirstLevelApproval.aspx.cs" Inherits="LTMSWebApp.TrxTicketInventoryUnSoldFirstLevelApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript' language="javascript">
        function checkAllBoxes() {
            var totalChkBoxes = parseInt('<%= this.GvData.Rows.Count %>');
        var gvControl = document.getElementById('<%= this.GvData.ClientID %>');
        var gvChkBoxControl = "chkSelect";
        var mainChkBox = document.getElementById("chkBoxAll");
        var inputTypes = gvControl.getElementsByTagName("input");
        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Ticket Inventory</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="First Level Approve Un Sold Ticket "></asp:Label>
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

                    <div class="gridDivCustom" style="height: 60vh;" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            Width="1200px" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkBoxAll" type="checkbox" onclick="checkAllBoxes()" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Download" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Download" runat="server" ImageUrl="~/Content/Images/Pdf.png" ToolTip="Download" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
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
                                
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>
                    <div class="well well-sm" style="margin-top: 15px;">
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 100%;">
                                    <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Approve?');"  CommandName="Approve" OnClick="btnSave_Click" />
                                </td>
                                <td style="text-align: left; vertical-align: top; width: 100%;">
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Reject?');" CommandName="Reject" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

