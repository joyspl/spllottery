<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxGenerateLot.aspx.cs" Inherits="LTMSWebApp.TrxGenerateLot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document.body).on("keydown", ".number", function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1
                || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true))
                || (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57))
                && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    </script>
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Press Activity</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Generate Lot Number"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdnID" Value="0" runat="server" />
                <asp:HiddenField ID="hdnReqID" Value="0" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="text-align: right;">Requisition No.<font color="red">*</font></td>
                            <td>
                                <asp:DropDownList ID="ddlReqNo" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                            </td>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Series1 Start<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSeries1Start" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Series1 End<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSeries1End" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Series2 Start<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSeries2Start" Width="100%" runat="server" class="form-control text-uppercase"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Series2 End<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSeries2End" Width="100%" runat="server" class="form-control text-uppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Start Number<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtStartNumber" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">End Number<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtEndNumber" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="3" style="text-align: right;">
                                <asp:Button ID="btnSaveData" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure to save lot information?');" CommandName="Save" OnClick="btnSaveData_Click" />
                                &nbsp;
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
                                    <td style="width: 10%; text-align: right;">
                                        <asp:Button runat="server" ID="btnAddNewLot" Text="Add New Lot" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New Lot (Alt + N)" OnClick="btnAddNewLot_Click" />
                                    </td>
                                    <td style="width: 10%; text-align: right;">Requisition No.</td>
                                    <td style="width: 20%;">
                                        <asp:DropDownList ID="ddlReqNumber" Width="100%" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlReqNumber_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="gvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="ID" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="gvData_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqId" runat="server" Text='<%# Bind("ReqId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Series1 Start">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeries1Start" runat="server" Text='<%# Bind("Series1Start") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Series1 End">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeries1End" runat="server" Text='<%# Bind("Series1End") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Series2 Start">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeries2Start" runat="server" Text='<%# Bind("Series2Start") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Series2 End">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeries2End" runat="server" Text='<%# Bind("Series2End") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumStart" runat="server" Text='<%# Bind("NumStart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumEnd" runat="server" Text='<%# Bind("NumEnd") %>'></asp:Label>
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

