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
    public partial class LCS_Edit_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        LCS_Service oLCS_Service = new LCS_Service();
        CYCLE_Service oCYCLE_Service = new CYCLE_Service();

        //Static Variable
        //static string user;
        //static string bu = null;
        //static string id = null;
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
            lblID.Text = lcsData.Rows[0]["ID"].ToString();

            //
            DataTable cycleData = oUtility.getActive(oCYCLE_Service.getData(lcsData.Rows[0]["BU_CODE"].ToString(), "2"), "CYCLE_STATUS", "A");
            oUtility.DDL(DDLCycleDelivery, cycleData, "CYCLE_TIME", "CYCLE_ID", "Please select");

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
                TextBox gvtxtShelfCount = (TextBox)(e.Row.FindControl("gvtxtShelfCount"));
                gvtxtShelfCount.Text = DataBinder.Eval(e.Row.DataItem, "SHELF_COUNT_QTY").ToString();

                //Set Max Qty
                Label gvlblMaxIssue = (Label)(e.Row.FindControl("gvlblMaxIssue"));
                gvlblMaxIssue.Text = ((int)DataBinder.Eval(e.Row.DataItem, "PAR_QTY") - (int)DataBinder.Eval(e.Row.DataItem, "SHELF_COUNT_QTY")).ToString();

                //Set Issue Qty
                TextBox gvtxtIssueQty = (TextBox)(e.Row.FindControl("gvtxtIssueQty"));
                if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ISSUE_QTY")) > 0)
                {
                    gvtxtIssueQty.Text = DataBinder.Eval(e.Row.DataItem, "ISSUE_QTY").ToString();
                }
                else
                {
                    gvtxtIssueQty.Text = "";
                }
                //Set Short Qty
                Label gvlblShortIssue = (Label)(e.Row.FindControl("gvlblShortIssue"));
                gvlblShortIssue.Text = DataBinder.Eval(e.Row.DataItem, "SHORT_QTY").ToString();

                //Set Over Qty
                Label gvlblOverIssue = (Label)(e.Row.FindControl("gvlblOverIssue"));
                gvlblOverIssue.Text = DataBinder.Eval(e.Row.DataItem, "OVER_QTY").ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (RdLCSStatus.SelectedValue == "C")
            {
                if (DDLCycleDelivery.SelectedValue != "-1")
                {
                    updateDataLCSDetail();
                    oLCS_Service.updateLCSStatus(lblID.Text, DDLCycleDelivery.SelectedValue, ViewState["user"].ToString());
                    Response.Redirect("/Views/LCS/LCS_Detail_Request.aspx?id=" + lblID.Text);
                }
                else
                {
                    oUtility.MsgAlert(this, "Please select delivery time");
                }
            }
            else
            {
                updateDataLCSDetail();
            }
        }

        protected void updateDataLCSDetail()
        {
            if (GridView.Rows.Count > 0)
            {
                int index = 0;
                foreach (GridViewRow row in GridView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label gvlblId = GridView.Rows[index].FindControl("gvlblId") as Label;
                        TextBox gvtxtShelfCount = GridView.Rows[index].FindControl("gvtxtShelfCount") as TextBox;
                        Label gvlblMaxIssue = GridView.Rows[index].FindControl("gvlblMaxIssue") as Label;
                        TextBox gvtxtIssueQty = GridView.Rows[index].FindControl("gvtxtIssueQty") as TextBox;
                        Label gvlblShortIssue = GridView.Rows[index].FindControl("gvlblShortIssue") as Label;
                        Label gvlblOverIssue = GridView.Rows[index].FindControl("gvlblOverIssue") as Label;

                        //Set Short & Over

                        if (!string.IsNullOrEmpty(gvtxtIssueQty.Text))
                        {
                            gvlblShortIssue.Text = (Convert.ToDouble(gvlblMaxIssue.Text) - Convert.ToDouble(gvtxtIssueQty.Text)).ToString();
                            gvlblOverIssue.Text = (Convert.ToDouble(gvtxtIssueQty.Text) - Convert.ToDouble(gvlblMaxIssue.Text)).ToString();
                            if (Convert.ToDouble(gvlblShortIssue.Text) < 0)
                            {
                                gvlblShortIssue.Text = "0";
                            }
                            if (Convert.ToDouble(gvlblOverIssue.Text) < 0)
                            {
                                gvlblOverIssue.Text = "0";
                            }
                        }
                        else
                        {
                            gvtxtIssueQty.Text = "0";
                        }


                        //if (Convert.ToDouble(gvlblMaxIssue.Text) > Convert.ToDouble(gvtxtIssueQty.Text))
                        //{

                        //}
                        //else if (Convert.ToDouble(gvlblMaxIssue.Text) < Convert.ToDouble(gvtxtIssueQty.Text))
                        //{

                        //}
                        //else
                        //{
                        //    gvlblShortIssue.Text = "0";
                        //    gvlblOverIssue.Text = "0";
                        //}

                        oLCS_Service.updateDataLCSDetail(gvlblId.Text, gvtxtShelfCount.Text, gvtxtIssueQty.Text, gvlblShortIssue.Text, gvlblOverIssue.Text, ViewState["user"].ToString());
                    }
                    index++;
                }
                oUtility.MsgAlert(this, "Success");
            }
        }

        protected void gvtxtIssueQty_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = ((GridViewRow)(((Control)(sender)).NamingContainer));

            Label gvlblMaxIssue = (Label)(gridViewRow.FindControl("gvlblMaxIssue"));
            TextBox gvtxtIssueQty = (TextBox)(gridViewRow.FindControl("gvtxtIssueQty"));
            Label gvlblShortIssue = (Label)(gridViewRow.FindControl("gvlblShortIssue"));
            Label gvlblOverIssue = (Label)(gridViewRow.FindControl("gvlblOverIssue"));

            if (Convert.ToDouble(gvlblMaxIssue.Text) > Convert.ToDouble(gvtxtIssueQty.Text))
            {
                gvlblShortIssue.Text = (Convert.ToDouble(gvlblMaxIssue.Text) - Convert.ToDouble(gvtxtIssueQty.Text)).ToString();
            }
            else if (Convert.ToDouble(gvlblMaxIssue.Text) < Convert.ToDouble(gvtxtIssueQty.Text))
            {
                gvlblOverIssue.Text = (Convert.ToDouble(gvtxtIssueQty.Text) - Convert.ToDouble(gvlblMaxIssue.Text)).ToString();
            }
            else
            {
                gvlblShortIssue.Text = "0";
                gvlblOverIssue.Text = "0";
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
        }
    }
}