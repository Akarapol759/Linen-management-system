using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class SOILED_Service : Service
    {
        string SQL;

        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.LAUNDRY_CODE, A.SOILED_REQUEST_NO, B.BU_SHORT_NAME, D.LAUNDRY_NAME, A.SOILED_REQUEST_STATUS, CASE WHEN A.SOILED_REQUEST_STATUS = 'C' THEN 'Complete' ELSE 'On Process' END as SOILED_REQUEST_STATUS_NAME FROM NLN_R_SOILED A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_LAUNDRY D on A.BU_CODE = D.BU_CODE and A.LAUNDRY_CODE = D.LAUNDRY_CODE ORDER BY A.SOILED_REQUEST_NO ASC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.LAUNDRY_CODE, A.SOILED_REQUEST_NO, B.BU_SHORT_NAME, D.LAUNDRY_NAME, A.SOILED_REQUEST_STATUS, CASE WHEN A.SOILED_REQUEST_STATUS = 'C' THEN 'Complete' ELSE 'On Process' END as SOILED_REQUEST_STATUS_NAME FROM NLN_R_SOILED A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_LAUNDRY D on A.BU_CODE = D.BU_CODE and A.LAUNDRY_CODE = D.LAUNDRY_CODE WHERE  A.BU_CODE = '" + buCode + "' ORDER BY A.SOILED_REQUEST_NO ASC" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable insertSOILED(string buCode, string laundryCode, string cyclePickup, string cycleLaundry, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_R_SOILED (BU_CODE, LAUNDRY_CODE, CYCLE_ID_PICKUP, CYCLE_ID_TO_LAUNDRY, SOILED_REQUEST_STATUS, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE) VALUES ('" + buCode + "', '" + laundryCode + "', '" + cyclePickup + "', '" + cycleLaundry + "', 'O', '" + user + "', GETDATE(), '" + user + "', GETDATE());SELECT SCOPE_IDENTITY() AS insertId;" });
            return this.ExecuteReader(this.SQL);
        }
        public void updateSOILED(string soiledId, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_SOILED SET SOILED_REQUEST_STATUS = '" + status + "', UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + soiledId + "';" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void insertSOILEDDetail(string soiledId, string soiledNo, string cusCode, string typeBag, string qty, string weight, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_R_SOILED_DETAIL (SOILED_REQUEST_ID, SOILED_REQUEST_NO, CUS_CODE, SOILED_REQUEST_TYPE, SOILED_REQUEST_QTY, SOILED_REQUEST_WEIGHT, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE) VALUES ('" + soiledId + "', '" + soiledNo + "', '" + cusCode + "', '" + typeBag + "', " + qty + ", " + weight + ", '" + user + "', GETDATE(), '" + user + "', GETDATE());SELECT SCOPE_IDENTITY() AS insertId;" });
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable getDataDetail(string soiledId)
        {
            this.SQL = string.Concat(new string[] { "SELECT B.ID, B.CUS_CODE, C.CUS_NAME, B.SOILED_REQUEST_NO, B.SOILED_REQUEST_QTY, B.SOILED_REQUEST_WEIGHT, B.SOILED_REQUEST_TYPE, CASE WHEN B.SOILED_REQUEST_TYPE = '1' THEN 'Green' ELSE 'Red' END as SOILED_REQUEST_TYPE_NAME FROM NLN_R_SOILED A INNER JOIN NLN_R_SOILED_DETAIL B on A.ID = B.SOILED_REQUEST_ID INNER JOIN NLN_M_CUSTOMER C on A.BU_CODE = C.BU_CODE AND B.CUS_CODE = C.CUS_CODE  WHERE B.SOILED_REQUEST_ID = '" + soiledId + "' ORDER BY B.ID ASC" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataSOILED(string soiledId)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM VW_GET_SOILED_DATA WHERE ID = '" + soiledId + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public void deleteSOILEDDetail(string Id)
        {
            this.SQL = string.Concat(new string[] { "DELETE NLN_R_SOILED_DETAIL WHERE ID = '" + Id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}