using NLN_Linen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Views.MASTER.PAR
{
    public partial class PAR_Edit_Master : System.Web.UI.Page
    {
        Utility oUtility = new Utility();
        BU_Service oBU_Service = new BU_Service();
        ITEM_Service oITEM_Service = new ITEM_Service();
        CUSTOMER_Service oCUSTOMER_Service = new CUSTOMER_Service();
        PAR_Service oPAR_Service = new PAR_Service();

        //Static Variable
        //static string user;
        //static string id = null;
        //static string bu = null;
        //static string cusCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = btnSave.UniqueID;
            if (!Page.IsPostBack)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    if (!string.IsNullOrEmpty(ticket.Name.ToString()))
                    {
                        ViewState["user"] = ticket.Name.ToString();
                        ViewState["id"] = Request.QueryString["id"];
                        ViewState["cusCode"] = Request.QueryString["cusCode"];
                        ViewState["bu"] = Request.QueryString["buCode"];
                        if (ticket.Version == 1)
                        {
                            BindData(ViewState["id"].ToString(), ViewState["cusCode"].ToString(), ViewState["bu"].ToString());
                        }
                        else
                        {
                            BindData(ViewState["id"].ToString(), ViewState["cusCode"].ToString(), ViewState["bu"].ToString());
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
        protected void BindData(string id, string cusCode, string buCode)
        {
            //Administrator
            DataTable itemData = oUtility.getActive(oITEM_Service.getData(buCode), "ITEM_STATUS", "A");
            oUtility.DDL(DDLAddItem, itemData, "ITEM_NAME", "ITEM_CODE", "Please select");
            oUtility.DDL(DDLEditItem, itemData, "ITEM_NAME", "ITEM_CODE", "Please select");

            DataTable cusData = oCUSTOMER_Service.getDataById(id);
            lblCusId.Text = cusData.Rows[0]["CUS_ID"].ToString();
            lblBusinessUnit.Text = cusData.Rows[0]["BU_SHORT_NAME"].ToString();
            lblBuCode.Text = cusData.Rows[0]["BU_CODE"].ToString();
            lblCusCode.Text = cusData.Rows[0]["CUS_CODE"].ToString();
            lblCusName.Text = cusData.Rows[0]["CUS_NAME"].ToString();
            RdStatus.Text = cusData.Rows[0]["CUS_STATUS"].ToString();

            ViewState["parData"] = oPAR_Service.getData(cusCode, buCode);
            GridView.DataSource = (DataTable)ViewState["parData"];
            GridView.DataBind();

        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView.Rows[index];
                string status = row.Cells[5].Text;
                if (status == "Active")
                {
                    DataTable dt = (DataTable)ViewState["parData"];
                    IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                                 where i.Field<Int64>("PAR_ID").Equals(Convert.ToInt64(GridView.DataKeys[index].Value.ToString()))
                                                 select i;
                    if (query.Any())
                    {
                        DataTable data = query.CopyToDataTable<DataRow>();
                        lblEditParId.Text = GridView.DataKeys[index].Value.ToString();
                        DDLEditItem.SelectedValue = data.Rows[0]["ITEM_CODE"].ToString();
                        txtEditParQty.Text = data.Rows[0]["PAR_QTY"].ToString();
                        RdEditStatus.SelectedValue = data.Rows[0]["PAR_STATUS"].ToString();
                        //Show Edit Par
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#editModal').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    oUtility.MsgAlert(this, "Cannot edit");
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/MASTER/PAR/PAR_Master.aspx");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1" && !string.IsNullOrEmpty(txtAddParQty.Text))
            {
                oPAR_Service.insertPAR(lblCusCode.Text, DDLAddItem.SelectedValue, lblBuCode.Text, txtAddParQty.Text.Trim(), "A", ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
                BindData(ViewState["id"].ToString(), ViewState["cusCode"].ToString(), ViewState["bu"].ToString());
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void btnEditRecord_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditParQty.Text))
            {
                oPAR_Service.updatePAR(lblEditParId.Text, txtEditParQty.Text.Trim(), RdEditStatus.SelectedValue, ViewState["user"].ToString());
                oUtility.MsgAlert(this, "Success");
                BindData(ViewState["id"].ToString(), ViewState["cusCode"].ToString(), ViewState["bu"].ToString());
            }
            else
            {
                oUtility.MsgAlert(this, "Please put data all fields");
            }
        }

        protected void DDLAddItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DDLAddItem.SelectedValue != "-1")
            {
                DataTable dup = oPAR_Service.checkItemDuplicate(lblCusCode.Text, DDLAddItem.SelectedValue, lblBuCode.Text);
                if (dup.Rows.Count > 0)
                {
                    lblDup.Visible = true;
                    lblDup.Text = "Duplicated";
                    lblDup.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblDup.Visible = true;
                    lblDup.Text = "Available";
                    lblDup.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}