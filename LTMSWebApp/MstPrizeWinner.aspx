<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="MstPrizeWinner.aspx.cs" Inherits="LTMSWebApp.MstPrizeWinner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Winner Prize</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Prize Winner Entry"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 15%; text-align: right;">Lottery Type</td>
                            <td style="width: 20%;"><asp:TextBox ID="txtLotteryType" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 15%; text-align: right;">Lottery Name</td>
                            <td style="width: 20%;"><asp:TextBox ID="txtLottery" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                            <td style="width: 15%; text-align: right;">Draw Date</td>
                            <td style="width: 15%;"><asp:TextBox ID="txtDrawDate" Width="100%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td style="text-align: right;">Requisition No<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtReqCode" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Requisition Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtReqDate" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                             <td style="text-align: right;">Draw No<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDrawNo" Width="100%" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;">Judge Name 1<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtJudgesName1" Width="100%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Judge Name 2</td>
                            <td>
                                <asp:TextBox ID="txtJudgesName2" Width="100%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                             <td style="text-align: right;">Judge Name 3</td>
                            <td>
                                <asp:TextBox ID="txtJudgesName3" Width="100%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Venue<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtPlayingAddress" Width="100%" runat="server"  class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Claim Date</td>
                            <td>
                                <asp:TextBox ID="txtClaimDate" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Draw Time</td>
                            <td>
                                <asp:TextBox ID="txtDrawTime" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: right;" colspan="6">
                                <div class="gridDivCustom" style="height: 55vh;" id="dvPriveDtl" runat="server">
                                    <asp:GridView ID="gvPrizeWinnerDetails" DataKeyNames="RowNo" runat="server" OnRowDataBound="gvPrizeWinnerDetails_RowDataBound" CssClass="table table-bordered table-hover" HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px" AutoGenerateColumns="false" Width="900px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row No." ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                   <%-- <asp:HiddenField ID="hdUniqueId" Value='<%# Bind("RowNo") %>' runat="server" />--%>
                                                     <asp:CheckBox ID="chkValidationForUnsold" Visible="false" runat="server" Checked='<%# (Eval("ValidationForUnsold").ToString().ToUpper()=="Y"?true:false) %>'  />
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("RowNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name Of Prize" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("NameOfPrize") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prize Amount" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrizeAmount" runat="server" Text='<%# Bind("PrizeAmount") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="No of static Prize Entry" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfWinner" runat="server" Text='<%# Bind("NoOfWinner") %>' ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:Label>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="No of Digit for Static Entry" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfDigitInStatic" runat="server" Text='<%# Bind("NoOfDigitInStatic") %>' ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wining Serial No" ItemStyle-Width="250px" >
                                                <ItemTemplate>
                                                    <asp:GridView ID="gvWiningSerialNo" Width="250px" 
                                                        runat="server" ShowHeader="false" AutoGenerateColumns="false">
                                                         <Columns>
                                                             <asp:TemplateField HeaderText="Row No" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("WiningSerialRowNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="No Of Winner" ItemStyle-Width="150px">
                                                                <ItemTemplate>
                                                                     <asp:TextBox ID="txtWiningSerialNo" Width="150px" Text='<%# Bind("WiningSerialNo") %>'  runat ="server" class="form-control input-lg"></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                             </asp:TemplateField>
                                                         </Columns>
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="6">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CommandName="Save" Text="Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-primary" CommandName="Confirm" Text="Submit" OnClick="btnSave_Click" />
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
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1450px" 
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="~/Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Download" runat="server" ImageUrl="~/Content/Images/Pdf.png" ToolTip="Download" Height="15px" Width="15px" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblStatus" runat="server" Text='<%# Bind("SaveStatus") %>'></asp:Label></ItemTemplate>
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
