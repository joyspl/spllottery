<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxValidateTicket.aspx.cs" Inherits="LTMSWebApp.TrxValidateTicket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Validate Ticket</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Validate"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="true">
                <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryType" Width="40%" runat="server"  class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLotteryType_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Name<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLotteryName" Width="40%" runat="server" class="form-control"  ></asp:DropDownList></td> 
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw Date<font color="red">*</font></td>
                            <td style="width: 50%;">
                                <asp:TextBox ID="txtDrawDate" Width="40%" runat="server"  class="form-control"></asp:TextBox>
                            </td>                                
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%; padding-left:70px;">OR</td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Draw No<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:TextBox ID="txtDrawNo" Width="40%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">Lottery Ticket No<font color="red">*</font></td>
                            <td style="width: 50%;"><asp:TextBox ID="txtLotteryNo" Width="40%" runat="server" class="form-control"></asp:TextBox></td>
                        </tr>
                        
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;" >
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" CommandName="Save" OnClick="btnView_Click"  />                                
                                <asp:Button ID="btnCancel" runat="server" Text="Clear All" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr style="text-align: center;">
                            <td colspan="2"><asp:Label ID="lblValidTicket" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                   
                </asp:Panel>

            </div>
        </div>
    </div>
</asp:Content>

