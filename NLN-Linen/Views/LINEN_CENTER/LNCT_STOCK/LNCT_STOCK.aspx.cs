using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.LINEN_CENTER.LNCT_STOCK
{
    public partial class LNCT_STOCK : System.Web.UI.Page
    {
        //Service
        LNCT_Service oLNCT_Service = new LNCT_Service();

        //Static Variable
        static string user;
        static string bucode;
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
                        if (ticket.UserData == "Administrators")
                        {
                            user = ticket.Name.ToString();
                            BindData();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Response.Redirect("/Account/Login.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("/Account/Login.aspx");
            }
        }
        protected void BindData()
        {
            //Admin
            ViewState["dtInvoice"] = oLNCT_Service.getData("I");
            //ViewState["dtSearch"] = (DataTable)ViewState["dtInvoice"];
            dtCustomerGroup = customerGroupService.customerGroupData();
            dtCustomer = customerService.customerData();
            dtCustomerDepartment = customerDepartmentService.customerDepartmentData();
            dtCustomerShipping = customerShippingService.customerShippingData();
            dtItem = itemService.itemData("'I'");
            dtPrice = priceService.priceData();
            dtSale = saleService.saleData();
            dtDeliveryRoute = deliveryRouteService.deliveryRouteData();
            dtWarehouse = warehouseService.warehouseData();
            //Bind Data to GridViewInvoice
            GridViewInvoice.DataSource = (DataTable)ViewState["dtInvoice"];
            GridViewInvoice.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}