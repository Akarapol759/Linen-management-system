using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class LNCT_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.LNCT_STOCK_ID, B.BU_FULL_NAME, A.ITEM_CODE, C.ITEM_NAME, A.LNCT_STOCK_QTY FROM NLN_S_LNCT A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_ITEM C ON A.ITEM_CODE = C.ITEM_CODE ORDER BY C.ITEM_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.LNCT_STOCK_ID, B.BU_FULL_NAME, A.ITEM_CODE, C.ITEM_NAME, A.LNCT_STOCK_QTY FROM NLN_S_LNCT A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_ITEM C ON A.ITEM_CODE = C.ITEM_CODE WHERE BU_CODE = '" + buCode + "' ORDER BY C.ITEM_NAME ASC" });
            }
            return this.ExecuteReader(this.SQL);
        }
    }
}