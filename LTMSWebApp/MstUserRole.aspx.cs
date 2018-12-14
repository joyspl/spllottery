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
    public partial class MstUserRole : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsUserRole objUserRole = new ClsUserRole();
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
                    // FillComboBox();
                    BindGvData();
                    btnAddNew.Visible = objMenuOptions.AllowEntry;
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
                DataTable dtUserAccess = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objUserRole.UserRole = txtUserRole.Text.Trim();                
                objUserRole.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objUserRole.IpAdd = Request.UserHostAddress;
                DataRow dtRow = null;
                dtUserAccess.Columns.Add(new DataColumn("ManuCode", typeof(string)));
                dtUserAccess.Columns.Add(new DataColumn("uiMenuEntryAccessAllowed", typeof(string)));
                dtUserAccess.Columns.Add(new DataColumn("uiMenuEditAccessAllowed", typeof(string)));
                dtUserAccess.Columns.Add(new DataColumn("uiMenuDeleteAccessAllowed", typeof(string)));
                dtUserAccess.Columns.Add(new DataColumn("uiMenuViewAccessAllowed", typeof(string)));
                foreach (GridViewRow row in gvMenuData.Rows)
                {
                    dtRow = dtUserAccess.NewRow();
                    dtRow["ManuCode"] = ((Label)gvMenuData.Rows[row.RowIndex].Cells[0].FindControl("lblMenuCode")).Text.Trim();
                    dtRow["uiMenuEntryAccessAllowed"] = (((CheckBox)gvMenuData.Rows[row.RowIndex].Cells[0].FindControl("chkMenuEntryAccessAllowed")).Checked ? "Y" : "N");
                    dtRow["uiMenuEditAccessAllowed"] = (((CheckBox)gvMenuData.Rows[row.RowIndex].Cells[0].FindControl("chkMenuEditAccessAllowed")).Checked ? "Y" : "N");
                    dtRow["uiMenuDeleteAccessAllowed"] = (((CheckBox)gvMenuData.Rows[row.RowIndex].Cells[0].FindControl("chkMenuDeleteAccessAllowed")).Checked ? "Y" : "N");
                    dtRow["uiMenuViewAccessAllowed"] = (((CheckBox)gvMenuData.Rows[row.RowIndex].Cells[0].FindControl("chkMenuViewAccessAllowed")).Checked ? "Y" : "N");
                    dtUserAccess.Rows.Add(dtRow);
                }
                dtUserAccess.TableName = "dtUserAccessTable";
                if (hdUniqueId.Value.ToString().Trim() == "")
                {
                    bool IsAdded = objLtmsService.InsertInUserRole(objUserRole, dtUserAccess);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Type information Saved successfully.');", true);
                    }
                }
                else
                {
                    objUserRole.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInUserRole(objUserRole, dtUserAccess);
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
            
            if (txtUserRole.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter User Role.');", true);
                txtUserRole.Focus();
                return false;
            }
            
            bool bAnythingSelected = false;
            for (int iCtr = 0; iCtr < gvMenuData.Rows.Count; iCtr++)
            {
                if (((CheckBox)gvMenuData.Rows[iCtr].Cells[0].FindControl("chkMenuEntryAccessAllowed")).Checked ||
                    ((CheckBox)gvMenuData.Rows[iCtr].Cells[0].FindControl("chkMenuEditAccessAllowed")).Checked ||
                    ((CheckBox)gvMenuData.Rows[iCtr].Cells[0].FindControl("chkMenuDeleteAccessAllowed")).Checked ||
                    ((CheckBox)gvMenuData.Rows[iCtr].Cells[0].FindControl("chkMenuViewAccessAllowed")).Checked
                   )
                {
                    bAnythingSelected = true;
                    break;
                }
            }
            if (bAnythingSelected == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select User Role Menu.');", true);
                return bAnythingSelected;
            }
           
            return true;
        }
        private void BindGvData()
        {
            try
            {
                GvData.DataSource = objLtmsService.GetUserRoleDtl();
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
                    dtInfo = objLtmsService.GetUserRoleDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtUserRole.Text = dtInfo.Rows[0]["UserRole"].ToString();                      
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                    ShowUserRoleDetails(Convert.ToInt64(hdUniqueId.Value));
                   // lblSubHead.Text = "Update Location Information";
                    txtUserRole.Focus();
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
                    
                    //bool isDeleted = objLtmsService.DeleteInUserRole(Convert.ToInt64(hdUniqueId.Value));
                    //if (isDeleted == true)
                    //{
                    //    BindGvData();
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('User Role information deleted.');", true);
                    //    // ResetSearchFilte();
                    //    //BindLocationInformationDetails(cmbSearch.SelectedValue.ToString(), "", cmbSearch.SelectedValue.ToString(), true);
                    //}
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
            ShowUserRoleDetails(0);
         
         
            hdUniqueId.Value = null;
            txtUserRole.Focus();
        }
        private bool ShowUserRoleDetails(Int64 RoleId)
        {
            try
            {
                gvMenuData.DataSource = objLtmsService.GetGetMenuListForUserRole(Convert.ToInt64(RoleId));
                gvMenuData.DataBind();
                return true;
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Unable to load User details.\\nCould not load Menu Access details ");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                return false;
            }
        }
        protected void chkMenuAccessLP_CheckChanged(object sender, EventArgs e)
        {
            ToggleCheckBoxesLP(((CheckBox)sender));
            Page.SetFocus(((CheckBox)sender).ClientID);
        }
        private void ToggleCheckBoxesLP(CheckBox sender)
        {
            bool IsChecked = false;
            String SelectedMenuCode = String.Empty;
            int ColIndex = 2;       //Default set to Entry CheckBox
            if (sender.ClientID.ToUpper().Contains("ENTRY"))
                ColIndex = 2;
            if (sender.ClientID.ToUpper().Contains("EDIT"))
                ColIndex = 3;
            if (sender.ClientID.ToUpper().Contains("DELETE"))
                ColIndex = 4;
            if (sender.ClientID.ToUpper().Contains("VIEW"))
                ColIndex = 5;

            int Loop = 0;
            IsChecked = sender.Checked;

            //Loop through the MenuList(Hierarchy) and find the current row (where the event has fired)
            //Get the selectionState of the same elements depending upon the parameter
            for (Loop = 0; Loop < gvMenuData.Rows.Count; Loop++)
            {
                if (gvMenuData.Rows[Loop].Controls[ColIndex].FindControl(sender.ID).ClientID == sender.ClientID)
                {
                    //SelectedMenuCode = gvMenuData.Rows[Loop].Cells[0].Text;
                    SelectedMenuCode = gvMenuData.DataKeys[Loop].Value.ToString();
                    if ((ColIndex == 2 || ColIndex == 3 || ColIndex == 4 && IsChecked))
                    {
                        ((CheckBox)gvMenuData.Rows[Loop].Controls[ColIndex].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;     //View is allowed by default when Edit / Delete / Audit is allowed
                    }
                    else if (ColIndex == 5 && !IsChecked)
                    {
                        ((CheckBox)gvMenuData.Rows[Loop].Controls[ColIndex].FindControl("chkMenuEditAccessAllowed")).Checked = IsChecked;
                        ((CheckBox)gvMenuData.Rows[Loop].Controls[ColIndex].FindControl("chkMenuDeleteAccessAllowed")).Checked = IsChecked;
                        ((CheckBox)gvMenuData.Rows[Loop].Controls[ColIndex].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;
                    }
                    break;
                }
            }

            //Loop through the Grid and Update the Status of the the Parent and Child rows....
            for (Loop = 0; Loop < gvMenuData.Rows.Count; Loop++)
            {
                if (gvMenuData.DataKeys[Loop].Value.ToString().Length < SelectedMenuCode.Length)
                {
                    //Recursively loop through to ensure the CheckState of the Parent Menu to selected if the current menuItem is Checked
                    if (SelectedMenuCode.Substring(0, gvMenuData.DataKeys[Loop].Value.ToString().Length) == gvMenuData.DataKeys[Loop].Value.ToString())
                    {
                        if (IsChecked)
                        {
                            if (ColIndex == 2)
                            {
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[2].FindControl("chkMenuEntryAccessAllowed")).Checked = IsChecked;
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;
                            }
                            else if (ColIndex == 3)
                            {
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[3].FindControl("chkMenuEditAccessAllowed")).Checked = IsChecked;
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;
                            }
                            else if (ColIndex == 4)
                            {
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[4].FindControl("chkMenuDeleteAccessAllowed")).Checked = IsChecked;
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;
                            }
                            else if (ColIndex == 5)
                            {
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = IsChecked;
                            }
                        }
                    }
                }
                else if (gvMenuData.DataKeys[Loop].Value.ToString().Length > SelectedMenuCode.Length)
                {
                    if (gvMenuData.DataKeys[Loop].Value.ToString().Substring(0, SelectedMenuCode.Length) == SelectedMenuCode)
                    {
                        //Recursively loop through to ensure the CheckState of the Child Menus to selected if the current menuItem is Checked
                        if (ColIndex == 2)
                        {
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[2].FindControl("chkMenuEntryAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[2].FindControl("chkMenuEntryAccessAllowed")).Visible && IsChecked ? true : false;
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Visible && IsChecked ? true : ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked;
                        }
                        else if (ColIndex == 3)
                        {
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[3].FindControl("chkMenuEditAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[3].FindControl("chkMenuEditAccessAllowed")).Visible && IsChecked ? true : false;
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Visible && IsChecked ? true : ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked;
                        }
                        else if (ColIndex == 4)
                        {
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[4].FindControl("chkMenuDeleteAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[4].FindControl("chkMenuDeleteAccessAllowed")).Visible && IsChecked ? true : false;
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Visible && IsChecked ? true : ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked;
                        }
                        else if (ColIndex == 5)
                        {
                            ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[5].FindControl("chkMenuViewAccessAllowed")).Visible && IsChecked ? true : false;
                            if (!IsChecked)
                            {
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[3].FindControl("chkMenuEditAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[3].FindControl("chkMenuEditAccessAllowed")).Visible && IsChecked ? true : false;
                                ((CheckBox)gvMenuData.Rows[Loop].Controls[4].FindControl("chkMenuDeleteAccessAllowed")).Checked = ((CheckBox)gvMenuData.Rows[Loop].Controls[4].FindControl("chkMenuDeleteAccessAllowed")).Visible && IsChecked ? true : false;
                            }
                        }
                    }
                }
            }
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
            Session["ReportName"] = "UserRole";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}