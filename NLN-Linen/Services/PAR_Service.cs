using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class PAR_Service : Service
    {
        string SQL;
        public DataTable getData(string cusCode, string buCode)
        {
            if(string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.PAR_ID, A.ITEM_CODE, B.ITEM_NAME, A.PAR_QTY, A.PAR_STATUS, CASE WHEN A.PAR_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as PAR_STATUS_NAME FROM NLN_M_PAR A INNER JOIN NLN_M_ITEM B on A.ITEM_CODE = B.ITEM_CODE WHERE A.CUS_CODE = '" + cusCode + "' ORDER BY B.ITEM_NAME asc" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.PAR_ID, A.ITEM_CODE, B.ITEM_NAME, A.PAR_QTY, A.PAR_STATUS, CASE WHEN A.PAR_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as PAR_STATUS_NAME FROM NLN_M_PAR A INNER JOIN NLN_M_ITEM B on A.ITEM_CODE = B.ITEM_CODE WHERE A.BU_CODE = '" + buCode + "' and A.CUS_CODE = '" + cusCode + "' ORDER BY B.ITEM_NAME asc" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkItemDuplicate(string cusCode, string itemCode, string buCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_PAR WHERE CUS_CODE = '" + cusCode + "' AND ITEM_CODE = '" + itemCode + "' AND BU_CODE = '" + buCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertPAR(string cusCode, string itemCode, string buCode, string parQty, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_PAR (CUS_CODE, ITEM_CODE, BU_CODE, PAR_QTY, PAR_STATUS, PAR_CREATE_BY, PAR_CREATE_DATE, PAR_UPDATE_BY, PAR_UPDATE_DATE) VALUES ('" + cusCode + "', '" + itemCode + "', '" + buCode + "', '" + parQty + "', '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updatePAR(string parId, string parQty, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_PAR SET PAR_QTY = '" + parQty + "', PAR_STATUS = '" + status + "', PAR_UPDATE_BY = '" + user + "', PAR_UPDATE_DATE = GETDATE() WHERE PAR_ID = '" + parId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}