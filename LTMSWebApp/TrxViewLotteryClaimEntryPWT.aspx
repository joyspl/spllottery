<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxViewLotteryClaimEntryPWT.aspx.cs" Inherits="LTMSWebApp.TrxViewLotteryClaimEntryPWT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Prize Claimed Application</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="PWT Prize Claimed Application Details"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry" style="width: 100%;">
                        <tr>
                            <td colspan="2" style="font-family: Verdana, sans-serif 16px #4e1b31; color: #582239; border-bottom: 1px groove #808080; font-weight: bold;">
                                <asp:Label ID="lblApplicationId" runat="server" Text="Application Id :"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30%; font-weight: bold;">Lottery Type</td>
                            <td style="width: 70%;">
                                <asp:Label ID="lblLotteryType" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Lottery Name</td>
                            <td>
                                <asp:Label ID="lblLotteryName" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Draw No</td>
                            <td>
                                <asp:Label ID="lblDrawNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Draw Date</td>
                            <td>
                                <asp:Label ID="lblDrawDate" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Claim Type</td>
                            <td>
                                <asp:Label ID="lblClaimType" runat="server" Text=""></asp:Label></td>
                        </tr>
                        
                        <tr>
                            <td style="font-weight: bold;">Prize Type</td>
                            <td>
                                <asp:Label ID="lbllblNameOfPrize" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Lottery No</td>
                            <td>
                                <asp:Label ID="lblLotteryNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Mobile No</td>
                            <td>
                                <asp:Label ID="lblMobileNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Email Id</td>
                            <td>
                                <asp:Label ID="lblEmailId" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Name</td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                        </tr>
                         <tr>
                            <td style="font-weight: bold;">Name of Father/Husband/Guardian</td>
                            <td>
                                <asp:Label ID="lblFatherOrGuardianName" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Address.</td>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Aadhar No.</td>
                            <td>
                                <asp:Label ID="lblAadharNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Pan No.</td>
                            <td>
                                <asp:Label ID="lblPanCard" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Bank Name</td>
                            <td>
                                <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Bank Account No</td>
                            <td>
                                <asp:Label ID="lblBankAccountNo" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">IFSC Code</td>
                            <td>
                                <asp:Label ID="lblIFSCCode" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                    <table border="0" class="divDataEntry" style="width: 100%; border-top: 2px groove #808080;">
                        <tr>
                            <td style="font-weight: bold;">Photo</td>
                            <td>
                                <asp:Image ID="imgPhoto" runat="server" Height="150px" Width="100px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td>
                        </tr>
                        <tr id="trPwtTicket" runat="server" visible="false">
                            <td style="font-weight: bold;">Prize Winning Ticket</td>
                            <td>
                                <asp:Image ID="imgPwtTicket" runat="server" Height="200px" Width="300px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Pan Card</td>
                            <td>
                                <asp:Image ID="imgPan" runat="server" Height="250px" Width="350px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Aadhar Card</td>
                            <td>
                                <asp:Image ID="imgAadharPic" runat="server" Height="250px" Width="350px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Bank Details</td>
                            <td>
                                <asp:Image ID="imgBankDtl" runat="server" Height="250px" Width="350px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td>
                        </tr>

                    </table>
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Status</td>
                            <td style="width: 50%;">
                                <div class="form-control" style="padding: 4px;">
                                    <asp:RadioButton ID="rdoApproveStatus1" GroupName="ApproveStatus"  runat="server" Text="Approve" />
                                    <asp:RadioButton ID="rdoApproveStatus2" GroupName="ApproveStatus"  runat="server" Text="Reject" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Prize Amount</td>
                            <td>
                                <asp:TextBox ID="txtPrizeAmount" Width="40%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold;">Payable to Winner</td>
                            <td>
                                <asp:TextBox ID="txtPayableToWinner" Width="40%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 50%; text-align: right;">Remarks<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="100%" MaxLength="250" Height="120px" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;" colspan="2">
                                <asp:Button ID="btnConfirm" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Confirm" OnClick="btnSave_Click" />
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
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="3000px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Print" runat="server" ImageUrl="~/Content/Images/Print.png" ToolTip="Print" Height="20px" Width="20px" />
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="Name of Father/Husband/Guardian" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFatherOrGuardianName" runat="server" Text='<%# Bind("FatherOrGuardianName") %>'></asp:Label></ItemTemplate>
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
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

