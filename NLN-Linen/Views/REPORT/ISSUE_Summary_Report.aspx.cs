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

namespace NLN_Linen.Views.REPORT
{
    public partial class ISSUE_Summary_Report : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        REPORT_Service oREPORT_Service = new REPORT_Service();

        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnSearch.UniqueID;
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
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            Session["bu"] = ticket.UserData.ToString();
                            BindData(Session["bu"].ToString(), ticket.Version);
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
        protected void BindData(string buCode, int role)
        {
            DataTable buData = oUtility.getActive(oBU_Service.getData(buCode), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            DataTable cusDate = oUtility.getActive(oCUSTOMER_Service.getData(buCode), "CUS_STATUS", "A");
            oUtility.DDL(DDLCustomer, cusDate, "CUS_NAME", "CUS_CODE", "All Department");
            //Administrator
            if (role != 1)
            {
                DDLBusinessUnit.Enabled = false;
                DDLBusinessUnit.SelectedValue = buCode;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(DDLBusinessUnit.SelectedValue != "-1" && !string.IsNullOrEmpty(txtStdDate.Text.Trim()) 
                && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                DateTime dateFrom = DateTime.ParseExact(txtStdDate.Text.Trim(), "dd/MM/yyyy", null);
                DateTime dateTo = DateTime.ParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", null);
                if (dateFrom > dateTo)
                {
                    oUtility.MsgAlert(this, "Plese select end date more than start date.");
                }
                else
                {
                    DataTable result = null;
                    if(DDLCustomer.SelectedValue == "-1")
                    {
                        result = oREPORT_Service.getData_VW_RPT_SUMMARY(DDLBusinessUnit.SelectedValue, DDLCustomer.SelectedValue, txtStdDate.Text.Trim(), txtEndDate.Text.Trim());
                    }
                    else
                    {
                        result = oREPORT_Service.getData_VW_RPT_SUMMARY(DDLBusinessUnit.SelectedValue, DDLCustomer.SelectedValue, txtStdDate.Text.Trim(), txtEndDate.Text.Trim());
                    }
                    ReportViewer1.Reset();
                    ReportViewer1.Visible = true;
                    ReportDataSource dt = new ReportDataSource("DataSet1", result);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(dt);
                    if (DDLReportType.SelectedValue == "A")
                    {
                        ReportViewer1.LocalReport.ReportPath = @"Reports\VW_RPT_SUMMARY.rdlc";
                    }
                    else
                    {
                        ReportViewer1.LocalReport.ReportPath = @"Reports\VW_RPT_SUMMARY_DETAIL.rdlc";
                    }
                    string oHeader = "Summary Report from " + txtStdDate.Text.Trim() + " - " + txtEndDate.Text.Trim();
                    string oDepartment = "Department : " + DDLCustomer.SelectedItem.ToString();
                    ReportParameter header = new ReportParameter("header", oHeader);
                    ReportParameter department = new ReportParameter("department", oDepartment);
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { header, department });
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
                    //byte[] bytes = ReportViewer1.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    //Response.Buffer = true;
                    //Response.ClearHeaders();
                    //Response.Clear();
                    //Response.ContentType = mimeType;
                    //Response.AddHeader("content-disposition", "attachment; filename= VW_RPT_SUMMARY." + extension);
                    //Response.BinaryWrite(bytes);
                    //Response.Flush();
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Plese select all fields.");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void DDLBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(DDLBusinessUnit.SelectedValue), "CUS_STATUS", "A");
                oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "All Department");
            }
            else
            {
                oUtility.MsgAlert(this, "Please select business unit");
            }
        }
    }
}