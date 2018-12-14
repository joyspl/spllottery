using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxGenerateLot : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsRequisition objRequisition = new ClsRequisition();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
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
                    FillRequisitionCombo();
                    BindGvData(default(int));
                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlReqNumber.SelectedItem.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select Requisition Number.');", true);
                return;
            }
            BindGvData(Convert.ToInt32(ddlReqNumber.SelectedItem.Value));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "myPopup", "window.open('ToTicketToExcel.aspx');", true);


            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance. The error is as below " + Ex.Message + "");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        private void FillRequisitionCombo()
        {
            try
            {
                var dt = objLtmsService.GetRequisitiondataForDDL(default(int), default(int));
                if (dt != null && dt.Rows.Count > default(int))
                {
                    ddlReqNumber.DataSource = dt;
                    ddlReqNo.DataSource = dt;
                    ddlReqNumber.DataValueField = "DataUniqueId";
                    ddlReqNo.DataValueField = "DataUniqueId";
                    ddlReqNumber.DataTextField = "ReqCode";
                    ddlReqNo.DataTextField = "ReqCode";
                    ddlReqNumber.DataBind();
                    ddlReqNo.DataBind();
                    ddlReqNumber.Items.Insert(0, new ListItem("<<--Select Requisition-->>", "0"));
                    ddlReqNo.Items.Insert(0, new ListItem("<<--Select Requisition-->>", "0"));
                    ddlReqNumber.SelectedIndex = 0;
                    ddlReqNo.SelectedIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        private void BindGvData(int reqid)
        {
            try
            {
                gvData.DataSource = objLtmsService.GetSeriesGenerationListByReqId(reqid);
                gvData.DataBind();
                gvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                gridDiv.Visible = objMenuOptions.AllowView;
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdnID.Value = gvData.DataKeys[gvrow.RowIndex].Value.ToString();
                if (e.CommandName == "EditEntry")
                {
                    dtInfo = objLtmsService.GetSeriesGenerationById(Convert.ToInt64(hdnID.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        objValidateData.ClearAllInputField(pnlDataEntry);

                        txtSeries1Start.Text = dtInfo.Rows[0]["Series1Start"].ToString();
                        txtSeries1End.Text = dtInfo.Rows[0]["Series1End"].ToString();
                        txtSeries2Start.Text = dtInfo.Rows[0]["Series2Start"].ToString();
                        txtSeries2End.Text = dtInfo.Rows[0]["Series2End"].ToString();
                        txtStartNumber.Text = dtInfo.Rows[0]["NumStart"].ToString();
                        txtEndNumber.Text = dtInfo.Rows[0]["NumEnd"].ToString();
                        ddlReqNo.ClearSelection();
                        ddlReqNo.Items.FindByValue(dtInfo.Rows[0]["ReqId"].ToString()).Selected = true;
                        ddlReqNo.Enabled = false;
                        hdnReqID.Value = dtInfo.Rows[0]["ReqId"].ToString();
                    }
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
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


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hdnID.Value = "0";
            hdnReqID.Value = "0";
            ddlReqNo.Enabled = true;
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }

        private bool IsValidEntry()
        {
            if (ddlReqNo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Requisition Number.');", true);
                ddlReqNo.Focus();
                return false;
            }
            if (objValidateData.IsLongInteger(txtSeries1Start.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Series1 Start should be numeric.');", true);
                txtSeries1Start.Focus();
                return false;
            }
            if (objValidateData.IsLongInteger(txtSeries1End.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Series1 End should be numeric.');", true);
                txtSeries1End.Focus();
                return false;
            }
            if (objValidateData.IsLongIntegerWithZero(txtStartNumber.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Start Number should be numeric.');", true);
                txtStartNumber.Focus();
                return false;
            }
            if (objValidateData.IsLongInteger(txtEndNumber.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('End Number should be numeric.');", true);
                txtEndNumber.Focus();
                return false;
            }
            if (objValidateData.istValidAlphabets(txtSeries2Start.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Series2 Start should be an alphabet.');", true);
                txtSeries2Start.Focus();
                return false;
            }
            if (objValidateData.istValidAlphabets(txtSeries2End.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Series2 End should be an alphabet.');", true);
                txtSeries2End.Focus();
                return false;
            }
            return true;
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            string InsertedId = string.Empty;
            long id = default(long);
            try
            {
                if (IsValidEntry() == false) { return; }
                ClsSeriesGeneration obj = new ClsSeriesGeneration();
                if (!string.IsNullOrWhiteSpace(hdnID.Value))
                {
                    long.TryParse(hdnID.Value, out id);
                }
                obj.ID = id;
                obj.Series1Start = Convert.ToInt64(txtSeries1Start.Text.Trim());
                obj.Series1End = Convert.ToInt64(txtSeries1End.Text.Trim());
                obj.Series2Start = txtSeries2Start.Text.Trim().ToUpper();
                obj.Series2End = txtSeries2End.Text.Trim().ToUpper();
                obj.NumStart = Convert.ToInt64(txtStartNumber.Text.Trim());
                obj.NumEnd = Convert.ToInt64(txtEndNumber.Text.Trim());
                obj.ReqId = Convert.ToInt32(ddlReqNo.SelectedItem.Value);
                bool insertResult = obj.ID == default(long) ? objLtmsService.InsertInSeriesGeneration(obj, out InsertedId) : objLtmsService.UpdateInSeriesGeneration(obj);
                if (insertResult)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", string.Format("alert('Lot information have been {0} successfully.');", (obj.ID == default(long) ? "saved" : "updated")), true);
                    btnCancel_Click(sender, e);
                    btnGo_Click(sender, e);
                }
                else
                {
                    throw new Exception("Unable to save data");
                }
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                btnCancel_Click(sender, e);
            }
        }

        protected void ddlReqNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindGvData(Convert.ToInt32(ddlReqNumber.SelectedItem.Value));
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        protected void btnAddNewLot_Click(object sender, EventArgs e)
        {
            try
            {
                ddlReqNo.Enabled = true;
                objValidateData.ClearAllInputField(pnlDataEntry);
                pnlDataEntry.Visible = true;
                pnlDataDisplay.Visible = false;
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
    }
}