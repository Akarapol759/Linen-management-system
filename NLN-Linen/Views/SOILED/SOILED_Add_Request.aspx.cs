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
    public partial class SOILED_Add_Request : System.Web.UI.Page
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
            DataTable buData = oUtility.getActive(oBU_Service.getData(buCode), "BU_STATUS", "A");
            DataTable laundryData = oUtility.getActive(oLAUNDRY_Service.getData(buCode), "LAUNDRY_STATUS", "A");
            DataTable cyclePickupData = oUtility.getActive(oCYCLE_Service.getData(buCode, "3"), "CYCLE_STATUS", "A");
            DataTable cycleLaundryData = oUtility.getActive(oCYCLE_Service.getData(buCode, "4"), "CYCLE_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            oUtility.DDL(DDLLaundry, laundryData, "LAUNDRY_NAME", "LAUNDRY_CODE", "Please select");
            oUtility.DDL(DDLCyclePickup, cyclePickupData, "CYCLE_TIME", "CYCLE_ID", "Please select");
            oUtility.DDL(DDLCycleToLaundry, cycleLaundryData, "CYCLE_TIME", "CYCLE_ID", "Please select");
            //Administrator
            if(role != 1)
            {
                DDLBusinessUnit.Enabled = false;
                DDLBusinessUnit.SelectedValue = buCode;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1" && DDLLaundry.SelectedValue != "-1" && DDLCyclePickup.SelectedValue != "-1"
                && DDLCycleToLaundry.SelectedValue != "-1")
            {
                DataTable result = oSOILED_Service.insertSOILED(DDLBusinessUnit.SelectedValue, DDLLaundry.SelectedValue, DDLCyclePickup.SelectedValue, DDLCycleToLaundry.SelectedValue, ViewState["user"].ToString());
                string idSOILED = result.Rows[0]["insertId"].ToString();
                Response.Redirect("/Views/SOILED/SOILED_Edit_Request.aspx?id=" + idSOILED);
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }
        protected void DDLBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable laundryData = oUtility.getActive(oLAUNDRY_Service.getData(DDLBusinessUnit.SelectedValue), "LAUNDRY_STATUS", "A");
                DataTable cyclePickupData = oUtility.getActive(oCYCLE_Service.getData(DDLBusinessUnit.SelectedValue, "3"), "CYCLE_STATUS", "A");
                DataTable cycleLaundryData = oUtility.getActive(oCYCLE_Service.getData(DDLBusinessUnit.SelectedValue, "4"), "CYCLE_STATUS", "A");
                oUtility.DDL(DDLLaundry, laundryData, "LAUNDRY_NAME", "LAUNDRY_CODE", "Please select");
                oUtility.DDL(DDLCyclePickup, cyclePickupData, "CYCLE_TIME", "CYCLE_ID", "Please select");
                oUtility.DDL(DDLCycleToLaundry, cycleLaundryData, "CYCLE_TIME", "CYCLE_ID", "Please select");
            }
            else
            {
                oUtility.MsgAlert(this, "Please select business unit");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/SOILED/SOILED_Request.aspx");
        }
    }
}