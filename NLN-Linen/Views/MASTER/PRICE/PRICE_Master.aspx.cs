using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.PRICE
{
    public partial class PRICE_Master : System.Web.UI.Page
    {
        //Service
        PRICE_Service oPRICE_Service = new PRICE_Service();
        CATEGORY_Service oCATEGORY_Service = new CATEGORY_Service();
        BU_Service oBU_Service = new BU_Service();
        Utility oUtility = new Utility();

        //Static Variable
        //static string user;
        //static string bu = null;
        //static string effectiveDate = null;
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
            DataTable categoryData = oUtility.getActive(oCATEGORY_Service.getData(), "CATEGORY_STATUS", "A");
            oUtility.DDL(DDLCatergory, categoryData, "CATEGORY_NAME", "CATEGORY_ID", "Please select");
            //User
            if (role != 1)
            {
                DDLBusinessUnit.SelectedValue = buCode;
                DDLBusinessUnit.Enabled = false;
            }
            ViewState["PriceData"] = oPRICE_Service.getData(buCode, null);
            GridView.DataSource = (DataTable)ViewState["PriceData"];
            GridView.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/PRICE/PRICE_Add_Master.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string effectiveDate = null;
            string bu = null;
            if (!string.IsNullOrEmpty(txtEffectiveDate.Text.Trim()))
            {
                DateTime date = DateTime.ParseExact(txtEffectiveDate.Text.Trim(), "dd/MM/yyyy", null);
                effectiveDate = date.ToString("dd/MM/yyyy");
            }
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oPRICE_Service.getData(bu, effectiveDate).AsEnumerable()
                                         select i;
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("BU_CODE").Contains(DDLBusinessUnit.SelectedValue)
                        select i;
            }
            if (DDLCatergory.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("CATEGORY_ID").Contains(DDLCatergory.SelectedValue)
                        select i;
            }
            if (query.Any())
            {
                ViewState["PriceData"] = query.CopyToDataTable();
                GridView.DataSource = (DataTable)ViewState["PriceData"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Data not found");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtItemCode.Text = "";
            //txtItemName.Text = "";
            //DDLItemStatus.SelectedValue = "-1";
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                //string status = (row.FindControl("LCS_REQUEST_STATUS") as Label).Text;
                Response.Redirect("/Views/MASTER/PRICE/PRICE_Edit_Master.aspx?id=" + GridView.DataKeys[index].Value.ToString());
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["PriceData"];
            GridView.DataBind();
        }
    }
}