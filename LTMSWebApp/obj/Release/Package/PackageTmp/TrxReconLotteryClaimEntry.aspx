<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxReconLotteryClaimEntry.aspx.cs" Inherits="LTMSWebApp.TrxReconLotteryClaimEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Deposite</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Reconcile Deposit Amount"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">

               
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                        <%--<aspctr:CalendarExtender ID="calFromDate" runat="server"  TargetControlID="txtFromDate" Format="dd-MMM-yyyy" ></aspctr:CalendarExtender>--%>
                                    </td>
                                    <td style="width: 10%; text-align: right;">To Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                        <%--<aspctr:CalendarExtender ID="calToDate" runat="server"  TargetControlID="txtToDate" Format="dd-MMM-yyyy" ></aspctr:CalendarExtender>--%>
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
                            Width="90%" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Reconcilation" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Lottery Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DrawNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Deposit Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MobileNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmailId">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailId" runat="server" Text='<%# Bind("EmailId") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryNo" runat="server" Text='<%# Bind("LotteryNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="AadharNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAadharNo" runat="server" Text='<%# Bind("AadharNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="PanNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPanNo" runat="server" Text='<%# Bind("PanNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bank Account No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankAccountNo" runat="server" Text='<%# Bind("BankAccountNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="IFSCCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIFSCCode" runat="server" Text='<%# Bind("IFSCCode") %>'></asp:Label></ItemTemplate>
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

