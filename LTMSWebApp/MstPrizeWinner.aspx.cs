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
    public partial class MstPrizeWinner : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsPrizeWinner objPrizeWinner = new ClsPrizeWinner();
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
        ////Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-110"));
                items.Add(new ListItem("Pending", "56"));
                items.Add(new ListItem("Draft", "61"));
                items.Add(new ListItem("Confirm", "62"));
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 0;
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

                DataTable dtPrizeWinnerDtl = new DataTable();
                dtPrizeWinnerDtl.TableName = "PrizeWinnerDetails";
                DataRow dr = null;
                dtPrizeWinnerDtl.Columns.Add(new DataColumn("RowNo", typeof(string)));
                dtPrizeWinnerDtl.Columns.Add(new DataColumn("WiningSerialRowNo", typeof(string)));
                dtPrizeWinnerDtl.Columns.Add(new DataColumn("WiningSerialNo", typeof(string)));              
                for (int iCtr = 0; iCtr < gvPrizeWinnerDetails.Rows.Count; iCtr++)
                {
                    GridView gvWiningSerialNo = ((GridView)gvPrizeWinnerDetails.Rows[iCtr].Cells[0].FindControl("gvWiningSerialNo"));
                    for (int jCnt = 0; jCnt < gvWiningSerialNo.Rows.Count; jCnt++)
                    {
                        dr = dtPrizeWinnerDtl.NewRow();
                        dr["RowNo"] = gvPrizeWinnerDetails.DataKeys[iCtr].Value.ToString();
                        dr["WiningSerialRowNo"] = Convert.ToInt16(((Label)gvWiningSerialNo.Rows[jCnt].Cells[0].FindControl("lblRowNo")).Text.Trim());
                        dr["WiningSerialNo"] = ((TextBox)gvWiningSerialNo.Rows[jCnt].Cells[0].FindControl("txtWiningSerialNo")).Text.Trim();
                        dtPrizeWinnerDtl.Rows.Add(dr);
                    }
                }
                if (((Button)sender).CommandName == "Save")
                {
                    objPrizeWinner.SaveStatus = 61;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objPrizeWinner.SaveStatus = 62;
                }
                //objPrizeWinner.LotteryId = Convert.ToInt16(ddlLottery.SelectedValue.ToString());
                objPrizeWinner.JudgesName1 = txtJudgesName1.Text.Trim();
                objPrizeWinner.JudgesName2 = txtJudgesName2.Text.Trim();
                objPrizeWinner.JudgesName3 = txtJudgesName3.Text.Trim();
                objPrizeWinner.PlayingAddress = txtPlayingAddress.Text.Trim();
                objPrizeWinner.DrawTime = txtDrawTime.Text.Trim();

               // objPrizeWinner.DrawDate = Convert.ToDateTime(txtDrawDate.Text);
                objPrizeWinner.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objPrizeWinner.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() != "")
                {
                    objPrizeWinner.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInPrizeWinner(objPrizeWinner, dtPrizeWinnerDtl);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('PrizeWinnerWinner Type information updated successfully.');", true);
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
           

            bool IsError = false;
            string ErrorMsg = "",Msg="";

            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtLotteryInfo = new DataTable();

            dtLotteryInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
            if (dtLotteryInfo.Rows.Count > 0)
            {

                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtLotteryInfo.Rows[0]["DataUniqueId"].ToString());
                objGeneratedNo.DrawNo = Convert.ToInt16(dtLotteryInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtLotteryInfo.Rows[0]["DrawDate"].ToString());
                objGeneratedNo.FnStart = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnStart"].ToString());
                objGeneratedNo.FnEnd = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnEnd"].ToString());
                objGeneratedNo.AlphabetSeries = dtLotteryInfo.Rows[0]["AlphabetSeries"].ToString();
                objGeneratedNo.TnStart = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnStart"].ToString());
                objGeneratedNo.TnEnd = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnEnd"].ToString());
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery information not generated in inventory.');", true);
                return false;
            }

            for (int iCtr = 0; iCtr < gvPrizeWinnerDetails.Rows.Count; iCtr++)
            {
                Label lblNoOfDigitInStatic = (Label)gvPrizeWinnerDetails.Rows[iCtr].Cells[0].FindControl("lblNoOfDigitInStatic");
                CheckBox chkValidationForUnsold = (CheckBox)gvPrizeWinnerDetails.Rows[iCtr].Cells[0].FindControl("chkValidationForUnsold");
                
                GridView gvWiningSerialNo = ((GridView)gvPrizeWinnerDetails.Rows[iCtr].Cells[0].FindControl("gvWiningSerialNo"));
                for (int jCnt = 0; jCnt < gvWiningSerialNo.Rows.Count; jCnt++)
                {
                    TextBox txtWiningSerialNo = (TextBox)gvWiningSerialNo.Rows[jCnt].Cells[0].FindControl("txtWiningSerialNo");
                    if (txtWiningSerialNo.Text.Trim() == "")
                    {
                        IsError = true;
                        ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Wining SerialNo name can not be balnk in row no. " + (iCtr + 1) + " for box"+(jCnt+1);
                    }
                    else if (txtWiningSerialNo.Text.Trim() != "" && txtWiningSerialNo.Text.Trim().Length != Convert.ToInt16(lblNoOfDigitInStatic.Text))
                    {
                        IsError = true;
                        ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "No of character in Wining Serial No. should be " + Convert.ToInt16(lblNoOfDigitInStatic.Text) + "  in row no. " + (iCtr + 1) + " for box" + (jCnt + 1);
                    }
                    else if (txtWiningSerialNo.Text.Trim() != "" && txtWiningSerialNo.Text.Trim().Length == Convert.ToInt16(lblNoOfDigitInStatic.Text))
                    {
                        if (dtLotteryInfo.Rows.Count > 0) {
                            Msg = objValidateData.ValidatePartialTicketNo(objGeneratedNo, txtWiningSerialNo.Text);
                            if (Msg.Length > 0)
                            {
                                IsError = true;
                                ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + Msg + "  in row no. " + (iCtr + 1) + " for box" + (jCnt + 1);
                            }
                            if (Msg.Length==0 && chkValidationForUnsold.Checked == true)
                            {
                                int FnNo = 0, TicketLength = 0, FnEndLength = 0, AlphabetSeriesLength, TnEndLength=0;
                                string Alphabet = "";
                                Int64 TnNo = 0;

                                string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                                string[] values = AlphabetSeries.Split(',');
                                int AlphabetLen = Convert.ToInt16(values[values.Length - 1].ToString().Length);

                                FnEndLength = (objGeneratedNo.FnEnd == 0 ? 0 : objGeneratedNo.FnEnd.ToString().Length);
                                AlphabetSeriesLength = values[values.Length - 1].ToString().Trim().Length;
                                TnEndLength = objGeneratedNo.TnEnd.ToString().Length;

                                TicketLength = (objGeneratedNo.FnEnd == 0 ? 0 : FnEndLength) + AlphabetSeriesLength + TnEndLength;
                                if (txtWiningSerialNo.Text.Trim().ToUpper().Length == TicketLength)
                                {
                                    FnNo = Convert.ToInt16(((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : txtWiningSerialNo.Text.Trim().Substring(0, objGeneratedNo.FnEnd.ToString().Length)));
                                    Alphabet = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? txtWiningSerialNo.Text.Trim().Substring(0, AlphabetLen) : txtWiningSerialNo.Text.Trim().Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                                    TnNo = Convert.ToInt64(txtWiningSerialNo.Text.Trim().Substring(txtWiningSerialNo.Text.Trim().Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length));

                                    DataSet dtLotteryDtl = objLtmsService.GetLotteryDtlInClaimAndUnSold(objGeneratedNo.DataUniqueId, txtWiningSerialNo.Text.Trim().ToUpper(), FnNo, Alphabet, TnNo);
                                    if (dtLotteryDtl != null)
                                    {
                                        if (dtLotteryDtl.Tables[1].Rows.Count > 0)
                                        {                                            
                                            IsError = true;
                                            ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "The Wining Serial No. " + txtWiningSerialNo.Text.Trim().ToUpper() + " is available in unsold ticket in row no. " + (iCtr + 1) + " for box" + (jCnt + 1);
                                        }
                                    }
                                }
                                else if (txtWiningSerialNo.Text.Trim().ToUpper().Length <= TnEndLength && objValidateData.IsIntegerWithZero(txtWiningSerialNo.Text.Trim().ToUpper()))
                                {
                                    if (Convert.ToInt32(txtWiningSerialNo.Text.Trim()) <= objGeneratedNo.TnEnd)
                                    {
                                        FnNo = 0;
                                        Alphabet = "";
                                        TnNo = Convert.ToInt64(txtWiningSerialNo.Text.Trim());

                                        DataSet dtLotteryDtl = objLtmsService.GetLotteryDtlInClaimAndUnSold(objGeneratedNo.DataUniqueId, txtWiningSerialNo.Text.Trim().ToUpper(), FnNo, Alphabet, TnNo);
                                        if (dtLotteryDtl != null)
                                        {
                                            if (dtLotteryDtl.Tables[2].Rows.Count > 0)
                                            {
                                                IsError = true;
                                                ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "The Wining Serial No. " + txtWiningSerialNo.Text.Trim().ToUpper() + " is available in unsold ticket in row no. " + (iCtr + 1) + " for box" + (jCnt + 1);
                                            }
                                        }
                                    }
                                }


                               
                            }
                        }
                        //
                    }

                                        
                }
            }
            
            if (IsError == true)
            {
                var message = new JavaScriptSerializer().Serialize("Some errors in PrizeWinner details table as below.\n'" + ErrorMsg);
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                // ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Some error in PrizeWinner details table as below.\n'"+ ErrorMsg +");", true);
                return false;
            }

            if (txtJudgesName1.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Judges Name 1 can not be left blank.');", true);
                txtJudgesName1.Focus();
                return false;
            }
            
            if (txtPlayingAddress.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Playing Address can not be left blank.');", true);
                txtPlayingAddress.Focus();
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
                GvData.DataSource = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
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
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    gvPrizeWinnerDetails.DataSource = null;
                    gvPrizeWinnerDetails.DataBind();
                    int Status = 0;
                   // dtInfo = objLtmsService.GetPrizeWinnerDtlById(Convert.ToInt64(hdUniqueId.Value));
                    dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        txtLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();
                        txtLottery.Text = dtInfo.Rows[0]["LotteryName"].ToString();
                        txtDrawDate.Text =Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtClaimDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).AddDays(Convert.ToInt16(dtInfo.Rows[0]["ClaimDays"])).ToString("dd-MMM-yyyy");

                        Status = Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString());
                        btnSave.Visible = false;
                        btnConfirm.Visible = false;
                        if (Status == 56)
                        {
                            btnSave.Visible = true;
                        }
                        else if (Status == 61 || Status == 64)
                        {
                            btnSave.Visible = true;
                            btnConfirm.Visible = true;
                        }

                        DataTable dtPrizeInfo = objLtmsService.GetWinneEntryDtlByRequisitionId(Convert.ToInt64(hdUniqueId.Value));
                        if (dtPrizeInfo.Rows.Count > 0)
                        {
                            gvPrizeWinnerDetails.DataSource = dtPrizeInfo;
                            gvPrizeWinnerDetails.DataBind();
                        }
                        dtPrizeInfo.Dispose();
                        DataTable dtPrizeWinnerInfo = objLtmsService.GetWinnePrizeWinningSlNoDtlById(Convert.ToInt64(hdUniqueId.Value), 1);
                        if (dtPrizeWinnerInfo.Rows.Count > 0)
                        {
                            txtJudgesName1.Text = dtPrizeWinnerInfo.Rows[0]["JudgesName1"].ToString();
                            txtJudgesName2.Text = dtPrizeWinnerInfo.Rows[0]["JudgesName2"].ToString();
                            txtJudgesName3.Text = dtPrizeWinnerInfo.Rows[0]["JudgesName3"].ToString();
                            txtPlayingAddress.Text = dtPrizeWinnerInfo.Rows[0]["PlayingAddress"].ToString();
                            txtDrawTime.Text = dtPrizeWinnerInfo.Rows[0]["DrawTime"].ToString();
                        }

                    }                    
                    dtInfo.Dispose();

                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                  
                }
                if (e.CommandName == "PrintEntry")
                {
                    clsInputParameter objInputParameter = new clsInputParameter();
                    objInputParameter.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                    objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
                    Session["InputParameter"] = objInputParameter;
                    Session["ReportName"] = "WinneListDtlById";
                    Response.Redirect("rptViewAppReport.aspx");

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
                Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                string Status = "";
                int StatusVal = Convert.ToInt16(lblStatus.Text);
                if (StatusVal == 56) { Status = "<font color='red'>Pending</font>"; }
                else if (StatusVal == 61) { Status = "Draft"; }
                else if (StatusVal == 62) { Status = "Confirm"; }
                else if (StatusVal == 63 || StatusVal > 64) { Status = "Approved"; }
                else if (StatusVal == 64) { Status = "Rejected"; }
                //else if (StatusVal == 1000) { Status = "Transaction Complited"; }
                //else { Status = "Transaction In-Progress"; }
                lblStatus.Text = Status;
                if (StatusVal >= 72)
                {
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                }

            }
        }
        protected void gvPrizeWinnerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int NoOfWinner = 0;
                NoOfWinner = Convert.ToInt16(((Label)e.Row.FindControl("lblNoOfWinner")).Text);

                Int64 PrizeWinnerId = Convert.ToInt64(hdUniqueId.Value);
                Int16 RowNo = Convert.ToInt16(gvPrizeWinnerDetails.DataKeys[e.Row.RowIndex].Value.ToString());
                //DataTable GetWinnePrizeWinningSlNoDtlById(Int64 InDataUniqueId, Int16 RowNo)
                GridView gvWiningSerialNo = e.Row.FindControl("gvWiningSerialNo") as GridView;
                gvWiningSerialNo.DataSource = null;
                gvWiningSerialNo.DataBind();
                DataTable dtPrizeWinnerInfo = objLtmsService.GetWinnePrizeWinningSlNoDtlById(PrizeWinnerId, RowNo);
                if (dtPrizeWinnerInfo.Rows.Count > 0)
                {
                    gvWiningSerialNo.DataSource = dtPrizeWinnerInfo;
                    gvWiningSerialNo.DataBind();
                }
                else
                {
                  //  GridView gvWiningSerialNo = e.Row.FindControl("gvWiningSerialNo") as GridView;
                    DataTable dtPrizeWinnerDtl = new DataTable();
                    dtPrizeWinnerDtl.TableName = "PrizeWinnerDetails";
                    DataRow dr = null;
                    dtPrizeWinnerDtl.Columns.Add(new DataColumn("WiningSerialRowNo", typeof(string)));
                    dtPrizeWinnerDtl.Columns.Add(new DataColumn("WiningSerialNo", typeof(string)));
                    for (int iCnt = 0; iCnt < NoOfWinner; iCnt++)
                    {
                        dr = dtPrizeWinnerDtl.NewRow();
                        dr["WiningSerialRowNo"] = (iCnt + 1).ToString();
                        dr["WiningSerialNo"] = "";
                        dtPrizeWinnerDtl.Rows.Add(dr);
                    }
                    // GridView gvWiningSerialNo = e.Row.FindControl("gvWiningSerialNo") as GridView;
                    gvWiningSerialNo.DataSource = dtPrizeWinnerDtl;
                    gvWiningSerialNo.DataBind();

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
            Session["ReportName"] = "PrizeWinner";
            Response.Redirect("rptViewAppReport.aspx");
        }

        

        
    }
}