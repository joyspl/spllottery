<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxPeriodicMISReport.aspx.cs" Inherits="LTMSWebApp.TrxPeriodicMISReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">MIS Report</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Report"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">

                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server" style="width: 100%;">
                        <div style="margin-top: 10px; border: 1px solid #277a91; padding: 15px;">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td style="width: 25%;">From Date:</td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 25%; text-align: right;">To Date:</td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>                                   
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Report Type<font color="red">*</font></td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlReportType" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">&nbsp;</td>
                                    <td colspan="3">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" CommandName="View" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>

