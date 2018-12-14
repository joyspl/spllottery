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
    public partial class MstGovermentOrder : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsGovermentOrder objGovermentOrder = new ClsGovermentOrder();
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
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType", "");
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryName, "LotteryNameByLotteryTypeID", ddlLotteryType.SelectedValue);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objGovermentOrder.LotteryId=  Convert.ToInt64(ddlLotteryName.SelectedValue.ToString());
                objGovermentOrder.GovermentOrder = txtGovermentOrder.Text.Trim();
                objGovermentOrder.UnSoldPercentage =Convert.ToDouble(txtUnSoldPercentage.Text.Trim());
                objGovermentOrder.NoOfAlphabet = Convert.ToInt64(txtNoOfAlphabet.Text.Trim());
                objGovermentOrder.AlphabetName = txtAlphabetName.Text.Trim();
                objGovermentOrder.TicketSeriallNoFrom = txtTicketSlNoFrom.Text.Trim();
                objGovermentOrder.TicketSerialNoTo = txtTicketSlNoTo.Text.Trim();
                objGovermentOrder.TotalTickets = Convert.ToInt64(txtTotalTickets.Text.Trim());
                objGovermentOrder.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objGovermentOrder.IpAdd = Request.UserHostAddress;
                objGovermentOrder.ModifiedLotteryName = txtModifiedLotteryName.Text.Trim().ToUpper();
                if (hdUniqueId.Value.ToString().Trim() == "")
                {
                    bool IsAdded = objLtmsService.InsertInGovermentOrder(objGovermentOrder);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Government Order information Saved successfully.');", true);
                    }
                }
                else
                {
                    objGovermentOrder.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInGovermentOrder(objGovermentOrder);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Government Order information updated successfully.');", true);
                    }
                }
                BindGvData();
                btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            if (ddlLotteryType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Type.');", true);
                ddlLotteryType.Focus();
                return false;
            }
            if (ddlLotteryName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Name.');", true);
                ddlLotteryName.Focus();
                return false;
            }
            if (txtGovermentOrder.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Deposit Id.');", true);
                txtGovermentOrder.Focus();
                return false;
            }
            if(objValidateData.IsDoubleWithZero(txtUnSoldPercentage.Text.Trim())==false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid Un-Sold Percentage');", true);
                txtUnSoldPercentage.Focus();
                return false;
            }
            
            if (Convert.ToDouble(txtUnSoldPercentage.Text.Trim())>100)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Un-Sold Percentage should be less or equal to 100');", true);
                txtUnSoldPercentage.Focus();
                return false;
            }
            return true;
        }
        private void BindGvData()
        {
            try
            {
                GvData.DataSource = objLtmsService.GetGovermentOrderDtl();
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
                gridDiv.Visible = objMenuOptions.AllowView;
                btnPrint.Visible = (GvData.Rows.Count > 0 ? true : false);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
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
                    dtInfo = objLtmsService.GetGovermentOrderDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtGovermentOrder.Text = dtInfo.Rows[0]["GovermentOrder"].ToString();
                        ddlLotteryType.SelectedValue = dtInfo.Rows[0]["LotteryTypeId"].ToString();
                        txtUnSoldPercentage.Text = dtInfo.Rows[0]["UnSoldPercentage"].ToString();
                        txtNoOfAlphabet.Text = dtInfo.Rows[0]["NoOfAlphabet"].ToString();
                        txtAlphabetName.Text = dtInfo.Rows[0]["AlphabetName"].ToString();
                        txtTicketSlNoFrom.Text = dtInfo.Rows[0]["TicketSlNoFrom"].ToString();
                        txtTicketSlNoTo.Text = dtInfo.Rows[0]["TicketSlNoTo"].ToString();
                        txtTotalTickets.Text = dtInfo.Rows[0]["TotalTickets"].ToString();
                        txtModifiedLotteryName.Text = dtInfo.Rows[0]["ModifiedLotteryName"].ToString();

                        ddlLotteryType_SelectedIndexChanged(ddlLotteryType, null);
                        if (ddlLotteryName.Items.Count > 0)
                        {
                            ddlLotteryName.SelectedValue = dtInfo.Rows[0]["LotteryId"].ToString();
                        }
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                    // lblSubHead.Text = "Update Location Information";
                    txtGovermentOrder.Focus();
                }
                //Delete the location information
                if (e.CommandName == "DeleteEntry")
                {
                    if (objMenuOptions.AllowDelete == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('You do not have proper privilege for deleting a record.');", true);
                        return;
                    }
                    //dtInfo = objMstLocation.IsChildRecordExistForLocation(hdUniqueId.Value);
                    //if (dtInfo.Rows.Count > 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('This data already exist in another dependent table. Deletion failed !');", true);
                    //    dtInfo.Dispose();
                    //    return;
                    //}
                    bool isDeleted = objLtmsService.DeleteInGovermentOrder(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        BindGvData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Goverment Order information deleted.');", true);
                        // ResetSearchFilte();
                        //BindLocationInformationDetails(cmbSearch.SelectedValue.ToString(), "", cmbSearch.SelectedValue.ToString(), true);
                    }
                }
                dtInfo.Dispose();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
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
            txtGovermentOrder.Focus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ReportName"] = "GovermentOrder";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}