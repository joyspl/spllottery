using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace LTMSWebApp
{
    public partial class TrxViewLotteryClaimEntrySuperTicket : System.Web.UI.Page
    {
        ClsLotteryClaimApprovalDetails objClsLotteryClaimApprovalDetails = new ClsLotteryClaimApprovalDetails();
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLottery objClsLottery = new ClsLottery();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnConfirm);
            if (Session["MenuInfo"] == null)
            {
                Session["CustomError"] = "Invalid Navigation option. Please use the Navigation panel to access this page.";
                Server.Transfer("appError.aspx");
                return;
            }
            else
            {
                objMenuOptions = (ClsMenuInfo)Session["MenuInfo"];
            }
            if (IsPostBack == false)
            {
                if (((bool)Session["FromNavigation"]) == false)
                {
                    Session["CustomError"] = "Invalid Navigation option. Please use the Navigation panel to access this page.";
                    Server.Transfer("appError.aspx");
                }
                else
                {
                    Session["FromNavigation"] = false;
                    if (!(objMenuOptions.AllowEntry || objMenuOptions.AllowEdit || objMenuOptions.AllowDelete || objMenuOptions.AllowView))
                    {
                        Session["CustomError"] = "You do not have proper Privilege to access the selected module";
                        Server.Transfer("appError.aspx");
                        return;
                    }
                    txtFromDate.Text = DateTime.Now.AddMonths(-2).ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    hdUniqueId.Value = null;
                    FillComboBox();
                    BindGvData();
                }
            }
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-1"));
                items.Add(new ListItem("New Application", "0"));
                items.Add(new ListItem("Approve", "1"));
                items.Add(new ListItem("Reject", "2"));  
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 1;
            }
            catch (Exception Ex)
            {
               objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdUniqueId.Value.ToString().Trim() != "") {
                     if (IsValidEntry() == false) { return; }//Check for valid data entry
                    objClsLotteryClaimApprovalDetails.DataUniqueId = Convert.ToInt64(hdUniqueId.Value.ToString().Trim());
                    objClsLotteryClaimApprovalDetails.SaveStatus = (rdoApproveStatus1.Checked == true ? 1 : 2); //1-Approve....2-Reject
                    objClsLotteryClaimApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    objClsLotteryClaimApprovalDetails.PayableAmount = Convert.ToDouble(txtPayableToWinner.Text.Trim());
                    objClsLotteryClaimApprovalDetails.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                    objClsLotteryClaimApprovalDetails.IpAdd = Request.UserHostAddress;

                    bool IsUpdated = objLtmsService.UpdateApprovalInLotteryClaimEntry(objClsLotteryClaimApprovalDetails);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Information updated successfully.');", true);
                    }

                }
                BindGvData();
                btnCancel_Click(sender, e);
               
                
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        //Checking for valid data entry 
        private bool IsValidEntry()
        {

            if (objValidateData.IsDouble(txtPayableToWinner.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Payable To Winner should be numeric.');", true);
                txtPayableToWinner.Focus();
                return false;
            }
            if (rdoApproveStatus1.Checked==false && rdoApproveStatus2.Checked==false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select approval ro reject option.');", true);
                return false;
            }
            if (rdoApproveStatus2.Checked == true && txtRemarks.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter rejection remarks.');", true);
                return false;
            }
            
            return true;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (objValidateData.isValidDate(txtFromDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtFromDate.Focus();
                return;
            }
            if (objValidateData.isValidDate(txtToDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtToDate.Focus();
                return;
            }
            BindGvData();
        }
        private void BindGvData()
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                GvData.DataSource = objLtmsService.GetLotteryClaimEntryDtlByStatus(objInputParameter,2);//1==PWT entry
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                gridDiv.Visible = objMenuOptions.AllowView;
                btnPrint.Visible = (GvData.Rows.Count > 0 ? true : false);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int SaveStatus=0;
                DataSet dtInfo = new DataSet();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                objValidateData.ClearAllInputField(pnlDataEntry);
                dtInfo = objLtmsService.GetLotteryClaimEntryDtlByReqId(Convert.ToInt64(hdUniqueId.Value));
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {                   
                    if (dtInfo.Tables[0].Rows.Count > 0)
                    {
                        rdoApproveStatus1.Checked = true;
                        lblApplicationId.Text = "Application Id : " + dtInfo.Tables[0].Rows[0]["ReqCode"].ToString();
                        lblLotteryType.Text = dtInfo.Tables[0].Rows[0]["LotteryType"].ToString();
                        lblLotteryName.Text = dtInfo.Tables[0].Rows[0]["LotteryName"].ToString();
                        lblDrawNo.Text = dtInfo.Tables[0].Rows[0]["DrawNo"].ToString();
                        lblDrawDate.Text = Convert.ToDateTime(dtInfo.Tables[0].Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        lblLotteryNo.Text = dtInfo.Tables[0].Rows[0]["LotteryNo"].ToString();
                        lblMobileNo.Text = dtInfo.Tables[0].Rows[0]["MobileNo"].ToString();
                        lblEmailId.Text = dtInfo.Tables[0].Rows[0]["EmailId"].ToString();
                        lblName.Text = dtInfo.Tables[0].Rows[0]["Name"].ToString();
                        lblFatherOrGuardianName.Text = dtInfo.Tables[0].Rows[0]["FatherOrGuardianName"].ToString();
                        lblAddress.Text = dtInfo.Tables[0].Rows[0]["Address"].ToString();
                        lblAadharNo.Text = dtInfo.Tables[0].Rows[0]["AadharNo"].ToString();
                        lblPanCard.Text = dtInfo.Tables[0].Rows[0]["PanNo"].ToString();
                        lblBankName.Text = dtInfo.Tables[0].Rows[0]["BankName"].ToString();
                        lblBankAccountNo.Text = dtInfo.Tables[0].Rows[0]["BankAccountNo"].ToString();
                        lblIFSCCode.Text = dtInfo.Tables[0].Rows[0]["IFSCCode"].ToString();
                        lbllblNameOfPrize.Text = dtInfo.Tables[0].Rows[0]["NameOfPrize"].ToString();
                        string ClaimType = "";

                        ClaimType = dtInfo.Tables[0].Rows[0]["ClaimType"].ToString();
                        if (ClaimType == "2")
                        {
                            txtPrizeAmount.Text = dtInfo.Tables[0].Rows[0]["SuperTicketAmount"].ToString();
                            txtPayableToWinner.Text = dtInfo.Tables[0].Rows[0]["UpdatedSuperTicketAmount"].ToString();
                        }
                       
                        Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                        lblClaimType.Text = objClaimType[ClaimType];

                        if (dtInfo.Tables[0].Rows[0]["ClaimType"].ToString() == "1")
                        {
                            trPwtTicket.Visible = true;
                        }

                        if (dtInfo.Tables[0].Rows[0]["Photo"].ToString().Length > 0)
                        {
                            byte[] Photo = ((byte[])dtInfo.Tables[0].Rows[0]["Photo"]);
                            imgPhoto.Visible = true;
                            imgPhoto.BorderWidth = 1;
                            imgPhoto.ImageUrl = "data:image;base64," + Convert.ToBase64String(Photo);
                        }

                        if (dtInfo.Tables[0].Rows[0]["PwtTicket"].ToString().Length > 0)
                        {
                            byte[] PwtTicket = ((byte[])dtInfo.Tables[0].Rows[0]["PwtTicket"]);
                            imgPwtTicket.Visible = true;
                            imgPwtTicket.BorderWidth = 1;
                            imgPwtTicket.ImageUrl = "data:image;base64," + Convert.ToBase64String(PwtTicket);
                        }

                        byte[] aadharPic = ((byte[])dtInfo.Tables[0].Rows[0]["AadharFile"]);
                        imgAadharPic.Visible = true;
                        imgAadharPic.BorderWidth = 1;
                        imgAadharPic.ImageUrl = "data:image;base64," + Convert.ToBase64String(aadharPic);

                        byte[] PanPic = ((byte[])dtInfo.Tables[0].Rows[0]["PanFile"]);
                        imgPan.Visible = true;
                        imgPan.BorderWidth = 1;
                        imgPan.ImageUrl = "data:image;base64," + Convert.ToBase64String(PanPic);

                        byte[] BankDtlPic = ((byte[])dtInfo.Tables[0].Rows[0]["BankDtl"]);
                        imgBankDtl.Visible = true;
                        imgBankDtl.BorderWidth = 1;
                        imgBankDtl.ImageUrl = "data:image;base64," + Convert.ToBase64String(BankDtlPic);

                        btnConfirm.Visible = true;
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                }
                if (e.CommandName == "PrintEntry")
                {
                    if (dtInfo.Tables[0].Rows.Count > 0)
                    {
                        Session["ApplicationId"] = dtInfo.Tables[0].Rows[0]["ReqCode"].ToString();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('TrxLotteryClaimEntryPrint.aspx','_blank')", true);
                    }
                }
                dtInfo.Dispose();
            }
            catch (Exception Ex)
            {
               objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }       
        protected void GvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 0) { Status = "New Application"; }
                    else if (StatusVal == 1) { Status = "Confirmed"; }
                    else if (StatusVal == 2) { Status = "Rejected"; }
                    else if (StatusVal == 3) { Status = "Second Leval Approved"; }
                    else if (StatusVal == 4) { Status = "Second Leval Reject"; }
                    else if (StatusVal == 5) { Status = "Final Leval Approved"; }
                    else if (StatusVal == 6) { Status = "Final Leval Reject"; }					
                    lblStatus.Text = Status;
                    if (StatusVal == 1 || StatusVal == 3 || StatusVal == 5)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = false;
                    }
                    Label lblClaimType = ((Label)e.Row.FindControl("lblClaimType"));
                    Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                    lblClaimType.Text = objClaimType[lblClaimType.Text];
                }
                catch(Exception ex) {
                    var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                
                }
            }
        }             
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate =Convert.ToDateTime(txtFromDate.Text.Trim());
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim());
            objInputParameter.InStatus =Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "TicketInventoryViewDtl";            
            Response.Redirect("rptViewAppReport.aspx");
        }

        
    }
}