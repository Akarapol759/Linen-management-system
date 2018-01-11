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
    public partial class SOILED_Edit_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        SOILED_Service oSOILED_Service = new SOILED_Service();
        LAUNDRY_Service oLAUNDRY_Service = new LAUNDRY_Service();
        CYCLE_Service oCYCLE_Service = new CYCLE_Service();

        //Static Variable
        //static string user;
        //static string bu = null;
        //static int role = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnSave.UniqueID;
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
            DataTable soiledData = oSOILED_Service.getDataSOILED(id);

            lblBusinessUnit.Text = soiledData.Rows[0]["BU_FULL_NAME"].ToString();
            lblLaundry.Text = soiledData.Rows[0]["LAUNDRY_NAME"].ToString();
            lblCyclePickup.Text = soiledData.Rows[0]["CYCLE_TIME_PICKUP"].ToString();
            lblCycleToLaundry.Text = soiledData.Rows[0]["CYCLE_TIME_TO_LAUNDRY"].ToString();
            lblSOILEDReqNo.Text = soiledData.Rows[0]["SOILED_REQUEST_NO"].ToString();
            lblID.Text = soiledData.Rows[0]["ID"].ToString();

            DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(soiledData.Rows[0]["BU_CODE"].ToString()), "CUS_STATUS", "A");
            oUtility.DDL(DDLAddCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");

            ViewState["detail"] = oSOILED_Service.getDataDetail(id);
            GridView.DataSource = (DataTable)ViewState["detail"];
            GridView.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            oSOILED_Service.updateSOILED(lblID.Text, RdSOILEDStatus.SelectedValue, ViewState["user"].ToString());
            Response.Redirect("/Views/SOILED/SOILED_Detail_Request.aspx?id=" + lblID.Text);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SOILED/SOILED_Request.aspx");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            if(DDLAddCustomer.SelectedValue != "-1" && !string.IsNullOrEmpty(txtAddQty.Text.Trim()) && !string.IsNullOrEmpty(txtAddWeight.Text.Trim()))
            {
                oSOILED_Service.insertSOILEDDetail(lblID.Text, lblSOILEDReqNo.Text, DDLAddCustomer.SelectedValue, DDLAddTypeBag.SelectedValue, txtAddQty.Text.Trim(), txtAddWeight.Text.Trim(), ViewState["user"].ToString());
                ViewState["detail"] = oSOILED_Service.getDataDetail(lblID.Text);
                GridView.DataSource = (DataTable)ViewState["detail"];
                GridView.DataBind();
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string id = GridView.DataKeys[index].Value.ToString();
                DataTable detail = (DataTable)ViewState["detail"];
                IEnumerable<DataRow> query = from i in detail.AsEnumerable()
                                             where i.Field<Int64>("Id").Equals(Convert.ToInt64(id))
                                             select i;
                if (query.Any())
                {
                    DataTable dt = query.CopyToDataTable<DataRow>();
                    oSOILED_Service.deleteSOILEDDetail(dt.Rows[0]["ID"].ToString());
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("alert('Not found data');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", sb.ToString(), false);
                }
            }
        }
    }
}