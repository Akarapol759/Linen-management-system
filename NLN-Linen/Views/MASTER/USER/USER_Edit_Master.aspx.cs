using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.USER
{
    public partial class USER_Edit_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        USER_Service oUSER_Service = new USER_Service();

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
            DataTable buData = oUtility.getActive(oBU_Service.getData(null), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");

            DataTable cusData = oUSER_Service.getDataById(id);
            lblUserId.Text = cusData.Rows[0]["USER_ID"].ToString();
            DDLBusinessUnit.SelectedValue = cusData.Rows[0]["BU_CODE"].ToString();
            txtUserCode.Text = cusData.Rows[0]["USER_CODE"].ToString();
            txtUserName.Text = cusData.Rows[0]["USER_NAME"].ToString();
            RdStatus.Text = cusData.Rows[0]["USER_STATUS"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                oUSER_Service.updateCUSTOMER(lblUserId.Text, txtUserName.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
                Response.Redirect("/Views/MASTER/USER/USER_Master.aspx");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/USER/USER_Master.aspx");
        }
    }
}