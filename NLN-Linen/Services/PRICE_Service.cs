using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class PRICE_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode, string effectiveDate)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                if (string.IsNullOrEmpty(effectiveDate))
                {
                    this.SQL = string.Concat(new string[] { "SELECT A.PRICE_ID, A.BU_CODE, C.BU_SHORT_NAME, A.CATEGORY_ID, B.CATEGORY_NAME, A.PRICE, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_FROM,103) as PRICE_EFFECTIVE_DATE_FROM, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_TO,103) as PRICE_EFFECTIVE_DATE_TO FROM NLN_M_PRICE A INNER JOIN NLN_M_CATEGORY B on A.CATEGORY_ID = B.CATEGORY_ID INNER JOIN NLN_M_BU C on A.BU_CODE = C.BU_CODE ORDER BY A.PRICE_ID DESC" });
                }
                else
                {
                    this.SQL = string.Concat(new string[] { "SELECT A.PRICE_ID, A.BU_CODE, C.BU_SHORT_NAME, A.CATEGORY_ID, B.CATEGORY_NAME, A.PRICE, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_FROM,103) as PRICE_EFFECTIVE_DATE_FROM, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_TO,103) as PRICE_EFFECTIVE_DATE_TO FROM NLN_M_PRICE A INNER JOIN NLN_M_CATEGORY B on A.CATEGORY_ID = B.CATEGORY_ID INNER JOIN NLN_M_BU C on A.BU_CODE = C.BU_CODE WHERE to_date(to_char('" + effectiveDate + "', 'DD/MM/YYYY'),'DD/MM/YYYY') BETWEEN to_date(PRICE_EFFECTIVE_DATE_FROM,'DD/MM/YYYY') AND to_date(PRICE_EFFECTIVE_DATE_TO,'DD/MM/YYYY') ORDER BY A.PRICE_ID DESC" });
                }
            }
            else
            {
                if (string.IsNullOrEmpty(effectiveDate))
                {
                    this.SQL = string.Concat(new string[] { "SELECT A.PRICE_ID, A.BU_CODE, C.BU_SHORT_NAME, A.CATEGORY_ID, B.CATEGORY_NAME, A.PRICE, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_FROM,103) as PRICE_EFFECTIVE_DATE_FROM, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_TO,103) as PRICE_EFFECTIVE_DATE_TO FROM NLN_M_PRICE A INNER JOIN NLN_M_CATEGORY B on A.CATEGORY_ID = B.CATEGORY_ID INNER JOIN NLN_M_BU C on A.BU_CODE = C.BU_CODE WHERE BU_CODE = '" + buCode + "' ORDER BY A.PRICE_ID DESC" });
                }
                else
                {
                    this.SQL = string.Concat(new string[] { "SELECT A.PRICE_ID, A.BU_CODE, C.BU_SHORT_NAME, A.CATEGORY_ID, B.CATEGORY_NAME, A.PRICE, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_FROM,103) as PRICE_EFFECTIVE_DATE_FROM, CONVERT(VARCHAR(10),A.PRICE_EFFECTIVE_DATE_TO,103) as PRICE_EFFECTIVE_DATE_TO FROM NLN_M_PRICE A INNER JOIN NLN_M_CATEGORY B on A.CATEGORY_ID = B.CATEGORY_ID INNER JOIN NLN_M_BU C on A.BU_CODE = C.BU_CODE WHERE BU_CODE = '" + buCode + "' AND to_date(to_char('" + effectiveDate + "', 'DD/MM/YYYY'),'DD/MM/YYYY') BETWEEN to_date(PRICE_EFFECTIVE_DATE_FROM,'DD/MM/YYYY') AND to_date(PRICE_EFFECTIVE_DATE_TO,'DD/MM/YYYY') ORDER BY A.PRICE_ID DESC" });
                }
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkPrice(string buCode, string categoryId)
        {
            this.SQL = string.Concat(new string[] { "SELECT PRICE_ID, BU_CODE, CATEGORY_ID, CONVERT(VARCHAR(24),DATEADD(day,1,PRICE_EFFECTIVE_DATE_TO),103) as PRICE_EFFECTIVE_DATE_TO FROM NLN_M_PRICE WHERE BU_CODE = '" + buCode + "' AND CATEGORY_ID = '" + categoryId + "' ORDER BY PRICE_ID DESC" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertPrice(string buCode, string categortId, string price, string dateFrom, string dateTo, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_PRICE (BU_CODE, CATEGORY_ID, PRICE, PRICE_EFFECTIVE_DATE_FROM, PRICE_EFFECTIVE_DATE_TO, PRICE_STATUS, PRICE_CREATE_BY, PRICE_CREATE_DATE, PRICE_UPDATE_BY, PRICE_UPDATE_DATE) VALUES ('" + buCode + "', '" + categortId + "', '" + price + "', CONVERT(Date, '" + dateFrom + "', 105), CONVERT(Date, '" + dateTo + "', 105), '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}