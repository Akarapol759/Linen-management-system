using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.SOILED
{
    public partial class SOILED_Request : System.Web.UI.Page
    {
        //Service
        SOILED_Service oSOILED_Service = new SOILED_Service();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
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
                        Session["user"] = ticket.Name.ToString();
                        if (ticket.Version == 1)
                        {
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            Session["bu"] = ticket.UserData.ToString();
                            BindData(Session["bu"].ToString(), ticket.Version);
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
            DataTable laundryData = oUtility.getActive(oLAUNDRY_Service.getData(buCode), "LAUNDRY_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
            oUtility.DDL(DDLLaundry, laundryData, "LAUNDRY_NAME", "LAUNDRY_CODE", "Please select");

            //Administrator
            if (role != 1)
            {
                DDLBusinessUnit.Enabled = false;
                DDLBusinessUnit.SelectedValue = buCode;
            }
            ViewState["SoiledReqData"] = oSOILED_Service.getData(buCode);
            GridView.DataSource = (DataTable)ViewState["SoiledReqData"];
            GridView.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detailRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string status = row.Cells[5].Text;
                if (status == "Complete")
                {
                    Response.Redirect("/Views/SOILED/SOILED_Detail_Request.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
                else
                {
                    Response.Redirect("/Views/SOILED/SOILED_Edit_Request.aspx?id=" + GridView.DataKeys[index].Value.ToString());
                }
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SOILED/SOILED_Add_Request.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query = from i in oSOILED_Service.getData(Session["bu"].ToString()).AsEnumerable()
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
                if (DDLLaundry.SelectedValue != "-1")
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("LAUNDRY_CODE").Equals(DDLLaundry.SelectedValue)
                            select i;
                }
                if (!string.IsNullOrEmpty(txtSoiledRequestNo.Text.Trim()))
                {
                    query = from i in query.AsEnumerable()
                            where i.Field<string>("SOILED_REQUEST_NO").Contains(txtSoiledRequestNo.Text.Trim())
                            select i;
                }
                if (query.Any())
                {
                    ViewState["SoiledReqData"] = query.CopyToDataTable();
                    GridView.DataSource = (DataTable)ViewState["SoiledReqData"];
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
            DDLBusinessUnit.SelectedValue = "-1";
            txtSoiledRequestNo.Text = "";
            DDLCustomer.SelectedValue = "-1";
            DDLLaundry.SelectedValue = "-1";
        }

        protected void DDLBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(DDLBusinessUnit.SelectedValue), "CUS_STATUS", "A");
                DataTable laundryData = oUtility.getActive(oLAUNDRY_Service.getData(DDLBusinessUnit.SelectedValue), "LAUNDRY_STATUS", "A");
                oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
                oUtility.DDL(DDLLaundry, laundryData, "LAUNDRY_NAME", "LAUNDRY_CODE", "Please select");
            }
            else
            {
                oUtility.MsgAlert(this, "Please select business unit");
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            GridView.DataSource = (DataTable)ViewState["SoiledReqData"];
            GridView.DataBind();
        }
    }
}