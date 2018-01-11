using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NLN_Linen.Services
{
    public class Utility : Service
    {
        public void DDL(DropDownList DDL, DataTable Dt, string strText, string strValue, string showText)
        {
            DDL.Items.Clear();
            if (Dt.Rows.Count > 0)
            {
                DDL.DataSource = Dt;
                DDL.DataTextField = strText;
                DDL.DataValueField = strValue;
                DDL.DataBind();
            }
            DDL.Items.Insert(0, new ListItem(" " + showText + " ", "-1"));
        }

        public DataTable getActive(DataTable dt, string fieldName, string parameter)
        {
            IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                         where i.Field<string>(fieldName).Equals(parameter)
                                         select i;
            if (query.Any())
            {
                dt = query.CopyToDataTable<DataRow>();
            }
            return dt;
        }

        public void MsgAlert(Control control,string msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("alert('"+ msg +"');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(control, this.GetType(), "Alert", sb.ToString(), false);
        }
    }
}