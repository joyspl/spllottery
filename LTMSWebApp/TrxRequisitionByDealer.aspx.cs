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
    public partial class TrxRequisitionByDealer : System.Web.UI.Page
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
                    FillComboBox();
                    BindGvData();
                    
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
                items.Add(new ListItem("<<--All-->>", "-40"));
                items.Add(new ListItem("Pending Only", "17"));
                items.Add(new ListItem("Draft", "31"));
                items.Add(new ListItem("Confirm", "32"));                            
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

        public void CalculateBalence()
        {
            double DepositeAmountLD = 0, DepositeAmountSPL = 0, LDBalence = 0, SPLBalence = 0;
            if (IsValidGridEntry() == false) { return; }//Check for valid data entry               

            DepositeAmountLD = Convert.ToDouble(txtDepositeAmountLD.Text);
            DepositeAmountSPL = Convert.ToDouble(txtDepositeAmountSPL.Text);

            txtLDBalence.Text = "0";
            txtSPLBalence.Text = "0";

            for (int iCtr = 0; iCtr < gvAvailableInventory.Rows.Count; iCtr++)
            {
                TextBox txtQtyRequired = (TextBox)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("txtQtyRequired");
                HiddenField hdTicketRate = (HiddenField)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("hdTicketRate");
                HiddenField hdRateForSpl = (HiddenField)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("hdRateForSpl");

                LDBalence = LDBalence + (Convert.ToInt64(txtQtyRequired.Text.Trim()) * Convert.ToDouble(hdTicketRate.Value));
                SPLBalence = SPLBalence + (Convert.ToInt64(txtQtyRequired.Text.Trim()) * Convert.ToDouble(hdRateForSpl.Value));
            }
            txtLDBalence.Text = (LDBalence).ToString("0.00");
            txtSPLBalence.Text = (SPLBalence).ToString("0.00");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReqCode="";
                DataTable dtInfo = new DataTable();
                CalculateBalence();
               
                DataTable dtDirectorRequisitionDtl = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry  
                dtDirectorRequisitionDtl.TableName = "tblDirectorRequisitionDtl";                
                DataRow dr = null;
                dtDirectorRequisitionDtl.Columns.Add(new DataColumn("RequisitionId", typeof(string)));
                dtDirectorRequisitionDtl.Columns.Add(new DataColumn("QtyRequired", typeof(string)));
                for (int iCtr = 0; iCtr < gvAvailableInventory.Rows.Count; iCtr++)
                {
                    TextBox txtNameOfPrize = (TextBox)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("txtNameOfPrize");
                    TextBox txtQtyRequired = (TextBox)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("txtQtyRequired");

                    dr = dtDirectorRequisitionDtl.NewRow();
                    dr["RequisitionId"] = gvAvailableInventory.DataKeys[iCtr].Value.ToString(); ;
                    dr["QtyRequired"] = txtQtyRequired.Text.Trim();
                    dtDirectorRequisitionDtl.Rows.Add(dr);
                }
                objRequisition.ReqCode = txtReqCode.Text;
                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objRequisition.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objRequisition.SaveStatus = 31;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objRequisition.SaveStatus = 32;
                }
                objRequisition.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                bool IsUpdated = objLtmsService.UpdateInRequisitionDealer(objRequisition, dtDirectorRequisitionDtl);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Requisition information updated successfully.');", true);
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
            double DepositeAmountLD = 0, DepositeAmountSPL = 0, TicketAmount = 0, SPLBalence = 0, BankGurantee = 0;            
            if (txtLDBalence.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Diroctors balence amount can not be blank.');", true);
                return false;
            }
            if (txtSPLBalence.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('SPL balence amount balence amount can not be blank.');", true);
                return false;
            }
            DepositeAmountLD = Convert.ToDouble(txtDepositeAmountLD.Text);
            BankGurantee = Convert.ToDouble(txtBankGurantee.Text);
            TicketAmount = Convert.ToDouble(txtLDBalence.Text);
            //   DepositeAmountSPL = Convert.ToDouble(txtDepositeAmountSPL.Text); 
            //   SPLBalence = Convert.ToDouble(txtSPLBalence.Text);
            if (TicketAmount > (DepositeAmountLD + BankGurantee))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Deposit amount should be more or equal to Ticket Amount.');", true);
                return false;
            }
            //if (SPLBalence > DepositeAmountSPL)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('SPL balence amount can not be more than the deposit amount.');", true);
            //    return false;
            //}
            return true;
        }
        private bool IsValidGridEntry()
        {
            for (int iCtr = 0; iCtr < gvAvailableInventory.Rows.Count; iCtr++)
            {
                Label lblQty = (Label)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("lblQty");
                TextBox txtQtyRequired = (TextBox)gvAvailableInventory.Rows[iCtr].Cells[0].FindControl("txtQtyRequired");
                if (objValidateData.IsLongInteger(txtQtyRequired.Text.Trim()) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Quantity Required can not be blank in row no " + (iCtr + 1) + ".');", true);
                    return false;
                }
                if (Convert.ToInt64(txtQtyRequired.Text.Trim()) > Convert.ToInt64(lblQty.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Quantity Required can not be greator than the available quantity in row no " + (iCtr + 1) + ".');", true);
                    return false;
                }
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
                    dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        SaveStatus=Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString().ToUpper());
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        gvAvailableInventory.DataSource = dtInfo;
                        gvAvailableInventory.DataBind();                        
                        GetDealerDepositInHand();
                        CalculateBalence();
                        if (SaveStatus == 17)
                        {
                            btnSave.Visible = true;                            
                        }
                        if (SaveStatus == 31)
                        {
                            btnSave.Visible = true;
                            btnSubmit.Visible = true;                           
                        }                        
                    }
                    dtInfo.Dispose();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 17) { Status = "<font color='red'>Pending</font>"; }
                    else if (StatusVal == 31 ) { Status = "Draft"; }
                    else if (StatusVal == 32 ) { Status = "Confirm"; }
                    lblStatus.Text = Status;
                    if (StatusVal > 32) 
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                    }
                }
                catch { } 
                
            }
        }        
        private void GetDealerDepositInHand()
        {
            txtDepositeAmountLD.Text = "0";
            txtDepositeAmountSPL.Text = "0";
            DataSet dsDepositeAmountInHand=new DataSet();
            dsDepositeAmountInHand = objLtmsService.GetDealerDepositInHandDtlById(Convert.ToInt64(hdUniqueId.Value));
            if (dsDepositeAmountInHand.Tables.Count > 0)
            {
                if (dsDepositeAmountInHand.Tables[0].Rows.Count > 0)
                {
                    txtDepositeAmountLD.Text = dsDepositeAmountInHand.Tables[0].Rows[0]["DepositInHandAmountLD"].ToString();
                }
                if (dsDepositeAmountInHand.Tables[1].Rows.Count > 0)
                {
                    txtBankGurantee.Text = dsDepositeAmountInHand.Tables[1].Rows[0]["DepositInHandAmountLD"].ToString();
                }

                if (dsDepositeAmountInHand.Tables[2].Rows.Count > 0)
                {
                    txtDepositeAmountSPL.Text = dsDepositeAmountInHand.Tables[2].Rows[0]["DepositInHandAmountSPL"].ToString();
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
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "RequisitionDealerViewDtl";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}