using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.LAUNDRY
{
    public partial class LAUNDRY_Master : System.Web.UI.Page
    {
        //Service
        BU_Service oBU_Service = new BU_Service();
        LAUNDRY_Service oLAUNDRY_Service = new LAUNDRY_Service();
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

            ViewState["LaundryData"] = oLAUNDRY_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["LaundryData"];
            GridView.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/LAUNDRY/LAUNDRY_Add_Master.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bu = null;
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oLAUNDRY_Service.getData(bu).AsEnumerable()
                                         select i;
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("BU_CODE").Contains(DDLBusinessUnit.SelectedValue)
                        select i;
            }
            if (DDLLaundryStatus.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("LAUNDRY_STATUS").Equals(DDLLaundryStatus.SelectedValue)
                        select i;
            }
            if(!string.IsNullOrEmpty(txtLaundryCode.Text.Trim()))
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("LAUNDRY_CODE").Contains(txtLaundryCode.Text.Trim())
                        select i;
            }
            if (!string.IsNullOrEmpty(txtLaundryName.Text))
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("LAUNDRY_NAME").Contains(txtLaundryName.Text)
                        select i;
            }
            if (query.Any())
            {
                ViewState["LaundryData"] = query.CopyToDataTable();
                GridView.DataSource = (DataTable)ViewState["LaundryData"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Data not found");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLaundryCode.Text = "";
            txtLaundryName.Text = "";
            DDLLaundryStatus.SelectedValue = "-1";
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                //string status = (row.FindControl("LCS_REQUEST_STATUS") as Label).Text;
                Response.Redirect("/Views/MASTER/LAUNDRY/LAUNDRY_Edit_Master.aspx?id=" + GridView.DataKeys[index].Value.ToString());
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["LaundryData"];
            GridView.DataBind();
        }
    }
}