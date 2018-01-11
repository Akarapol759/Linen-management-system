using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.CUSTOMER
{
    public partial class CUSTOMER_Edit_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();

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
            DataTable groupData = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "GROUP_STATUS", "A");
            oUtility.DDL(DDLCustomerGroup, groupData, "GROUP_NAME", "GROUP_CODE", "Please select");

            DataTable cusData = oCUSTOMER_Service.getDataById(id);
            lblCusId.Text = cusData.Rows[0]["CUS_ID"].ToString();
            DDLBusinessUnit.SelectedValue = cusData.Rows[0]["BU_CODE"].ToString();
            DDLCustomerGroup.SelectedValue = cusData.Rows[0]["GROUP_CODE"].ToString();
            txtCusCode.Text = cusData.Rows[0]["CUS_CODE"].ToString();
            txtCusName.Text = cusData.Rows[0]["CUS_NAME"].ToString();
            RdStatus.Text = cusData.Rows[0]["CUS_STATUS"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusName.Text))
            {
                //oCUSTOMER_Service.updateCUSTOMER(lblCusId.Text, txtCusName.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                oCUSTOMER_Service.updateCUSTOMER(lblCusId.Text, DDLCustomerGroup.SelectedValue, txtCusName.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
                Response.Redirect("/Views/MASTER/CUSTOMER/CUSTOMER_Master.aspx");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/CUSTOMER/CUSTOMER_Master.aspx");
        }
    }
}