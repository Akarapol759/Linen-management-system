using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class CUSTOMER_GROUP_Service : Service
    {
        string SQL;
        public DataTable getData()
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_CUSTOMER_GROUP ORDER BY GROUP_CODE ASC" });
            return this.ExecuteReader(this.SQL);
        }
    }
}