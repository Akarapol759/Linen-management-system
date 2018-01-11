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
    public partial class CYCLE_Edit_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CYCLE_Service oCYCLE_Service = new CYCLE_Service();

        //Static Variable
        //static string user;
        //static string bu = null;
        //static string id = null;
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
                            BindData(null, Request.QueryString["id"]);
                        }
                        else
                        {
                            BindData(ticket.UserData.ToString(), Request.QueryString["id"]);
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
        protected void BindData(string buCode, string id)
        {
            DataTable buData = oUtility.getActive(oBU_Service.getData(buCode), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");

            DataTable cycleData = oCYCLE_Service.getDataById(id);
            lblCycleId.Text = cycleData.Rows[0]["CYCLE_ID"].ToString();
            DDLBusinessUnit.SelectedValue = cycleData.Rows[0]["BU_CODE"].ToString();
            DDLCycleType.Text = cycleData.Rows[0]["CYCLE_TYPE"].ToString();
            lblTime.Text = cycleData.Rows[0]["CYCLE_TIME"].ToString();
            RdStatus.Text = cycleData.Rows[0]["CYCLE_STATUS"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            oCYCLE_Service.updateCYCLE(lblCycleId.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
            oUtility.MsgAlert(this, "Success");
            Response.Redirect("/Views/MASTER/CYCLE/CYCLE_Master.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/CYCLE/CYCLE_Master.aspx");
        }
    }
}