<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="appError.aspx.cs" Inherits="LTMSWebApp.appError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div id="dvContent" runat="server" style="width: 965px; height: 50px; border: 0px solid #277a91;">
        <table style="width: 945px; vertical-align: text-top;" border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td style="text-align: left; vertical-align: top; width: 945px; font-family: Verdana, Sans-Serif; font-size: 12px; font-weight: bold; color: Red;">
                    <asp:Literal ID="ltrlError" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
