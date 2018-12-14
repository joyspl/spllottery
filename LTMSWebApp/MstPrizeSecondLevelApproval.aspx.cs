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
    public partial class MstPrizeSecondLevelApproval : System.Web.UI.Page
    {

        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsPrize objPrize = new ClsPrize();
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

                }
            }
        }
        private void FillComboBox()
        {
            try
            {               
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-3"));
                items.Add(new ListItem("Pending", "3"));
                items.Add(new ListItem("Approved", "5"));
                items.Add(new ListItem("Reject", "6"));
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 1;
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
               
                if (IsValidEntry() == false) { return; }//Check for valid data entry

                objPrize.DataUniqueId = Convert.ToInt16(hdUniqueId.Value.ToString().Trim());
                objPrize.ClaimDays = Convert.ToInt16(txtClaimDays.Text);
                objPrize.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objPrize.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Approve")
                {
                    objPrize.SaveStatus = 5;
                }
                else if (((Button)sender).CommandName == "Reject")
                {
                    objPrize.SaveStatus = 6;
                }

                if (hdUniqueId.Value.ToString().Trim() != "")
                {
                    bool IsUpdated = objLtmsService.UpdateApprovalInPrize(objPrize);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Prize Type information updated successfully.');", true);
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
            if (objValidateData.IsInteger(txtClaimDays.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Number of Digit in series should be numeric.');", true);
                txtClaimDays.Focus();
                return false;
            }
                      

            return true;
        }
        private void BindGvData()
        {
            try
            {
              
                GvData.DataSource = objLtmsService.GetGovermentOrderDtlbyStatus(Convert.ToInt16(ddlStatus.SelectedValue));
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
        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindGvData();
        }
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet dtInfo = new DataSet();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    int StatusVal = 0;
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    dtInfo = objLtmsService.GetPrizeDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Tables[0].Rows.Count > 0)
                    {
                        txtUnSoldPercentage.Text = dtInfo.Tables[0].Rows[0]["UnSoldPercentage"].ToString();
                        txtGovermentOrder.Text = dtInfo.Tables[0].Rows[0]["GovermentOrder"].ToString();
                        txtLotteryType.Text = dtInfo.Tables[0].Rows[0]["LotteryType"].ToString();
                        txtLotteryName.Text = dtInfo.Tables[0].Rows[0]["LotteryName"].ToString();
                        txtClaimDays.Text = dtInfo.Tables[0].Rows[0]["ClaimDays"].ToString();
                        chkIncludeConsPrize.Checked = Convert.ToBoolean(dtInfo.Tables[0].Rows[0]["IncludeConsPrize"]);
                        StatusVal = Convert.ToInt16(dtInfo.Tables[0].Rows[0]["SaveStatus"].ToString());
                    }
                    if (dtInfo.Tables[1].Rows.Count > 0)
                    {
                        gvPrizeDetails.DataSource = dtInfo.Tables[1];
                        gvPrizeDetails.DataBind();
                    }                    
                    dtInfo.Dispose();
                    btnSave.Visible = true;
                    btnConfirm.Visible = true;
                    if (StatusVal >= 5)
                    {
                        btnSave.Visible = false;
                        btnConfirm.Visible = false;
                    }

                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
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
            try
            {
                ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                string Status = "";
                int StatusVal = Convert.ToInt16(lblStatus.Text);
                if (StatusVal == 3) { Status = "<font color='red'>Approval Pending</font>"; }               
                else if (StatusVal == 5) { Status = "Second Leval Approved"; }
                else if (StatusVal == 6) { Status = "Second Leval Reject"; }
                else if (StatusVal > 6) { Status = "Second Leval Approved"; }
                //else if (StatusVal == 7) { Status = "Final Leval Approved"; }
                //else if (StatusVal == 8) { Status = "Final Leval Reject"; }
                lblStatus.Text = Status;
                
            }
            catch { }
        }        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;

            
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "Prize";
            Response.Redirect("rptViewAppReport.aspx");

        }
    }
}