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
    public partial class MstLotteryType : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLotteryType objLotteryType = new ClsLotteryType();
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
                    //FillComboBox();
                    BindGvData();
                    dvAddNew.Visible = objMenuOptions.AllowEntry;
                   // btnAddNew.Visible = objMenuOptions.AllowEntry;
                }
            }
          
        }
        ////Populate the dropdownlist for Brand, category and company from the database 
        //private void FillComboBox()
        //{
        //    try
        //    {
        //        objValidateData.FillDropDownList(ddlDepositMethod, "DepositMethod");                
        //    }
        //    catch (Exception Ex)
        //    {
        //       objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
        //        var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
        //        var script = string.Format("alert({0});", message);
        //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
        //    }
        //}
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objLotteryType.LotteryType=txtLotteryType.Text.Trim();                
                objLotteryType.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objLotteryType.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() == "")
                {                    
                    bool IsAdded = objLtmsService.InsertInLotteryType(objLotteryType);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information Saved successfully.');", true);
                    }
                }
                else
                {
                    objLotteryType.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInLotteryType(objLotteryType);
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
            
            if (txtLotteryType.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Deposit Id.');", true);
                txtLotteryType.Focus();
                return false;
            }            
            return true;
        }
        private void BindGvData()
        {
            try
            {
                GvData.DataSource = objLtmsService.GetLotteryTypeDtl();
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
                    dtInfo = objLtmsService.GetLotteryTypeDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();                      
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    txtLotteryType.Focus();
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
                    bool isDeleted = objLtmsService.DeleteInLotteryType(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        BindGvData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information deleted.');", true);
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
            txtLotteryType.Focus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
           
           
        }
        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ReportName"] = "LotteryType";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}