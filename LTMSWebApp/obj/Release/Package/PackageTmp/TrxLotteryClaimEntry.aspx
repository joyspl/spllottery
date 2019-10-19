<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrxLotteryClaimEntry.aspx.cs" Inherits="LTMSWebApp.TrxLotteryClaimEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="StartPageResource/css/bootstrap.min.css" rel="stylesheet" />

<%--    <link href="Design/dist/sidebar-menu.css" rel="stylesheet" />--%>
    <link href="Content/AppStyleSheet.css" rel="stylesheet" />
    <title></title>

    <style>
       
    table.DocUpload td, th {
        padding: 4px 4px 4px 4px;
        vertical-align: top;
        border-bottom: 1px solid;
         border-right: 1px groove;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <aspctr:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></aspctr:ToolkitScriptManager>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="navbar-header" >
                        <%--<a class="navbar-brand" href="#">Logo</a>--%>
                            <asp:HiddenField ID="hdUniqueId" runat="server" />
                    </div>
                    
                    <div class="row content" style="border-top:8px groove #808080; ">
                        <div style="width: 800px; margin: 0 auto;">
                            <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="width: 30%; text-align: right;">Claim Type<font color="red">*</font></td>
                                    <td style="width: 70%;"> <asp:Label ID="lblClaimType" runat="server" Text=""></asp:Label> </td>
                                </tr>
                               
                                <tr>
                                    <td style="width: 30%; text-align: right;">Lottery Type<font color="red">*</font></td>
                                    <td style="width: 70%;">
                                        <asp:DropDownList ID="ddlLotteryType" Width="60%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Lottery Name<font color="red">*</font></td>
                                    <td>
                                        <asp:DropDownList ID="ddlLotteryName" Width="60%" runat="server" class="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Draw No<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtDrawNo" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtDrawDate" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                        <aspctr:CalendarExtender ID="CalExtenderEndDate" runat="server" TargetControlID="txtDrawDate" Format="dd-MMM-yyyy" Enabled="true"></aspctr:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Lottery No<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtLotteryNo" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Mobile No<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtMobileNo" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Email Id<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtEmailId" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Name<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtName" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Name of Father/Husband/Guardian<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtFatherOrGuardianName" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trSoDoWo" runat="server">
                                    <td style="text-align: right;">S/o, D/o, W/o<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtSoDoWo" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trSurety" runat="server">
                                    <td style="text-align: right;">Surety<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtSurety" TextMode="MultiLine" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trProprietorOf" runat="server">
                                    <td style="text-align: right;">Owner/Proprietor of<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtProprietorOf" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Address.<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Aadhar No.<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtAadharNo" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Pan No.<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtPanCard" Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Bank Name & Branch Name<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtBankName"  Width="80%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Bank Account No<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtBankAccountNo"  Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">IFSC Code<font color="red">*</font></td>
                                    <td>
                                        <asp:TextBox ID="txtIFSCCode"  Width="40%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-style: groove; border-width: 2px;">Upload Document<font color="red">*</font></td>
                                    <td style="border-style: groove; border-width: 2px;">
                                        <table class="DocUpload">
                                            <tr id="trPwtTicket" runat="server" visible="false">                                          
                                                <td>Copy Of PWT Ticket</td>
                                                <td>
                                                    <asp:FileUpload ID="fuPWTTicket" runat="server" />
                                                    <asp:Image ID="imgPWTTicket" class="btn btn-primary  btn-sm" runat="server" Height="150px" Width="250px" BackColor="Beige" Visible="false" BorderStyle="Solid"  />
                                                </td>
                                                <td><asp:Button ID="btnPWTTicket" runat="server" class="btn btn-primary  btn-sm" Text="Upload"   BorderWidth="0"  OnClick="btnPWTTicket_Click" /></td>
                                            </tr>
                                            <tr>
                                                <td>Photo</td>
                                                <td>
                                                    <asp:FileUpload ID="flupPhoto" runat="server" />
                                                    <asp:Image ID="imgPhoto" class="btn btn-primary  btn-sm" runat="server" Height="150px" Width="120px" BackColor="Beige" Visible="false" BorderStyle="Solid"  />
                                                </td>
                                                <td><asp:Button ID="btnPhoto" runat="server" class="btn btn-primary  btn-sm" Text="Upload"   BorderWidth="0"  OnClick="btnPhoto_Click" /></td>
                                            </tr>
                                            <tr>
                                                <td>Pan Card</td>
                                                <td>
                                                    <asp:FileUpload ID="flupPan" runat="server" />
                                                    <asp:Image ID="imgPan" runat="server" Height="150px" Width="250px" BackColor="Beige" Visible="false"  BorderStyle="Solid" />
                                                </td>
                                                <td><asp:Button ID="btnUploadPan" runat="server" class="btn btn-primary  btn-sm" Text="Upload"  BorderWidth="0" OnClick="btnUploadPan_Click"  /></td>
                                            </tr>
                                            <tr>
                                                <td>Aadhar Card / Passport</td>
                                                <td>
                                                    <asp:FileUpload ID="flupAadhar" runat="server" />
                                                    <asp:Image ID="imgAadharPic" runat="server" Height="150px" Width="250px" BackColor="Beige" Visible="false" BorderStyle="Solid"  />
                                                </td>
                                                <td><asp:Button ID="btnUploadAadhar" runat="server" class="btn btn-primary  btn-sm" Text="Upload"   BorderWidth="0" OnClick="btnUploadAadhar_Click" /></td>
                                            </tr>
                                            <tr>
                                                <td>Bank Details / Cancelled Cheque</td>
                                                <td>
                                                    <asp:FileUpload ID="flupBankDtl" runat="server" />
                                                    <asp:Image ID="imgBankDtl" class="btn btn-primary  btn-sm" runat="server" Height="150px" Width="250px" BackColor="Beige" Visible="false" BorderStyle="Solid"  />
                                                </td>
                                                <td><asp:Button ID="btnUploadBankDtl" runat="server" class="btn btn-primary  btn-sm" Text="Upload"   BorderWidth="0"  OnClick="btnUploadBankDtl_Click" /></td>
                                            </tr>                                           
                                        </table>                                        
                                    </td>
                                </tr>
                                <tr><td colspan="2" style="border-style: groove; border-width: 2px;color:#4800ff;">Only jpg,gif,png,bmp,jpeg  image type allowed. and file size should not exceed 2mb.</td></tr>
                                <tr>
                                    <td style="text-align: right;"><asp:Button ID="btnConfirm" runat="server" class="btn btn-primary" OnClientClick="return confirm('Are you sure do you want to Submit the form?');" Text="Submit" BorderWidth="0" OnClick="btnSubmit_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" Visible="true" runat="server" class="btn btn-primary" Text="Close" BorderWidth="0" OnClick="btnClose_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
