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
    public partial class ITEM_Edit_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CATEGORY_Service oCATEGORY_Service = new CATEGORY_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        BU_Service oBU_Service = new BU_Service();

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
            //Administrator
            DataTable buData = oUtility.getActive(oBU_Service.getData(null), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            DataTable categoryData = oUtility.getActive(oCATEGORY_Service.getData(), "CATEGORY_STATUS", "A");
            oUtility.DDL(DDLCategory, categoryData, "CATEGORY_NAME", "CATEGORY_ID", "Please select");

            DataTable itemData = oITEM_Service.getDataById(id);
            lblItemId.Text = itemData.Rows[0]["ITEM_ID"].ToString();
            DDLBusinessUnit.SelectedValue = itemData.Rows[0]["BU_CODE"].ToString();
            DDLCategory.SelectedValue = itemData.Rows[0]["CATEGORY_ID"].ToString();
            txtItemCode.Text = itemData.Rows[0]["ITEM_CODE"].ToString();
            txtItemName.Text = itemData.Rows[0]["ITEM_NAME"].ToString();
            txtItemDescription.Text = itemData.Rows[0]["ITEM_DESC"].ToString();
            txtItemWeight.Text = itemData.Rows[0]["ITEM_WEIGHT"].ToString();
            txtItemCost.Text = itemData.Rows[0]["ITEM_COST"].ToString();
            RdStatus.Text = itemData.Rows[0]["ITEM_STATUS"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemName.Text) && !string.IsNullOrEmpty(txtItemWeight.Text.Trim()))
            {
                oITEM_Service.updateITEM(lblItemId.Text, txtItemName.Text, txtItemDescription.Text, txtItemWeight.Text, txtItemCost.Text, RdStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/ITEM/ITEM_Master.aspx");
        }
    }
}