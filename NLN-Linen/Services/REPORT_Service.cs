using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class REPORT_Service : Service
    {
        string SQL;
        public DataTable getData_VW_RPT_SUMMARY(string buCode, string cusCode, string stdDate, string endDate)
        {
            if (cusCode == "-1")
            {
                //this.SQL = string.Concat(new string[] { "SELECT * FROM VW_RPT_SUMMARY WHERE BU_CODE = '" + buCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105)" });
                this.SQL = string.Concat(new string[] { "SELECT A.BU_CODE, C.BU_SHORT_NAME, A.CUS_CODE, D.CUS_NAME, B.ITEM_CODE, E.ITEM_NAME, B.PAR_QTY, E.ITEM_WEIGHT, SUM(B.ISSUE_QTY) AS ISSUE_QTY, CONVERT(VARCHAR(10), A.CREATE_DATE, 103) AS CREATE_DATE, B.ITEM_PRICE AS PRICE, D.GROUP_CODE, G.GROUP_NAME FROM dbo.NLN_R_LCS AS A LEFT OUTER JOIN dbo.NLN_R_LCS_DETAIL AS B ON A.ID = B.LCS_REQUEST_ID LEFT OUTER JOIN dbo.NLN_M_BU AS C ON A.BU_CODE = C.BU_CODE LEFT OUTER JOIN dbo.NLN_M_CUSTOMER AS D ON A.CUS_CODE = D.CUS_CODE AND A.BU_CODE = D.BU_CODE LEFT OUTER JOIN dbo.NLN_M_ITEM AS E ON A.BU_CODE = E.BU_CODE AND B.ITEM_CODE = E.ITEM_CODE LEFT OUTER JOIN dbo.NLN_M_CYCLE AS F ON A.CYCLE_ID_SHELF_COUNT = F.CYCLE_ID AND F.CYCLE_STATUS = 'A' LEFT OUTER JOIN dbo.NLN_M_CUSTOMER_GROUP AS G ON D.GROUP_CODE = G.GROUP_CODE WHERE  (A.LCS_REQUEST_STATUS = 'C') AND A.BU_CODE = '" + buCode + "' and CONVERT(Date, A.CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105) GROUP BY A.BU_CODE, C.BU_SHORT_NAME, A.CUS_CODE, D.CUS_NAME, A.LCS_REQUEST_NO, B.ITEM_CODE, E.ITEM_NAME, B.PAR_QTY, E.ITEM_WEIGHT, A.CREATE_DATE, B.ITEM_PRICE, D.GROUP_CODE, G.GROUP_NAME" });
            }
            else
            {
                //this.SQL = string.Concat(new string[] { "SELECT * FROM VW_RPT_SUMMARY WHERE BU_CODE = '" + buCode + "' and CUS_CODE = '" + cusCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105)" });
                this.SQL = string.Concat(new string[] { "SELECT A.BU_CODE, C.BU_SHORT_NAME, A.CUS_CODE, D.CUS_NAME, B.ITEM_CODE, E.ITEM_NAME, B.PAR_QTY, E.ITEM_WEIGHT, SUM(B.ISSUE_QTY) AS ISSUE_QTY, CONVERT(VARCHAR(10), A.CREATE_DATE, 103) AS CREATE_DATE, B.ITEM_PRICE AS PRICE, D.GROUP_CODE, G.GROUP_NAME FROM dbo.NLN_R_LCS AS A LEFT OUTER JOIN dbo.NLN_R_LCS_DETAIL AS B ON A.ID = B.LCS_REQUEST_ID LEFT OUTER JOIN dbo.NLN_M_BU AS C ON A.BU_CODE = C.BU_CODE LEFT OUTER JOIN dbo.NLN_M_CUSTOMER AS D ON A.CUS_CODE = D.CUS_CODE AND A.BU_CODE = D.BU_CODE LEFT OUTER JOIN dbo.NLN_M_ITEM AS E ON A.BU_CODE = E.BU_CODE AND B.ITEM_CODE = E.ITEM_CODE LEFT OUTER JOIN dbo.NLN_M_CYCLE AS F ON A.CYCLE_ID_SHELF_COUNT = F.CYCLE_ID AND F.CYCLE_STATUS = 'A' LEFT OUTER JOIN dbo.NLN_M_CUSTOMER_GROUP AS G ON D.GROUP_CODE = G.GROUP_CODE WHERE  (A.LCS_REQUEST_STATUS = 'C') AND A.BU_CODE = '" + buCode + "' and A.CUS_CODE = '" + cusCode + "' and CONVERT(Date, A.CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105) GROUP BY A.BU_CODE, C.BU_SHORT_NAME, A.CUS_CODE, D.CUS_NAME, A.LCS_REQUEST_NO, B.ITEM_CODE, E.ITEM_NAME, B.PAR_QTY, E.ITEM_WEIGHT, A.CREATE_DATE, B.ITEM_PRICE, D.GROUP_CODE, G.GROUP_NAME" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getData_VW_RPT_USAGE(string buCode, string cusCode, string stdDate, string endDate)
        {
            if (cusCode == "-1")
            {
                this.SQL = string.Concat(new string[] { "SELECT * FROM VW_RPT_USAGE WHERE BU_CODE = '" + buCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105)" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT * FROM VW_RPT_USAGE WHERE BU_CODE = '" + buCode + "' and CUS_CODE = '" + cusCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105)" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getData_VW_RPT_BILLING(string buCode, string cusCode, string stdDate, string endDate)
        {
            this.SQL = string.Concat(new string[] { "exec SP_BILLING_REPORT '" + buCode + "', '" + cusCode + "', '" + stdDate + "', '" + endDate + "';" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getData_SP_REPLENISHMENT_REORT(string buCode, string stdDate, string endDate)
        {
            this.SQL = string.Concat(new string[] { "exec SP_REPLENISHMENT_REPORT '" + stdDate + "', '" + endDate + "', '" + buCode + "';" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getData_VW_RPT_RETURN(string buCode, string cusCode, string stdDate, string endDate)
        {
            if (cusCode == "-1")
            {
                this.SQL = string.Concat(new string[] { "SELECT BU_CODE, BU_SHORT_NAME, CUS_CODE, CUS_NAME, ITEM_CODE, ITEM_NAME, ITEM_WEIGHT, RTN_QTY, TOTAL_WEIGHT, AMOUNT, CREATE_DATE, PRICE, CATEGORY_ID, CATEGORY_NAME, GROUP_CODE, GROUP_NAME FROM VW_RPT_RETURN WHERE BU_CODE = '" + buCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105) ORDER BY CONVERT(Date, CREATE_DATE, 105) asc" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT BU_CODE, BU_SHORT_NAME, CUS_CODE, CUS_NAME, ITEM_CODE, ITEM_NAME, ITEM_WEIGHT, RTN_QTY, TOTAL_WEIGHT, AMOUNT, CREATE_DATE, PRICE, CATEGORY_ID, CATEGORY_NAME, GROUP_CODE, GROUP_NAME FROM VW_RPT_RETURN WHERE BU_CODE = '" + buCode + "' and CUS_CODE = '" + cusCode + "' and CONVERT(Date, CREATE_DATE, 105) BETWEEN CONVERT(Date, '" + stdDate + "', 105) and CONVERT(Date, '" + endDate + "', 105) ORDER BY CONVERT(Date, CREATE_DATE, 105) asc" });
            }
            return this.ExecuteReader(this.SQL);
        }
    }
}