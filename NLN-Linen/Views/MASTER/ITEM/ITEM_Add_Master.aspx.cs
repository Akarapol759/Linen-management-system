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
    public partial class ITEM_Add_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CATEGORY_Service oCATEGORY_Service = new CATEGORY_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        BU_Service oBU_Service = new BU_Service();

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
                            BindData(null, ticket.Version);
                        }
                        else
                        {
                            BindData(ticket.UserData.ToString(), ticket.Version);
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
            txtItemCost.Text = "0";
            //Administrator
            DataTable buData = oUtility.getActive(oBU_Service.getData(buCode), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            DataTable categoryData = oUtility.getActive(oCATEGORY_Service.getData(), "CATEGORY_STATUS", "A");
            oUtility.DDL(DDLCategory, categoryData, "CATEGORY_NAME", "CATEGORY_ID", "Please select");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1" && DDLCategory.SelectedValue != "-1" && !string.IsNullOrEmpty(txtItemCode.Text.Trim()) 
                && !string.IsNullOrEmpty(txtItemName.Text) && !string.IsNullOrEmpty(txtItemWeight.Text.Trim()))
            {
                DataTable item = oITEM_Service.checkItemCode(txtItemCode.Text.Trim());
                if (item.Rows.Count > 0)
                {
                    oUtility.MsgAlert(this, "Duplicate");
                }
                else
                {
                    oITEM_Service.insertITEM(DDLBusinessUnit.SelectedValue, DDLCategory.SelectedValue, txtItemCode.Text.Trim(), txtItemName.Text, txtItemDescription.Text, txtItemWeight.Text, txtItemCost.Text, "A", ViewState["user"].ToString());
                    oUtility.MsgAlert(this, "Success");
                }
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

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                DataTable item = oITEM_Service.checkItemCode(txtItemCode.Text.Trim());
                if(item.Rows.Count > 0)
                {
                    lblDup.Text = "Duplicated";
                    lblDup.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblDup.Text = "Available";
                    lblDup.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}