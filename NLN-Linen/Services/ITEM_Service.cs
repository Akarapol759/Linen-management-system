using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class ITEM_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ITEM_ID, A.BU_CODE, A.CATEGORY_ID, C.CATEGORY_NAME, A.ITEM_CODE, A.ITEM_NAME, A.ITEM_DESC, A.ITEM_WEIGHT, A.ITEM_COST, A.ITEM_STATUS, CASE WHEN A.ITEM_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as ITEM_STATUS_NAME, B.BU_SHORT_NAME FROM NLN_M_ITEM A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CATEGORY C on A.CATEGORY_ID = C.CATEGORY_ID ORDER BY A.ITEM_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ITEM_ID, A.BU_CODE, A.CATEGORY_ID, C.CATEGORY_NAME, A.ITEM_CODE, A.ITEM_NAME, A.ITEM_DESC, A.ITEM_WEIGHT, A.ITEM_COST, A.ITEM_STATUS, CASE WHEN A.ITEM_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as ITEM_STATUS_NAME, B.BU_SHORT_NAME FROM NLN_M_ITEM A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CATEGORY C on A.CATEGORY_ID = C.CATEGORY_ID WHERE A.BU_CODE = '" + buCode + "' ORDER BY A.ITEM_NAME ASC" });
            }
                return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataById(string itemId)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_ITEM WHERE ITEM_ID = '" + itemId + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkItemCode(string itemCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_ITEM WHERE ITEM_CODE = '" + itemCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkItemPrice(string itemCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT B.PRICE FROM NLN_M_ITEM A inner join NLN_M_PRICE B on A.BU_CODE = B.BU_CODE and A.CATEGORY_ID = B.CATEGORY_ID WHERE A.ITEM_CODE = '" + itemCode + "' AND (GETDATE() between B.PRICE_EFFECTIVE_DATE_FROM and B.PRICE_EFFECTIVE_DATE_TO)" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertITEM(string buCode, string categortId, string itemCode, string itemName, string itemDescription, string itemWeigh, string itemCost, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_ITEM (BU_CODE, CATEGORY_ID, ITEM_CODE, ITEM_NAME, ITEM_DESC, ITEM_WEIGHT, ITEM_COST, ITEM_STATUS, ITEM_CREATE_BY, ITEM_CREATE_DATE, ITEM_UPDATE_BY, ITEM_UPDATE_DATE) VALUES ('" + buCode + "', '" + categortId + "', '" + itemCode + "', '" + itemName + "', '" + itemDescription + "', " + itemWeigh + ", " + itemCost + ", '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateITEM(string itemId, string itemName, string itemDescription, string itemWeigh, string itemCost, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_ITEM SET ITEM_NAME = '" + itemName + "', ITEM_DESC = '" + itemDescription + "', ITEM_WEIGHT = " + itemWeigh + ", ITEM_COST = " + itemCost + ", ITEM_STATUS = '" + status + "', ITEM_UPDATE_BY = '" + user + "', ITEM_UPDATE_DATE = GETDATE() WHERE ITEM_ID = '" + itemId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable checkItemName(string itemName)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_ITEM WHERE ITEM_NAME = '" + itemName + "'" });
            return this.ExecuteReader(this.SQL);
        }
    }
}