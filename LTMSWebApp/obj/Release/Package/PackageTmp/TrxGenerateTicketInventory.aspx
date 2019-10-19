<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="TrxGenerateTicketInventory.aspx.cs" Inherits="LTMSWebApp.TrxGenerateTicketInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document.body).on("keydown", ".number", function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1
                || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true))
                || (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57))
                && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        function DisableEnableDownload(isdisabled) {
            $('#<%=btnZip.ClientID%>').prop("disabled", isdisabled);
            $('#<%=ddlLotInfo.ClientID%>').prop("disabled", isdisabled);
            $('#<%=btnDynGen.ClientID%>').prop("disabled", isdisabled);
        }

        function CheckZipProgress(zipid) {
            var pgUrl = '<%=ResolveUrl("hxCheckProgress.ashx")%>';
            $.ajax({
                url: pgUrl + '/GetCurrentZipProgress?id=' + zipid,
                type: 'GET'
            }).done(function (data) {
                if (data.Success > 0) {
                    $('#<%=lblProgress.ClientID%>').html("Zipping file, " + data.Message + "% done...");
                    if (data.Message == "100") {
                        setTimeout(function () {
                            DisableEnableDownload(false);
                            window.open('<%=ResolveUrl("hxDownloader.ashx")%>' + '?user=' + $('#<%=hdnUserID.ClientID%>').val() + '&key=' + $('#<%=hdnReqCode.ClientID%>').val(), '_blank', '');
                            $('#<%=lblProgress.ClientID%>').empty();
                        }, 3000);
                    } else {
                        if (data.Message == "-1") {
                            $('#<%=lblProgress.ClientID%>').empty().html("Download process failed. Please try again.");
                            DisableEnableDownload(false);
                        } else {
                            setTimeout(CheckZipProgress(zipid), 2000);
                        }
                    }
                }
            });
        }
    </script>
    <asp:HiddenField ID="hdnUserID" runat="server" />
    <asp:HiddenField ID="hdnReqCode" runat="server" />
    <asp:HiddenField ID="hdUniqueId" runat="server" />
    <asp:HiddenField ID="hdnSlabLimit" runat="server" />
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">Press Activity</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="Generate Ticket"></asp:Label>
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
                            <td style="text-align: right;">Modified Lottery Name<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtModifiedLotteryName" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Lottery Type<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtLotteryType" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Draw No<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDrawNo" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Draw Date<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtDrawDate" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">Slab Limit<font color="red">*</font></td>
                            <td>
                                <asp:TextBox ID="txtSlabLimit" ReadOnly="true" Width="100%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: left;" colspan="3">
                                <font color="green"><small><asp:Label runat="server" ID="lblDefaultSlabLimitThreshold"></asp:Label></small></font>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" /></td>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2" style="width: 20%; text-align: right; border-left: 1px solid #ccc; border-bottom: 1px solid #ccc; border-top: 1px solid #ccc;">Lot<font color="red">*</font></td>
                                        <td colspan="10" style="width: 60%; border-right: 1px solid #ccc; border-bottom: 1px solid #ccc; border-top: 1px solid #ccc;">
                                            <asp:DropDownList ID="ddlLotInfo" Width="100%" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLotInfo_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <%--<td style="width: 20%; border-right: 1px solid #ccc; border-bottom: 1px solid #ccc; border-top: 1px solid #ccc;">
                                            <asp:Button ID="btnDynGen" runat="server" Text="Generate" CssClass="btn btn-info" AccessKey="G" ToolTip="Cancel (Alt + G)" OnClick="btnDynGen_Click" />
                                        </td>--%>
                                    </tr>
                                    <tr runat="server" id="trHdr">
                                        <td colspan="4" style="text-align: center; border: 1px solid #ccc;">
                                            Series 1
                                        </td>
                                        <td colspan="4" style="text-align: center; border: 1px solid #ccc;">
                                            Series 2
                                        </td>
                                        <td colspan="4" style="text-align: center; border: 1px solid #ccc;">
                                            Number
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr runat="server" id="trDtl">
                                        <td style="text-align: right; border-left: 1px solid #ccc; border-bottom: 1px solid #ccc;">From</td>
                                        <td style="border-bottom: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtSeries1From" runat="server" class="form-control number"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; border-bottom: 1px solid #ccc;">To</td>
                                        <td style="border-right: 1px solid #ccc; border-bottom: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtSeries1To" runat="server" class="form-control number"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; border-bottom: 1px solid #ccc;">From</td>
                                        <td style="border-bottom: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtSeries2From" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; border-bottom: 1px solid #ccc;">To</td>
                                        <td style="border-bottom: 1px solid #ccc; border-right: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtSeries2To" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; border-bottom: 1px solid #ccc;">From</td>
                                        <td style="border-bottom: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtNumFrom" runat="server" class="form-control number"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; border-bottom: 1px solid #ccc;">To</td>
                                        <td style="border-bottom: 1px solid #ccc; border-right: 1px solid #ccc;">
                                            <asp:TextBox ReadOnly="true" ID="txtNumTo" runat="server" class="form-control number"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDynGen" runat="server" Text="Generate" CssClass="btn btn-info" AccessKey="G" ToolTip="Cancel (Alt + G)" OnClick="btnDynGen_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlDownloadOrder" runat="server" Visible="false">
                                                <asp:ListItem Value="0" Text="Decending"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Ascending"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="btnZip" CssClass="btn btn-warning" Text="Download All" runat="server" AccessKey="Z" ToolTip="Cancel (Alt + Z)" OnClick="btnZip_Click" />
                                        </td>
                                        <%--<td colspan="3">
                                            <asp:Button ID="btnDownloadAllLots" CssClass="btn btn-warning" Text="Download All Lots" runat="server" AccessKey="Q" ToolTip="Cancel (Alt + Q)" OnClick="btnDownloadAllLots_Click" />
                                        </td>--%>
                                        <td colspan="8">
                                            <asp:Label runat="server" ID="lblProgress"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            &nbsp;
                                            <asp:HiddenField runat="server" ID="hdnGenID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <div class="gridDivCustom" id="Div1" style="margin-left: auto; margin-right: auto;" runat="server">
                                                <asp:GridView ID="gvGenarateNo" runat="server" CssClass="table table-bordered table-hover"
                                                    AutoGenerateColumns="false" Width="750px"
                                                    HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                                                    OnRowCommand="gvGenarateNo_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="InsertedId" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInsertedId" runat="server" Text='<%# Bind("InsertedId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RowFnStart" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowFnStart" runat="server" Text='<%# Bind("RowFnStart") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RowFnEnd" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowFnEnd" runat="server" Text='<%# Bind("RowFnEnd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RowFnAlphabet" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowFnAlphabet" runat="server" Text='<%# Bind("RowFnAlphabet") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Row No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("RowNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Initial Sl. No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInitialSlNo" runat="server" Text='<%# Bind("InitialSlNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Start No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndNo" runat="server" Text='<%# Bind("EndNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="End No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartNo" runat="server" Text='<%# Bind("StartNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgDownload" CommandName="Download" Text="Download" runat="server" ImageUrl="~/Content/Images/Load.png" ToolTip="Download" Height="15px" Width="15px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
                                        <asp:TextBox ID="txtFromDate" Width="100%" runat="server" class="form-control"></asp:TextBox></td>
                                    <td style="width: 10%; text-align: right;">To Date:</td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtToDate" Width="100%" runat="server" class="form-control"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: right;">Status:</td>
                                    <td style="width: 15%;">
                                        <asp:DropDownList ID="ddlStatus" Width="100%" runat="server" class="form-control"></asp:DropDownList></td>
                                    <td style="width: 10%;">
                                        <asp:Button ID="btnGo" runat="server" Text="" CssClass="btnGo" OnClick="btnGo_Click" AccessKey="G" ToolTip="Go (Alt + G)" /></td>
                                    <td style="text-align: right; width: 15%;">
                                        <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="1750px"
                            HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                            OnRowCommand="GvData_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit/View/<br/> Print" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqCode" runat="server" Text='<%# Bind("ReqCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReqDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lottery Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified Lottery Name" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModifiedLotteryName" runat="server" Text='<%# Bind("ModifiedLotteryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnStart" runat="server" Text='<%# Bind("FnStart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFnEnd" runat="server" Text='<%# Bind("FnEnd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alphabet Series" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlphabetSeries" runat="server" Text='<%# Bind("AlphabetSeries") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third Start No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnStart" runat="server" Text='<%# Bind("TnStart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Third End No" ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTnEnd" runat="server" Text='<%# Bind("TnEnd") %>'></asp:Label>
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

