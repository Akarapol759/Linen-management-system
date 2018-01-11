using Microsoft.Reporting.WebForms;
using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.SOILED
{
    public partial class SOILED_Detail_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        SOILED_Service oSOILED_Service = new SOILED_Service();
        LAUNDRY_Service oLAUNDRY_Service = new LAUNDRY_Service();

        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = btnSave.UniqueID;
            if (!Page.IsPostBack)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    if (!string.IsNullOrEmpty(ticket.Name.ToString()))
                    {
                        Session["user"] = ticket.Name.ToString();
                        if (ticket.Version == 1)
                        {
                            BindData(Request.QueryString["id"]);
                        }
                        else
                        {
                            BindData(Request.QueryString["id"]);
                        }
                    }
                    else
                    {
                        Response.Redirect("/Account/Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Account/Login.aspx");
                }
            }
        }
        protected void BindData(string id)
        {
            //Get Head
            DataTable soiledData = oSOILED_Service.getDataSOILED(id);
            lblBusinessUnit.Text = soiledData.Rows[0]["BU_FULL_NAME"].ToString();
            lblCyclePickup.Text = soiledData.Rows[0]["CYCLE_TIME_PICKUP"].ToString();
            lblLaundry.Text = soiledData.Rows[0]["LAUNDRY_NAME"].ToString();
            lblCycleToLaundry.Text = soiledData.Rows[0]["CYCLE_TIME_TO_LAUNDRY"].ToString();
            lblSOILEDReqNo.Text = soiledData.Rows[0]["SOILED_REQUEST_NO"].ToString();
            lblID.Text = soiledData.Rows[0]["ID"].ToString();

            ViewState["detail"] = oSOILED_Service.getDataDetail(id);
            GridView.DataSource = (DataTable)ViewState["detail"];
            GridView.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SOILED/SOILED_Request.aspx");
        }
        protected void btnTopImgPrint_Click(object sender, EventArgs e)
        {
            DataTable data = oSOILED_Service.getDataSOILED(lblID.Text);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#printSoiledModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintModalScript", sb.ToString(), false);
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            ReportDataSource dt = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dt);
            ReportViewer1.LocalReport.ReportPath = @"Reports\SOILED_LIST.rdlc";
            //ReportViewer1.LocalReport.Refresh();
            //ReportViewer1.DataBind();
            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.ClearHeaders();
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "inline; filename= " + lblSOILEDReqNo.Text + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}