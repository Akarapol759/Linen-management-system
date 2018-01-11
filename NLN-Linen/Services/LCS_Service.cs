using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class LCS_Service : Service
    {
        string SQL;
        public DataTable getData(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.LCS_REQUEST_NO, A.LCS_REQUEST_STATUS, CASE WHEN A.LCS_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.LCS_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as LCS_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, D.CYCLE_TIME as CYCLE_TIME_SHELF_COUNT, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_LCS A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE INNER JOIN NLN_M_CYCLE D ON A.CYCLE_ID_SHELF_COUNT = D.CYCLE_ID WHERE A.CREATE_DATE BETWEEN DATEADD(day,-30,GETDATE()) AND GETDATE() ORDER BY A.ID DESC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.LCS_REQUEST_NO, A.LCS_REQUEST_STATUS, CASE WHEN A.LCS_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.LCS_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as LCS_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, D.CYCLE_TIME as CYCLE_TIME_SHELF_COUNT, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_LCS A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE INNER JOIN NLN_M_CYCLE D ON A.CYCLE_ID_SHELF_COUNT = D.CYCLE_ID WHERE  A.BU_CODE = '" + buCode + "' AND A.CREATE_DATE BETWEEN DATEADD(day,-30,GETDATE()) AND GETDATE() ORDER BY A.ID DESC" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataSearch(string buCode)
        {
            if (string.IsNullOrEmpty(buCode))
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.LCS_REQUEST_NO, A.LCS_REQUEST_STATUS, CASE WHEN A.LCS_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.LCS_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as LCS_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, D.CYCLE_TIME as CYCLE_TIME_SHELF_COUNT, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_LCS A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE INNER JOIN NLN_M_CYCLE D ON A.CYCLE_ID_SHELF_COUNT = D.CYCLE_ID ORDER BY A.ID DESC" });
            }
            else
            {
                this.SQL = string.Concat(new string[] { "SELECT A.ID, A.BU_CODE, A.CUS_CODE, A.LCS_REQUEST_NO, A.LCS_REQUEST_STATUS, CASE WHEN A.LCS_REQUEST_STATUS = 'C' THEN 'Complete' WHEN A.LCS_REQUEST_STATUS = 'N' THEN 'Cancel' ELSE 'On Process' END as LCS_REQUEST_STATUS_NAME, B.BU_SHORT_NAME, B.BU_FULL_NAME, C.CUS_NAME, D.CYCLE_TIME as CYCLE_TIME_SHELF_COUNT, CONVERT(VARCHAR(10),A.CREATE_DATE,103) as CREATE_DATE FROM NLN_R_LCS A INNER JOIN NLN_M_BU B ON A.BU_CODE = B.BU_CODE INNER JOIN NLN_M_CUSTOMER C ON A.CUS_CODE = C.CUS_CODE AND C.CUS_STATUS = 'A' AND A.BU_CODE = C.BU_CODE INNER JOIN NLN_M_CYCLE D ON A.CYCLE_ID_SHELF_COUNT = D.CYCLE_ID WHERE  A.BU_CODE = '" + buCode + "' ORDER BY A.ID DESC" });
            }
            return this.ExecuteReader(this.SQL);
        }
        public DataTable checkPar(string buCode, string cusCode)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM NLN_M_PAR WHERE BU_CODE = '" + buCode + "' and CUS_CODE = '" + cusCode + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable insertLCS(string buCode, string cusCode, string cycleId, string user, string status)
        {
            this.SQL = string.Concat(new string[] { "INSERT INTO NLN_R_LCS (BU_CODE, CUS_CODE, CYCLE_ID_SHELF_COUNT, LCS_REQUEST_TIME, LCS_REQUEST_STATUS, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE) VALUES ('" + buCode + "', '" + cusCode + "', '" + cycleId + "', CONVERT(time,GETDATE(),108), 'O', '" + user + "', GETDATE(), '" + user + "', GETDATE());SELECT SCOPE_IDENTITY() AS insertId;" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataLCS(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM VW_GET_LCS_DATA WHERE ID = '" + id + "'" });
            return this.ExecuteReader(this.SQL);
        }
        public DataTable getDataLCSDetail(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT A.ID, B.ITEM_NAME, A.PAR_QTY, A.SHELF_COUNT_QTY, A.ISSUE_QTY, A.SHORT_QTY, A.OVER_QTY FROM NLN_R_LCS_DETAIL A LEFT JOIN NLN_M_ITEM B on A.ITEM_CODE = B.ITEM_CODE WHERE A.LCS_REQUEST_ID = '" + id + "' ORDER BY B.ITEM_NAME ASC" });
            return this.ExecuteReader(this.SQL);
        }
        public void updateDataLCSDetail(string id, string shelftCount, string issueQty, string shortIssue, string overIssue, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_LCS_DETAIL SET SHELF_COUNT_QTY = " + shelftCount + " , ISSUE_QTY = " + issueQty + " , SHORT_QTY = " + shortIssue+ " , OVER_QTY = " + overIssue+ " ,  UPDATE_BY = " + user +" , UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateLCSStatus(string id, string cycleDelivery, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_LCS SET LCS_REQUEST_STATUS = 'C', CYCLE_ID_DELIVERY = '" + cycleDelivery + "' , UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public void updateLCSStatus(string id, string user)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_R_LCS SET LCS_REQUEST_STATUS = 'N', UPDATE_BY = '" + user + "', UPDATE_DATE = GETDATE() WHERE ID = '" + id + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
        public DataTable getDataIsseList(string id)
        {
            this.SQL = string.Concat(new string[] { "SELECT * FROM VW_GET_ISSUE_LIST WHERE ID = '" + id + "'" });
            return this.ExecuteReader(this.SQL);
        }
    }
}