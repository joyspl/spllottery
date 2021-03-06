﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxTicketInventoryChallan.aspx.cs" Inherits="LTMSWebApp.TrxTicketInventoryChallan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript' language="javascript">
        function checkAllBoxes() {
            var totalChkBoxes = parseInt('<%= this.gvChallan.Rows.Count %>');
            var gvControl = document.getElementById('<%= this.gvChallan.ClientID %>');
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
    <asp:HiddenField ID="hdChallanId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Challan</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Update Challan"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                         <tr>
                            <td style="width: 25%; text-align: right;">Requisition No<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtReqCode" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                            <td style="width: 25%; text-align: right;">Requisition Date<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtReqDate" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtLotteryName" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style=" text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtLotteryType" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Draw No<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtDrawNo" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDrawDate" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>

                            </td>
                        </tr>                      
                        <tr>
                            <td style="text-align: right;">Transporter Name<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtTransporterName" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Consignment No<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtConsignmentNo" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                        <tr>
                            <td style="text-align: right;">Customer Order No<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtCustomerOrderNo" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Customer Orde Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtCustomerOrdeDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                <aspctr:CalendarExtender ID="CalCustomerOrdeDate" runat="server" TargetControlID="txtCustomerOrdeDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                            </td>
                        </tr>  
                        <tr>
                            <td style="text-align: right;">VehicleNo<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtVehicleNo" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Subject<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSubject" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                        <tr>
                            <td style="text-align: right;">SACCode<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtSACCode" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">No<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtNo" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                        <tr>
                            <td style="text-align: right;">Delivered Quantity<font color="red">*</font></td>
                            <td >
                                <asp:TextBox ID="txtDeliveredQuantity" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">No Of Box Bundle<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtNoOfBoxBundle" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                         <tr>
                            <td style="text-align: right;">Remarks<font color="red">*</font></td>
                            <td  colspan="3">
                                <asp:TextBox ID="txtRemarks" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>  
                        <tr>
                            <td style="text-align: right;">&nbsp;</td>
                           <td colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" CommandName="Save" OnClick="btnSave_Click" />                               
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="dvChallanDtl" runat="server" style="position: relative;" Visible="false">
                    <div id="Div2" runat="server" style="width: 100%;">
                         <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add New Challan" CssClass="btn btn-primary" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="gridDiv" id="Div3" runat="server">
                        <asp:GridView ID="gvChallan" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="ChallanId" AutoGenerateColumns="false" Width="2250px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="gvChallan_RowCommand" OnRowDataBound="gvChallan_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDeleteEntry" CommandName="DeleteEntry" Text="Delete" runat="server" ImageUrl="Content/Images/Delete.png" ToolTip="Delete" Height="15px" Width="15px" OnClientClick="return confirm('Are you sure you want to Delete the record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkBoxAll" type="checkbox" onclick="checkAllBoxes()" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" /></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ChallanStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Challan Code" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChallanNo" runat="server" Text='<%# Bind("ChallanNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChallanDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ChallanDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                                       
                                <asp:TemplateField HeaderText="Customer Order No." ItemStyle-Width="180px">
                                    <ItemTemplate><asp:Label ID="lblCustomerOrderNo" runat="server" Text='<%# Bind("CustomerOrderNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Requisition Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerOrdeDate" runat="server" Text='<%# Convert.ToDateTime(Eval("CustomerOrdeDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                  
                                <asp:TemplateField HeaderText="Transporter Name" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblTransporterName" runat="server" Text='<%# Bind("TransporterName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consignment No"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblConsignmentNo" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="VehicleNo" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblVehicleNo" runat="server" Text='<%# Bind("VehicleNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Subject" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="SACCode" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblSACCode" runat="server" Text='<%# Bind("SACCode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                       
                                 <asp:TemplateField HeaderText="Delivered Quantity" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblDeliveredQuantity" runat="server" Text='<%# Bind("DeliveredQuantity") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="No Of Box Bundle" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblNoOfBoxBundle" runat="server" Text='<%# Bind("NoOfBoxBundle") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>  
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>
                    <div style="margin-top:15px;"></div>
                    <table>
                        <tr>
                            <td style="text-align: left;">
                                 <asp:Button ID="btnSubmit" runat="server" Text="Confirm" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Confirm?');" CommandName="Approve" OnClick="btnConfirm_Click" />
                            </td>
                           <td style="text-align: right;"> <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                </div>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 10%;">From Date:</td>
                                    <td style="width: 15%;"><asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 10%; text-align: right;">To Date:</td><td style="width: 15%;"><asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td><td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;"><asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;"><asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;"><asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1750px" 
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
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate><asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label></ItemTemplate>
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

