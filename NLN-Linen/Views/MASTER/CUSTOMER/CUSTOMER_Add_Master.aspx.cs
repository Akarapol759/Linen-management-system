﻿using NLN_Linen.Services;
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
    public partial class CUSTOMER_Add_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        CUSTOMER_GROUP_Service oCUSTOMER_GROUP_Service = new CUSTOMER_GROUP_Service();
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
            oUtility.DDL(DDLBusinessUnit, buData, "BU_SHORT_NAME", "BU_CODE", "Please select");
            DataTable groupData = oUtility.getActive(oCUSTOMER_GROUP_Service.getData(), "GROUP_STATUS", "A");
            oUtility.DDL(DDLCustomerGroup, groupData, "GROUP_NAME", "GROUP_CODE", "Please select");
            //User
            if (role != 1)
            {
                DDLBusinessUnit.SelectedValue = buCode;
                DDLBusinessUnit.Enabled = false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DDLBusinessUnit.SelectedValue != "-1" && DDLCustomerGroup.SelectedValue != "-1"
                && !string.IsNullOrEmpty(txtCustomerCode.Text.Trim()) 
                && !string.IsNullOrEmpty(txtCustomerName.Text))
            {
                DataTable customer = oCUSTOMER_Service.checkCusCode(DDLBusinessUnit.SelectedValue, txtCustomerCode.Text.Trim());
                if (customer.Rows.Count > 0)
                {
                    oUtility.MsgAlert(this, "This customer is duplicate");
                }
                else
                {
                    oCUSTOMER_Service.insertCUSTOMER(DDLBusinessUnit.SelectedValue, DDLCustomerGroup.SelectedValue, txtCustomerCode.Text.Trim(), txtCustomerName.Text, "A", ViewState["user"].ToString());
                    oUtility.MsgAlert(this, "Success");
                    Response.Redirect("/Views/MASTER/CUSTOMER/CUSTOMER_Master.aspx");
                }
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

        protected void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(DDLBusinessUnit.SelectedValue) && !string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                DataTable customer = oCUSTOMER_Service.checkCusCode(DDLBusinessUnit.SelectedValue, txtCustomerCode.Text.Trim());
                if(customer.Rows.Count > 0)
                {
                    lblDup.Text = "Duplicate";
                    lblDup.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblDup.Text = "Available";
                    lblDup.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                oUtility.MsgAlert(this, "Please selected business unit");
            }
        }
    }
}