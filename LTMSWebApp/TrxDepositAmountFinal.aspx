﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxDepositAmountFinal.aspx.cs" Inherits="LTMSWebApp.TrxDepositAmountFinal" %>

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
    <asp:HiddenField ID="hdDepositId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Upload</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Deposit Entry by Distributor"></asp:Label>
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

                            <tr>
                                <td style="text-align: right;">Deposit To<font color="red">*</font></td>
                                <td>
                                    <asp:DropDownList ID="ddlDepositTo" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                                </td>
                                <td style="text-align: right;">Deposit Date<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtDepositDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    <aspctr:CalendarExtender ID="caltDepositDate" runat="server" CssClass="MyCalendar" TargetControlID="txtDepositDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align: right;">Deposit Amount<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtDepositAmount" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>

                                <td style=" text-align: right;">Deposit ID<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtDepositId" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align: right;">Deposit Method<font color="red">*</font></td>
                                <td>
                                    <asp:DropDownList ID="ddlDepositMethod" Width="100%" runat="server" class="form-control" OnSelectedIndexChanged="ddlDepositMethod_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="rwBankGuarantee" runat="server" visible="false">
                                <td style=" text-align: right;">Bank Name<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtBankName" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>

                                <td style=" text-align: right;">Bank Gurantee Validity Days<font color="red">*</font></td>
                                <td>
                                    <asp:TextBox ID="txtBankValidityDays" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align: right;">Remarks</td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">&nbsp;</td>
                                <td colspan="3">
                                    <asp:Button ID="btnUpload" runat="server" CommandName="Upload" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure do you want to Upload?');" Text="Upload" />
                                    <%-- <asp:Button ID="btnConfirm" runat="server" Text="Save" CssClass="btn btn-primary"  CommandName="Confirm" OnClick="btnSave_Click" />--%>
                                    <asp:Button ID="btnCancel" runat="server" AccessKey="C" CssClass="btn btn-primary" OnClick="btnCancel_Click" Text="Cancel" ToolTip="Cancel (Alt + C)" />
                                </td>
                        </table>
                    </div>
                    <div style="margin-top: 20px;"></div>
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
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDeleteEntry" CommandName="DeleteEntry" Text="Delete" runat="server" ImageUrl="Content/Images/Delete.png" ToolTip="Delete" Height="15px" Width="15px" OnClientClick="return confirm('Are you sure you want to Delete the record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposited To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositTo" runat="server" Text='<%# Bind("DepositTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DepositDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositAmount" runat="server" Text='<%# Bind("DepositAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositId" runat="server" Text='<%# Bind("DepositId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit Method">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositMethod" runat="server" Text='<%# Bind("DepositMethod") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Validity Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBGValidityDays" runat="server" Text='<%# Bind("BGValidityDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>
                    <div class="well well-sm" style="width: 80%;margin: auto;margin-top: 15px;" >
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 25%;">
                                    <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Approve?');"  CommandName="Confirm" OnClick="btnApprove_Click" />
                                </td>
                                <td style="width: 75%;"></td>
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
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label>
                                        <asp:Label ID="lblTicketClaimedUploadStatus" Visible="false" runat="server" Text='<%# Bind("TicketClaimedUploadStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Count of Claimed Ticket" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimedCount" runat="server" Text='<%# Bind("ClaimedCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Prize Amount" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPrizeAmount" runat="server" Text='<%# Bind("TotPrizeAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Payable To Winner" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotPayableToWinner" runat="server" Text='<%# Bind("TotPayableToWinner") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnStart" runat="server" Text='<%# Bind("FnStart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnEnd" runat="server" Text='<%# Bind("FnEnd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alphabet Series" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlphabetSeries" runat="server" Text='<%# Bind("AlphabetSeries") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnStart" runat="server" Text='<%# Bind("TnStart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnEnd" runat="server" Text='<%# Bind("TnEnd") %>'></asp:Label>
                                    </ItemTemplate>
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

