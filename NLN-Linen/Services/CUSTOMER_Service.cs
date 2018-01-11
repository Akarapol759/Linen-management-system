using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class CUSTOMER_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.CUS_ID, A.BU_CODE, B.BU_SHORT_NAME, A.CUS_CODE, A.CUS_NAME, A.CUS_STATUS, CASE WHEN A.CUS_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as CUS_STATUS_NAME FROM NLN_M_CUSTOMER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE ORDER BY CUS_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.CUS_ID, A.BU_CODE, B.BU_SHORT_NAME, A.CUS_CODE, A.CUS_NAME, A.CUS_STATUS, CASE WHEN A.CUS_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as CUS_STATUS_NAME FROM NLN_M_CUSTOMER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.BU_CODE = '" + buCode + "' ORDER BY CUS_NAME ASC" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataById(string cusId)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_CUSTOMER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.CUS_ID = '" + cusId + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkCusCode(string buCode, string cusCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_CUSTOMER WHERE BU_CODE = '" + buCode + "' and CUS_CODE = '" + cusCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertCUSTOMER(string buCode, string cusGroup, string cusCode, string cusName, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_CUSTOMER (BU_CODE, GROUP_CODE, CUS_CODE, CUS_NAME, CUS_STATUS, CUS_CREATE_BY, CUS_CREATE_DATE, CUS_UPDATE_BY, CUS_UPDATE_DATE) VALUES ('" + buCode + "', '" + cusGroup + "', '" + cusCode + "', '" + cusName + "', '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCUSTOMER(string cusId, string cusGroup, string cusName, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_CUSTOMER SET GROUP_CODE = '" + cusGroup + "', CUS_NAME = '" + cusName + "', CUS_STATUS = '" + status + "', CUS_UPDATE_BY = '" + user + "', CUS_UPDATE_DATE = GETDATE() WHERE CUS_ID = '" + cusId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}