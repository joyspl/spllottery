<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxViewLotteryClaimEntryPWTSendToGovernment.aspx.cs" Inherits="LTMSWebApp.TrxViewLotteryClaimEntryPWTSendToGovernment" %>

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
            <li class="breadcrumb-item"><a href="#">Prize Claimed Application</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Send To Government for PWT Prize Claimed Application"></asp:Label>
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
                                    <td style="width: 8%;">&nbsp;</td>
                                    <td style="width: 14%;">&nbsp;</td>
                                    <td style="text-align: right; width: 3%;">
                                        <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" />
                                    </td>

                                </tr>
                                <tr>
                                    <td >Lottery Type:</td>
                                    <td >
                                        <asp:DropDownList ID="ddlLotteryType" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; ">Lottery Name:</td>
                                    <td>
                                       <asp:DropDownList ID="ddlLotteryName" Width="100%" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryName_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                    </td>                               
                                    <td style="text-align: right;">Gov. Order No:</td>
                                    <td >
                                        <asp:DropDownList ID="ddlGovernmentOrder" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                                    </td>                                
                                    <td style="text-align: right;">Claim Type:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlClaimType" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; ">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" />
                                    </td>                                    
                                   
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="gridDivCustom" style="height: 50vh;" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            Width="3200px" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkBoxAll" type="checkbox" onclick="checkAllBoxes()" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application ID" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimType" runat="server" Text='<%# Bind("ClaimType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prize Amount" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrizeAmount1" runat="server" Text='<%# Bind("PrizeAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payable To Winner" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayableToWinner1" runat="server" Text='<%# Bind("UpdatedPayableAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prize Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameOfPrize" runat="server" Text='<%# Bind("NameOfPrize") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Goverment Order" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGovermentOrder" runat="server" Text='<%# Bind("GovermentOrder") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LotteryNo" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryNo" runat="server" Text='<%# Bind("LotteryNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MobileNo" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmailId" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AadharNo" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAadharNo" runat="server" Text='<%# Bind("AadharNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PanNo" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPanNo" runat="server" Text='<%# Bind("PanNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankName" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankAccountNo" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankAccountNo" runat="server" Text='<%# Bind("BankAccountNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IFSCCode" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIFSCCode" runat="server" Text='<%# Bind("IFSCCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sent To Gov. Req. Code" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSentToGovReqCode" runat="server" Text='<%# Bind("SentToGovReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>
                    <div class="well well-sm" style="margin-top: 15px;">
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 100%;">
                                    <asp:Button ID="btnSave" runat="server" Text="Send" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Send the claim application to Goverment?');"  CommandName="Approve" OnClick="btnSave_Click" />
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

