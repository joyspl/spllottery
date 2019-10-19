<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="MstGovermentOrder.aspx.cs" Inherits="LTMSWebApp.MstGovermentOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Master</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Government Order"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryType" Width="40%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged" class="form-control"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td>
                                <asp:DropDownList ID="ddlLotteryName" Width="40%" runat="server" class="form-control" ></asp:DropDownList>
                            </td> 
                        </tr>
                        <tr>
                            <td style="text-align: right;">Government Order<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtGovermentOrder" Width="80%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Modified Lottery Name<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtModifiedLotteryName" Width="80%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr> 
                         <tr>
                            <td style="text-align: right;">Un-Sold Percentage (%)<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtUnSoldPercentage" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">No of Series<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtNoOfAlphabet" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">Series Description<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtAlphabetName" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">Ticket Sl No in each Series, From<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtTicketSlNoFrom" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">Ticket Sl No in each Series, To<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtTicketSlNoTo" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">Total Tickets<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtTotalTickets" Width="40%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  

                          
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary"  OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"  AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server">
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 50%;">
                                    <asp:Button ID="btnAddNew" runat="server" Text="New Government Order" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />&nbsp;
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
                                 <asp:TemplateField HeaderText="Government Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGovermentOrder" runat="server" Text='<%# Bind("GovermentOrder") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Modified Lottery Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModifiedLotteryName" runat="server" Text='<%# Bind("ModifiedLotteryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Un-Sold Percentage">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnSoldPercentage" runat="server" Text='<%# Bind("UnSoldPercentage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NoOfAlphabet">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfAlphabet" runat="server" Text='<%# Bind("NoOfAlphabet") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="AlphabetName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlphabetName" runat="server" Text='<%# Bind("AlphabetName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="TicketSlNoFrom">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketSlNoFrom" runat="server" Text='<%# Bind("TicketSlNoFrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="TicketSlNoTo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketSlNoTo" runat="server" Text='<%# Bind("TicketSlNoTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="TotalTickets">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalTickets" runat="server" Text='<%# Bind("TotalTickets") %>'></asp:Label>
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
