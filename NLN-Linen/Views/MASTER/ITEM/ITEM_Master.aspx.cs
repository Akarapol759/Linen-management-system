using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.ITEM
{
    public partial class ITEM_Master : System.Web.UI.Page
    {
        //Service
        ITEM_Service oITEM_Service = new ITEM_Service();
        CATEGORY_Service oCATEGORY_Service = new CATEGORY_Service();
        BU_Service oBU_Service = new BU_Service();
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
            DataTable categoryData = oUtility.getActive(oCATEGORY_Service.getData(), "CATEGORY_STATUS", "A");
            oUtility.DDL(DDLCatergory, categoryData, "CATEGORY_NAME", "CATEGORY_ID", "Please select");
            //User
            lblrole.Text = role.ToString();
            if (role != 1)
            {
                btnRequest.Visible = false;
                DDLBusinessUnit.SelectedValue = buCode;
                DDLBusinessUnit.Enabled = false;
            }
            ViewState["ItemData"] = oITEM_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["ItemData"];
            GridView.DataBind();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/ITEM/ITEM_Add_Master.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bu = null;
            if (ViewState["bu"].ToString() != "0")
            {
                bu = ViewState["bu"].ToString();
            }
            IEnumerable<DataRow> query = from i in oITEM_Service.getData(bu).AsEnumerable()
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
            if (DDLItemStatus.SelectedValue != "-1")
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("ITEM_STATUS").Equals(DDLItemStatus.SelectedValue)
                        select i;
            }
            if(!string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("ITEM_CODE").Contains(txtItemCode.Text.Trim())
                        select i;
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                query = from i in query.AsEnumerable()
                        where i.Field<string>("ITEM_NAME").Contains(txtItemName.Text)
                        select i;
            }
            if (query.Any())
            {
                ViewState["ItemData"] = query.CopyToDataTable();
                GridView.DataSource = (DataTable)ViewState["ItemData"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Data not found");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtItemCode.Text = "";
            txtItemName.Text = "";
            DDLItemStatus.SelectedValue = "-1";
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string status = row.Cells[6].Text;
                if (status == "Active")
                {
                    if (lblrole.Text != "1")
                    {
                        oUtility.MsgAlert(this, "Cannot edit");
                    }
                    else
                    {
                        Response.Redirect("/Views/MASTER/ITEM/ITEM_Edit_Master.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Cannot edit");
                }
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["ItemData"];
            GridView.DataBind();
        }
    }
}