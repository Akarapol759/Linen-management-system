using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class RTN_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.RTN_REQUEST_NO, A.RTN_REQUEST_STATUS, CASE WHEN A.RTN_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.RTN_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as RTN_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_RTN A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE ORDER BY A.ID DESC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.RTN_REQUEST_NO, A.RTN_REQUEST_STATUS, CASE WHEN A.RTN_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.RTN_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as RTN_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_RTN A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE WHERE  A.BU_CODE = '" + buCode + "' ORDER BY A.ID DESC" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataRTN(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM VW_GET_RTN_DATA WHERE ID = '" + id + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataRTNDetail(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT A.ID, B.ITEM_NAME, A.RTN_QTY FROM NLN_R_RTN_DETAIL A LEFT JOIN NLN_M_ITEM B on A.ITEM_CODE = B.ITEM_CODE WHERE A.RTN_REQUEST_ID = '" + id + "' ORDER BY A.ID ASC" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable insertRTN(string buCode, string cusCode, string user, string status)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_R_RTN (BU_CODE, CUS_CODE, RTN_REQUEST_TIME, RTN_REQUEST_STATUS, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE) VALUES ('" + buCode + "', '" + cusCode + "', CONVERT(time,GETDATE(),108), 'O', '" + user + "', GETDATE(), '" + user + "', GETDATE());SELECT SCOPE_IDENTITY() AS insertId;" });
            return this.ExecuteReader(this.SQL);
        }
        public void insertRTNDetail(string id, string regNo, string itemCode, string itemPrice, string rtnQty, string user)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_R_RTN_DETAIL VALUES ('" + id + "', '" + regNo + "', '" + itemCode + "', " + itemPrice + ", " + rtnQty + ", '" + user + "', GETDATE(), '" + user + "', GETDATE());" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateDataRTNDetail(string id, string rtnQty, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_RTN_DETAIL SET RTN_QTY = " + rtnQty + " ,  UPDATE_BY = " + user + " , UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateRTN(string id, string rtnName, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_RTN SET RTN_REQUEST_NAME = '" + rtnName + "', UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateRTNStatus(string id, string rtnName, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_RTN SET RTN_REQUEST_NAME = '" + rtnName + "', RTN_REQUEST_STATUS = '" + status + "', UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateRTNCancel(string id, string status, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_RTN SET RTN_REQUEST_STATUS = '" + status + "', UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable getDataRtnList(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM VW_GET_RTN_LIST WHERE ID = '" + id + "'" });
            return this.ExecuteReader(this.SQL);
        }
    }
}