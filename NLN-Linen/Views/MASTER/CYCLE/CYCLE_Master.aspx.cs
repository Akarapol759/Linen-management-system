using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.CYCLE
{
    public partial class CYCLE_Master : System.Web.UI.Page
    {
        //Service
        BU_Service oBU_Service = new BU_Service();
        CYCLE_Service oCYCLE_Service = new CYCLE_Service();
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

            ViewState["CycleData"] = oCYCLE_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["CycleData"];
            GridView.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/CYCLE/CYCLE_Add_Master.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bu = null;
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oCYCLE_Service.getData(bu).AsEnumerable()
                                         select i;
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("BU_CODE").Contains(DDLBusinessUnit.SelectedValue)
                        select i;
            }
            if (DDLCycleType.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("CYCLE_TYPE").Equals(DDLCycleType.SelectedValue)
                        select i;
            }
            if (DDLCycleStatus.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("CYCLE_STATUS").Equals(DDLCycleStatus.SelectedValue)
                        select i;
            }
            if (query.Any())
            {
                ViewState["CycleData"] = query.CopyToDataTable();
                GridView.DataSource = (DataTable)ViewState["CycleData"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Data not found");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            DDLCycleType.SelectedValue = "-1";
            DDLCycleStatus.SelectedValue = "-1";
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                //string status = (row.FindControl("LCS_REQUEST_STATUS") as Label).Text;
                Response.Redirect("/Views/MASTER/CYCLE/CYCLE_Edit_Master.aspx?id=" + GridView.DataKeys[index].Value.ToString());
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["CycleData"];
            GridView.DataBind();
        }
    }
}