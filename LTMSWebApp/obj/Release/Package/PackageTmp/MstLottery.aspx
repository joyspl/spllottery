<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="MstLottery.aspx.cs" Inherits="LTMSWebApp.MstLottery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script>
        function CalTotRate() {
            var TicketRateLD = (document.getElementById('<%= txtTicketRate.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtTicketRate.ClientID %>').value);
            var RateForSpl = (document.getElementById('<%= txtRateForSpl.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtRateForSpl.ClientID %>').value);
            document.getElementById('<%= txtTotRate.ClientID %>').value = Number(Number(TicketRateLD) + Number(RateForSpl));
           
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Master</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Lottery"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 25%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:DropDownList ID="ddlLotteryType" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                            </td>
                            <td style="width: 25%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtLotteryName" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">No of Charecter including Series<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtNoOfDigit" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Rate for Distributor<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtSyndicateRate" Width="100%" runat="server"   class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;">Rate for Printing per Ticket<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtRateForSpl" Width="100%" runat="server"   class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Ticket MRP<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtTotTicketRate" Width="100%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">GST %<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtGstRate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Prize Category<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtPrizeCategory" Width="100%" MaxLength="2" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Include Consolation Prize<font color="red">*</font></td>
                            <td >
                                <asp:CheckBox ID="chkIncludeConsPrize" Enabled="true" Checked="true" runat="server" />
                            </td>                    
                            <td style="text-align: right;">Claim Days for Fixed Prize<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtClaimDays" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Claim Days for Variable Prize<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtClaimDaysVariable" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Size of Ticket<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtSizeOfTicket" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Paper Quality<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtPaperQuality" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                         <tr>   
                            <td>&nbsp;</td>                        
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary"  OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"  AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                             <td colspan="2">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server">
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 50%;">
                                    <asp:Button ID="btnAddNew" runat="server" Text="New Lottery" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />&nbsp;
                                </td>
                                <td style="text-align: right; vertical-align: top; width: 50%;">
                                    <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            Width="1800px" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
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
                                <asp:TemplateField HeaderText="Lottery Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number of Digit in series">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfDigit" runat="server" Text='<%# Bind("NoOfDigit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Rate for Distributor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSyndicateRate" runat="server" Text='<%# Bind("SyndicateRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SPL Ticket Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSplTicketRate" runat="server" Text='<%# Bind("RateForSpl") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Ticket Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketRate" runat="server" Text='<%# Bind("TotTicketRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="GST Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGstRate" runat="server" Text='<%# Bind("GstRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prize Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrizeCategory" runat="server" Text='<%# Bind("PrizeCategory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Include Cons. Prize">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncludeConsPrize" runat="server" Text='<%# (Eval("IncludeConsPrize").ToString().ToUpper()=="TRUE"?"Yes":"No")   %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Days for Fixed Prize">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimDays" runat="server" Text='<%# Bind("ClaimDays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Days for Variable Prize">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimDaysVariable" runat="server" Text='<%# Bind("ClaimDaysVariable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Size Of Ticket">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSizeOfTicket" runat="server" Text='<%# Bind("SizeOfTicket") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Paper Quality">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaperQuality" runat="server" Text='<%# Bind("PaperQuality") %>'></asp:Label>
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
