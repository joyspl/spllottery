﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxRequisitionByDealer.aspx.cs" Inherits="LTMSWebApp.TrxRequisitionByDealer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript' language="javascript">
        function checkAllBoxes() {
            var totalChkBoxes = parseInt('<%= this.gvAvailableInventory.Rows.Count %>');
                var gvControl = document.getElementById('<%= this.gvAvailableInventory.ClientID %>');
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
            <li class="breadcrumb-item"><a href="#">Requisition & Issue By Dealer</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Requisition By Dealer"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />                 
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false" style="width: 950px; margin: auto;margin-top: 5px;" >
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 100%;" colspan="4">
                                <div style="width: 100%; margin: auto; border: 1px solid #277a91; padding: 5px; margin-top: 15px;">
                                    <table border="0">
                                        <tr>
                                            <td style="width: 25%; text-align: right;">Requisition No<font color="red">*</font></td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtReqCode" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;">Requisition Date<font color="red">*</font></td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtReqDate" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="4">
                                <div style="width: 100%; margin: auto; border: 1px solid #277a91;  padding: 5px; margin-top: 15px;">
                                    <table border="0">
                                        <tr>
                                            <td style="width: 24%; text-align: right;">Deposit Amount on DSL A/c (Rs.)</td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtDepositeAmountLD" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                                             <td style="width: 24%; text-align: right;">Bank Gurantee on DSL A/c (Rs.)</td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtBankGurantee" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>

                                            <td style="width: 1%; text-align: right;">&nbsp;</td>
                                            <td style="width: 1%;"><asp:TextBox ID="txtDepositeAmountSPL" Width="100%" Visible="false" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                                          
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="4">
                                <div  style="width: 100%; margin: auto; height: 30vh;border: 1px solid #277a91;overflow: auto; padding: 15px;margin-top: 15px;" id="gvInventory" runat="server">
                                    <asp:GridView ID="gvAvailableInventory" runat="server" CssClass="table table-bordered table-hover"
                                        DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                                        HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                                        Width="900px" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                                        <Columns>
                                           <asp:TemplateField HeaderText="Requisition Code">
                                                <ItemTemplate>
                                                      <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Draw No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Draw Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                     <asp:HiddenField ID="hdTicketRate" Value='<%# Bind("SyndicateRate") %>' runat="server" /> 
                                                    <asp:HiddenField ID="hdRateForSpl" Value='<%# Bind("RateForSpl") %>' runat="server" />                                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity Required">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQtyRequired" ReadOnly="true" Width="150px" Text='<%# Bind("Qty") %>'  runat="server" class="form-control" Font-Size="12px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100%;" colspan="4">
                                <div style="width: 100%; margin: auto; border: 1px solid #277a91;  padding: 5px; margin-top: 15px;">
                                    <table border="0">
                                        
                                        <tr>
                                            <td style="width: 50%; text-align: right;">Ticket Amount (Rs.)</td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtLDBalence" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtSPLBalence" Width="100%" Visible="false" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; " colspan="2">
                                 <asp:Button ID="btnSave" runat="server" Text="Save" Width="150px" CssClass="btn btn-primary" CommandName="Save" OnClick="btnSave_Click" />
                                
                            </td>
                            <td style="width: 50%;text-align: right;" colspan="2"> 
                                <asp:Button ID="btnSubmit" runat="server" Text="Confirm" Width="150px" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Confirm?');" CommandName="Confirm" OnClick="btnSave_Click" />
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
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1850px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                                         
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Lottery Name"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="First Start No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblFnStart" runat="server" Text='<%# Bind("FnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="First End No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblFnEnd" runat="server" Text='<%# Bind("FnEnd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Alphabet Series" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblAlphabetSeries" runat="server" Text='<%# Bind("AlphabetSeries") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                       
                                 <asp:TemplateField HeaderText="Third Start No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblTnStart" runat="server" Text='<%# Bind("TnStart") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Third End No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblTnEnd" runat="server" Text='<%# Bind("TnEnd") %>'></asp:Label></ItemTemplate>
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

