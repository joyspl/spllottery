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
    public partial class TrxValidateTicket : System.Web.UI.Page
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

                    FillComboBox();

                }
            }
           
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType","");                
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            objValidateData.ClearAllInputField(pnlDataEntry);
            ddlLotteryName.Items.Clear();
            ddlLotteryName.Items.Add("<<--Select-->>"); 
           
        }    
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblValidTicket.Text = "";            
                if (IsValidEntry() == false) {return; }//Check for valid data entry
                lblValidTicket.Text = "<font color='Green'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is valid ticket</b></font>"; 

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
            if (ddlLotteryType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Type.');", true);
                ddlLotteryType.Focus();
                return false;
            }
            if (ddlLotteryName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Name.');", true);
                ddlLotteryName.Focus();
                return false;
            }
            
            if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == false && txtDrawNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw date or Draw no.');", true);
                txtDrawNo.Focus();
                return false;
            }
            if (txtDrawDate.Text.Trim() != "")
            {
                if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw date.');", true);
                    txtDrawDate.Focus();
                    return false;
                }
            }
            if (txtDrawNo.Text.Trim() != "")
            {
                if (objValidateData.IsIntegerWithZero(txtDrawNo.Text.Trim())==false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw No.');", true);
                    txtDrawNo.Focus();
                    return false;
                }
            }

            if (txtLotteryNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Lottery No.');", true);
                txtLotteryNo.Focus();
                return false;
            }
           
            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtLotteryInfo = new DataTable();

            Int64 InLotteryId=Convert.ToInt64(ddlLotteryName.SelectedValue);
            int? InDrawNo = string.IsNullOrEmpty(txtDrawNo.Text.Trim()) ? (int?)null : int.Parse(txtDrawNo.Text.Trim());  
            DateTime? InDrawDate = string.IsNullOrEmpty(txtDrawDate.Text.Trim()) ? (DateTime?)null : DateTime.Parse(txtDrawDate.Text.Trim());
            
            dtLotteryInfo = objLtmsService.GetLotteryDtlFromRequisitionDtl(InLotteryId, InDrawNo,  InDrawDate);
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

                string ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, txtLotteryNo.Text);
                if (ErrorMsg.Trim().Length > 0)
                {
                    lblValidTicket.Text = "<font color='red'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is Not valid ticket</b></font>";
                    var message = new JavaScriptSerializer().Serialize(ErrorMsg);
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                    txtLotteryNo.Focus();
                    return false;
                }

                int FnNo = 0;
                string Alphabet = "";
                Int64 TnNo = 0;


                string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                string[] values = AlphabetSeries.Split(',');
                int AlphabetLen = Convert.ToInt16(values[values.Length - 1].ToString().Length);

                FnNo = Convert.ToInt16(((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : txtLotteryNo.Text.Trim().Substring(0, objGeneratedNo.FnEnd.ToString().Length)));
                Alphabet = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? txtLotteryNo.Text.Trim().Substring(0, AlphabetLen) : txtLotteryNo.Text.Trim().Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                TnNo = Convert.ToInt64(txtLotteryNo.Text.Trim().Substring(txtLotteryNo.Text.Trim().Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length));

                DataSet dtLotteryDtl = objLtmsService.GetLotteryDtlInClaimAndUnSold(objGeneratedNo.DataUniqueId, txtLotteryNo.Text.Trim().ToUpper(), FnNo,Alphabet,TnNo);
                if (dtLotteryDtl != null) 
                { 
                    if (dtLotteryDtl.Tables[1].Rows.Count > 0)
                    {
                        lblValidTicket.Text = "<font color='red'><b>The Lottery Ticket No " + txtLotteryNo.Text.Trim() + " is not available for Prize.</b></font>";
                        return false;
                    }
                }
                DataSet dtLotteryWiningSerialNoDtl = objLtmsService.GetLotteryWiningSerialNoDtlByLotteryNo(objGeneratedNo.DataUniqueId, txtLotteryNo.Text.Trim());
                if (dtLotteryWiningSerialNoDtl.Tables[0].Rows.Count > 0)
                {
                    lblValidTicket.Text = "<font color='Green'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is available for " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["NameOfPrize"].ToString() + " and Prize Amount is " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["PrizeAmount"].ToString() + "</b></font>";
                    return false;
                }
                bool isFound = false;
                dtLotteryWiningSerialNoDtl = objLtmsService.GetLotteryWiningSerialNoDtlByLotteryNo(objGeneratedNo.DataUniqueId, "");
                if (dtLotteryWiningSerialNoDtl.Tables[1].Rows.Count > 0)
                {
                    for (int rwCnt = 0; rwCnt < dtLotteryWiningSerialNoDtl.Tables[1].Rows.Count; rwCnt++)
                    {
                        if (txtLotteryNo.Text.Trim().EndsWith(dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WiningSerialNo"].ToString()) == true)
                        {
                            isFound = true;
                            lblValidTicket.Text = "<font color='Green'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is available for " + dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["NameOfPrize"].ToString() + " and Prize Amount is " + dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["PrizeAmount"].ToString() + "</b></font>";
                            return false;
                        }
                    }                   
                }
                if (isFound == false)
                {
                    lblValidTicket.Text = "<font color='red'><b>The Lottery Ticket No " + txtLotteryNo.Text.Trim() + " is not a Prize Winning Ticket.</b></font>";
                   // ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid ticket no. The ticket no you entered is not available in any Wining Serial No.');", true);
                    return false;
                }
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid ticket no. The ticket no you entered is not correct for the date and draw no you specified');", true);
                return false;
            }
           // GetLotteryDtlFromRequisitionDtl(Int64 InLotteryId, int InDrawNo, DateTime InDrawDate)

            return true;
        }

        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlLotteryType.SelectedIndex == 0) { return;}

                objValidateData.FillDropDownList(ddlLotteryName, "LotteryNameByLotteryTypeID", ddlLotteryType.SelectedValue);
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