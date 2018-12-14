using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace LTMSWebApp
{
    public partial class TrxLotteryClaimEntry : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnUploadPan);
            scriptManager.RegisterPostBackControl(this.btnUploadAadhar);
            scriptManager.RegisterPostBackControl(this.btnUploadBankDtl);
            scriptManager.RegisterPostBackControl(this.btnPWTTicket);
            scriptManager.RegisterPostBackControl(this.btnPhoto);
           
            Page.MaintainScrollPositionOnPostBack = true;
            if (IsPostBack == false)
            {
                
                FillComboBox();
                FillLotteryClaimDetails();
                
            }
        }
        
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType", "");
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryName, "LotteryNameByLotteryTypeID", ddlLotteryType.SelectedValue);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }

        }
        private void FillLotteryClaimDetails()
        {
            if (Session["LotteryClaimDetails"] == null)
            {
                Session["CustomError"] = "Invalid Navigation option. Please use the proper panel to access this page.";
                Server.Transfer("appError.aspx");
                return;
            }
            else {
                ddlLotteryType.Enabled = false;
                ddlLotteryName.Enabled = false;
                txtDrawNo.ReadOnly=true;
                txtDrawDate.ReadOnly=true;
                txtMobileNo.ReadOnly = true;
                txtEmailId.ReadOnly = true;
                txtLotteryNo.ReadOnly = true;
                ddlLotteryType.SelectedValue = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).LotteryTypeId.ToString();
                ddlLotteryType_SelectedIndexChanged(ddlLotteryType,null);
                ddlLotteryName.SelectedValue = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).LotteryId.ToString();

                txtDrawNo.Text = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).DrawNo.ToString();
                if (objValidateData.isValidDate(((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).DrawDate.ToString()) == true)
                {
                    txtDrawDate.Text =Convert.ToDateTime(((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).DrawDate).ToString("dd-MMM-yyyy");
                }
                txtLotteryNo.Text = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).LotteryNo.ToString();
                txtMobileNo.Text = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).MobileNo.ToString();
                txtEmailId.Text = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).EmailId.ToString();
                hdUniqueId.Value=((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).DataUniqueId;
                Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                lblClaimType.Text = objClaimType[((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString()];
                if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "1")
                {
                    trProprietorOf.Visible = false;
                    trSoDoWo.Visible = false;
                    trSurety.Visible = false;
                    trPwtTicket.Visible = true;
                }
                else if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "2"
                    || ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "3"
                    )
                {
                    trProprietorOf.Visible = true;
                    trSoDoWo.Visible = false;
                    trSurety.Visible = false;
                
                }
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).PwtTicket = null;
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Photo = null;
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Pan = null;
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Aadhar = null;
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).BankDtl = null;


            }
        }

        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsUploaded = IsValidPhotoFile();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        protected void btnPWTTicket_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsUploaded = IsValidPwtFile();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnUploadPan_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsUploaded=IsValidPanFile();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnUploadBankDtl_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsUploaded = IsValidBankDtlFile();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnUploadAadhar_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsUploaded = IsValidAadharFile();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {               
                ClsLotteryClaimDetails objClsLotteryClaimDetails = new ClsLotteryClaimDetails();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objClsLotteryClaimDetails.UserId = "Anonymous";
                objClsLotteryClaimDetails.IpAdd = Request.UserHostAddress;
                objClsLotteryClaimDetails.LotteryId = Convert.ToInt64(ddlLotteryName.SelectedValue);
                objClsLotteryClaimDetails.LotteryTypeId = Convert.ToInt64(ddlLotteryType.SelectedValue);
                objClsLotteryClaimDetails.DataUniqueId = hdUniqueId.Value;
                objClsLotteryClaimDetails.MobileNo = txtMobileNo.Text.Trim();
                objClsLotteryClaimDetails.EmailId = txtEmailId.Text.Trim();
                objClsLotteryClaimDetails.LotteryNo = txtLotteryNo.Text.Trim();
                objClsLotteryClaimDetails.ClaimType = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType;
                objClsLotteryClaimDetails.Name =txtName.Text.Trim();
                objClsLotteryClaimDetails.ProprietorOf = (txtProprietorOf.Text.Trim()==""?"":txtProprietorOf.Text.Trim());
                objClsLotteryClaimDetails.SoDoWo = (txtSoDoWo.Text.Trim() == "" ? "" : txtSoDoWo.Text.Trim());
                objClsLotteryClaimDetails.Surety = (txtSurety.Text.Trim() == "" ? "" : txtSurety.Text.Trim());
                objClsLotteryClaimDetails.FatherOrGuardianName = txtFatherOrGuardianName.Text.Trim();
                objClsLotteryClaimDetails.Address = txtAddress.Text.Trim();
                objClsLotteryClaimDetails.AadharNo = txtAadharNo.Text.Trim();
                objClsLotteryClaimDetails.PanNo =txtPanCard.Text.Trim();
                objClsLotteryClaimDetails.BankName = txtBankName.Text.Trim();
                objClsLotteryClaimDetails.BankAccountNo = txtBankAccountNo.Text.Trim();
                objClsLotteryClaimDetails.IFSCCode = txtIFSCCode.Text.Trim();
                objClsLotteryClaimDetails.Pan = new byte[0];
                objClsLotteryClaimDetails.Aadhar = new byte[0];
                objClsLotteryClaimDetails.BankDtl = new byte[0];
                objClsLotteryClaimDetails.PwtTicket = new byte[0];

                objClsLotteryClaimDetails.Captcha = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Captcha;
                objClsLotteryClaimDetails.OTP = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).OTP;
                objClsLotteryClaimDetails.Pan = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Pan;
                objClsLotteryClaimDetails.Aadhar = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Aadhar;
                objClsLotteryClaimDetails.BankDtl = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).BankDtl;                
                objClsLotteryClaimDetails.Photo = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Photo;
                if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "1")
                {
                    objClsLotteryClaimDetails.PwtTicket = ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).PwtTicket;
                }
                string TransactionNo="";
                bool IsAdded = objLtmsService.InserInLotteryClaimEntry(objClsLotteryClaimDetails, out  TransactionNo);
                if (IsAdded == true)
                {                   
                    btnConfirm.Visible = false;
                    btnClose.Visible = true;
                    Session["LotteryClaimDetails"] = null;
                    Session["ApplicationId"] = TransactionNo;
                    Response.Redirect("TrxLotteryClaimEntryPrint.aspx", false);
                }

            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

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

            if (objValidateData.IsInteger(txtDrawNo.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Draw No should be numeric.');", true);
                txtDrawNo.Focus();
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Name.');", true);
                txtName.Focus();
                return false;
            }
            if (txtFatherOrGuardianName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Name of Father/Husband/Guardian.');", true);
                txtFatherOrGuardianName.Focus();
                return false;
            }
           
            if (txtAddress.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Address.');", true);
                txtAddress.Focus();
                return false;
            }

            if (txtAadharNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Aadhar No.');", true);
                txtAadharNo.Focus();
                return false;
            }
            if (txtPanCard.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Pan No.');", true);
                txtPanCard.Focus();
                return false;
            }

            if (txtBankName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Bank Name');", true);
                txtBankName.Focus();
                return false;
            }
            if (txtBankAccountNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Bank Account No');", true);
                txtBankAccountNo.Focus();
                return false;
            }
            if (txtIFSCCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter IFSC Code');", true);
                txtIFSCCode.Focus();
                return false;
            }
            if (txtPanCard.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Pan No.');", true);
                txtPanCard.Focus();
                return false;
            }
            if (txtPanCard.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Pan No.');", true);
                txtPanCard.Focus();
                return false;
            }
            if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ToString() == "1")
            {
                if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).PwtTicket == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please upload Prize Winning Ticket.');", true);
                    return false;
                }

            }
            //if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "1")
            //{
            //    if (txtSoDoWo.Text.Trim() == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter S/o, D/o, W/o.');", true);
            //        txtSoDoWo.Focus();
            //        return false;
            //    }
            //    if (txtSurety.Text.Trim() == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Surety.');", true);
            //        txtSurety.Focus();
            //        return false;
            //    }
            //}
            else if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "2"
                || ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).ClaimType.ToString() == "3"
                )
            {
                if (txtProprietorOf.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Owner/Proprietor of.');", true);
                    txtProprietorOf.Focus();
                    return false;
                }               
            }


            if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Photo == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please upload Photo.');", true);
                return false;
            }
            if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Pan == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please upload pan card file.');", true);
                return false;
            }
            if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Aadhar == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please upload Aadhar card file.');", true);
                return false;
            }
            if (((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).BankDtl == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please upload Bank details file.');", true);
                return false;
            }
           
            return true;
        }
        private bool IsValidPwtFile()
        {
            string UploadFileName = "";
            string fileExt = "";
            string FileContentType = "";
            double filesize = 0;
            if (fuPWTTicket.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select Prize Winning Ticket file.');", true);
                return false;
            }
            if (fuPWTTicket.HasFile)
            {
                UploadFileName = fuPWTTicket.PostedFile.FileName;
                fileExt = System.IO.Path.GetExtension(UploadFileName).ToLower();

                FileContentType = fuPWTTicket.PostedFile.ContentType;
                if (!((FileContentType == "image/bmp") || (FileContentType == "image/x-windows-bmp") || (FileContentType == "image/jpeg") || (FileContentType == "image/pjpeg") || (FileContentType == "image/png") || (FileContentType == "image/gif") || (FileContentType == "image/tiff") || (FileContentType == "image/x-tiff")) && !((fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png")))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Only jpg,gif,png,bmp,jpeg  image type allowed for Pan Card.');", true);
                    return false;

                }
                /*check for pictue file upload size.
                 * the default file upload size is  2gb*/
                filesize = fuPWTTicket.FileBytes.Length;
                if (filesize > 2097152)//1024 =1kb
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Prize Winning Ticket file size should not exceed 2mb.');", true);
                    return false;
                }
                MemoryStream PWTTicketStream = new MemoryStream(fuPWTTicket.FileBytes);
                byte[] PWTTicketPic = PWTTicketStream.ToArray();
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).PwtTicket = PWTTicketPic;
                Session["PicToShow"] = PWTTicketPic;
                imgPWTTicket.Visible = true;
                imgPWTTicket.BorderWidth = 1;
                imgPWTTicket.ImageUrl = "data:image;base64," + Convert.ToBase64String(PWTTicketPic);
            }
            return true;
        }
        private bool IsValidPhotoFile()
        {
            string UploadFileName = "";
            string fileExt = "";
            string FileContentType = "";
            double filesize = 0;
            if (flupPhoto.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select Photo.');", true);
                return false;
            }
            if (flupPhoto.HasFile)
            {
                UploadFileName = flupPhoto.PostedFile.FileName;
                fileExt = System.IO.Path.GetExtension(UploadFileName).ToLower();

                FileContentType = flupPhoto.PostedFile.ContentType;
                if (!((FileContentType == "image/bmp") || (FileContentType == "image/x-windows-bmp") || (FileContentType == "image/jpeg") || (FileContentType == "image/pjpeg") || (FileContentType == "image/png") || (FileContentType == "image/gif") || (FileContentType == "image/tiff") || (FileContentType == "image/x-tiff")) && !((fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png")))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Only jpg,gif,png,bmp,jpeg  image type allowed for Photo Card.');", true);
                    return false;

                }
                /*check for pictue file upload size.
                 * the default file upload size is  2gb*/
                filesize = flupPhoto.FileBytes.Length;
                if (filesize > 2097152)//1024 =1kb
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Photo card file size should not exceed 2mb.');", true);
                    return false;
                }
                MemoryStream PhotoStream = new MemoryStream(flupPhoto.FileBytes);
                byte[] PhotoPic = PhotoStream.ToArray();
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Photo = PhotoPic;
                Session["PicToShow"] = PhotoPic;
                imgPhoto.Visible = true;
                imgPhoto.BorderWidth = 1;
                imgPhoto.ImageUrl = "data:image;base64," + Convert.ToBase64String(PhotoPic);
            }
            return true;
        }
        private bool IsValidPanFile() {
            string UploadFileName = "";
            string fileExt = "";
            string FileContentType = "";
            double filesize = 0;
            if (flupPan.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select Pan card file.');", true);
                return false;
            }
            if (flupPan.HasFile)
            {
                UploadFileName = flupPan.PostedFile.FileName;
                fileExt = System.IO.Path.GetExtension(UploadFileName).ToLower();

                FileContentType = flupPan.PostedFile.ContentType;
                if (!((FileContentType == "image/bmp") || (FileContentType == "image/x-windows-bmp") || (FileContentType == "image/jpeg") || (FileContentType == "image/pjpeg") || (FileContentType == "image/png") || (FileContentType == "image/gif") || (FileContentType == "image/tiff") || (FileContentType == "image/x-tiff")) && !((fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png")))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Only jpg,gif,png,bmp,jpeg  image type allowed for Pan Card.');", true);
                    return false;

                }
                /*check for pictue file upload size.
                 * the default file upload size is  2gb*/
                filesize = flupPan.FileBytes.Length;
                if (filesize > 2097152)//1024 =1kb
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Pan card file size should not exceed 2mb.');", true);
                    return false;
                }
                MemoryStream panStream = new MemoryStream(flupPan.FileBytes);
                byte[] panPic = panStream.ToArray();
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Pan = panPic;
                Session["PicToShow"] = panPic;
                imgPan.Visible = true;
                imgPan.BorderWidth = 1;
                imgPan.ImageUrl = "data:image;base64," + Convert.ToBase64String(panPic);
            }
            return true;
        }
        private bool IsValidAadharFile()
        {
            string UploadFileName = "";
            string fileExt = "";
            string FileContentType = "";
            double filesize = 0;
            if (flupAadhar.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select Aadhar Card file.');", true);
                return false;
            }

           
            if (flupAadhar.HasFile)
            {
                UploadFileName = flupAadhar.PostedFile.FileName;
                fileExt = System.IO.Path.GetExtension(UploadFileName).ToLower();

                FileContentType = flupAadhar.PostedFile.ContentType;
                if (!((FileContentType == "image/bmp") || (FileContentType == "image/x-windows-bmp") || (FileContentType == "image/jpeg") || (FileContentType == "image/pjpeg") || (FileContentType == "image/png") || (FileContentType == "image/gif") || (FileContentType == "image/tiff") || (FileContentType == "image/x-tiff")) && !((fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png")))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Only jpg,gif,png,bmp,jpeg  image type allowed for Aadhar card file.');", true);
                    return false;

                }
                /*check for pictue file upload size.
                 * the default file upload size is  2gb*/
                filesize = flupAadhar.FileBytes.Length;
                if (filesize > 2097152)//1024 =1kb
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Aadhar card file size should not exceed 2mb.');", true);
                    return false;
                }
                MemoryStream aadharStream = new MemoryStream(flupAadhar.FileBytes);
                byte[] aadharPic = aadharStream.ToArray();
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).Aadhar = aadharPic;
                Session["PicToShow"] = aadharPic;
                imgAadharPic.Visible = true;
                imgAadharPic.BorderWidth = 1;
                imgAadharPic.ImageUrl = "data:image;base64," + Convert.ToBase64String(aadharPic);
            }
            return true;
        }
        private bool IsValidBankDtlFile()
        {
            string UploadFileName = "";
            string fileExt = "";
            string FileContentType = "";
            double filesize = 0;
            if (flupBankDtl.HasFile == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select bank details file.');", true);
                return false;
            }
           
            if (flupBankDtl.HasFile)
            {
                UploadFileName = flupBankDtl.PostedFile.FileName;
                fileExt = System.IO.Path.GetExtension(UploadFileName).ToLower();

                FileContentType = flupBankDtl.PostedFile.ContentType;
                if (!((FileContentType == "image/bmp") || (FileContentType == "image/x-windows-bmp") || (FileContentType == "image/jpeg") || (FileContentType == "image/pjpeg") || (FileContentType == "image/png") || (FileContentType == "image/gif") || (FileContentType == "image/tiff") || (FileContentType == "image/x-tiff")) && !((fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".jpeg" || fileExt == ".png")))
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Only jpg,gif,png,bmp,jpeg  image type allowed for Bank details file.');", true);
                    return false;
                }
                /*check for pictue file upload size.
                 * the default file upload size is  2gb*/
                filesize = flupBankDtl.FileBytes.Length;
                if (filesize > 2097152)//1024 =1kb
                {
                    ClientScript.RegisterStartupScript(typeof(string), "MyMsg", "alert('Bank Details file size should not exceed 2mb.');", true);
                    return false;
                }
                MemoryStream bankDtlStream = new MemoryStream(flupBankDtl.FileBytes);
                byte[] bankDtlPic = bankDtlStream.ToArray();
                ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).BankDtl = bankDtlPic;
               imgBankDtl.Visible = true;
               imgBankDtl.BorderWidth = 1;
               imgBankDtl.ImageUrl = "data:image;base64," + Convert.ToBase64String(bankDtlPic);
            }
            return true;
        }
        public Image byteArrayToImage(byte[] imgBytes)
        {
            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                return Image.FromStream(imgStream);
            }
        }

    }
}