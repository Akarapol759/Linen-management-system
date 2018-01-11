using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class CATEGORY_Service : Service
    {
        string SQL;
        public DataTable getData()
        {
            this.SQL = string.Concat(new string[] { "SELECT CATEGORY_ID, CATEGORY_NAME, CATEGORY_STATUS FROM NLN_M_CATEGORY ORDER BY CATEGORY_ID ASC" });
            return this.ExecuteReader(this.SQL);
        }
    }
}