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
    public partial class RTN_Request : System.Web.UI.Page
    {
        //Service
        RTN_Service oRTN_Service = new RTN_Service();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        Utility oUtility = new Utility();
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
                        ViewState["user"] = ticket.Name.ToString();
                        if (ticket.Version == 1)
                        {
                            ViewState["bu"] = "0";
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            ViewState["bu"] = ticket.UserData.ToString();
                            BindData(ViewState["bu"].ToString(), ticket.Version);
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
            DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(buCode), "CUS_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
            //Administrator
            if (role != 1)
            {
                DDLBusinessUnit.Enabled = false;
                DDLBusinessUnit.SelectedValue = buCode;
            }
            ViewState["ReqData"] = oRTN_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["ReqData"];
            GridView.DataBind();
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string status = row.Cells[6].Text;
                if (status == "Complete")
                {
                    Response.Redirect("/Views/RTN/RTN_Detail_Request.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else if (status == "Cancel")
                {
                    Response.Redirect("/Views/RTN/RTN_Detail_Request.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/RTN/RTN_Edit_Request.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bu = null;
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oRTN_Service.getData(bu).AsEnumerable()
                                         select i;
            if (query.Any())
            {
                if (DDLBusinessUnit.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("BU_CODE").Equals(DDLBusinessUnit.SelectedValue)
                            select i;
                }
                if (DDLCustomer.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("CUS_CODE").Equals(DDLCustomer.SelectedValue)
                            select i;
                }
                if (!string.IsNullOrEmpty(txtSearchRTNRequestNo.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("RTN_REQUEST_NO").Contains(txtSearchRTNRequestNo.Text.Trim())
                            select i;
                }
                if (DDLRTNStatus.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("RTN_REQUEST_STATUS").Equals(DDLRTNStatus.SelectedValue)
                            select i;
                }
                if (query.Any())
                {
                    ViewState["ReqData"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["ReqData"];
                    GridView.DataBind();
                }
                else
                {
                    oUtility.MsgAlert(this, "Data not found");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data or select criteria");
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchRTNRequestNo.Text = "";
            DDLCustomer.SelectedValue = "-1";
        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/RTN/RTN_Add_Request.aspx");
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["ReqData"];
            GridView.DataBind();
        }

        protected void DDLBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(DDLBusinessUnit.SelectedValue), "CUS_STATUS", "A");
                oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
            }
            else
            {
                oUtility.MsgAlert(this, "Please select business unit");
            }
        }

    }
}