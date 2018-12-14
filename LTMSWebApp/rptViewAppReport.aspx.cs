using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class rptViewAppReport : System.Web.UI.Page
    {
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ViewReport(Session["ReportName"].ToString());
            }
        }
       protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("appNavigate.aspx?ID=" + ((clsInputParameter)Session["InputParameter"]).RequestUrl +"&UID=" + Guid.NewGuid().ToString());                      
        }

       public void ViewReport(string ReportName)
        {
            try
            {
                //ReportName
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(((clsInputParameter)Session["InputParameter"]).InFromDate);
                objInputParameter.InToDate = Convert.ToDateTime(((clsInputParameter)Session["InputParameter"]).InToDate);
                objInputParameter.InStatus = Convert.ToInt16(((clsInputParameter)Session["InputParameter"]).InStatus);                
                
                string DataSource = "",ReportPath="";
                DataTable dtReport = new DataTable();
                #region Fill the dataset
                switch (ReportName)
                {
                    case "DealerDepositViewDtl":                       
                        lblReport.Text = "Dealer Deposit Report for the period: " + Convert.ToDateTime(objInputParameter.InFromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(objInputParameter.InToDate).ToString("dd-MMM-yyyy");
                        dtReport = objLtmsService.GetDealerDepositViewDtl(objInputParameter);
                        DataSource = "dsDealerDepositViewDtl";
                        ReportPath = @"~/ReportTemplate/rptDealerDepositViewDtl.rdlc";
                        break;                        
                    case "DealerDepositReconByDirViewDtl":
                        lblReport.Text = "Dealer Deposit Reconcilation Report for the period: " + Convert.ToDateTime(objInputParameter.InFromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(objInputParameter.InToDate).ToString("dd-MMM-yyyy");
                        dtReport = objLtmsService.GetDealerDepositViewForApprovalDtl(objInputParameter,1);
                        DataSource = "dsDealerDepositViewDtl";
                        ReportPath = @"~/ReportTemplate/rptDealerDepositViewDtl.rdlc";
                        break;
                    case "DealerDepositReconBySplViewDtl":
                        lblReport.Text = "Dealer Deposit Reconcilation Report for the period: " + Convert.ToDateTime(objInputParameter.InFromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(objInputParameter.InToDate).ToString("dd-MMM-yyyy");
                        dtReport = objLtmsService.GetDealerDepositViewForApprovalDtl(objInputParameter, 2);
                        DataSource = "dsDealerDepositViewDtl";
                        ReportPath = @"~/ReportTemplate/rptDealerDepositViewDtl.rdlc";
                        break;
                     case "DealerDepositApprovalByDir":
                        lblReport.Text = "Dealer Deposit Report for the period: " + Convert.ToDateTime(objInputParameter.InFromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(objInputParameter.InToDate).ToString("dd-MMM-yyyy");
                        dtReport = objLtmsService.GetDealerDepositViewForApprovalDtl(objInputParameter,1);
                        DataSource = "dsDealerDepositViewDtl";
                        ReportPath = @"~/ReportTemplate/rptDealerDepositViewDtl.rdlc";
                        break;
                     case "DealerDepositApprovalBySpl":
                        lblReport.Text = "Dealer Deposit Report for the period: " + Convert.ToDateTime(objInputParameter.InFromDate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(objInputParameter.InToDate).ToString("dd-MMM-yyyy");
                        dtReport = objLtmsService.GetDealerDepositViewForApprovalDtl(objInputParameter, 2);
                        DataSource = "dsDealerDepositViewDtl";
                        ReportPath = @"~/ReportTemplate/rptDealerDepositViewDtl.rdlc";
                        break;  
                     case "RequisitionViewDtl":
                        lblReport.Text = "Requisition Report";
                        dtReport = objLtmsService.GetRequisitionDtl(objInputParameter);
                        DataSource = "dsRequisitionViewDtl";
                        ReportPath = @"~/ReportTemplate/rptRequisitionViewDtl.rdlc";
                        break;
                     case "RequisitionApprovalDtl":
                        lblReport.Text = "Requisition Report";
                        dtReport = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
                        DataSource = "dsRequisitionViewDtl";
                        ReportPath = @"~/ReportTemplate/rptRequisitionApprovalViewDtl.rdlc";
                        break;   

                    case "LotteryType":
                        lblReport.Text = "Lottery Type Master Report";
                        dtReport = objLtmsService.GetLotteryTypeDtl();
                        DataSource = "dsMstLotteryType";
                        ReportPath = @"~/ReportTemplate/rptLotteryType.rdlc";
                        break;
                    case "Lottery":
                        lblReport.Text = "Lottery  Master Report";
                        dtReport = objLtmsService.GetLotteryDtl();
                        DataSource = "dsLottery";
                        ReportPath = @"~/ReportTemplate/rptLottery.rdlc";
                        break;
                    case "Prize":
                        lblReport.Text = "Prize  Master Report";
                        dtReport = objLtmsService.GetPrizeDtl();
                        DataSource = "dsPrizeMaster";
                        ReportPath = @"~/ReportTemplate/rptPrizeMaster.rdlc";
                        break;
                    case "WinneListDtlById":
                        lblReport.Text = "Winne List Details";
                        dtReport = objLtmsService.GetWinneListDtlById(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId) );
                        DataSource = "dsWinneListDtl";
                        ReportPath = @"~/ReportTemplate/rptWinneListDtl.rdlc";
                        break;
                    case "UnSoldSummary":
                        lblReport.Text = "Un-Sold Summary";
                        dtReport = objLtmsService.GetUnSoldSummaryById(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsUnSoldSummary";
                        ReportPath = @"~/ReportTemplate/rptUnSoldSummary.rdlc";
                        break;
                    case "SendToGovAnnextureIII":
                        lblReport.Text = "Send To Gov. Annexture-III";
                        dtReport = objLtmsService.GetSendToGovAnnextureIIIByID(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsSendToGovAnnextureIII";
                        ReportPath = @"~/ReportTemplate/rptSendToGovAnnextureIII.rdlc";
                        break;
                    case "VariableClaimPrize":
                        lblReport.Text = "Variable Claim Prize";
                        dtReport = objLtmsService.GetVariableClaimByClaimId(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsVariableClaimPrize";
                        ReportPath = @"~/ReportTemplate/rptVariableClaimPrize.rdlc";
                        break;
                    case "UnsoldPrize":
                        lblReport.Text = "Unsold Prize";
                        dtReport = objLtmsService.GetLotteryUnsold(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "DsUnsoldReport";
                        ReportPath = @"~/ReportTemplate/RptUnsoldReport.rdlc";
                        break;
                    case "VariableClaimSuper":
                        lblReport.Text = "VariableClaimSuper";
                        dtReport = objLtmsService.GetVariableIncentiveByReqId(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId), "SUPER");
                        DataSource = "dsVariableClaimSuper";
                        ReportPath = @"~/ReportTemplate/rptVariableClaimSuper.rdlc";
                        break;
                    case "VariableClaimSpecial":
                        lblReport.Text = "VariableClaimSpecial";
                        dtReport = objLtmsService.GetVariableIncentiveByReqId(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId), "SPECIAL");
                        DataSource = "dsVariableClaimSpecial";
                        ReportPath = @"~/ReportTemplate/rptVariableClaimSpecial.rdlc";
                        break;
                    case "PrintOrder":
                        lblReport.Text = "PrintOrder";
                        dtReport = objLtmsService.GetPrintOrderDtlById(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsPrintingOrder";
                        ReportPath = @"~/ReportTemplate/rptPrintOrder.rdlc";
                        break;
                    case "TicketInventoryAdjustment":
                        lblReport.Text = "Ticket Inventory Adjustment";
                        dtReport = objLtmsService.GetDealerListOfTransactionDtl();
                        DataSource = "dsTicketInventoryAdjustment";
                        ReportPath = @"~/ReportTemplate/rptTicketInventoryAdjustment.rdlc";
                        break;
                    case "UnsoldPrizeDistributor":
                        lblReport.Text = "Unsold Prize Distributor";
                        dtReport = objLtmsService.GetLotteryUnsold(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "DsUnsoldReport";
                        ReportPath = @"~/ReportTemplate/RptUnsoldReportfrormDistributor.rdlc";
                        break;
                    case "UnsoldPrizeRemain":
                        lblReport.Text = "Unsold Prize Remain";
                        dtReport = objLtmsService.GetLotteryUnsold(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "DsUnsoldReport";
                        ReportPath = @"~/ReportTemplate/RptUnsoldReportRemain.rdlc";
                        break;
                    case "ProfitLossDtlByReqId":
                        lblReport.Text = "Ticket Inventory Profit And Loss";
                        dtReport = objLtmsService.GetProfitLossDtlByReqId(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsTicketInventoryAdjustment";
                        ReportPath = @"~/ReportTemplate/rptProfitLossDtlByReqId.rdlc";
                        break;

                    case "BankGuranteeLedger":
                        lblReport.Text = "Bank Gurantee Ledger";
                        dtReport = objLtmsService.GetBankGuranteeLedger(objInputParameter);
                        DataSource = "dsBankGuranteeLedger";
                        ReportPath = @"~/ReportTemplate/rptBankGuranteeLedger.rdlc";
                        break;
                    case "DrawwisePrintingCostOfLotteryTicket":
                        lblReport.Text = "Drawwise Printing Cost of Lottery Ticket";
                        dtReport = objLtmsService.GetPrintOrderDtlById(Convert.ToInt64(((clsInputParameter)Session["InputParameter"]).DataUniqueId));
                        DataSource = "dsDrawwisePrintingCostOfLotteryTicket";
                        ReportPath = @"~/ReportTemplate/rptDrawwisePrintingCostOfLotteryTicket.rdlc";
                        break;
                    case "DrawwiseSalesReport":
                        lblReport.Text = "Draw wise Sales Report";
                        dtReport = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
                        DataSource = "dsReqDtl";
                        ReportPath = @"~/ReportTemplate/rptDrawWiseSalesReport.rdlc";
                        break;
                        
                    default:
                        dtReport = null;
                        break;
                }
                #endregion
                rptViewer.Visible = true;
                rptViewer.Style.Add("width", "99%");
                rptViewer.Height = (Unit)480;
                rptViewer.BorderStyle = BorderStyle.Solid;
                rptViewer.BorderColor = System.Drawing.Color.Gray;
                rptViewer.BorderWidth = (Unit)1;
                rptViewer.ShowRefreshButton = false;
                rptViewer.ShowFindControls = false;
                rptViewer.ShowPrintButton = true;
                rptViewer.SizeToReportContent = false;
                rptViewer.ShowExportControls = true;
                rptViewer.AsyncRendering = true;
                rptViewer.Reset();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.DataSources.Clear();

                this.rptViewer.LocalReport.ReportPath = Server.MapPath(ReportPath);
                ReportDataSource rds = new ReportDataSource(DataSource, dtReport);

                if (ReportName == "WinneListDtlById" && dtReport.Rows.Count > 0)
                {
                    ReportParameter Param0 = new ReportParameter("DrawNo",objValidateData.ToOrdinal(Convert.ToInt16(dtReport.Rows[0]["DrawNo"].ToString())));
                    ReportParameter Param1 = new ReportParameter("DrawDate", Convert.ToDateTime(dtReport.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy"));
                    ReportParameter Param2 = new ReportParameter("LotteryName", dtReport.Rows[0]["LotteryName"].ToString());
                    rptViewer.LocalReport.SetParameters(new ReportParameter[] { Param0, Param1, Param2 });
                }
                this.rptViewer.LocalReport.DataSources.Clear();
                this.rptViewer.LocalReport.DataSources.Add(rds);
                this.rptViewer.DataBind();
                this.rptViewer.LocalReport.Refresh();
                if (ReportName == "WinneListDtlById" || 
                    ReportName == "UnSoldSummary" || 
                    ReportName == "SendToGovAnnextureIII" || 
                   // ReportName=="VariableClaimPrize" ||
                    ReportName == "TicketInventoryAdjustment" ||
                    ReportName == "ProfitLossDtlByReqId"
                    )
                {
                    byte[] Bytes = this.rptViewer.LocalReport.Render(format: "PDF", deviceInfo: "");
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    string ReportFileName = ReportName;
                    if (ReportName == "WinneListDtlById" || ReportName == "UnSoldSummary" || ReportName == "VariableClaimPrize")
                    {
                        ReportFileName = ReportFileName + "_DrNo_" + dtReport.Rows[0]["DrawNo"].ToString() + "_DrDt_" + Convert.ToDateTime(dtReport.Rows[0]["DrawDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else if (ReportName == "SendToGovAnnextureIII")
                    {
                        ReportFileName = ReportFileName + "_" + dtReport.Rows[0]["SentToGovReqCode"].ToString().Replace('/','_');
                    }
                    //SentToGovReqCode
                    byte[] bytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename= " + ReportFileName + ".pdf");
                    Response.BinaryWrite(bytes); // create the file    
                    Response.Flush();
                }

            }
            catch (Exception Ex)
            {
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

    }
}