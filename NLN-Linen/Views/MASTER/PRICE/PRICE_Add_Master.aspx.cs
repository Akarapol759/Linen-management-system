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
    public partial class PRICE_Add_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        CATEGORY_Service oCATEGORY_Service = new CATEGORY_Service();
        PRICE_Service oPRICE_Service = new PRICE_Service();
        BU_Service oBU_Service = new BU_Service();

        //Static Variable
        //static string user;
        //static string bu = null;
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
            //Administrator
            DataTable buData = oUtility.getActive(oBU_Service.getData(buCode), "BU_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            DataTable categoryData = oUtility.getActive(oCATEGORY_Service.getData(), "CATEGORY_STATUS", "A");
            oUtility.DDL(DDLCategory, categoryData, "CATEGORY_NAME", "CATEGORY_ID", "Please select");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1" && DDLCategory.SelectedValue != "-1"
                && !string.IsNullOrEmpty(txtPrice.Text.Trim()) && !string.IsNullOrEmpty(txtEffectiveDateFrom.Text.Trim())
                && !string.IsNullOrEmpty(txtEffectiveDateTo.Text.Trim()))
            {
                DateTime dateFrom = DateTime.ParseExact(txtEffectiveDateFrom.Text.Trim(), "dd/MM/yyyy", null);
                DateTime dateTo = DateTime.ParseExact(txtEffectiveDateTo.Text.Trim(), "dd/MM/yyyy", null);
                if (dateFrom > dateTo)
                {
                    oUtility.MsgAlert(this, "Plese select effective date to more than effective date from.");
                }
                else
                {
                    oPRICE_Service.insertPrice(DDLBusinessUnit.SelectedValue, DDLCategory.SelectedValue, txtPrice.Text.Trim(), txtEffectiveDateFrom.Text.Trim(), txtEffectiveDateTo.Text.Trim(), "A", ViewState["user"].ToString());
                    oUtility.MsgAlert(this, "Success");
                    Response.Redirect("/Views/MASTER/PRICE/PRICE_Master.aspx");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/PRICE/PRICE_Master.aspx");
        }

        protected void txtEffectiveDateFrom_TextChanged(object sender, EventArgs e)
        {
            if(DDLBusinessUnit.SelectedValue != "-1" && DDLCategory.SelectedValue != "-1")
            {
                DataTable price = oPRICE_Service.checkPrice(DDLBusinessUnit.SelectedValue, DDLCategory.SelectedValue);
                if(price.Rows.Count > 0)
                {
                    txtEffectiveDateFrom.Text = price.Rows[0]["PRICE_EFFECTIVE_DATE_TO"].ToString();
                    txtEffectiveDateFrom.Enabled = false;
                }
            }
            else
            {
                txtEffectiveDateFrom.Enabled = true;
                txtEffectiveDateFrom.Text = "";
                oUtility.MsgAlert(this, "Please select businesss unit and category.");
            }
        }

        protected void DDLCategory_TextChanged(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable price = oPRICE_Service.checkPrice(DDLBusinessUnit.SelectedValue, DDLCategory.SelectedValue);
                if (price.Rows.Count > 0)
                {
                    txtEffectiveDateFrom.Text = price.Rows[0]["PRICE_EFFECTIVE_DATE_TO"].ToString();
                    txtEffectiveDateFrom.Enabled = false;
                }
            }
            else
            {
                txtEffectiveDateFrom.Enabled = true;
                txtEffectiveDateFrom.Text = "";
                oUtility.MsgAlert(this, "Please select businesss unit.");
            }
        }
    }
}