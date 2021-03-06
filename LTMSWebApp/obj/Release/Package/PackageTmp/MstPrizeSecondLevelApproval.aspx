﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="MstPrizeSecondLevelApproval.aspx.cs" Inherits="LTMSWebApp.MstPrizeSecondLevelApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Prize Master</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Second Level Approval"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:HiddenField ID="hdIsUpdate" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                         <tr>
                            <td style="width: 20%; text-align: right;">Government Ordere<font color="red">*</font></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtGovermentOrder" Width="80%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20%; text-align: right;">Un-Sold Percentage</td>
                            <td style="width: 30%;"><asp:TextBox ID="txtUnSoldPercentage" Width="80%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtLotteryType" Width="80%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtLotteryName" Width="80%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right;">Include Cons. Prize</td>
                            <td style="width: 30%;">
                                <asp:CheckBox ID="chkIncludeConsPrize" Enabled="false" Checked="true" runat="server" />
                            </td>
                            <td style="width: 20%; text-align: right;">Claim Days<font color="red">*</font></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtClaimDays" Width="80%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: right;" colspan="4">
                                <div class="gridDivCustom" style="height: 50vh;width:1200px;" id="dvPriveDtl" runat="server">
                                    <asp:GridView ID="gvPrizeDetails" runat="server" ShowFooter="false" CssClass="table table-bordered table-hover" HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px" AutoGenerateColumns="false" Width="1200px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate><asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("RowNo") %>'></asp:Label></ItemTemplate>                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name Of Prize">
                                                <ItemTemplate><asp:TextBox ID="txtNameOfPrize" Width="120px" ReadOnly="true" Text='<%# Bind("NameOfPrize") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prize Amount" ItemStyle-BackColor="#99ccff">
                                                <ItemTemplate><asp:TextBox ID="txtPrizeAmount" Width="100px" Text='<%# Bind("PrizeAmount") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Administrative Charge in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtAdministrativeChargePercentage" Width="100px" Text='<%# Bind("AdministrativeChargePercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Administrative Charge in Rs.">
                                                <ItemTemplate><asp:TextBox ID="txtAdministrativeCharge" Width="100px" ReadOnly="true" Text='<%# Bind("AdministrativeCharge") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Prize Amount">
                                                <ItemTemplate><asp:TextBox ID="txtGrossPrizeAmount" Width="100px" ReadOnly="true" Text='<%# Bind("GrossPrizeAmount") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduction of IT in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtTaxDeductionPercentage" Width="100px" Text='<%# Bind("TaxDeductionPercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Deduction of IT in Rs">
                                                <ItemTemplate><asp:TextBox ID="txtDeductionOfIT" Width="100px" ReadOnly="true" Text='<%# Bind("DeductionOfIT") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable To Wiiner">
                                                <ItemTemplate><asp:TextBox ID="txtPayableToWinner" Width="100px" ReadOnly="true" Text='<%# Bind("PayableToWinner") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Special Ticket Amout" ItemStyle-BackColor="#99ccff">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketAmount" Width="100px"  Text='<%# Bind("SpecialTicketAmount") %>' runat="server"  class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Special Ticket Administrative Charge in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketAdministrativeChargePercentage" Width="100px" Text='<%# Bind("SpecialTicketAdministrativeChargePercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Special Ticket Administrative Charge in Rs.">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketAdministrativeCharge" Width="100px" ReadOnly="true" Text='<%# Bind("SpecialTicketAdministrativeCharge") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Special Ticket Gross Prize Amount">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketGrossPrizeAmount" Width="100px" ReadOnly="true" Text='<%# Bind("SpecialTicketGrossPrizeAmount") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Special Ticket Deduction of IT in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketTaxDeductionPercentage" Width="100px" Text='<%# Bind("SpecialTicketTaxDeductionPercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Special Ticket Deduction of IT in Rs">
                                                <ItemTemplate><asp:TextBox ID="txtSpecialTicketDeductionOfIT" Width="100px" ReadOnly="true" Text='<%# Bind("SpecialTicketDeductionOfIT") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable To Special Ticket Wiiner">
                                                <ItemTemplate><asp:TextBox ID="txtPayableToSpecialTicketWinner" Width="100px" ReadOnly="true" Text='<%# Bind("PayableToSpecialTicketWinner") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Super Ticket Amount" ItemStyle-BackColor="#99ccff">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketAmount" Width="100px"  Text='<%# Bind("SuperTicketAmount") %>' runat="server"  class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Super Ticket Administrative Charge in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketAdministrativeChargePercentage" Width="100px" Text='<%# Bind("SuperTicketAdministrativeChargePercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Super Ticket Administrative Charge in Rs.">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketAdministrativeCharge" Width="100px" ReadOnly="true" Text='<%# Bind("SuperTicketAdministrativeCharge") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Super Ticket Gross Prize Amount">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketGrossPrizeAmount" Width="100px" ReadOnly="true" Text='<%# Bind("SuperTicketGrossPrizeAmount") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Super Ticket Deduction of IT in Percentage(%)">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketTaxDeductionPercentage" Width="100px" Text='<%# Bind("SuperTicketTaxDeductionPercentage") %>'  runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Super Ticket Deduction of IT in Rs">
                                                <ItemTemplate><asp:TextBox ID="txtSuperTicketDeductionOfIT" Width="100px" ReadOnly="true" Text='<%# Bind("SuperTicketDeductionOfIT") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable To Super Ticket Wiiner">
                                                <ItemTemplate><asp:TextBox ID="txtPayableToSuperTicketWinner" Width="100px" ReadOnly="true" Text='<%# Bind("PayableToSuperTicketWinner") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of static Prize Entry">
                                                <ItemTemplate><asp:TextBox ID="txtNoOfWinner" Width="100px" Text='<%# Bind("NoOfWinner") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Digit for Static Entry ">
                                                <ItemTemplate><asp:TextBox ID="txtNoOfDigitInStatic" Width="100px" Text='<%# Bind("NoOfDigitInStatic") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Fixed Or Variable Prize" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFixedOrVariable" runat="server" Checked='<%# (Eval("FixedOrVariable").ToString().ToUpper()=="Y"?true:false) %>'  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Validation for unsold?" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkValidationForUnsold" runat="server" Checked='<%# (Eval("ValidationForUnsold").ToString().ToUpper()=="Y"?true:false) %>'  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description 1">
                                                <ItemTemplate><asp:TextBox ID="txtDescription1" Width="100px" Text='<%# Bind("Description1") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Description 2">
                                                <ItemTemplate><asp:TextBox ID="txtDescription2" Width="100px" Text='<%# Bind("Description2") %>' runat="server" class="form-control"></asp:TextBox></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;" colspan="6">
                                 <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CommandName="Approve" Text="Approve" OnClick="btnSave_Click" />
                                 <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-primary" Text="Reject" CommandName="Reject"  OnClick="btnSave_Click" />
                                 <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                     <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>                                    
                                    <td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 30%;">
                                        <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList>
                                    </td>
                                    <td style="width: 60%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                            Width="70%" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Search.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Status" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Government Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGovermentOrder" runat="server" Text='<%# Bind("GovermentOrder") %>'></asp:Label>
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
