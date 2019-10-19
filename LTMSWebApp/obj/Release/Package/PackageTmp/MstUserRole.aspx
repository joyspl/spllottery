<%@ Page Title="" Language="C#" MasterPageFile="~/WebPageMaster.Master" AutoEventWireup="true" CodeBehind="MstUserRole.aspx.cs" Inherits="LTMSWebApp.MstUserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AppContentPlaceHolder" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="#">System Admin</a></li>
            <li class="breadcrumb-item active">
                <asp:Label ID="lblHeader" runat="server" Text="User Role"></asp:Label>
            </li>
        </ol>
        <div class="row">
            <div class="col-12">
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <asp:Panel ID="pnlDataEntry" runat="server" Visible="false">
                    <table border="0" class="divDataEntry">
                        <tr>
                            <td style="width: 25%; text-align: right;">User Role<font color="red">*</font></td>
                            <td style="width: 25%;">
                                <asp:TextBox ID="txtUserRole" Width="80%" runat="server" class="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 50%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="gridDivCustom" style="height: 60vh;" id="dvMenu" runat="server">
                                    <asp:GridView ID="gvMenuData" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="MenuCode" AutoGenerateColumns="false"
                                        HeaderStyle-Font-Size="14px" RowStyle-Font-Size="12px"
                                        Width="100%" OnRowDataBound="gvMenuData_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Menu Code" Visible="false" ItemStyle-Width="1px" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenuCode" runat="server" Text='<%# Bind("MENUCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Menu Description" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenuDesc" runat="server" Text='<%# Bind("MENUDESCRIPTION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Access Allowed" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMenuEntryAccessAllowed" runat="server" Checked='<%# (Eval("ENTRYACCESSALLOWED").ToString().ToUpper()=="Y"?true:false) %>' Visible='<%# (Eval("ENTRYPOSSIBLE").ToString().ToUpper()=="Y"?true:false) %>' AutoPostBack="true" OnCheckedChanged="chkMenuAccessLP_CheckChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit Access Allowed" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMenuEditAccessAllowed" runat="server" Checked='<%# (Eval("EDITACCESSALLOWED").ToString().ToUpper()=="Y"?true:false) %>' Visible='<%# (Eval("EDITPOSSIBLE").ToString().ToUpper()=="Y"?true:false) %>' AutoPostBack="true" OnCheckedChanged="chkMenuAccessLP_CheckChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete Access Allowed" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMenuDeleteAccessAllowed" runat="server" Checked='<%# (Eval("DELETEACCESSALLOWED").ToString().ToUpper()=="Y"?true:false) %>' Visible='<%# (Eval("DELETEPOSSIBLE").ToString().ToUpper()=="Y"?true:false) %>' AutoPostBack="true" OnCheckedChanged="chkMenuAccessLP_CheckChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Access Allowed" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMenuViewAccessAllowed" runat="server" Checked='<%# (Eval("VIEWACCESSALLOWED").ToString().ToUpper()=="Y"?true:false) %>' Visible='<%# (Eval("VIEWPOSSIBLE").ToString().ToUpper()=="Y"?true:false) %>' AutoPostBack="true" OnCheckedChanged="chkMenuAccessLP_CheckChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">&nbsp;</td>
                            <td style="width: 50%;">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSave_Click" />
                                 <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" Text="Cancel" AccessKey="C" ToolTip="Cancel (Alt + C)" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div id="pnlDataDisplay" runat="server" style="position: relative;">
                    <div id="searchDiv" runat="server">
                        <table style="width: 100%; vertical-align: text-top;" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 50%;">
                                     <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" Text="New User Role" AccessKey="N" ToolTip="Add New (Alt + N)" OnClick="btnAddNew_Click" />
                                </td>
                                <td style="text-align: right; vertical-align: top; width: 50%;">
                                    <asp:Button ID="btnPrint" runat="server" Text="" CssClass="btnPrint" OnClick="btnPrint_Click" AccessKey="P" ToolTip="Print (Alt + P)" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="table table-bordered table-hover"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false"
                            Width="90%" OnRowCommand="GvData_RowCommand" OnRowDataBound="GvData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditEntry" CommandName="EditEntry" Text="Edit" runat="server" ImageUrl="Content/Images/Edit.png" ToolTip="Edit" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Font-Size="8px" HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDeleteEntry" CommandName="DeleteEntry" Text="Delete" runat="server" ImageUrl="Content/Images/Delete.png" ToolTip="Delete" Height="15px" Width="15px" OnClientClick="return confirm('Are you sure you want to Delete the record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserRole" runat="server" Text='<%# Bind("UserRole") %>'></asp:Label>
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

