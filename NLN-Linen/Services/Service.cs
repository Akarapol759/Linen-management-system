using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NLN_Linen.Services
{
    public class Service
    {
        SqlCommand Com = new SqlCommand();
        SqlConnection Con = new SqlConnection();
        //string SQL;

        public Service()
        {
            this.Con = new SqlConnection();
            this.Com = new SqlCommand();
            this.Con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public void ExecuteNonQuery(string SQL)
        {
            try
            {
                this.Con.Open();
                this.Com.Connection = this.Con;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = SQL;
                this.Com.ExecuteNonQuery();
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
            }
            this.Con.Close();
            this.Com.Dispose();
        }

        public DataTable ExecuteReader(string SQL)
        {
            DataTable table = new DataTable();
            try
            {
                this.Con.Open();
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL, Con);
                DataSet ds = new DataSet();
                dAdapter.Fill(ds);
                table = ds.Tables[0];
                //this.Com.Connection = this.Con;
                //this.Com.CommandType = CommandType.Text;
                //this.Com.CommandText = SQL;
                //table.Load(this.Com.ExecuteReader());
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
                return table;
            }
            this.Con.Close();
            this.Com.Dispose();
            return table;
        }

        public DataSet ExecuteReaderDataSet(string SQL)
        {
            DataSet ds = new DataSet();
            try
            {
                this.Con.Open();
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL, Con);
                dAdapter.Fill(ds);
                //this.Com.Connection = this.Con;
                //this.Com.CommandType = CommandType.Text;
                //this.Com.CommandText = SQL;
                //table.Load(this.Com.ExecuteReader());
            }
            catch (SqlException exception1)
            {
                this.Con.Close();
                this.Com.Dispose();
                return ds;
            }
            this.Con.Close();
            this.Com.Dispose();
            return ds;
        }
    }
}