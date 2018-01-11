using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class CYCLE_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.CYCLE_ID, A.BU_CODE, B.BU_SHORT_NAME, A.CYCLE_TYPE, CASE WHEN A.CYCLE_TYPE = '1' THEN 'Shelf Count' WHEN A.CYCLE_TYPE = '2' THEN 'Delivery' WHEN A.CYCLE_TYPE = '3' THEN 'Pickup' ELSE 'To Laundry' END as CYCLE_TYPE_NAME, A.CYCLE_TIME, A.CYCLE_STATUS, CASE WHEN A.CYCLE_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as CYCLE_STATUS_NAME FROM NLN_M_CYCLE A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE ORDER BY A.BU_CODE, A.CYCLE_TIME ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.CYCLE_ID, A.BU_CODE, B.BU_SHORT_NAME, A.CYCLE_TYPE, CASE WHEN A.CYCLE_TYPE = '1' THEN 'Shelf Count' WHEN A.CYCLE_TYPE = '2' THEN 'Delivery' WHEN A.CYCLE_TYPE = '3' THEN 'Pickup' ELSE 'To Laundry' END as CYCLE_TYPE_NAME, A.CYCLE_TIME, A.CYCLE_STATUS, CASE WHEN A.CYCLE_STATUS = 'A' THEN 'Active' ELSE 'Inactive' END as CYCLE_STATUS_NAME FROM NLN_M_CYCLE A INNER JOIN NLN_M_BU B on A.BU_CODE = B.BU_CODE WHERE A.BU_CODE = '" + buCode + "' ORDER BY A.BU_CODE, A.CYCLE_TYPE, A.CYCLE_TIME ASC " });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getData(string buCode, string cycleType)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT CYCLE_ID, CYCLE_TIME, CYCLE_STATUS, CASE WHEN CYCLE_STATUS = 'C' THEN 'Active' ELSE 'Inactive' END as CYCLE_STATUS_NAME FROM NLN_M_CYCLE WHERE CYCLE_TYPE = '" + cycleType + "'" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT CYCLE_ID, CYCLE_TIME, CYCLE_STATUS, CASE WHEN CYCLE_STATUS = 'C' THEN 'Active' ELSE 'Inactive' END as CYCLE_STATUS_NAME FROM NLN_M_CYCLE WHERE BU_CODE = '" + buCode + "' AND CYCLE_TYPE = '" + cycleType + "'" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataById(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_CYCLE WHERE CYCLE_ID = '" + id + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkCycle(string buCode, string cycleType, string time)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_CYCLE WHERE BU_CODE = '" + buCode + "' and CYCLE_TYPE = '" + cycleType + "' and CYCLE_TIME = '" + time + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertCycle(string buCode, string cycleType, string cycleTime, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_M_CYCLE (BU_CODE, CYCLE_TYPE, CYCLE_TIME, CYCLE_STATUS, CYCLE_CREATE_BY, CYCLE_CREATE_DATE, CYCLE_UPDATE_BY, CYCLE_UPDATE_DATE) VALUES ('" + buCode + "', '" + cycleType + "', '" + cycleTime + "', '" + status + "', '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateCYCLE(string cycleId, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_CYCLE SET CYCLE_STATUS = '" + status + "', CYCLE_UPDATE_BY = '" + user + "', CYCLE_UPDATE_DATE = GETDATE() WHERE CYCLE_ID = '" + cycleId + "' ;" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}