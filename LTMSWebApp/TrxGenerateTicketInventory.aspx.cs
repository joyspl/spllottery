using Ionic.Zip;
using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxGenerateTicketInventory : System.Web.UI.Page
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
                Server.Transfer("~/appError.aspx");
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
                    Server.Transfer("~/appError.aspx");
                }
                else
                {
                    Session["FromNavigation"] = false;
                    if (!(objMenuOptions.AllowEntry || objMenuOptions.AllowEdit || objMenuOptions.AllowDelete || objMenuOptions.AllowView))
                    {
                        Session["CustomError"] = "You do not have proper Privilege to access the selected module";
                        Server.Transfer("~/appError.aspx");
                        return;
                    }
                    txtFromDate.Text = DateTime.Now.AddMonths(-2).ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    hdUniqueId.Value = null;
                    FillComboBox();
                    BindGvData();
                    btnZip.Visible = false;
                }
            }
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-35"));
                items.Add(new ListItem("Pending", "33"));
                items.Add(new ListItem("Draft", "41"));
                items.Add(new ListItem("Confirm", "42"));
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
            finally
            {
                trHdr.Visible = false;
                trDtl.Visible = false;
            }
        }

        private void FillLotInfoCombo(int reqid)
        {
            try
            {
                var dt = objLtmsService.GetSeriesGenerationListByReqId(reqid);
                if (dt != null && dt.Rows.Count > default(int))
                {
                    /*ddlLotInfo.DataSource = dt;
                    ddlLotInfo.DataValueField = "ID";
                    ddlLotInfo.DataTextField = "ReqCode";
                    ddlLotInfo.DataBind();*/
                    //ddlLotInfo.ClearSelection();
                    ddlLotInfo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddlLotInfo.Items.Insert(i, new ListItem(string.Format("Series1-{0}~{1}, Series2-{2}~{3} (No.-{4}~{5})"
                            , dt.Rows[i]["Series1Start"].ToString()
                            , dt.Rows[i]["Series1End"].ToString()
                            , dt.Rows[i]["Series2Start"].ToString()
                            , dt.Rows[i]["Series2End"].ToString()
                            , dt.Rows[i]["NumStart"].ToString()
                            , dt.Rows[i]["NumEnd"].ToString())
                            , dt.Rows[i]["ID"].ToString()));
                    }
                    ddlLotInfo.Items.Insert(0, new ListItem("<<--Select Lot-->>", "-1"));
                    ddlLotInfo.SelectedIndex = 0;
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
        protected void gvGenarateNo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                if (e.CommandName == "Download")
                {
                    Label lblInsertedId = (Label)gvrow.FindControl("lblInsertedId");
                    Label lblRowNo = (Label)gvrow.FindControl("lblRowNo");
                    Label lblStartNo = (Label)gvrow.FindControl("lblStartNo");
                    Label lblEndNo = (Label)gvrow.FindControl("lblEndNo");
                    Label lblRowFnStart = (Label)gvrow.FindControl("lblRowFnStart");
                    Label lblRowFnEnd = (Label)gvrow.FindControl("lblRowFnEnd");
                    Label lblRowFnAlphabet = (Label)gvrow.FindControl("lblRowFnAlphabet");

                    ClsTicketGenRequest objClsTicketGenRequest = new ClsTicketGenRequest();
                    objClsTicketGenRequest.ID = Convert.ToInt64(hdnGenID.Value);
                    objClsTicketGenRequest.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                    objClsTicketGenRequest.RowNo = Convert.ToInt32(lblRowNo.Text);
                    objClsTicketGenRequest.TnStart = Convert.ToInt64(lblStartNo.Text);
                    objClsTicketGenRequest.TnEnd = Convert.ToInt64(lblEndNo.Text);
                    objClsTicketGenRequest.AlphabetSeries = lblRowFnAlphabet.Text;
                    objClsTicketGenRequest.FnStart = Convert.ToInt32(lblRowFnStart.Text);
                    objClsTicketGenRequest.FnEnd = Convert.ToInt32(lblRowFnEnd.Text);
                    objClsTicketGenRequest.InsertedId = Convert.ToInt64(!string.IsNullOrWhiteSpace(lblInsertedId.Text.Trim()) ? lblInsertedId.Text.Trim() : "0");

                    Session["TicketGenRequest"] = objClsTicketGenRequest;

                    ScriptManager.RegisterStartupScript(this, typeof(string), "myPopup", "window.open('ToTicketToExcel.aspx');", true);

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
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnDynGen_Click(object sender, EventArgs e)
        {
            string alphaValues = string.Empty;
            string InsertedId = string.Empty;
            //string[] alphArr = { "A", "B", "C", "D", "E", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            try
            {
                InsertedId = ddlLotInfo.SelectedItem.Value;
                if (System.Configuration.ConfigurationManager.AppSettings["AlphaValues"] != null)
                {
                    alphaValues = System.Configuration.ConfigurationManager.AppSettings["AlphaValues"];
                }
                else
                {
                    alphaValues = "A,B,C,D,E,G,H,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                }
                string[] alphArr = alphaValues.Split(',');

                string Series1From = txtSeries1From.Text.Trim();
                string Series1To = txtSeries1To.Text.Trim();
                string Series2From = txtSeries2From.Text.ToUpper().Trim();
                string Series2To = txtSeries2To.Text.ToUpper().Trim();
                string NumFrom = txtNumFrom.Text.Trim();
                string NumTo = txtNumTo.Text.Trim();

                int FnStart = default(int);
                int FnEnd = default(int);
                int TnStart = default(int);
                int TnEnd = default(int);

                int.TryParse(Series1From, out FnStart);
                int.TryParse(Series1To, out FnEnd);
                int.TryParse(NumFrom, out TnStart);
                int.TryParse(NumTo, out TnEnd);

                //LTMSWebApp.LTMSServiceRef.ClsSeriesGeneration obj = new LTMSWebApp.LTMSServiceRef.ClsSeriesGeneration();
                /*ClsSeriesGeneration obj = new ClsSeriesGeneration();
                obj.Series1Start = Convert.ToInt64(FnStart);
                obj.Series1End = Convert.ToInt64(FnEnd);
                obj.Series2Start = Series2From;
                obj.Series2End = Series2To;
                obj.NumStart = Convert.ToInt64(TnStart);
                obj.NumEnd = Convert.ToInt64(TnEnd);
                obj.ReqId = Convert.ToInt32(hdUniqueId.Value);
                bool insertResult = objLtmsService.InsertInSeriesGeneration(obj, out InsertedId);*/

                //string[] AlphabetSeries = (alphArr.ToList().GetRange(alphArr.ToList().IndexOf(Series2From), alphArr.ToList().IndexOf(Series2To) + 1)).ToArray();
                string[] a1 = (alphArr.ToList().GetRange(0, alphArr.ToList().IndexOf(Series2To) + 1)).ToArray();
                string[] AlphabetSeries = (a1.ToList().GetRange(a1.ToList().IndexOf(Series2From), ((a1.Length - 1) - (a1.ToList().IndexOf(Series2From))) + 1)).ToArray();




                long Slab = Convert.ToInt64(txtSlabLimit.Text.Trim() == "0" ? hdnSlabLimit.Value : txtSlabLimit.Text.Trim());
                long TicketGroup = default(long);
                bool IsStartNoRequire = ((FnStart == 0 && FnEnd == 0) ? false : true);
                long DiffOfInitialNo = (((FnStart == 0 && FnEnd == 0) ? 1 : ((FnEnd - FnStart) + 1)) * AlphabetSeries.Length);

                DataTable dtTicket = new DataTable();
                dtTicket.TableName = "dtTicketNew";
                dtTicket.Columns.Add(new DataColumn("InsertedId", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("RowNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("RowFnStart", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("RowFnEnd", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("RowFnAlphabet", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("InitialSlNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("StartNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("EndNo", typeof(string)));
                dtTicket.Columns.Add(new DataColumn("Total", typeof(string)));
                DataRow dtRw = null;
                TicketGroup = (int)Math.Ceiling((double)(((TnEnd - TnStart) + 1) / (Slab + 1)));
                if (TicketGroup == 0)
                {
                    TicketGroup = 1;
                    Slab = (int)Math.Ceiling((double)(TnEnd - TnStart));
                }

                Int64 startSlabNo = 0, EndSlabNo = 0;
                startSlabNo = TnStart;
                Int32 Counter = 0;
                for (Int64 iCnt = 1; iCnt <= TicketGroup; iCnt++)
                {
                    Counter++;

                    EndSlabNo = (startSlabNo + Slab);
                    dtRw = dtTicket.NewRow();
                    dtRw["InsertedId"] = InsertedId.Trim();
                    dtRw["RowNo"] = Counter.ToString();
                    dtRw["RowFnStart"] = FnStart.ToString();
                    dtRw["RowFnEnd"] = FnEnd.ToString();
                    dtRw["RowFnAlphabet"] = string.Join(",", AlphabetSeries);
                    dtRw["InitialSlNo"] = (IsStartNoRequire == true ? FnStart.ToString().PadLeft(FnEnd.ToString().Length, '0') + "-" + FnEnd.ToString() : "") + " " + string.Join(",", AlphabetSeries);
                    dtRw["StartNo"] = startSlabNo;
                    dtRw["EndNo"] = EndSlabNo;
                    dtRw["Total"] = DiffOfInitialNo + " X " + ((EndSlabNo - startSlabNo) + 1) + " = " + (((EndSlabNo - startSlabNo) + 1) * DiffOfInitialNo);
                    dtTicket.Rows.Add(dtRw);
                    startSlabNo = EndSlabNo + 1;
                }

                gvGenarateNo.DataSource = dtTicket;
                gvGenarateNo.DataBind();
                gvGenarateNo.Visible = true;

                btnZip.Visible = true;
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
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int SaveStatus = 0;
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtDrawDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        txtLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();
                        txtLotteryName.Text = dtInfo.Rows[0]["LotteryName"].ToString();
                        txtModifiedLotteryName.Text = dtInfo.Rows[0]["ModifiedLotteryName"].ToString();
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtSlabLimit.Text = dtInfo.Rows[0]["SlabLimit"].ToString();
                        hdnSlabLimit.Value = dtInfo.Rows[0]["SlabLimit"].ToString() == "0" ? (System.Configuration.ConfigurationManager.AppSettings["SlabLimit"] != null ? System.Configuration.ConfigurationManager.AppSettings["SlabLimit"] : "999") : dtInfo.Rows[0]["SlabLimit"].ToString();
                        lblDefaultSlabLimitThreshold.Text = string.Format("In case of Slab Limit 0, then the default Slab Limit (i.e. {0}) will consider.", (System.Configuration.ConfigurationManager.AppSettings["SlabLimit"] != null ? System.Configuration.ConfigurationManager.AppSettings["SlabLimit"] : "999"));
                        SaveStatus = Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString().ToUpper());


                        objGeneratedNo.DataUniqueId = Convert.ToInt64(dtInfo.Rows[0]["DataUniqueId"].ToString());
                        objGeneratedNo.ReqDate = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"].ToString());
                        objGeneratedNo.DrawNo = Convert.ToInt16(dtInfo.Rows[0]["DrawNo"].ToString());
                        objGeneratedNo.DrawDate = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"].ToString());
                        objGeneratedNo.FnStart = Convert.ToInt16(dtInfo.Rows[0]["FnStart"].ToString());
                        objGeneratedNo.FnEnd = Convert.ToInt16(dtInfo.Rows[0]["FnEnd"].ToString());
                        objGeneratedNo.AlphabetSeries = dtInfo.Rows[0]["AlphabetSeries"].ToString();
                        objGeneratedNo.TnStart = Convert.ToInt64(dtInfo.Rows[0]["TnStart"].ToString());
                        objGeneratedNo.TnEnd = Convert.ToInt64(dtInfo.Rows[0]["TnEnd"].ToString());
                        objGeneratedNo.StrReqDate = objGeneratedNo.ReqDate.Minute.ToString();

                        try
                        {
                            FillLotInfoCombo(Convert.ToInt32(dtInfo.Rows[0]["DataUniqueId"].ToString()));
                        }
                        catch { }

                        int FnStart = objGeneratedNo.FnStart;
                        int FnEnd = objGeneratedNo.FnEnd;
                        string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                        string[] values = AlphabetSeries.Split(',');
                        Int64 TnStart = objGeneratedNo.TnStart;
                        Int64 TnEnd = objGeneratedNo.TnEnd;
                        bool IsStartNoRequire = false;
                        // string QRUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["QRUrl"].ToString() + "?JB="; 
                        // Int64 Slab = 9999;
                        Int64 Slab = Convert.ToInt64(txtSlabLimit.Text.Trim() == "0" ? hdnSlabLimit.Value : txtSlabLimit.Text.Trim());
                        Int64 TicketGroup = 0;
                        IsStartNoRequire = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? false : true);

                        Int64 DiffOfInitialNo = (((FnStart == 0 && FnEnd == 0) ? 1 : ((FnEnd - FnStart) + 1)) * values.Length);

                        DataTable dtTicket = new DataTable();
                        dtTicket.TableName = "dtTicket";
                        dtTicket.Columns.Add(new DataColumn("InsertedId", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("RowNo", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("RowFnStart", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("RowFnEnd", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("RowFnAlphabet", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("InitialSlNo", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("StartNo", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("EndNo", typeof(string)));
                        dtTicket.Columns.Add(new DataColumn("Total", typeof(string)));
                        DataRow dtRw = null;
                        TicketGroup = (int)Math.Ceiling((double)(((TnEnd - TnStart) + 1) / (Slab + 1)));
                        if (TicketGroup == 0)
                        {
                            TicketGroup = 1;
                            Slab = (int)Math.Ceiling((double)(TnEnd - TnStart));
                        }

                        Int64 startSlabNo = 0, EndSlabNo = 0;
                        startSlabNo = TnStart;
                        Int32 Counter = 0;
                        for (Int64 iCnt = 1; iCnt <= TicketGroup; iCnt++)
                        {
                            Counter++;

                            EndSlabNo = (startSlabNo + Slab);
                            dtRw = dtTicket.NewRow();
                            dtRw["InsertedId"] = "0";
                            dtRw["RowNo"] = Counter.ToString();
                            dtRw["RowFnStart"] = FnStart.ToString();
                            dtRw["RowFnEnd"] = FnEnd.ToString();
                            dtRw["RowFnAlphabet"] = string.Join(",", AlphabetSeries);
                            dtRw["InitialSlNo"] = (IsStartNoRequire == true ? FnStart.ToString().PadLeft(FnEnd.ToString().Length, '0') + "-" + FnEnd.ToString() : "") + " " + AlphabetSeries;
                            dtRw["StartNo"] = startSlabNo;
                            dtRw["EndNo"] = EndSlabNo;
                            dtRw["Total"] = DiffOfInitialNo + " X " + ((EndSlabNo - startSlabNo) + 1) + " =" + (((EndSlabNo - startSlabNo) + 1) * DiffOfInitialNo);
                            dtTicket.Rows.Add(dtRw);
                            startSlabNo = EndSlabNo + 1;

                        }

                        gvGenarateNo.DataSource = dtTicket;
                        gvGenarateNo.DataBind();
                        gvGenarateNo.Visible = false;
                    }
                    dtInfo.Dispose();
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
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim());
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "TicketInventoryViewDtl";
            Response.Redirect("rptViewAppReport.aspx");
        }

        protected void btnDownloadAllLots_Click(object sender, EventArgs e)
        {
            List<long> lst = new List<long>();
            try
            {
                foreach (ListItem item in ddlLotInfo.Items)
                {
                    lst.Add(Convert.ToInt64(item.Value));
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlLotInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtInfo = objLtmsService.GetSeriesGenerationById(Convert.ToInt64(ddlLotInfo.SelectedItem.Value));
                if (dtInfo != null && dtInfo.Rows.Count > default(int))
                {
                    hdnGenID.Value = dtInfo.Rows[0]["ID"].ToString();
                    txtSeries1From.Text = dtInfo.Rows[0]["Series1Start"].ToString();
                    txtSeries1To.Text = dtInfo.Rows[0]["Series1End"].ToString();
                    txtSeries2From.Text = dtInfo.Rows[0]["Series2Start"].ToString();
                    txtSeries2To.Text = dtInfo.Rows[0]["Series2End"].ToString();
                    txtNumFrom.Text = dtInfo.Rows[0]["NumStart"].ToString();
                    txtNumTo.Text = dtInfo.Rows[0]["NumEnd"].ToString();
                    trHdr.Visible = true;
                    trDtl.Visible = true;
                    if (btnZip.Enabled == false)
                    {
                        btnZip.Enabled = true;
                    }
                    if (btnDynGen.Enabled == false)
                    {
                        btnDynGen.Enabled = true;
                    }
                    if (ddlLotInfo.Enabled == false)
                    {
                        ddlLotInfo.Enabled = true;
                    }
                }
                else
                {
                    trHdr.Visible = false;
                    trDtl.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                trHdr.Visible = false;
                trDtl.Visible = false;
            }
            finally
            {
                btnZip.Visible = false;
                gvGenarateNo.Visible = false;
            }
        }

        protected void btnZip_Click(object sender, EventArgs e)
        {
            string finalFilename = string.Empty;
            try
            {
                //string[] splitDate = txtDrawDate.Text.Trim().Split('-');
                string lotInfo = ddlLotInfo.SelectedItem.Text.Trim().Replace(" ", "").Replace("Series1", "").Replace("Series2", "").Replace("(", "").Replace(")", "").Replace("No.", "").Replace(",", "").Replace("-", "#").Replace("~", "-").Replace("#", "-");
                //finalFilename = string.Format("{0}_{1}", txtDrawNo.Text.Trim(), splitDate[0].Trim().ToUpper(), splitDate[1].ToUpper().Trim(), splitDate[2].Trim().Substring(splitDate[2].Trim().Length - 2).ToUpper(), lotInfo.Replace("-", "_"));
                finalFilename = string.Format("{0}_{1}{2}", (ddlLotInfo.SelectedIndex).ToString("D3"), txtReqCode.Text.Trim().ToUpper().Replace("/", ""), lotInfo.Replace("-", "_"));

                int chunkSize = 2 * 1024; // 2KB
                List<string> inputFiles = new List<string>();
                string ZipID = string.Empty;
                long zipid = default(long);
                int totalRecords = default(int);
                int currentPointer = default(int);
                var userObj = ((ClsUserInfo)Session["UserInfo"]);
                bool IsAdded = objLtmsService.InsertInZipProgress(new ZipProgressMaster() { UserId = userObj.UserId, ReqCode = txtReqCode.Text }, out ZipID);
                hdnUserID.Value = userObj.UserId;
                //hdnReqCode.Value = txtReqCode.Text.Replace("/", "");
                hdnReqCode.Value = HttpUtility.UrlEncode(finalFilename);
                btnZip.Enabled = false;
                try
                {
                    btnDynGen.Enabled = false;
                }
                catch { }
                ddlLotInfo.Enabled = false;
                System.Threading.ThreadPool.QueueUserWorkItem(s =>
                {
                    /*using (ZipFile zip = new ZipFile())
                    {
                        try
                        {
                            if (gvGenarateNo.Rows.Count > default(int))
                            {
                                totalRecords = gvGenarateNo.Rows.Count;
                                long.TryParse(ZipID, out zipid);
                                foreach (GridViewRow row in gvGenarateNo.Rows)
                                {
                                    try
                                    {
                                        Label lblInsertedId = (Label)row.FindControl("lblInsertedId");
                                        Label lblRowNo = (Label)row.FindControl("lblRowNo");
                                        Label lblStartNo = (Label)row.FindControl("lblStartNo");
                                        Label lblEndNo = (Label)row.FindControl("lblEndNo");
                                        Label lblRowFnStart = (Label)row.FindControl("lblRowFnStart");
                                        Label lblRowFnEnd = (Label)row.FindControl("lblRowFnEnd");
                                        Label lblRowFnAlphabet = (Label)row.FindControl("lblRowFnAlphabet");

                                        ClsTicketGenRequest objClsTicketGenRequest = new ClsTicketGenRequest();
                                        objClsTicketGenRequest.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                                        objClsTicketGenRequest.RowNo = Convert.ToInt32(lblRowNo.Text);
                                        objClsTicketGenRequest.TnStart = Convert.ToInt64(lblStartNo.Text);
                                        objClsTicketGenRequest.TnEnd = Convert.ToInt64(lblEndNo.Text);
                                        objClsTicketGenRequest.AlphabetSeries = lblRowFnAlphabet.Text;
                                        objClsTicketGenRequest.FnStart = Convert.ToInt32(lblRowFnStart.Text);
                                        objClsTicketGenRequest.FnEnd = Convert.ToInt32(lblRowFnEnd.Text);
                                        objClsTicketGenRequest.InsertedId = Convert.ToInt64(!string.IsNullOrWhiteSpace(lblInsertedId.Text.Trim()) ? lblInsertedId.Text.Trim() : "0");

                                        currentPointer += 1;
                                        var filepath = CreateAndFetchExcelFile(objClsTicketGenRequest, userObj, zipid, ((currentPointer * 100) / totalRecords));
                                        //var filepath = CreateAndGenerateTextFile(objClsTicketGenRequest, userObj, zipid, ((currentPointer * 100) / totalRecords));
                                        if (!string.IsNullOrWhiteSpace(filepath))
                                        {
                                            zip.AddFile(filepath);
                                        }
                                    }
                                    catch (Exception efx)
                                    {
                                        throw efx;
                                    }
                                }
                                if (!Directory.Exists(Server.MapPath(string.Format("~/Downloads/zip/{0}/", userObj.UserId))))
                                {
                                    Directory.CreateDirectory(Server.MapPath(string.Format("~/Downloads/zip/{0}/", userObj.UserId)));
                                }
                                if (File.Exists(Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", userObj.UserId, txtReqCode.Text.Replace("/", "")))))
                                {
                                    File.Delete(Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", userObj.UserId, txtReqCode.Text.Replace("/", ""))));
                                }
                                zip.Save(Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", userObj.UserId, txtReqCode.Text.Replace("/", ""))));
                            }
                            else
                                throw new Exception("No file found");
                        }
                        catch (Exception exc)
                        {
                            var isUpdated = objLtmsService.UpdateInZipProgress(new ZipProgressMaster() { ID = zipid, ZipProgress = -1 });
                            throw exc;
                        }
                    }*/

                    try
                    {
                        if (gvGenarateNo.Rows.Count > default(int))
                        {
                            totalRecords = gvGenarateNo.Rows.Count;
                            long.TryParse(ZipID, out zipid);
                            foreach (GridViewRow row in gvGenarateNo.Rows)
                            {
                                try
                                {
                                    Label lblInsertedId = (Label)row.FindControl("lblInsertedId");
                                    Label lblRowNo = (Label)row.FindControl("lblRowNo");
                                    Label lblStartNo = (Label)row.FindControl("lblStartNo");
                                    Label lblEndNo = (Label)row.FindControl("lblEndNo");
                                    Label lblRowFnStart = (Label)row.FindControl("lblRowFnStart");
                                    Label lblRowFnEnd = (Label)row.FindControl("lblRowFnEnd");
                                    Label lblRowFnAlphabet = (Label)row.FindControl("lblRowFnAlphabet");

                                    ClsTicketGenRequest objClsTicketGenRequest = new ClsTicketGenRequest();
                                    objClsTicketGenRequest.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                                    objClsTicketGenRequest.RowNo = Convert.ToInt32(lblRowNo.Text);
                                    objClsTicketGenRequest.TnStart = Convert.ToInt64(lblStartNo.Text);
                                    objClsTicketGenRequest.TnEnd = Convert.ToInt64(lblEndNo.Text);
                                    objClsTicketGenRequest.AlphabetSeries = lblRowFnAlphabet.Text;
                                    objClsTicketGenRequest.FnStart = Convert.ToInt32(lblRowFnStart.Text);
                                    objClsTicketGenRequest.FnEnd = Convert.ToInt32(lblRowFnEnd.Text);
                                    objClsTicketGenRequest.InsertedId = Convert.ToInt64(!string.IsNullOrWhiteSpace(lblInsertedId.Text.Trim()) ? lblInsertedId.Text.Trim() : "0");

                                    currentPointer += 1;
                                    var filepath = CreateAndGenerateTextFile(objClsTicketGenRequest, userObj, zipid, ((currentPointer * 100) / totalRecords), currentPointer, finalFilename);
                                    if (!string.IsNullOrWhiteSpace(filepath))
                                    {
                                        inputFiles.Add(filepath);
                                    }
                                }
                                catch (Exception efx)
                                {
                                    throw efx;
                                }
                            }
                            if (!Directory.Exists(Server.MapPath(string.Format("~/Downloads/merged/{0}/", userObj.UserId))))
                            {
                                Directory.CreateDirectory(Server.MapPath(string.Format("~/Downloads/merged/{0}/", userObj.UserId)));
                            }

                            /*if (File.Exists(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, txtReqCode.Text.Replace("/", "")))))
                            {
                                File.Delete(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, txtReqCode.Text.Replace("/", ""))));
                            }*/
                            if (File.Exists(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, finalFilename))))
                            {
                                File.Delete(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, finalFilename)));
                            }
                            
                            //using (var output = File.Create(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, txtReqCode.Text.Replace("/", "")))))
                            using (var output = File.Create(Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", userObj.UserId, finalFilename))))
                            {
                                StringBuilder sbl = new StringBuilder();
                                sbl.AppendLine("LotteryNo.,QRUrl");
                                var ln1 = new UTF8Encoding(true).GetBytes(sbl.ToString());
                                output.Write(ln1, 0, ln1.Length);
                                //foreach (var file in (ddlDownloadOrder.SelectedValue == "0" ? inputFiles.OrderByDescending(x => x.ToString()).ToList() : inputFiles))
                                inputFiles.Reverse();
                                foreach (var file in inputFiles)
                                {
                                    using (var input = File.OpenRead(file))
                                    {
                                        var buffer = new byte[chunkSize];
                                        int bytesRead;
                                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            output.Write(buffer, 0, bytesRead);
                                        }
                                    }
                                }
                            }
                        }
                        else
                            throw new Exception("No file found");
                    }
                    catch (Exception exc)
                    {
                        var isUpdated = objLtmsService.UpdateInZipProgress(new ZipProgressMaster() { ID = zipid, ZipProgress = -1 });
                        throw exc;
                    }
                });
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "CheckZipProgress(" + ZipID + ");", true);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        private string CreateAndFetchExcelFile(ClsTicketGenRequest objClsTicketGenRequest, ClsUserInfo userObj, long zipid, int percent)
        {
            string QRUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["QRUrl"].ToString() + "?JB=";
            StringBuilder strReport = new StringBuilder();
            StringBuilder strTableReport = new StringBuilder();
            string fullFilePath = string.Empty;

            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtInfo = objLtmsService.GetRequisitionDtlById(objClsTicketGenRequest.DataUniqueId);
            if (dtInfo.Rows.Count > 0)
            {
                objGeneratedNo.ID = Convert.ToInt64(hdnGenID.Value);
                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtInfo.Rows[0]["DataUniqueId"].ToString());
                objGeneratedNo.ReqDate = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"].ToString());
                objGeneratedNo.DrawNo = Convert.ToInt16(dtInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"].ToString());
                objGeneratedNo.FnStart = objClsTicketGenRequest.FnStart;
                objGeneratedNo.FnEnd = objClsTicketGenRequest.FnEnd;
                objGeneratedNo.AlphabetSeries = objClsTicketGenRequest.AlphabetSeries;
                objGeneratedNo.TnStart = objClsTicketGenRequest.TnStart;
                objGeneratedNo.TnEnd = objClsTicketGenRequest.TnEnd;
                objGeneratedNo.ReqCode = dtInfo.Rows[0]["ReqCode"].ToString();
                objGeneratedNo.StrReqDate = objGeneratedNo.ReqDate.Minute.ToString();
                Int64 FnStartRange = objClsTicketGenRequest.TnStart;
                Int64 FnEndRange = objClsTicketGenRequest.TnEnd;
                string FileName = objClsTicketGenRequest.RowNo.ToString() + "_DR" + objGeneratedNo.DrawNo + "_" + Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("ddMMMyyyy").ToUpper() + "_" + objGeneratedNo.ReqCode.Replace("/", "") + "_" + FnStartRange.ToString() + "_" + FnEndRange.ToString() + ".xlsx";
                int FnStart = objGeneratedNo.FnStart;
                int FnEnd = objGeneratedNo.FnEnd;
                string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                string[] values = AlphabetSeries.Split(',');
                Int64 TnStart = objGeneratedNo.TnStart;
                bool IsStartNoRequire = false;
                Int64 TnEnd = objGeneratedNo.TnEnd;
                FnEnd = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnEnd);
                FnStart = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnStart);
                IsStartNoRequire = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? false : true);

                string AlphabetChar = "", LotteryNumber = "";

                DataTable dt = new DataTable();
                dt.TableName = "dt";
                dt.Columns.Add(new DataColumn("LotteryNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("QRUrl", typeof(string)));
                DataRow dtRw = null;

                for (Int64 eCnt = FnEndRange; eCnt >= FnStartRange; eCnt--)
                {
                    for (int sCnt = FnEnd; sCnt >= FnStart; sCnt--)
                    {
                        for (int aCnt = values.Length - 1; aCnt >= 0; aCnt--)
                        {
                            AlphabetChar = values[aCnt].ToString().Trim();
                            dtRw = dt.NewRow();
                            LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            dtRw["LotteryNumber"] = LotteryNumber;
                            dtRw["QRUrl"] = QRUrl + GenQRNo(objGeneratedNo, LotteryNumber);
                            dt.Rows.Add(dtRw);
                        }
                    }
                }
                dt.AcceptChanges();

                string directoryPath = Server.MapPath(string.Format("~/Downloads/{0}/{1}/", userObj.UserId, objGeneratedNo.ReqCode.Replace("/", "")));
                fullFilePath = directoryPath + FileName;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
                byte[] excelFileStream = GenerateExcel2007(dt);
                FileStream fileStream = File.Create(fullFilePath);
                fileStream.Close();
                File.WriteAllBytes(fullFilePath, excelFileStream);
                /*using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
                {
                    File.WriteAllBytes(fullFilePath, excelFileStream);
                }*/

                var isUpdated = objLtmsService.UpdateInZipProgress(new ZipProgressMaster() { ID = zipid, ZipProgress = percent });
            }
            return fullFilePath;
        }

        private string CreateAndGenerateTextFile(ClsTicketGenRequest objClsTicketGenRequest, ClsUserInfo userObj, long zipid, int percent, int sequenceno = 0, string finalFilename = "")
        {
            string QRUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["QRUrl"].ToString() + "?JB=";
            StringBuilder strReport = new StringBuilder();
            StringBuilder strTableReport = new StringBuilder();
            string fullFilePath = string.Empty;

            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtInfo = objLtmsService.GetRequisitionDtlById(objClsTicketGenRequest.DataUniqueId);
            if (dtInfo.Rows.Count > 0)
            {
                objGeneratedNo.ID = Convert.ToInt64(hdnGenID.Value);
                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtInfo.Rows[0]["DataUniqueId"].ToString());
                objGeneratedNo.ReqDate = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"].ToString());
                objGeneratedNo.DrawNo = Convert.ToInt16(dtInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"].ToString());
                objGeneratedNo.FnStart = objClsTicketGenRequest.FnStart;
                objGeneratedNo.FnEnd = objClsTicketGenRequest.FnEnd;
                objGeneratedNo.AlphabetSeries = objClsTicketGenRequest.AlphabetSeries;
                objGeneratedNo.TnStart = objClsTicketGenRequest.TnStart;
                objGeneratedNo.TnEnd = objClsTicketGenRequest.TnEnd;
                objGeneratedNo.ReqCode = dtInfo.Rows[0]["ReqCode"].ToString();
                objGeneratedNo.StrReqDate = objGeneratedNo.ReqDate.Minute.ToString();
                Int64 FnStartRange = objClsTicketGenRequest.TnStart;
                Int64 FnEndRange = objClsTicketGenRequest.TnEnd;
                //string FileName = objClsTicketGenRequest.RowNo.ToString() + "_DR" + objGeneratedNo.DrawNo + "_" + Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("ddMMMyyyy").ToUpper() + "_" + objGeneratedNo.ReqCode.Replace("/", "") + "_" + FnStartRange.ToString() + "_" + FnEndRange.ToString() + ".txt";
                string FileName = string.Format("{0}.txt", sequenceno);
                int FnStart = objGeneratedNo.FnStart;
                int FnEnd = objGeneratedNo.FnEnd;
                string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                string[] values = AlphabetSeries.Split(',');
                Int64 TnStart = objGeneratedNo.TnStart;
                bool IsStartNoRequire = false;
                Int64 TnEnd = objGeneratedNo.TnEnd;
                FnEnd = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnEnd);
                FnStart = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnStart);
                IsStartNoRequire = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? false : true);

                string AlphabetChar = "", LotteryNumber = "";

                DataTable dt = new DataTable();
                dt.TableName = "dt";
                dt.Columns.Add(new DataColumn("LotteryNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("QRUrl", typeof(string)));
                DataRow dtRw = null;

                for (Int64 eCnt = FnEndRange; eCnt >= FnStartRange; eCnt--)
                {
                    for (int sCnt = FnEnd; sCnt >= FnStart; sCnt--)
                    {
                        for (int aCnt = values.Length - 1; aCnt >= 0; aCnt--)
                        {
                            AlphabetChar = values[aCnt].ToString().Trim();
                            dtRw = dt.NewRow();
                            LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + " " + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            dtRw["LotteryNumber"] = LotteryNumber;
                            dtRw["QRUrl"] = QRUrl + GenQRNo(objGeneratedNo, LotteryNumber.Replace(" ", ""));
                            dt.Rows.Add(dtRw);
                        }
                    }
                }
                dt.AcceptChanges();

                //string directoryPath = Server.MapPath(string.Format("~/Downloads/{0}/{1}/", userObj.UserId, objGeneratedNo.ReqCode.Replace("/", "")));
                string directoryPath = Server.MapPath(string.Format("~/Downloads/{0}/{1}/", userObj.UserId, finalFilename));
                fullFilePath = directoryPath + FileName;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
                byte[] textFileStream = GenerateText(dt);
                FileStream fileStream = File.Create(fullFilePath);
                fileStream.Close();
                File.WriteAllBytes(fullFilePath, textFileStream);
                /*using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
                {
                    File.WriteAllBytes(fullFilePath, textFileStream);
                }*/

                var isUpdated = objLtmsService.UpdateInZipProgress(new ZipProgressMaster() { ID = zipid, ZipProgress = percent });
            }
            return fullFilePath;
        }

        private string GenQRNo(ClsTicketInventory objGeneratedNo, string LotteryNo)
        {
            string QRCode = "", RQId = "", RQDate = "", GenLotteryNo = "", GenCheckDigit = "";
            CheckDigitCalculation objCheckDigitCalculation = new CheckDigitCalculation();
            RQId = "R_" + objGeneratedNo.DataUniqueId.ToString() + "~";
            RQDate = "M_" + objGeneratedNo.StrReqDate + ":";
            GenLotteryNo = "X_" + LotteryNo + "-";
            GenCheckDigit = "S_" + objCheckDigitCalculation.GenCheckDigit(objGeneratedNo.DataUniqueId.ToString() + objGeneratedNo.StrReqDate + LotteryNo) + "!";


            Random rnd = new Random();
            int IdPosition = rnd.Next(1, 3);
            if (IdPosition == 1)
            {
                QRCode = GenLotteryNo + RQDate + GenCheckDigit + RQId + "X";
            }
            else if (IdPosition == 2)
            {
                QRCode = RQId + GenCheckDigit + GenLotteryNo + "X" + RQDate;
            }
            else if (IdPosition == 3)
            {
                QRCode = GenCheckDigit + RQDate + "X" + RQId + GenLotteryNo;
            }

            if (objGeneratedNo.ID > default(long))
                return string.Format("{0}_{1}", QRCode, objGeneratedNo.ID.ToString());
            else
                return QRCode;

        }
        private byte[] GenerateExcel2007(DataTable p_dsSrc)
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                //Create the woorkbook
                ExcelWorkbook objWorkbook = objExcelPackage.Workbook;
                //Create the worksheet    
                ExcelWorksheet objWorksheet = objWorkbook.Worksheets.Add(p_dsSrc.TableName);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                objWorksheet.Cells["A1"].LoadFromDataTable(p_dsSrc, true);
                objWorksheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 11));
                //Add autoFilter to all columns
                objWorksheet.Cells[objWorksheet.Dimension.Address].AutoFilter = true;
                //AutoFit All Columns
                objWorksheet.Cells.AutoFitColumns();
                //Format the header
                using (ExcelRange objRange = objWorksheet.Cells[1, 1, 1, objWorksheet.Dimension.End.Column])
                {
                    objRange.Style.Font.Bold = true;
                    objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    objRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFCC66"));
                }
                return objExcelPackage.GetAsByteArray();
            }
        }

        private byte[] GenerateText(DataTable p_dsSrc)
        {
            StringBuilder sb = new StringBuilder();
            byte[] bytes = null;
            if (p_dsSrc != null && p_dsSrc.Rows.Count > default(int))
            {
                foreach (DataRow row in p_dsSrc.Rows)
                {
                    object[] array = row.ItemArray;
                    for (int i = 0; i < array.Length; i++)
                    {
                        sb.Append(array[i].ToString() + ((i < array.Length - 1) ? "," : ""));
                    }
                    sb.AppendLine();
                }
                using (var ms = new MemoryStream())
                {
                    TextWriter tw = new StreamWriter(ms);
                    tw.Write(sb.ToString());
                    tw.Flush();
                    ms.Position = 0;
                    bytes = ms.ToArray();
                }
            }

            return bytes;
        }
    }
}