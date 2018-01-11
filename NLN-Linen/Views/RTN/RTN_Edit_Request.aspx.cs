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
    public partial class RTN_Edit_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        RTN_Service oRTN_Service = new RTN_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();

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
            DataTable rtnData = oRTN_Service.getDataRTN(id);
            lblRTNRequestNo.Text = rtnData.Rows[0]["RTN_REQUEST_NO"].ToString();
            lblBusinessUnit.Text = rtnData.Rows[0]["BU_FULL_NAME"].ToString();
            lblCustomer.Text = rtnData.Rows[0]["CUS_NAME"].ToString();
            txtRequestName.Text = rtnData.Rows[0]["RTN_REQUEST_NAME"].ToString();
            lblRequestDate.Text = rtnData.Rows[0]["CREATE_DATE"].ToString();
            lblRequestTime.Text = rtnData.Rows[0]["RTN_REQUEST_TIME"].ToString();
            lblID.Text = rtnData.Rows[0]["ID"].ToString();
            ViewState["rtnData"] = rtnData;

            DataTable cycleData = oUtility.getActive(oITEM_Service.getData(rtnData.Rows[0]["BU_CODE"].ToString()), "ITEM_STATUS", "A");
            oUtility.DDL(DDLAddItem, cycleData, "ITEM_NAME", "ITEM_CODE", "Please select");

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
                TextBox gvtxtRtnQty = (TextBox)(e.Row.FindControl("gvtxtRtnQty"));
                if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RTN_QTY")) > 0)
                {
                    gvtxtRtnQty.Text = DataBinder.Eval(e.Row.DataItem, "RTN_QTY").ToString();
                }
                else
                {
                    gvtxtRtnQty.Text = "";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRequestName.Text))
            {
                if (RdRTNStatus.SelectedValue == "C")
                {
                    updateDataRTNDetail();
                    oRTN_Service.updateRTNStatus(lblID.Text, txtRequestName.Text, RdRTNStatus.SelectedValue, ViewState["user"].ToString());
                    Response.Redirect("/Views/RTN/RTN_Detail_Request.aspx?id=" + lblID.Text);
                }
                else
                {
                    oRTN_Service.updateRTN(lblID.Text, txtRequestName.Text, ViewState["user"].ToString());
                    updateDataRTNDetail();
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put request name");
            }
        }

        protected void updateDataRTNDetail()
        {
            if (GridView.Rows.Count > 0)
            {
                int index = 0;
                foreach (GridViewRow row in GridView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label gvlblId = GridView.Rows[index].FindControl("gvlblId") as Label;
                        TextBox gvtxtRtnQty = GridView.Rows[index].FindControl("gvtxtRtnQty") as TextBox;

                        //Set Short & Over

                        if (string.IsNullOrEmpty(gvtxtRtnQty.Text))
                        {
                            gvtxtRtnQty.Text = "0";
                        }

                        oRTN_Service.updateDataRTNDetail(gvlblId.Text, gvtxtRtnQty.Text, ViewState["user"].ToString());
                    }
                    index++;
                }
                oUtility.MsgAlert(this, "Success");
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
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1" && !string.IsNullOrEmpty(txtAddItemQty.Text))
            {
                DataTable rtnData = (DataTable)ViewState["rtnData"];
                DataTable item = oITEM_Service.checkItemPrice(DDLAddItem.SelectedValue);
                oRTN_Service.insertRTNDetail(lblID.Text, rtnData.Rows[0]["RTN_REQUEST_NO"].ToString(), DDLAddItem.SelectedValue, item.Rows[0]["PRICE"].ToString(), txtAddItemQty.Text, ViewState["user"].ToString());
                BindData(lblID.Text);
            }
            else
            {

            }
        }
    }
}