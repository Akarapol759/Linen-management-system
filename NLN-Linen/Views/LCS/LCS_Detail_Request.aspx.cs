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

namespace NLN_Linen.Views.LCS
{
    public partial class LCS_Detail_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        LCS_Service oLCS_Service = new LCS_Service();

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
                        ViewState["user"] = ticket.Name.ToString();
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
            DataTable lcsData = oLCS_Service.getDataLCS(id);
            lblLCSRequestNo.Text = lcsData.Rows[0]["LCS_REQUEST_NO"].ToString();
            lblBusinessUnit.Text = lcsData.Rows[0]["BU_FULL_NAME"].ToString();
            lblCustomer.Text = lcsData.Rows[0]["CUS_NAME"].ToString();
            lblCycleShelfCount.Text = lcsData.Rows[0]["CYCLE_TIME_SHELF_COUNT"].ToString();
            lblCycleDelivery.Text = lcsData.Rows[0]["CYCLE_TIME_DELIVERY"].ToString();
            RdLCSStatus.SelectedValue = lcsData.Rows[0]["LCS_REQUEST_STATUS"].ToString();
            if (lcsData.Rows[0]["LCS_REQUEST_STATUS"].ToString() == "N")
            {
                btnCancel.Visible = false;
                btnDuplicate.Visible = true;
            }
            else
            {
                btnCancel.Visible = true;
                btnDuplicate.Visible = false;
            }
            lblID.Text = lcsData.Rows[0]["ID"].ToString();

            //Get Detail
            DataTable lcsDetailData = oLCS_Service.getDataLCSDetail(lblID.Text);
            GridView.DataSource = lcsDetailData;
            GridView.DataBind();
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Set Id
                Label gvlblId = (Label)(e.Row.FindControl("gvlblId"));
                gvlblId.Text = DataBinder.Eval(e.Row.DataItem, "ID").ToString();

                //Set Item Name
                Label gvlblItemName = (Label)(e.Row.FindControl("gvlblItemName"));
                gvlblItemName.Text = (string)DataBinder.Eval(e.Row.DataItem, "ITEM_NAME");

                //Set Par Qty
                Label gvlblParQty = (Label)(e.Row.FindControl("gvlblParQty"));
                gvlblParQty.Text = DataBinder.Eval(e.Row.DataItem, "PAR_QTY").ToString();

                //Set Shelft Count Qty
                Label gvlblShelfCount = (Label)(e.Row.FindControl("gvlblShelfCount"));
                gvlblShelfCount.Text = DataBinder.Eval(e.Row.DataItem, "SHELF_COUNT_QTY").ToString();

                //Set Max Qty
                Label gvlblMaxIssue = (Label)(e.Row.FindControl("gvlblMaxIssue"));
                gvlblMaxIssue.Text = ((int)DataBinder.Eval(e.Row.DataItem, "PAR_QTY") - (int)DataBinder.Eval(e.Row.DataItem, "SHELF_COUNT_QTY")).ToString();

                //Set Issue Qty
                Label gvlblIssueQty = (Label)(e.Row.FindControl("gvlblIssueQty"));
                gvlblIssueQty.Text = DataBinder.Eval(e.Row.DataItem, "ISSUE_QTY").ToString();

                //Set Short Qty
                Label gvlblShortIssue = (Label)(e.Row.FindControl("gvlblShortIssue"));
                gvlblShortIssue.Text = DataBinder.Eval(e.Row.DataItem, "SHORT_QTY").ToString();

                //Set Over Qty
                Label gvlblOverIssue = (Label)(e.Row.FindControl("gvlblOverIssue"));
                gvlblOverIssue.Text = DataBinder.Eval(e.Row.DataItem, "OVER_QTY").ToString();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/LCS/LCS_Request.aspx");
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable data = oLCS_Service.getDataIsseList(lblID.Text);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#printIssueModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintModalScript", sb.ToString(), false);
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            ReportDataSource dt = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dt);
            ReportViewer1.LocalReport.ReportPath = @"Reports\IssueList.rdlc";
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.DataBind();
            //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //Response.Buffer = true;
            //Response.ClearHeaders();
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "inline; filename= " + lblLCSRequestNo.Text + "." + extension);
            //Response.BinaryWrite(bytes);
            //Response.Flush();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            oLCS_Service.updateLCSStatus(lblID.Text, ViewState["user"].ToString());
            oUtility.MsgAlert(this, "Cancel success");
            Response.Redirect("/Views/LCS/LCS_Request.aspx");
        }

        protected void btnDuplicate_Click(object sender, EventArgs e)
        {
            DataTable dtDup = oLCS_Service.getDataLCSDetail(lblID.Text);

        }
    }
}