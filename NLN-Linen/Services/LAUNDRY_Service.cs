using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class LAUNDRY_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.LAUNDRY_ID, A.BU_CODE, B.BU_SHORT_NAME, A.LAUNDRY_CODE, A.LAUNDRY_NAME, A.LAUNDRY_STATUS, CASE WHEN A.LAUNDRY_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as LAUNDRY_STATUS_NAME FROM NLN_M_LAUNDRY A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE ORDER BY LAUNDRY_NAME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.LAUNDRY_ID, A.BU_CODE, B.BU_SHORT_NAME, A.LAUNDRY_CODE, A.LAUNDRY_NAME, A.LAUNDRY_STATUS, CASE WHEN A.LAUNDRY_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as LAUNDRY_STATUS_NAME FROM NLN_M_LAUNDRY A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.BU_CODE = '" + buCode + "'" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkLaundryCode(string buCode, string laundryCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_LAUNDRY WHERE BU_CODE = '" + buCode + "' and LAUNDRY_CODE = '" + laundryCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertLAUNDRY(string buCode, string laundryCode, string laundryName, string laundryDesc, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_LAUNDRY (BU_CODE, LAUNDRY_CODE, LAUNDRY_NAME, LAUNDRY_DESC, LAUNDRY_STATUS, LAUNDRY_CREATE_BY, LAUNDRY_CREATE_DATE, LAUNDRY_UPDATE_BY, LAUNDRY_UPDATE_DATE) VALUES ('" + buCode + "', '" + laundryCode + "', '" + laundryName + "', '" + laundryDesc + "', '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateLAUNDRY(string laundryId, string laundryName, string laundryDesc, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_LAUNDRY SET LAUNDRY_NAME = '" + laundryName + "', LAUNDRY_DESC = '" + laundryDesc + "', LAUNDRY_STATUS = '" + status + "', LAUNDRY_UPDATE_BY = '" + user + "', LAUNDRY_UPDATE_DATE = GETDATE() WHERE LAUNDRY_ID = '" + laundryId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable getDataById(string laundryId)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_LAUNDRY A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.LAUNDRY_ID = '" + laundryId + "'" });
            return this.ExecuteReader(this.SQL);
        }
    }
}