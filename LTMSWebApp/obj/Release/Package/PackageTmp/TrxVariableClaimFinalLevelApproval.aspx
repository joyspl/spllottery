<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxVariableClaimFinalLevelApproval.aspx.cs" Inherits="LTMSWebApp.TrxVariableClaimFinalLevelApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type='text/javascript' language="javascript">
         function checkAllBoxes() {
             var totalChkBoxes = parseInt('<%= this.GvData.Rows.Count %>');
            var gvControl = document.getElementById('<%= this.gvClaimUploadDtl.ClientID %>');
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
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Upload</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Final Level Approval by DSL "></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <div style="width: 80%; margin: auto;">
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
                                    <aspctr:CalendarExtender ID="caltDrawDate" runat="server" CssClass="MyCalendar" TargetControlID="txtDrawDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                                </td>
                            </tr>                            
                        </table>
                    </div>
                    <div style="margin-top:20px;"></div>
                    <div class="gridDivCustom" id="dvClaim" style="width: 80%; margin: auto; height: 40vh;" runat="server">
                        <asp:GridView ID="gvClaimUploadDtl" runat="server" CssClass="table table-bordered table-hover"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1400px" 
                            OnRowCommand="gvClaimUploadDtl_RowCommand"
                            OnRowDataBound="gvClaimUploadDtl_RowDataBound">
                            <Columns>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkBoxAll" type="checkbox" onclick="checkAllBoxes()" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Download" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Download" runat="server" ImageUrl="~/Content/Images/Pdf.png" ToolTip="Download" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VoucherNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVoucherDate" runat="server" Text='<%# Convert.ToDateTime(Eval("VoucherDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Count of Claimed Ticket" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimedCount" runat="server" Text='<%# Bind("TicketCount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Payable To Winner" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPayableToWinner" runat="server" Text='<%# Bind("ClaimAmount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Code" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date" ItemStyle-Width="150px">
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
                               
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                       
                    </div>
                     <div class="well well-sm" style="width: 80%;margin: auto;margin-top: 15px;" >
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 25%;">
                                    <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Approve?');"  CommandName="Approve" OnClick="btnSave_Click" />
                                </td>
                                <td style="text-align: left; vertical-align: top; width: 25%;">
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Reject?');" CommandName="Reject" OnClick="btnSave_Click" />
                                </td>
                                 <td style="text-align: left; vertical-align: top; width: 25%;">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"  OnClick="btnCancel_Click" />
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 10%; text-align: right;">To Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;">
                                        <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;">
                                        <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="2250px" 
                            OnRowCommand="GvData_RowCommand" 
                            OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Search.png" ToolTip="View" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("FileUploadStatus") %>'></asp:Label>                                        
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
                                <asp:TemplateField HeaderText="Count of Claimed Ticket" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimedCount" runat="server" Text='<%# Bind("ClaimedCount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Prize Amount" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPrizeAmount" runat="server" Text='<%# Bind("TotPrizeAmount") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Payable To Winner" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPayableToWinner" runat="server" Text='<%# Bind("TotPayableToWinner") %>'></asp:Label></ItemTemplate>
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

