using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTMSService
{
    public class DataBaseUtility
    {
        public SqlConnection Conn;
        int flag = 0;
        public DataBaseUtility()
        {
        }
        public int SqlConnection()
        {
          //  ConString = System.Web.HttpContext.Current.Application["ConnectionString"].ToString();
            // ConString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConn"].ToString();
            // string ConString = "Data Source=DELL-PC;Initial Catalog=WSMS;Integrated Security=True";
           //
            Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DbConn"].ToString());
            //Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
            flag = 1;
            return flag;
        }
        public void ConnectDb()
        {
            try
            {
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        public void DisconnectDb()
        {
            try
            { Conn.Close(); }
            catch (Exception ex)
            { throw new Exception(); }
        }
        public DataTable GetDataTable(SqlCommand Cmd)
        {
            try
            {
                SqlConnection();
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 0;
                SqlDataAdapter Da = new SqlDataAdapter(Cmd);
                DataTable Dt = new DataTable();
                Da.Fill(Dt);
                Dt.TableName = "dtData";
                Conn.Close();
                return Dt;
            }
            catch (Exception e)
            { throw new Exception(e.Message); }
            finally
            { Conn.Close(); }
        }
        public DataSet GetDataSet(SqlCommand Cmd)
        {
            try
            {
                SqlConnection();
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 0;
                SqlDataAdapter Da = new SqlDataAdapter(Cmd);
                DataSet DataReturn = new DataSet();
                Da.Fill(DataReturn, "returnTable");
                Conn.Close();
                return DataReturn;
            }
            catch (Exception e)
            { throw new Exception(e.Message); }
            finally
            { Conn.Close(); }
        }
        public int ExNonQuery(SqlCommand Cmd)
        {
            SqlTransaction SqlCmdTransaction;
            SqlConnection();
            Conn.Open();

            SqlCmdTransaction = Conn.BeginTransaction();
            try
            {
                Cmd.Connection = Conn;
                Cmd.CommandTimeout = 0;
                Cmd.Transaction = SqlCmdTransaction;
                int result = Cmd.ExecuteNonQuery();
                SqlCmdTransaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                SqlCmdTransaction.Rollback();
                throw new Exception(ex.Message);
                return 0;
            }
            finally
            { Conn.Close(); }
        }
        public string ExScaler(SqlCommand Cmd)
        {
            try
            {
                SqlConnection();
                Cmd.Connection = Conn;
                Conn.Open();
                string result = Cmd.ExecuteScalar().ToString();
                return result;
            }
            catch (Exception e)
            { throw new Exception(e.Message); }
            finally
            { Conn.Close(); }
        }
    }
}