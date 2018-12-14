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
    public partial class MstUser : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsUser objUser = new ClsUser();
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

            txtUserPassword.Attributes["value"] = txtUserPassword.Text;
           
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlUserRoleId, "UserRole", "");
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
                DataTable dtInfo = new DataTable();
                DataTable dtUserAccess = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objUser.TrxUserId =txtUserId.Text.Trim();               
                objUser.DisplayName = txtDisplayName.Text.Trim();
                objUser.EmailId = txtEmailId.Text.Trim();
                objUser.MobileNo = txtMobileNo.Text.Trim();
                objUser.Locked = chkLocked.Checked;
                objUser.UserRoleId =Convert.ToInt64(ddlUserRoleId.SelectedValue);
                objUser.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objUser.IpAdd = Request.UserHostAddress;
                
                if (hdUniqueId.Value.ToString().Trim() == "")
                {
                    objUser.IsFirstTime = true;
                    objUser.UserPassword = txtUserPassword.Text.Trim();
                    bool IsAdded = objLtmsService.InsertInUser(objUser);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information Saved successfully.');", true);
                    }
                }
                else
                {
                    objUser.UserPassword = hdPassword.Value.ToString();
                    if (txtUserPassword.Text.Trim().Length > 0 && hdPassword.Value.ToString() != txtUserPassword.Text.Trim())
                    {
                        objUser.UserPassword = txtUserPassword.Text.Trim();
                        objUser.IsFirstTime = true;
                    }
                    objUser.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInUser(objUser);
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
            if (txtUserId.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter User Id.');", true);
                txtUserId.Focus();
                return false;
            }
            

            if (hdUniqueId.Value.ToString().Trim() == "")
            {
                if (txtUserPassword.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Password.');", true);
                    txtUserPassword.Focus();
                    return false;
                }
            }
            if (txtDisplayName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Full Name.');", true);
                txtDisplayName.Focus();
                return false;
            }
            if (objValidateData.isEmail(txtEmailId.Text.Trim()) ==false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Email Id.');", true);
                txtEmailId.Focus();
                return false;
            }
            if (ddlUserRoleId.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select User role.');", true);
                ddlUserRoleId.Focus();
                return false;
            }
            return true;
        }
        private void BindGvData()
        {
            try
            {
                GvData.DataSource = objLtmsService.GetUserDtl();
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
                    dtInfo = objLtmsService.GetUserDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {                       
                        txtUserId.Text = dtInfo.Rows[0]["UserId"].ToString();
                        txtUserPassword.Text = dtInfo.Rows[0]["UserPassword"].ToString();
                        hdPassword.Value = dtInfo.Rows[0]["UserPassword"].ToString();
                        txtDisplayName.Text = dtInfo.Rows[0]["DisplayName"].ToString();
                        txtEmailId.Text = dtInfo.Rows[0]["EmailId"].ToString();
                        txtMobileNo.Text = dtInfo.Rows[0]["MobileNo"].ToString();
                        chkLocked.Checked =(dtInfo.Rows[0]["Locked"].ToString().ToUpper()=="TRUE"?true:false);
                        ddlUserRoleId.SelectedValue = dtInfo.Rows[0]["UserRoleId"].ToString();
                        ddlUserRoleId_SelectedIndexChanged(ddlUserRoleId, null);
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    txtUserId.Focus();
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
                    bool isDeleted = objLtmsService.DeleteInUser(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        BindGvData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('User information deleted.');", true);
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
            ddlUserRoleId_SelectedIndexChanged(ddlUserRoleId, null);
            hdPassword.Value = null;
            hdUniqueId.Value = null;
            txtUserId.Focus();
        }
       
        protected void gvMenuData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Label)e.Row.FindControl("lblMenuDesc")).Style["padding-left"] = (Convert.ToInt32(((System.Data.DataRowView)e.Row.DataItem).Row["CellPadding"].ToString()) * 5) + "px";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;          
           
        }
        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["ReportName"] = "User";
            Response.Redirect("rptViewAppReport.aspx");
        }

        protected void ddlUserRoleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvMenuData.DataSource = objLtmsService.GetGetMenuListForUserRole((ddlUserRoleId.SelectedValue.ToString() == "" ? 0 : Convert.ToInt64(ddlUserRoleId.SelectedValue)));
                gvMenuData.DataBind();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Unable to load User details.\\nCould not load Menu Access details ");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                
            }
        }
    }
}