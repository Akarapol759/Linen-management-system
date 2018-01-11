using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class LOGIN_Service : Service
    {
        string SQL;
        public void changePassword(string userName, string pass)
        {
            this.SQL = string.Concat(new string[] { "UPDATE NLN_M_LOGIN SET LOGIN_PASSWORD = '" + pass + "' where USER_CODE = '" + userName + "'" });
            this.ExecuteNonQuery(this.SQL);
        }
    }
}