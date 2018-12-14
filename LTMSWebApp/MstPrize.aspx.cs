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
    public partial class MstPrize : System.Web.UI.Page
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
                items.Add(new ListItem("<<--All-->>", "-1"));
                items.Add(new ListItem("Pending", "0"));
                items.Add(new ListItem("Draft", "1"));
                items.Add(new ListItem("Confirm", "2"));
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
                // string AlphabetSeries = "";
               
                double PrizeAmount = 0;
                double AdministrativeChargePercentage = 0;
                double AdministrativeCharge = 0;
                double GrossPrizeAmount = 0;
                double TaxDeductionPercentage = 0;
                double DeductionOfIT = 0;
                double PayableToWinner = 0;
                double SpecialTicketAmount = 0;
                double SuperTicketAmount = 0;
                DataTable dtPrizeDtl = new DataTable();
                dtPrizeDtl.TableName = "PrizeDetails";
                DataRow dr = null;
                dtPrizeDtl.Columns.Add(new DataColumn("RowNo", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("NameOfPrize", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("PrizeAmount", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("AdministrativeChargePercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("AdministrativeCharge", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("GrossPrizeAmount", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("TaxDeductionPercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("DeductionOfIT", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("PayableToWinner", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAmount", typeof(string)));                
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAdministrativeChargePercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAdministrativeCharge", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketGrossPrizeAmount", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketTaxDeductionPercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketDeductionOfIT", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("PayableToSpecialTicketWinner", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAmount", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAdministrativeChargePercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAdministrativeCharge", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketGrossPrizeAmount", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketTaxDeductionPercentage", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketDeductionOfIT", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("PayableToSuperTicketWinner", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("NoOfWinner", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("NoOfDigitInStatic", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("FixedOrVariable", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("ValidationForUnsold", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("Description1", typeof(string)));
                dtPrizeDtl.Columns.Add(new DataColumn("Description2", typeof(string)));
                
                for (int iCtr = 0; iCtr < gvPrizeDetails.Rows.Count; iCtr++)
                {
                    TextBox txtNameOfPrize = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNameOfPrize");
                    TextBox txtPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPrizeAmount");
                    TextBox txtAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtAdministrativeChargePercentage");
                    TextBox txtAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtAdministrativeCharge");
                    TextBox txtGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtGrossPrizeAmount");
                    TextBox txtTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtTaxDeductionPercentage");
                    TextBox txtDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtDeductionOfIT");
                    TextBox txtPayableToWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToWinner");

                    TextBox txtSpecialTicketAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAmount");                   
                    TextBox txtSpecialTicketAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAdministrativeChargePercentage");
                    TextBox txtSpecialTicketAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAdministrativeCharge");
                    TextBox txtSpecialTicketGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketGrossPrizeAmount");
                    TextBox txtSpecialTicketTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketTaxDeductionPercentage");
                    TextBox txtSpecialTicketDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketDeductionOfIT");
                    TextBox txtPayableToSpecialTicketWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToSpecialTicketWinner");
                    
                    TextBox txtSuperTicketAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAmount");
                    TextBox txtSuperTicketAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAdministrativeChargePercentage");
                    TextBox txtSuperTicketAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAdministrativeCharge");
                    TextBox txtSuperTicketGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketGrossPrizeAmount");
                    TextBox txtSuperTicketTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketTaxDeductionPercentage");
                    TextBox txtSuperTicketDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketDeductionOfIT");
                    TextBox txtPayableToSuperTicketWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToSuperTicketWinner");
                    
                    TextBox txtNoOfWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNoOfWinner");
                    TextBox txtNoOfDigitInStatic = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNoOfDigitInStatic");

                    CheckBox chkFixedOrVariable = (CheckBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("chkFixedOrVariable");
                    CheckBox chkValidationForUnsold = (CheckBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("chkValidationForUnsold");
                   
                    TextBox txtDescription1 = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtDescription1");
                    TextBox txtDescription2 = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtDescription2");



                    PrizeAmount = 0;
                    AdministrativeChargePercentage = 0;
                    AdministrativeCharge = 0;
                    GrossPrizeAmount = 0;
                    TaxDeductionPercentage = 0;
                    DeductionOfIT = 0;
                    PayableToWinner = 0;

                    PrizeAmount = (txtPrizeAmount.Text == "" ? 0 : Convert.ToDouble(txtPrizeAmount.Text));
                    AdministrativeChargePercentage = (txtAdministrativeChargePercentage.Text == "" ? 0 : Convert.ToDouble(txtAdministrativeChargePercentage.Text.Trim()));
                    AdministrativeCharge = PrizeAmount * (AdministrativeChargePercentage / 100);
                    txtAdministrativeCharge.Text = String.Format("{0:0.0#}", AdministrativeCharge);
                    GrossPrizeAmount = (PrizeAmount - AdministrativeCharge);
                    txtGrossPrizeAmount.Text = String.Format("{0:0.0#}", GrossPrizeAmount);
                    TaxDeductionPercentage = (txtTaxDeductionPercentage.Text == "" ? 0 : Convert.ToDouble(txtTaxDeductionPercentage.Text.Trim()));
                    DeductionOfIT = GrossPrizeAmount * (TaxDeductionPercentage / 100);
                    txtDeductionOfIT.Text = String.Format("{0:0.0#}",DeductionOfIT);
                    PayableToWinner = (GrossPrizeAmount - DeductionOfIT);                   
                    txtPayableToWinner.Text = String.Format("{0:0.0#}",PayableToWinner);

                    PrizeAmount = 0;
                    AdministrativeChargePercentage = 0;
                    AdministrativeCharge = 0;
                    GrossPrizeAmount = 0;
                    TaxDeductionPercentage = 0;
                    DeductionOfIT = 0;
                    PayableToWinner = 0;

                    PrizeAmount = (txtSpecialTicketAmount.Text == "" ? 0 : Convert.ToDouble(txtSpecialTicketAmount.Text));
                    AdministrativeChargePercentage = (txtSpecialTicketAdministrativeChargePercentage.Text == "" ? 0 : Convert.ToDouble(txtSpecialTicketAdministrativeChargePercentage.Text.Trim()));
                    AdministrativeCharge = PrizeAmount * (AdministrativeChargePercentage / 100);
                    txtSpecialTicketAdministrativeCharge.Text = String.Format("{0:0.0#}", AdministrativeCharge);
                    GrossPrizeAmount = (PrizeAmount - AdministrativeCharge);
                    txtSpecialTicketGrossPrizeAmount.Text = String.Format("{0:0.0#}", GrossPrizeAmount);
                    TaxDeductionPercentage = (txtSpecialTicketTaxDeductionPercentage.Text == "" ? 0 : Convert.ToDouble(txtSpecialTicketTaxDeductionPercentage.Text.Trim()));
                    DeductionOfIT = GrossPrizeAmount * (TaxDeductionPercentage / 100);
                    txtSpecialTicketDeductionOfIT.Text = String.Format("{0:0.0#}", DeductionOfIT);
                    PayableToWinner = (GrossPrizeAmount - DeductionOfIT);
                    txtPayableToSpecialTicketWinner.Text = String.Format("{0:0.0#}", PayableToWinner);

                    PrizeAmount = 0;
                    AdministrativeChargePercentage = 0;
                    AdministrativeCharge = 0;
                    GrossPrizeAmount = 0;
                    TaxDeductionPercentage = 0;
                    DeductionOfIT = 0;
                    PayableToWinner = 0;

                    PrizeAmount = (txtSuperTicketAmount.Text == "" ? 0 : Convert.ToDouble(txtSuperTicketAmount.Text));
                    AdministrativeChargePercentage = (txtSuperTicketAdministrativeChargePercentage.Text == "" ? 0 : Convert.ToDouble(txtSuperTicketAdministrativeChargePercentage.Text.Trim()));
                    AdministrativeCharge = PrizeAmount * (AdministrativeChargePercentage / 100);
                    txtSuperTicketAdministrativeCharge.Text = String.Format("{0:0.0#}", AdministrativeCharge);
                    GrossPrizeAmount = (PrizeAmount - AdministrativeCharge);
                    txtSuperTicketGrossPrizeAmount.Text = String.Format("{0:0.0#}", GrossPrizeAmount);
                    TaxDeductionPercentage = (txtSuperTicketTaxDeductionPercentage.Text == "" ? 0 : Convert.ToDouble(txtSuperTicketTaxDeductionPercentage.Text.Trim()));
                    DeductionOfIT = GrossPrizeAmount * (TaxDeductionPercentage / 100);
                    txtSuperTicketDeductionOfIT.Text = String.Format("{0:0.0#}", DeductionOfIT);
                    PayableToWinner = (GrossPrizeAmount - DeductionOfIT);
                    txtPayableToSuperTicketWinner.Text = String.Format("{0:0.0#}", PayableToWinner);

                    dr = dtPrizeDtl.NewRow();
                    dr["RowNo"] = (iCtr+1).ToString().Trim();
                    dr["NameOfPrize"] = txtNameOfPrize.Text.Trim();
                    dr["PrizeAmount"] = txtPrizeAmount.Text.Trim();
                    dr["AdministrativeChargePercentage"] = txtAdministrativeChargePercentage.Text.Trim();
                    dr["AdministrativeCharge"] = txtAdministrativeCharge.Text.Trim();
                    dr["GrossPrizeAmount"] = txtGrossPrizeAmount.Text.Trim();
                    dr["TaxDeductionPercentage"] = txtTaxDeductionPercentage.Text.Trim();
                    dr["DeductionOfIT"] = txtDeductionOfIT.Text.Trim();
                    dr["PayableToWinner"] = txtPayableToWinner.Text.Trim();
                    dr["SpecialTicketAmount"] = txtSpecialTicketAmount.Text;
                    dr["SpecialTicketAdministrativeChargePercentage"] = txtSpecialTicketAdministrativeChargePercentage.Text;
                    dr["SpecialTicketAdministrativeCharge"] = txtSpecialTicketAdministrativeCharge.Text;
                    dr["SpecialTicketGrossPrizeAmount"] = txtSpecialTicketGrossPrizeAmount.Text;
                    dr["SpecialTicketTaxDeductionPercentage"] = txtSpecialTicketTaxDeductionPercentage.Text;
                    dr["SpecialTicketDeductionOfIT"] = txtSpecialTicketDeductionOfIT.Text;
                    dr["PayableToSpecialTicketWinner"] = txtPayableToSpecialTicketWinner.Text;                  
                    dr["SuperTicketAmount"] = txtSuperTicketAmount.Text;
                    dr["SuperTicketAdministrativeChargePercentage"] = txtSuperTicketAdministrativeChargePercentage.Text;
                    dr["SuperTicketAdministrativeCharge"] = txtSuperTicketAdministrativeCharge.Text;
                    dr["SuperTicketGrossPrizeAmount"] = txtSuperTicketGrossPrizeAmount.Text;
                    dr["SuperTicketTaxDeductionPercentage"] = txtSuperTicketTaxDeductionPercentage.Text;
                    dr["SuperTicketDeductionOfIT"] = txtSuperTicketDeductionOfIT.Text;
                    dr["PayableToSuperTicketWinner"] = txtPayableToSuperTicketWinner.Text;
                    dr["NoOfWinner"] = txtNoOfWinner.Text.Trim();
                    dr["NoOfDigitInStatic"] = txtNoOfDigitInStatic.Text.Trim();
                    dr["FixedOrVariable"] = (chkFixedOrVariable.Checked==true?"Y":"N");
                    dr["ValidationForUnsold"] = (chkValidationForUnsold.Checked == true ? "Y" : "N");
                    dr["Description1"] = txtDescription1.Text.Trim();
                    dr["Description2"] = txtDescription2.Text.Trim();

                    dtPrizeDtl.Rows.Add(dr);
                }
                if (IsValidEntry() == false) { return; }//Check for valid data entry

                objPrize.DataUniqueId = Convert.ToInt16(hdUniqueId.Value.ToString().Trim());
                objPrize.ClaimDays = Convert.ToInt16(txtClaimDays.Text);
                objPrize.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objPrize.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objPrize.SaveStatus = 1;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objPrize.SaveStatus = 2;
                }
                if (hdUniqueId.Value.ToString().Trim() != "" && hdIsUpdate.Value.ToString().Trim()=="")
                {
                   
                    bool IsAdded = objLtmsService.InsertInPrize(objPrize, dtPrizeDtl);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Prize Type information Saved successfully.');", true);
                    }
                }
                else
                {                    
                   // objPrize.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInPrize(objPrize, dtPrizeDtl);
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

            int CountofFixedOrVariableChecked = 0;
            bool IsError = false;           
            string ErrorMsg = "";
            for (int iCtr = 0; iCtr < gvPrizeDetails.Rows.Count; iCtr++)
            {
                TextBox txtNameOfPrize = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNameOfPrize");
                TextBox txtPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPrizeAmount");
                TextBox txtAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtAdministrativeChargePercentage");
                TextBox txtAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtAdministrativeCharge");
                TextBox txtGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtGrossPrizeAmount");
                TextBox txtTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtTaxDeductionPercentage");
                TextBox txtDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtDeductionOfIT");
                TextBox txtPayableToWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToWinner");


                TextBox txtSpecialTicketAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAmount");
                TextBox txtSpecialTicketAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAdministrativeChargePercentage");
                TextBox txtSpecialTicketAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketAdministrativeCharge");
                TextBox txtSpecialTicketGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketGrossPrizeAmount");
                TextBox txtSpecialTicketTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketTaxDeductionPercentage");
                TextBox txtSpecialTicketDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSpecialTicketDeductionOfIT");
                TextBox txtPayableToSpecialTicketWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToSpecialTicketWinner");


                TextBox txtSuperTicketAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAmount");
                TextBox txtSuperTicketAdministrativeChargePercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAdministrativeChargePercentage");
                TextBox txtSuperTicketAdministrativeCharge = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketAdministrativeCharge");
                TextBox txtSuperTicketGrossPrizeAmount = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketGrossPrizeAmount");
                TextBox txtSuperTicketTaxDeductionPercentage = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketTaxDeductionPercentage");
                TextBox txtSuperTicketDeductionOfIT = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtSuperTicketDeductionOfIT");
                TextBox txtPayableToSuperTicketWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtPayableToSuperTicketWinner");


                TextBox txtNoOfWinner = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNoOfWinner");
                TextBox txtNoOfDigitInStatic = (TextBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("txtNoOfDigitInStatic");
                CheckBox chkFixedOrVariable = (CheckBox)gvPrizeDetails.Rows[iCtr].Cells[0].FindControl("chkFixedOrVariable");

                if (chkFixedOrVariable.Checked == true)
                {
                    CountofFixedOrVariableChecked += 1;
                }

                if (txtNameOfPrize.Text.Trim() == "")
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Prize name can not be balnk in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDouble(txtPrizeAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Prize amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtAdministrativeChargePercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Administrative Charges % should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtAdministrativeCharge.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Administrative Charges should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDouble(txtGrossPrizeAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Gross Prize amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtTaxDeductionPercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Tax Deduction Percentage should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtDeductionOfIT.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Deduction of IT should be numeric in row no. " + (iCtr + 1);
                }
               
                if (objValidateData.IsDouble(txtPayableToWinner.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Payable To Winner should be numeric in row no. " + (iCtr + 1);
                }                


                if (objValidateData.IsDoubleWithZero(txtSpecialTicketAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSpecialTicketAdministrativeChargePercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Administrative Charges % should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSpecialTicketAdministrativeCharge.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket  Administrative Charges should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSpecialTicketGrossPrizeAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Gross Prize amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSpecialTicketTaxDeductionPercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Tax Deduction Percentage should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSpecialTicketDeductionOfIT.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Deduction of IT should be numeric in row no. " + (iCtr + 1);
                }

                if (objValidateData.IsDoubleWithZero(txtPayableToSpecialTicketWinner.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Special Ticket Payable To Winner should be numeric in row no. " + (iCtr + 1);
                }
                                
                
                
                if (objValidateData.IsDoubleWithZero(txtSuperTicketAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSuperTicketAdministrativeChargePercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Administrative Charges % should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSuperTicketAdministrativeCharge.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket  Administrative Charges should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSuperTicketGrossPrizeAmount.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Gross Prize amount should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSuperTicketTaxDeductionPercentage.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Tax Deduction Percentage should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsDoubleWithZero(txtSuperTicketDeductionOfIT.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Deduction of IT should be numeric in row no. " + (iCtr + 1);
                }

                if (objValidateData.IsDoubleWithZero(txtPayableToSuperTicketWinner.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "Super Ticket Payable To Winner should be numeric in row no. " + (iCtr + 1);
                }
                
                if (objValidateData.IsInteger(txtNoOfWinner.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "No of Winner should be numeric in row no. " + (iCtr + 1);
                }
                if (objValidateData.IsInteger(txtNoOfDigitInStatic.Text.Trim()) == false)
                {
                    IsError = true;
                    ErrorMsg = (ErrorMsg != "" ? ErrorMsg + "\n" : "") + "No of No of Digit In Static should be numeric in row no. " + (iCtr + 1);
                }
                
            }
            if (IsError == true)
            {
                var message = new JavaScriptSerializer().Serialize("Some error in prize details table as below.\n'" + ErrorMsg);
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                // ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Some error in prize details table as below.\n'"+ ErrorMsg +");", true);
                return false;
            }
            if (CountofFixedOrVariableChecked == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select atleast one Fixed prize from the list');", true);
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
                    hdIsUpdate.Value = "";
                                   
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    int PrizeCategoryCnt = 0;
                    int Status = 0;
                    dtInfo = objLtmsService.GetPrizeDtlById(Convert.ToInt64(hdUniqueId.Value));
                    btnSave.Visible = false;
                    btnConfirm.Visible = false;
                    if (dtInfo.Tables[0].Rows.Count > 0)
                    {
                        txtUnSoldPercentage.Text = dtInfo.Tables[0].Rows[0]["UnSoldPercentage"].ToString();
                        txtGovermentOrder.Text = dtInfo.Tables[0].Rows[0]["GovermentOrder"].ToString();
                        txtLotteryType.Text = dtInfo.Tables[0].Rows[0]["LotteryType"].ToString();
                        txtLotteryName.Text = !string.IsNullOrWhiteSpace(dtInfo.Tables[0].Rows[0]["ModifiedLotteryName"].ToString()) ? dtInfo.Tables[0].Rows[0]["ModifiedLotteryName"].ToString() : dtInfo.Tables[0].Rows[0]["LotteryName"].ToString();
                        txtClaimDays.Text = dtInfo.Tables[0].Rows[0]["ClaimDays"].ToString();
                        PrizeCategoryCnt = Convert.ToInt16(dtInfo.Tables[0].Rows[0]["PrizeCategory"].ToString());
                        chkIncludeConsPrize.Checked = Convert.ToBoolean(dtInfo.Tables[0].Rows[0]["IncludeConsPrize"]);
                        Status = Convert.ToInt16(dtInfo.Tables[0].Rows[0]["SaveStatus"].ToString());
                        if (Status <=0)
                        {
                            btnSave.Visible = true;
                        }
                        else if(Status>=1 && Status<2)
                        {
                            btnSave.Visible = true;
                            btnConfirm.Visible = true;
                        }
                        else if (Status == 4 || Status == 6 || Status == 8)
                        {
                            btnSave.Visible = true;
                            btnConfirm.Visible = true;
                        }
                    }
                    if (dtInfo.Tables[1].Rows.Count > 0)
                    {
                       
                       // btnSave.Text = "Update";
                        hdIsUpdate.Value = hdUniqueId.Value;
                        gvPrizeDetails.DataSource = dtInfo.Tables[1];
                        gvPrizeDetails.DataBind();
                    }                    
                    else {
                       
                       // btnSave.Text = "Save";    
                        DataTable dtPrizeDtl = new DataTable();
                        DataRow dr = null;
                       
                        dtPrizeDtl.Columns.Add(new DataColumn("RowNo", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("NameOfPrize", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("PrizeAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("AdministrativeChargePercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("AdministrativeCharge", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("GrossPrizeAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("TaxDeductionPercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("DeductionOfIT", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("PayableToWinner", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAdministrativeChargePercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketAdministrativeCharge", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketGrossPrizeAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketTaxDeductionPercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SpecialTicketDeductionOfIT", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("PayableToSpecialTicketWinner", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAdministrativeChargePercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketAdministrativeCharge", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketGrossPrizeAmount", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketTaxDeductionPercentage", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("SuperTicketDeductionOfIT", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("PayableToSuperTicketWinner", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("NoOfWinner", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("NoOfDigitInStatic", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("FixedOrVariable", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("ValidationForUnsold", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("Description1", typeof(string)));
                        dtPrizeDtl.Columns.Add(new DataColumn("Description2", typeof(string)));
                        int RowCnt = 0;
                        for (int iCnt = 1; iCnt <= PrizeCategoryCnt; iCnt++)
                        {
                            RowCnt++;
                            if (chkIncludeConsPrize.Checked == true && iCnt == 2)
                            {
                                dr = dtPrizeDtl.NewRow();
                                dr["RowNo"] = RowCnt;
                                dr["NameOfPrize"] = "Cons. Prize";
                                dr["PrizeAmount"] = "0";
                                dr["AdministrativeChargePercentage"] = "0";
                                dr["AdministrativeChargePercentage"] = "0";
                                dr["AdministrativeCharge"] = "0";
                                dr["GrossPrizeAmount"] = "0";
                                dr["TaxDeductionPercentage"] = "0";
                                dr["DeductionOfIT"] = "0";
                                dr["PayableToWinner"] = "0";
                                dr["SpecialTicketAmount"] = "0";
                                dr["SpecialTicketAdministrativeChargePercentage"] = "0";
                                dr["SpecialTicketAdministrativeCharge"] = "0";
                                dr["SpecialTicketGrossPrizeAmount"] = "0";
                                dr["SpecialTicketTaxDeductionPercentage"] = "0";
                                dr["SpecialTicketDeductionOfIT"] = "0";
                                dr["PayableToSpecialTicketWinner"] = "0";
                                dr["SuperTicketAmount"] = "0";
                                dr["SuperTicketAdministrativeChargePercentage"] = "0";
                                dr["SuperTicketAdministrativeCharge"] = "0";
                                dr["SuperTicketGrossPrizeAmount"] = "0";
                                dr["SuperTicketTaxDeductionPercentage"] = "0";
                                dr["SuperTicketDeductionOfIT"] = "0";
                                dr["PayableToSuperTicketWinner"] = "0";
                                dr["NoOfWinner"] = string.Empty;
                                dr["NoOfDigitInStatic"] = string.Empty;
                                dr["FixedOrVariable"] = string.Empty;
                                dr["ValidationForUnsold"] =string.Empty;
                                dr["Description1"] = string.Empty;
                                dr["Description2"] = string.Empty;
                                dtPrizeDtl.Rows.Add(dr);
                                RowCnt++;
                            }
                            dr = dtPrizeDtl.NewRow();
                            dr["RowNo"] = RowCnt;
                            dr["NameOfPrize"] = objValidateData.ToOrdinal(iCnt) + " Prize";
                            dr["PrizeAmount"] = "0";
                            dr["AdministrativeChargePercentage"] = "0";
                            dr["AdministrativeChargePercentage"] = "0";
                            dr["AdministrativeCharge"] = "0";
                            dr["GrossPrizeAmount"] = "0";
                            dr["TaxDeductionPercentage"] = "0";
                            dr["DeductionOfIT"] = "0";
                            dr["PayableToWinner"] = "0";
                            dr["SpecialTicketAmount"] = "0";
                            dr["SpecialTicketAdministrativeChargePercentage"] = "0";
                            dr["SpecialTicketAdministrativeCharge"] = "0";
                            dr["SpecialTicketGrossPrizeAmount"] = "0";
                            dr["SpecialTicketTaxDeductionPercentage"] = "0";
                            dr["SpecialTicketDeductionOfIT"] = "0";
                            dr["PayableToSpecialTicketWinner"] = "0";
                            dr["SuperTicketAmount"] = "0";
                            dr["SuperTicketAdministrativeChargePercentage"] = "0";
                            dr["SuperTicketAdministrativeCharge"] = "0";
                            dr["SuperTicketGrossPrizeAmount"] = "0";
                            dr["SuperTicketTaxDeductionPercentage"] = "0";
                            dr["SuperTicketDeductionOfIT"] = "0";
                            dr["PayableToSuperTicketWinner"] = "0";
                            dr["NoOfWinner"] = string.Empty;
                            dr["NoOfDigitInStatic"] = string.Empty;
                            dr["FixedOrVariable"] = string.Empty;
                            dr["ValidationForUnsold"] = string.Empty;
                            dr["Description1"] = string.Empty;
                            dr["Description2"] = string.Empty;
                            dtPrizeDtl.Rows.Add(dr);
                        }
                        gvPrizeDetails.DataSource = dtPrizeDtl;
                        gvPrizeDetails.DataBind();
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                   // btnSave.Text = "Update";
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
                Label lblTicketUnSoldUploadStatus = ((Label)e.Row.FindControl("lblTicketUnSoldUploadStatus"));
                string Status = "";
                int StatusVal = Convert.ToInt16(lblStatus.Text);
                if (StatusVal == 0) { Status = "<font color='red'>Pending</font>"; }
                else if (StatusVal == 1) { Status = "Draft"; }
                else if (StatusVal >= 2) { Status = "Confirm"; }
              
                lblStatus.Text = Status;
                if (StatusVal >= 2)
                {
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                }
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