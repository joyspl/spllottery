using LTMSWebApp.LTMSServiceRef;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LTMSWebApp
{
    /// <summary>
    /// Summary description for hxCheckProgress
    /// </summary>
    public class hxCheckProgress : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string quer = context.Request.QueryString["id"];
            string msg = string.Empty;
            int success = default(int);
            long id = default(long);
            try
            {
                long.TryParse(quer, out id);
                DataTable obj = new LTMSServiceClient().GetZipProgressById(id);
                if (obj != null && obj.Rows.Count > default(int))
                {
                    var zipProgress = Convert.ToInt32(obj.Rows[0]["ZipProgress"].ToString());
                    success = 1;
                    msg = zipProgress.ToString();
                }
                else
                    throw new Exception("Unable to search Data");
            }
            catch (Exception)
            {
                msg = default(int).ToString();
            }
            context.Response.Write(JsonConvert.SerializeObject(new { Success = success, Message = msg }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}