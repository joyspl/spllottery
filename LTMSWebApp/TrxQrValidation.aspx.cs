using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxQrValidation : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["JB"]))
                {
                    string QueryString = Request.QueryString["JB"];
                    string LotteryNo = "", ReqId = "", ErrorMsg = "", RQDate = "", GenCheckDigit = "";
                    string UID = string.Empty;
                    long uidl = default(long);

                    int startIndex = QueryString.IndexOf("X_") + "X_".Length;
                    int endIndex = QueryString.IndexOf("-");
                    LotteryNo = QueryString.Substring(startIndex, endIndex - startIndex);

                    UID = !string.IsNullOrWhiteSpace(QueryString) ? (QueryString.Split('_').LastOrDefault() != null ? QueryString.Split('_').LastOrDefault() : default(long).ToString()) : default(long).ToString();

                    startIndex = QueryString.IndexOf("R_") + "R_".Length;
                    endIndex = QueryString.IndexOf("~");
                    ReqId = QueryString.Substring(startIndex, endIndex - startIndex);

                    startIndex = QueryString.IndexOf("M_") + "M_".Length;
                    endIndex = QueryString.IndexOf(":");
                    RQDate = QueryString.Substring(startIndex, endIndex - startIndex);

                    startIndex = QueryString.IndexOf("S_") + "S_".Length;
                    endIndex = QueryString.IndexOf("!");
                    GenCheckDigit = QueryString.Substring(startIndex, endIndex - startIndex);

                    CheckDigitCalculation objCheckDigitCalculation = new CheckDigitCalculation();
                    string CheckDigit = string.Empty;
                    try
                    {
                        CheckDigit = objCheckDigitCalculation.GenCheckDigit(ReqId + RQDate + LotteryNo);
                    }
                    catch (Exception)
                    {
                        lblMessage.Text = " <font color='red'>Incorrect QR code</font>"; ;
                        return;
                    }

                    if (CheckDigit != GenCheckDigit)
                    {
                        lblMessage.Text = " <font color='red'>" + LotteryNo + " Is Not Valid Ticket No</font>"; ;
                        return;
                    }

                    objValidateData.ClearAllInputField(pnlDataEntry);
                    ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
                    DataTable dtLotteryInfo = new DataTable();
                    dtLotteryInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt32(ReqId));
                    long sr1End = default(long);
                    try
                    {
                        long.TryParse(dtLotteryInfo.Rows[0]["FnEnd"].ToString(), out sr1End);
                    }
                    catch (Exception) { }
                    DataTable newMultiTableData = objLtmsService.GetSeriesGenerationByReqIdAndSr1End(Convert.ToInt32(ReqId), sr1End);

                    long.TryParse(UID, out uidl);
                    DataTable newTableData = new DataTable();
                    if (uidl > default(long))
                        newTableData = objLtmsService.GetSeriesGenerationByReqIdSpecific(Convert.ToInt32(ReqId), uidl);
                    else
                        newTableData = objLtmsService.GetSeriesGenerationByReqId(Convert.ToInt32(ReqId));

                    if (dtLotteryInfo.Rows.Count > 0)
                    {
                        objGeneratedNo.DataUniqueId = Convert.ToInt64(dtLotteryInfo.Rows[0]["DataUniqueId"].ToString());
                        objGeneratedNo.DrawNo = Convert.ToInt16(dtLotteryInfo.Rows[0]["DrawNo"].ToString());
                        objGeneratedNo.DrawDate = Convert.ToDateTime(dtLotteryInfo.Rows[0]["DrawDate"].ToString());
                        objGeneratedNo.AlphabetSeries = dtLotteryInfo.Rows[0]["AlphabetSeries"].ToString();

                        if (newTableData.Rows.Count > default(int))
                        {
                            objGeneratedNo.FnStart = Convert.ToInt16(newTableData.Rows[0]["Series1Start"].ToString());
                            objGeneratedNo.FnEnd = Convert.ToInt16(newTableData.Rows[0]["Series1End"].ToString());
                            objGeneratedNo.TnStart = Convert.ToInt64(newTableData.Rows[0]["NumStart"].ToString());
                            objGeneratedNo.TnEnd = Convert.ToInt64(newTableData.Rows[0]["NumEnd"].ToString());
                        }
                        else
                        {
                            objGeneratedNo.FnStart = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnStart"].ToString());
                            objGeneratedNo.FnEnd = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnEnd"].ToString());
                            objGeneratedNo.TnStart = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnStart"].ToString());
                            objGeneratedNo.TnEnd = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnEnd"].ToString());
                        }


                        ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, LotteryNo, newMultiTableData);
                        if (ErrorMsg.Trim().Length > 0)
                        {
                            lblMessage.Text = " <font color='red'>" + LotteryNo + " Is Not Valid Ticket No</font>"; ;
                        }
                        else
                        {

                            txtLotteryType.Text = dtLotteryInfo.Rows[0]["LotteryType"].ToString();
                            txtLotteryName.Text = !string.IsNullOrWhiteSpace(dtLotteryInfo.Rows[0]["ModifiedLotteryName"].ToString()) ? dtLotteryInfo.Rows[0]["ModifiedLotteryName"].ToString() : dtLotteryInfo.Rows[0]["LotteryName"].ToString();
                            txtDrawNo.Text = dtLotteryInfo.Rows[0]["DrawNo"].ToString();
                            txtDrawDate.Text = Convert.ToDateTime(dtLotteryInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                            txtLotteryNo.Text = LotteryNo;
                            lblMessage.Text = " <font color='green'>" + LotteryNo + " Is Valid Ticket No</font>"; ;

                        }
                    }
                }
            }
            catch(Exception ex) {
                lblMessage.Text = "Some Error occured. Please contact system administrator."; ;
            }

        }
       
    }
}