using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.LCS
{
    public partial class LCS_Add_Request : System.Web.UI.Page
    {
        //Service
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        LCS_Service oLCS_Service = new LCS_Service();
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
            DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(buCode), "CUS_STATUS", "A");
            DataTable cycleData = oUtility.getActive(oCYCLE_Service.getData(buCode, "1"), "CYCLE_STATUS", "A");
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
            oUtility.DDL(DDLCycle, cycleData, "CYCLE_TIME", "CYCLE_ID", "Please select");
            //Administrator
            if (role != 1)
            {
                DDLBusinessUnit.Enabled = false;
                DDLBusinessUnit.SelectedValue = buCode;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1" && DDLCustomer.SelectedValue != "-1" && DDLCycle.SelectedValue != "-1")
            {
                DataTable check = oLCS_Service.checkPar(DDLBusinessUnit.SelectedValue, DDLCustomer.SelectedValue);
                if (check.Rows.Count > 0)
                {
                    DataTable result = oLCS_Service.insertLCS(DDLBusinessUnit.SelectedValue, DDLCustomer.SelectedValue, DDLCycle.SelectedValue, ViewState["user"].ToString(), "O");
                    string idLCS = result.Rows[0]["insertId"].ToString();
                    Response.Redirect("/Views/LCS/LCS_Edit_Request.aspx?id=" + idLCS);
                }
                else
                {
                    oUtility.MsgAlert(this, "Not found par setup in department");
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }
        protected void DDLBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1")
            {
                DataTable cusData = oUtility.getActive(oCUSTOMER_Service.getData(DDLBusinessUnit.SelectedValue), "CUS_STATUS", "A");
                oUtility.DDL(DDLCustomer, cusData, "CUS_NAME", "CUS_CODE", "Please select");
                DataTable cycleData = oUtility.getActive(oCYCLE_Service.getData(DDLBusinessUnit.SelectedValue, "1"), "CYCLE_STATUS", "A");
                oUtility.DDL(DDLCycle, cycleData, "CYCLE_TIME", "CYCLE_ID", "Please select");
            }
            else
            {
                oUtility.MsgAlert(this, "Please select business unit");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/LCS/LCS_Request.aspx");
        }
    }
}