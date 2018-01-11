using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.PAR
{
    public partial class PAR_Master : System.Web.UI.Page
    {
        //Service
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        Utility oUtility = new Utility();

        //Static Variable
        //static string user;
        //static string bu = null;
        //static int role = 0;
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
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            //User
            if (role != 1)
            {
                DDLBusinessUnit.SelectedValue = buCode;
                DDLBusinessUnit.Enabled = false;
            }
            ViewState["CustomerData"] = oCUSTOMER_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["CustomerData"];
            GridView.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/CUSTOMER/CUSTOMER_Add_Master.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bu = null;
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oCUSTOMER_Service.getData(bu).AsEnumerable()
                                             select i;
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("BU_CODE").Contains(DDLBusinessUnit.SelectedValue)
                        select i;
            }
            if (DDLCustomerStatus.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("CUS_STATUS").Equals(DDLCustomerStatus.SelectedValue)
                        select i;
            }
            if(!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                query = from i in ((DataTable)ViewState["CustomerData"]).AsEnumerable()
                        where i.Field<string>("CUS_CODE").Contains(txtCustomerCode.Text.Trim())
                        select i;
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                query = from i in ((DataTable)ViewState["CustomerData"]).AsEnumerable()
                        where i.Field<string>("CUS_NAME").Contains(txtCustomerName.Text)
                        select i;
            }
            if (query.Any())
            {
                ViewState["CustomerData"] = query.CopyToDataTable();
                GridView.DataSource = (DataTable)ViewState["CustomerData"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Data not found");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            txtCustomerName.Text = "";
            DDLCustomerStatus.SelectedValue = "-1";
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string aa = GridView.DataKeys[index].Value.ToString();
                string cusCode = row.Cells[4].Text;
                string buCode = row.Cells[2].Text;
                Response.Redirect("/Views/MASTER/PAR/PAR_Edit_Master.aspx?id=" + GridView.DataKeys[index].Value.ToString() + "&cusCode=" + cusCode + "&buCode=" + buCode);
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["CustomerData"];
            GridView.DataBind();
        }
    }
}