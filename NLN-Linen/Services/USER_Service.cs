using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class USER_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.USER_ID, A.BU_CODE, B.BU_SHORT_NAME, A.USER_CODE, A.USER_NAME, A.USER_STATUS, CASE WHEN A.USER_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as USER_STATUS_NAME FROM NLN_M_USER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE ORDER BY USER_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.USER_ID, A.BU_CODE, B.BU_SHORT_NAME, A.USER_CODE, A.USER_NAME, A.USER_STATUS, CASE WHEN A.USER_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as USER_STATUS_NAME FROM NLN_M_USER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.BU_CODE = '" + buCode + "'" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataById(string userId)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_USER A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.USER_ID = '" + userId + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkUserCode(string buCode, string userCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_USER WHERE BU_CODE = '" + buCode + "' and USER_CODE = '" + userCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertUSER(string buCode, string userCode, string userName, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_USER (BU_CODE, USER_CODE, USER_NAME, USER_STATUS, USER_CREATE_BY, USER_CREATE_DATE, USER_UPDATE_BY, USER_UPDATE_DATE) VALUES ('" + buCode + "', '" + userCode + "', '" + userName + "', '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCUSTOMER(string userId, string userName, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_USER SET USER_NAME = '" + userName + "', USER_STATUS = '" + status + "', USER_UPDATE_BY = '" + user + "', USER_UPDATE_DATE = GETDATE() WHERE USER_ID = '" + userId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}