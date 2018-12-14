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
    public partial class TrxDealerDeposit : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsDealerDeposit objDealerDeposit = new ClsDealerDeposit();
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
                    txtFromDate.Text = DateTime.Now.AddMonths(-2).ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    hdUniqueId.Value = null;
                    FillComboBox();
                    BindGvData();
                    btnAddNew.Visible = objMenuOptions.AllowEntry;
                }
            }
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlDepositTo, "DepositTo","");
                if (ddlDepositTo.Items.Count >= 1)
                {
                    ddlDepositTo.SelectedIndex = 1;
                }
                objValidateData.FillDropDownList(ddlDepositMethod, "DepositMethod", "");               

                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-30"));
                items.Add(new ListItem("Draft", "0"));
                items.Add(new ListItem("Confirm", "1"));               
                items.Add(new ListItem("Realized", "2"));
                items.Add(new ListItem("Not-Realized", "3"));
                items.Add(new ListItem("Approve", "4"));
                items.Add(new ListItem("Reject", "5"));
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
               
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objDealerDeposit.DepositDate =Convert.ToDateTime(txtDepositDate.Text.Trim());
                objDealerDeposit.DepositAmount =Convert.ToDouble(txtDepositAmount.Text.Trim());
                objDealerDeposit.BankName = txtDepositId.Text.Trim();
                objDealerDeposit.DepositMethodId =Convert.ToInt64(ddlDepositMethod.SelectedValue.ToString());
                objDealerDeposit.DepositToId = Convert.ToInt16(ddlDepositTo.SelectedValue.ToString());
                objDealerDeposit.Remarks = txtRemarks.Text.Trim();
                objDealerDeposit.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objDealerDeposit.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objDealerDeposit.BGValidityDay = 0;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objDealerDeposit.BGValidityDay = 1;
                }
                if (hdUniqueId.Value.ToString().Trim() == "")
                {
                    bool IsAdded = objLtmsService.InsertInDealerDeposit(objDealerDeposit);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Information Saved successfully.');", true);
                    }
                }
                else
                {
                    objDealerDeposit.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInDealerDeposit(objDealerDeposit);
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
        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            if (objValidateData.isValidDate(txtDepositDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtDepositDate.Focus();
                return false;
            }

            if (objValidateData.IsDouble(txtDepositAmount.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Deposit Amount should be numeric.');", true);
                txtDepositAmount.Focus();
                return false;
            }
            if (txtDepositId.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Deposit Id.');", true);
                txtDepositId.Focus();
                return false;
            }

            if (ddlDepositMethod.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Deposit Method.');", true);
                ddlDepositMethod.Focus();
                return false;
            }

            if (ddlDepositTo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Deposit To.');", true);
                ddlDepositTo.Focus();
                return false;
            }
            return true;
        }
        private void BindGvData()
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);

                GvData.DataSource = objLtmsService.GetDealerDepositViewDtl(objInputParameter);
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
                int SaveStatus=0;
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    dtInfo = objLtmsService.GetDealerDepositDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtDepositDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DepositDate"]).ToString("dd-MMM-yyyy");
                        txtDepositAmount.Text = dtInfo.Rows[0]["DepositAmount"].ToString();
                        txtDepositId.Text = dtInfo.Rows[0]["DepositId"].ToString().ToUpper();
                        ddlDepositMethod.SelectedValue = dtInfo.Rows[0]["DepositMethodId"].ToString().ToUpper();
                        ddlDepositTo.SelectedValue = dtInfo.Rows[0]["DepositToId"].ToString().ToUpper();
                        txtRemarks.Text = dtInfo.Rows[0]["Remarks"].ToString().ToUpper();
                        SaveStatus=Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString().ToUpper());                       
                        btnSave.Visible = true;
                        btnConfirm.Visible = true;
                        if (SaveStatus == 1) {                           
                            btnSave.Visible = false;
                            btnConfirm.Visible = false;
                        }
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    txtDepositAmount.Focus();
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
                    bool isDeleted = objLtmsService.DeleteInDealerDeposit(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Deposit information deleted.');", true);
                        BindGvData();
                        
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
                try
                {
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    ((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = objMenuOptions.AllowDelete;

                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 0) { Status = "Draft"; }
                    else if (StatusVal == 1) { Status = "Confirm"; }
                    else if (StatusVal == 2) { Status = "Realized"; }
                    else if (StatusVal == 3) { Status = "Not-Realized"; }
                    else if (StatusVal == 4) { Status = "Approve"; }
                    else if (StatusVal == 5) { Status = "Reject"; }                   
                    lblStatus.Text = Status;
                    if (StatusVal > 0 )
                    {
                        ((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = false;
                    }
                    if (StatusVal >= 1 )
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = false;
                    }
                }
                catch { }
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            objValidateData.ClearAllInputField(pnlDataEntry);
            pnlDataEntry.Visible = true;
            pnlDataDisplay.Visible = false;
            btnSave.Text = "Save";
            btnSave.Visible = true;
            btnConfirm.Visible = true;
            hdUniqueId.Value = null;
            if (ddlDepositTo.Items.Count >= 1)
            {
                ddlDepositTo.SelectedIndex = 1;
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
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "DealerDepositViewDtl";            
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}