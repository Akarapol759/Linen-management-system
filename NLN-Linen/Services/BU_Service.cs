using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class BU_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT BU_CODE, BU_SHORT_NAME, BU_STATUS FROM NLN_M_BU ORDER BY BU_SHORT_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT BU_CODE, BU_SHORT_NAME, BU_STATUS FROM NLN_M_BU WHERE BU_CODE = '" + buCode + "'" });
            }
            return this.ExecuteReader(this.SQL);
        }
    }
}