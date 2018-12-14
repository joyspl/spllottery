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
    public partial class MstResultEntryApproval : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsRequisition objRequisition = new ClsRequisition();
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
                }
            }
        }
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-120"));
                items.Add(new ListItem("Pending", "62"));
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
                string ReqId = "";
                string Status = "";
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }
                foreach (GridViewRow row in GvData.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        ReqId = ReqId + GvData.DataKeys[row.RowIndex].Value.ToString() + ",";
                    }
                }
                ReqId = ReqId.TrimEnd(',');

                if (((Button)sender).CommandName == "Approve")
                {
                    Status = "Approved";
                    objRequisition.SaveStatus = 63;
                    objRequisition.UploadStatus = 2;
                }
                else if (((Button)sender).CommandName == "Reject")
                {
                    Status = "Rejected";
                    objRequisition.SaveStatus = 64;
                    objRequisition.UploadStatus = 3;
                }


                DataTable dtTicket = new DataTable();
                dtTicket.TableName = "dtTicket";
                dtTicket.Columns.Add(new DataColumn("RequisitionId", typeof(Int64)));
                dtTicket.Columns.Add(new DataColumn("RowNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("ClaimCount", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("UnsoldCount", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("DataCount", typeof(Int32)));
                dtTicket.Columns.Add(new DataColumn("WiningSerialNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("Amount", typeof(double)));
                dtTicket.Columns.Add(new DataColumn("Remarks", typeof(string)));
                DataRow dtRw = null;

                Int64 ClaimCount = 0, UnsoldCount = 0, FinalClaimCnt = 0;

                foreach (GridViewRow row in GvData.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        Int64 DataUniqueId = Convert.ToInt64(GvData.DataKeys[row.RowIndex].Value);
                        DataTable dtRequisitionId = objLtmsService.GetRequisitionDtlById(DataUniqueId);
                        if (dtRequisitionId.Rows.Count > 0)
                        {
                            DataSet dsPrizeInfo = objLtmsService.GetWinnigSlNoDtlByRequisitionId(DataUniqueId);
                            DataTable dtUnsold = objLtmsService.GetUnSoldSummaryById(DataUniqueId);
                            if (dsPrizeInfo.Tables[0].Rows.Count > 0)
                            {
                                for (int iRow = 0; iRow < dsPrizeInfo.Tables[0].Rows.Count; iRow++)
                                {
                                    ClaimCount = GetVariableTicketCount(DataUniqueId, dsPrizeInfo.Tables[0].Rows[iRow]["WiningSerialNo"].ToString(), ref dtRequisitionId);
                                    UnsoldCount = 0;
                                    if (dsPrizeInfo.Tables[0].Rows[iRow]["ValidationForUnsold"].ToString().ToUpper() == "Y")
                                    {
                                        UnsoldCount = GetUnsoldTicketCount(dsPrizeInfo.Tables[0].Rows[iRow]["WiningSerialNo"].ToString(), ref  dtUnsold);
                                    }
                                    if (ClaimCount > 0)
                                    {
                                        FinalClaimCnt = ClaimCount - UnsoldCount;
                                    }
                                    else
                                    {
                                        FinalClaimCnt = 0;
                                    }

                                    if (FinalClaimCnt > 0)
                                    {
                                        dtRw = dtTicket.NewRow();
                                        dtRw["RequisitionId"] = dsPrizeInfo.Tables[0].Rows[iRow]["RequisitionId"].ToString();
                                        dtRw["RowNo"] = dsPrizeInfo.Tables[0].Rows[iRow]["RowNo"].ToString();
                                        dtRw["ClaimCount"] = ClaimCount.ToString();
                                        dtRw["UnsoldCount"] = UnsoldCount.ToString();
                                        dtRw["DataCount"] = FinalClaimCnt.ToString();
                                        dtRw["WiningSerialNo"] = dsPrizeInfo.Tables[0].Rows[iRow]["WiningSerialNo"].ToString();
                                        dtRw["Amount"] = (FinalClaimCnt * Convert.ToDouble(dsPrizeInfo.Tables[0].Rows[iRow]["PayableToSuperTicketWinner"])).ToString();
                                        dtRw["Remarks"] = "SUPER";
                                        dtTicket.Rows.Add(dtRw);

                                        dtRw = dtTicket.NewRow();
                                        dtRw["RequisitionId"] = dsPrizeInfo.Tables[0].Rows[iRow]["RequisitionId"].ToString();
                                        dtRw["RowNo"] = dsPrizeInfo.Tables[0].Rows[iRow]["RowNo"].ToString();
                                        dtRw["ClaimCount"] = ClaimCount.ToString();
                                        dtRw["UnsoldCount"] = UnsoldCount.ToString();
                                        dtRw["DataCount"] = FinalClaimCnt.ToString();
                                        dtRw["WiningSerialNo"] = dsPrizeInfo.Tables[0].Rows[iRow]["WiningSerialNo"].ToString();
                                        dtRw["Amount"] = (FinalClaimCnt * Convert.ToDouble(dsPrizeInfo.Tables[0].Rows[iRow]["PayableToSpecialTicketWinner"])).ToString();
                                        dtRw["Remarks"] = "SPECIAL";
                                        dtTicket.Rows.Add(dtRw);
                                    }
                                }
                            }
                        }
                    }
                }

                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objRequisition.IpAdd = Request.UserHostAddress;
                bool IsUpdated = objLtmsService.UpdateApprovalInVariableClaim(objRequisition, ReqId, dtTicket);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Selected information " + Status + " successfully.');", true);
                }
                BindGvData();
                //btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        public Int64 GetVariableTicketCount(Int64 DataUniqueId, string WinningSerialNo, ref DataTable dtRequisitionId)
        {
            int FnStart = 0, FnEnd = 0;
            string AlphabetSeries = "";
            Int64 TnStart = 0, TnEnd = 0;
            Int64 ClaimCount = 0;
            Int64 sum = 0;

            FnStart = Convert.ToInt16(dtRequisitionId.Rows[0]["FnStart"].ToString());
            FnEnd = Convert.ToInt16(dtRequisitionId.Rows[0]["FnEnd"].ToString());
            AlphabetSeries = dtRequisitionId.Rows[0]["AlphabetSeries"].ToString();
            TnStart = Convert.ToInt64(dtRequisitionId.Rows[0]["TnStart"].ToString());
            TnEnd = Convert.ToInt64(dtRequisitionId.Rows[0]["TnEnd"].ToString());

            string StartNo = TnStart.ToString();
            string EndNo = TnEnd.ToString();
            if (Convert.ToInt32(WinningSerialNo) <= Convert.ToInt32(EndNo))
            {
                ClaimCount = ClaimCount + calculateClaim(WinningSerialNo, StartNo, EndNo);
            }
            if (ClaimCount > 0)
            {
                string[] values = AlphabetSeries.Split(',');
                ClaimCount = ClaimCount * values.Length * ((FnEnd - FnStart) + 1);
            }
            return ClaimCount;
        }

        public Int64 GetUnsoldTicketCount(string WinningSerialNo, ref DataTable dtUnsold)
        {
            Int64 UnsoldCount = 0;
            for (int iCnt = 0; iCnt < dtUnsold.Rows.Count; iCnt++)
            {

                string StartNo = dtUnsold.Rows[iCnt]["StartNo"].ToString();
                string EndNo = dtUnsold.Rows[iCnt]["EndNo"].ToString();
                if (Convert.ToInt32(WinningSerialNo) <= Convert.ToInt32(EndNo))
                {
                    UnsoldCount = UnsoldCount + calculateClaim(WinningSerialNo, StartNo, EndNo);
                }
            }
            return UnsoldCount;
        }

        public Int64 calculateClaim(string WinningSerialNo, string StartNo, string EndNo)
        {
            Int64 ClaimCount = 0;
            string InitialStartNo = StartNo.ToString().PadLeft(EndNo.Length, '0');

            string SecondStartNo = InitialStartNo.ToString().Substring(0, (InitialStartNo.Length - WinningSerialNo.Length));
            string SecondEndNo = EndNo.ToString().Substring(0, (EndNo.Length - WinningSerialNo.Length));

            string SecondStartNo2 = InitialStartNo.ToString().Substring(SecondStartNo.Length, WinningSerialNo.Length);
            string SecondEndNo2 = EndNo.ToString().Substring(SecondEndNo.Length, WinningSerialNo.Length);

            if (Convert.ToInt32(SecondEndNo) - Convert.ToInt32(SecondStartNo) == 1 && Convert.ToInt32(WinningSerialNo) >= Convert.ToInt32(SecondStartNo2))
            {
                ClaimCount = ClaimCount +1 ;
            }

            if (Convert.ToInt32(SecondEndNo) - Convert.ToInt32(SecondStartNo) == 1 && Convert.ToInt32(WinningSerialNo) <= Convert.ToInt32(SecondEndNo2))
            {
                ClaimCount = ClaimCount + 1;
            }
            
            if(Convert.ToInt32(SecondEndNo) - Convert.ToInt32(SecondStartNo) > 1)
            {
                ClaimCount = ClaimCount + (Convert.ToInt32(SecondEndNo) - Convert.ToInt32(SecondStartNo));
            }

            if ((Convert.ToInt32(WinningSerialNo) >= Convert.ToInt32(SecondStartNo2) &&
                Convert.ToInt32(WinningSerialNo) <= Convert.ToInt32(SecondEndNo2)) &&
                Convert.ToInt32(SecondEndNo2) >= Convert.ToInt32(SecondStartNo2))
            {
                ClaimCount = ClaimCount + 1;
            }
            return ClaimCount;
        }

        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            bool IsSelected = false;
            foreach (GridViewRow row in GvData.Rows)
            {
                CheckBox check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                    IsSelected = true;
                    break;
                }
            }
            if (IsSelected == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select atleast one Requisition from the list');", true);
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

                GvData.DataSource = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);// status 1= confirmed
                GvData.DataBind();
                //GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                //GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
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
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                if (e.CommandName == "PrintEntry")
                {
                    clsInputParameter objInputParameter = new clsInputParameter();
                    objInputParameter.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                    objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
                    Session["InputParameter"] = objInputParameter;
                    Session["ReportName"] = "WinneListDtlById";
                    Response.Redirect("rptViewAppReport.aspx");

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
        protected void GvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 62) { Status = "<font color='red'>Pending</font>"; }
                    else if (StatusVal == 63) { Status = "First Leval Approved"; }
                    else if (StatusVal == 64) { Status = "First Leval Reject"; }
                    else if (StatusVal > 64) { Status = "First Leval Approved"; }
                    lblStatus.Text = Status;
                    if (StatusVal >= 63)
                    {
                        ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                    }
                }
                catch { }
            }
        }


        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                GvData.DataSource = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "RequisitionApprovalDtl";
            Response.Redirect("rptViewAppReport.aspx");

        }
    }
}