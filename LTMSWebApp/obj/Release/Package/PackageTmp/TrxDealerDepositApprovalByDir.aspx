<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxDealerDepositApprovalByDir.aspx.cs" Inherits="LTMSWebApp.TrxDealerDepositApprovalByDir" %>
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
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Deposite</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Approve Deposit Amount By Director"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">                
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
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
                            Width="90%"  OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                 <asp:TemplateField>    
                                    <HeaderTemplate><input id="chkBoxAll" type="checkbox"  onclick="checkAllBoxes()" /></HeaderTemplate>
                                    <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>     
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
                    <div class="well well-sm" style="margin-top:15px;">
                 <table style="width: 100%; vertical-align: text-top;" border="0">
                    <tr>
                        <td style="text-align: left; vertical-align: top; width: 50%;">
                            <asp:Button ID="btnSave" runat="server" Text="Approve" CssClass="btn btn-primary" CommandName="Approve"  OnClick="btnSave_Click" />
                        </td>  
                        <td style="text-align: right; vertical-align: top; width: 50%;">
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

