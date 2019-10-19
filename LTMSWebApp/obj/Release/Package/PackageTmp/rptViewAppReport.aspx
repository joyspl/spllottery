<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="rptViewAppReport.aspx.cs" Inherits="LTMSWebApp.rptViewAppReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Report</a>
            </li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblReport" runat="server" Text="Lottery"></asp:Label></li>
        </ol>
        <div class="row">
            <div class="col-12">
                <div id="pnlReportData" runat="server" visible="true" style="position: relative;">
                    <div>
                        <table style="width: 99%; vertical-align: text-top;" border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td style="text-align: right; vertical-align: top; width: 100%;">
                                    <asp:Button ID="btnBack" runat="server" Text="" CssClass="btnBack" AccessKey="B" OnClick="btnCancel_Click" ToolTip="Back (Alt + B)" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="gridReportTable" runat="server">
                        <rsweb:ReportViewer ID="rptViewer" runat="server" Visible="false" InteractiveDeviceInfos="(Collection)" 
                            
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

