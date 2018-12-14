<%@ Page Title="" Language="C#" MasterPageFile="~/StartPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LTMSWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridDiv {
    padding: 15px;
    margin-top: 15px;
    border: 1px solid #277a91;
    width: 500px;
    height: 55vh;
    overflow: auto;
}

          table.divDataEntry {
            padding: 2px 2px 2px 2px;           
            font-family: Calibri,Arial,sans-serif; 
            font-size: 14px;
            border:1px solid;
        }

            table.divDataEntry td, th {
                padding: 2px 2px 2px 2px;
                border:1px solid;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- #home -->
    <section id="tm-section-4" class="row tm-section">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-5 col-xl-6 tm-contact-left">
            <h4 class="tm-section-header thin-font col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">Please Login In</h4>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-left">
                <div class="form-group">
                    <asp:TextBox ID="txtUserId" runat="server" class="form-control" placeholder="User Id"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtUserPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-right">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary"  AccessKey="L" ToolTip="Cancel (Alt + L)" OnClick="btnLogin_Click" />
                <a style="color:white; font-size:12px;" href="frmChangePassword.aspx">Change Password</a>
            </div>
            <div style="margin-top:50px;">
                
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-right">
                <asp:Button ID="btnClaim" runat="server" Text="Click here to Claim" Width="250px" class="btn btn-primary" BorderWidth="0"   OnClick="btnClaim_Click"/>  
            </div>
            <div style="margin-top:50px;">  </div>

             <div class="col-xs-12 col-sm-6 col-md-6 col-lg-12 col-xl-6 tm-contact-form-right">
                <asp:Button ID="btnValidateTicket" runat="server" Text="Validate Ticket" Width="250px" class="btn btn-primary" BorderWidth="0"   OnClick="btnValidateTicket_Click"/>  
            </div>

        </div>
        <div class="tm-white-curve-right col-xs-12 col-sm-6 col-md-6 col-lg-7 col-xl-6">
            <div class="tm-white-curve-right-circle"></div>
            <div class="tm-white-curve-right-rec"></div>
            <div class="tm-white-curve-text">
               <%-- <h2 class="tm-section-header green-text">Claim For Prize</h2>--%>
                <asp:HiddenField ID="hdUniqueId" runat="server" />
                <div style="margin-top:10px">  
                     <h3>Lottery Draw Result</h3>
                    <div class="gridDiv" id="gridDiv" runat="server">
                        <asp:GridView ID="GvData" runat="server" CssClass="divDataEntry"
                            DataKeyNames="DataUniqueId" AutoGenerateColumns="false" Width="500px" OnRowCommand="GvData_RowCommand" >
                            <Columns>
                                 <asp:TemplateField HeaderText="Download"  HeaderStyle-HorizontalAlign="center" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" CommandName="PrintEntry" Text="Print" runat="server" ImageUrl="~/Content/Images/Print.png" ToolTip="Print" Height="15px" Width="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Draw No." ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label ID="lblDrawNo" runat="server" Text='<%# Bind("DrawNo") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                                                              
                                <asp:TemplateField HeaderText="Draw Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label ID="lblDrawDate" runat="server" Text='<%# Convert.ToDateTime(Eval("DrawDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>                                 
                                 <asp:TemplateField HeaderText="Lottery Name"  ItemStyle-Width="150px">
                                    <ItemTemplate><asp:Label ID="lblLotteryName" runat="server" Text='<%# Bind("LotteryName") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Lottery Type" ItemStyle-Width="100px">
                                    <ItemTemplate><asp:Label ID="lblLotteryType" runat="server" Text='<%# Bind("LotteryType") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField> 
                               
                                 
                            </Columns>
                            <HeaderStyle BackColor="#337ab7" ForeColor="#FFFFFF" />
                        </asp:GridView>
                    </div>
                </div>
                <%--<p>Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize Claim For Prize </p>
                <h3 class="tm-section-subheader green-text">Our Address</h3>
                <address>
                    110-220 Praesent consectetur, Dictum massa 10550
                </address>
                <div class="contact-info-links-container">
                    <span class="green-text contact-info">Tel: <a href="tel:0100200340" class="contact-info-link">000-000-0000</a></span>
                    <span class="green-text contact-info">Email: <a href="mailto:info@company.com" class="contact-info-link">info@company.com</a></span>
                </div>--%>
            </div>
        </div>
    </section>
    <!-- #home -->
</asp:Content>
