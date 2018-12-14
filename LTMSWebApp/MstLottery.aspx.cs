using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class MstLottery : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLottery objLottery = new ClsLottery();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    hdUniqueId.Value = null;
                    FillComboBox();
                    BindGvData();
                    btnAddNew.Visible = objMenuOptions.AllowEntry;                   
                }
            }

          
        }
        ////Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType","");                           
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
               // string AlphabetSeries = "";
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objLottery.LotteryName = txtLotteryName.Text.Trim();
                objLottery.LotteryTypeId =Convert.ToInt16(ddlLotteryType.SelectedValue.ToString());
                objLottery.NoOfDigit = Convert.ToInt16(txtNoOfDigit.Text);
                objLottery.SyndicateRate = Convert.ToDouble(txtSyndicateRate.Text);
                objLottery.RateForSpl = Convert.ToDouble(txtRateForSpl.Text);
                objLottery.TotTicketRate = Convert.ToDouble(txtTotTicketRate.Text);
                objLottery.GstRate = Convert.ToDouble(txtGstRate.Text);
                objLottery.PrizeCategory = Convert.ToInt16(txtPrizeCategory.Text);
                objLottery.IncludeConsPrize = chkIncludeConsPrize.Checked;
                objLottery.ClaimDays = Convert.ToInt16(txtClaimDays.Text.Trim());
                objLottery.ClaimDaysVariable = Convert.ToInt16(txtClaimDaysVariable.Text.Trim());
                objLottery.SizeOfTicket = txtSizeOfTicket.Text.Trim();
                objLottery.PaperQuality = txtPaperQuality.Text.Trim();
                objLottery.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objLottery.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() == "")
                {
                    dtInfo = objLtmsService.GetLotteryDtlByName(objLottery.LotteryName);
                    if (dtInfo.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Name Already Exist. Duplicate Lottery name not allowed');", true);
                        return;
                    }
                    bool IsAdded = objLtmsService.InsertInLottery(objLottery);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information Saved successfully.');", true);
                    }
                }
                else
                {

                    objLottery.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    if (dtInfo.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Name Already Exist. Duplicate Lottery name not allowed');", true);
                        return;
                    }


                    bool IsUpdated = objLtmsService.UpdateInLottery(objLottery);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information updated successfully.');", true);
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
            if (txtLotteryName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Lottery Name.');", true);
                txtLotteryName.Focus();
                return false;
            }
            if (ddlLotteryType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Type.');", true);
                ddlLotteryType.Focus();
                return false;
            }
            if (objValidateData.IsInteger(txtNoOfDigit.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Number of Digit in series should be numeric.');", true);
                txtNoOfDigit.Focus();
                return false;
            }
            if (objValidateData.IsDouble(txtSyndicateRate.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Ticket Rate for Distributor should be numeric.');", true);
                txtSyndicateRate.Focus();
                return false;
            }

            if (objValidateData.IsDouble(txtTotTicketRate.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Ticket Rate sould be numeric.');", true);
                txtTotTicketRate.Focus();
                return false;
            }
            if (objValidateData.IsDoubleWithZero(txtRateForSpl.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('SPL Ticket Rate should be numeric.');", true);
                txtRateForSpl.Focus();
                return false;
            }
            if (objValidateData.IsDouble(txtGstRate.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('GST Ticket Rate should be numeric.');", true);
                txtGstRate.Focus();
                return false;
            }
            if (objValidateData.IsDouble(txtPrizeCategory.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Prize Category should be numeric.');", true);
                txtPrizeCategory.Focus();
                return false;
            }
            if (objValidateData.IsInteger(txtClaimDays.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claim Days for Fixed prize should be numeric.');", true);
                txtClaimDays.Focus();
                return false;
            }
            if (objValidateData.IsInteger(txtClaimDaysVariable.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claim Days for Variable Prize should be numeric.');", true);
                txtClaimDaysVariable.Focus();
                return false;
            }
            if (txtSizeOfTicket.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter SizeOfTicket.');", true);
                txtSizeOfTicket.Focus();
                return false;
            }
            if (txtPaperQuality.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Paper Quality.');", true);
                txtPaperQuality.Focus();
                return false;
            }
            return true;
        }
        private void BindGvData()
        {
            try
            {
                GvData.DataSource = objLtmsService.GetLotteryDtl();
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
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
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    dtInfo = objLtmsService.GetLotteryDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtLotteryName.Text = dtInfo.Rows[0]["LotteryName"].ToString();
                        ddlLotteryType.SelectedValue = dtInfo.Rows[0]["LotteryTypeId"].ToString();
                        txtNoOfDigit.Text = dtInfo.Rows[0]["NoOfDigit"].ToString();
                        txtSyndicateRate.Text = dtInfo.Rows[0]["SyndicateRate"].ToString();
                        txtRateForSpl.Text = dtInfo.Rows[0]["RateForSpl"].ToString();
                        txtTotTicketRate.Text = dtInfo.Rows[0]["TotTicketRate"].ToString();
                        txtGstRate.Text = dtInfo.Rows[0]["GstRate"].ToString();
                        txtPrizeCategory.Text = dtInfo.Rows[0]["PrizeCategory"].ToString();
                        chkIncludeConsPrize.Checked =Convert.ToBoolean(dtInfo.Rows[0]["IncludeConsPrize"].ToString());
                        txtClaimDays.Text = dtInfo.Rows[0]["ClaimDays"].ToString();
                        txtClaimDaysVariable.Text = dtInfo.Rows[0]["ClaimDaysVariable"].ToString();
                        txtSizeOfTicket.Text = dtInfo.Rows[0]["SizeOfTicket"].ToString();
                        txtPaperQuality.Text = dtInfo.Rows[0]["PaperQuality"].ToString();
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    txtLotteryName.Focus();
                }
                //Delete the location information
                if (e.CommandName == "DeleteEntry")
                {
                    if (objMenuOptions.AllowDelete == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('You do not have proper privilege for deleting a record.');", true);
                        return;
                    }
                    bool isDeleted = objLtmsService.DeleteInLottery(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        BindGvData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery information deleted.');", true);
                        // ResetSearchFilte();
                        //BindLocationInformationDetails(cmbSearch.SelectedValue.ToString(), "", cmbSearch.SelectedValue.ToString(), true);
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
                ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                ((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = objMenuOptions.AllowDelete;               
                
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            objValidateData.ClearAllInputField(pnlDataEntry);
            pnlDataEntry.Visible = true;
            pnlDataDisplay.Visible = false;
            btnSave.Text = "Save";
            hdUniqueId.Value = null;
            txtLotteryName.Focus();

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }
        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ReportName"] = "Lottery";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}