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

namespace NLN_Linen.Views.RTN
{
    public partial class RTN_Detail_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        RTN_Service oRTN_Service = new RTN_Service();

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
            DataTable rtnData = oRTN_Service.getDataRTN(id);
            lblRTNRequestNo.Text = rtnData.Rows[0]["RTN_REQUEST_NO"].ToString();
            lblBusinessUnit.Text = rtnData.Rows[0]["BU_FULL_NAME"].ToString();
            lblCustomer.Text = rtnData.Rows[0]["CUS_NAME"].ToString();
            RdRTNStatus.SelectedValue = rtnData.Rows[0]["RTN_REQUEST_STATUS"].ToString();
            if (rtnData.Rows[0]["RTN_REQUEST_STATUS"].ToString() == "N")
            {
                btnCancel.Visible = false;
                //btnDuplicate.Visible = true;
            }
            else
            {
                btnCancel.Visible = true;
                //btnDuplicate.Visible = false;
            }
            lblID.Text = rtnData.Rows[0]["ID"].ToString();

            //Get Detail
            DataTable rtnDetailData = oRTN_Service.getDataRTNDetail(lblID.Text);
            GridView.DataSource = rtnDetailData;
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

                //Set Issue Qty
                Label gvlblRtnQty = (Label)(e.Row.FindControl("gvlblRtnQty"));
                gvlblRtnQty.Text = DataBinder.Eval(e.Row.DataItem, "RTN_QTY").ToString();

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/RTN/RTN_Request.aspx");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable data = oRTN_Service.getDataRtnList(lblID.Text);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#printRtnModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintModalScript", sb.ToString(), false);
            ReportViewer1.Reset();
            ReportViewer1.Visible = true;
            ReportDataSource dt = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dt);
            ReportViewer1.LocalReport.ReportPath = @"Reports\RtnList.rdlc";
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
            oRTN_Service.updateRTNCancel(lblID.Text, "N", ViewState["user"].ToString());
            oUtility.MsgAlert(this, "Cancel success");
            Response.Redirect("/Views/RTN/RTN_Request.aspx");
        }

        //protected void btnDuplicate_Click(object sender, EventArgs e)
        //{
        //    DataTable dtDup = oRTN_Service.getDataLCSDetail(lblID.Text);

        //}
    }
}