<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxDealerDeposit.aspx.cs" Inherits="LTMSWebApp.TrxDealerDeposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Deposite</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Deposit Amount"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Deposit To<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlDepositTo" Width="40%" runat="server" class="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Deposit Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDepositDate" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                <aspctr:CalendarExtender ID="caltDepositDate" runat="server" CssClass="MyCalendar" TargetControlID="txtDepositDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Deposit Amount<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDepositAmount" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Deposit ID<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDepositId" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Deposit Method<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlDepositMethod" Width="40%" runat="server" class="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Remarks</td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Confirm?');" CommandName="Confirm" OnClick="btnSave_Click" />
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
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add New Deposit Amount" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />
                                </td>
                            </tr>
                        </table>
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
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            Width="90%" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
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

