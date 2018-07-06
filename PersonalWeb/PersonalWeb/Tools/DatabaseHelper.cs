using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace PersonalWeb
{
    public class DatabaseHelper
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string conStr = "Data Source=123.206.28.222;Initial Catalog=MyWebDataBase;User ID=sa;Password=peilongfei1994_;";
        private static SqlConnection conn;
        private static void OpenDataBase(SqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        private static void CloseDataBase(SqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ds1"></param>
        /// <returns></returns>
        public static bool GetData(string sql,out DataSet ds)
        {
            
            DataSet ds1 = new DataSet();
            if (string.IsNullOrEmpty(sql))
            {
                ds = null;
                return false;
            }
            conn = new SqlConnection();
            conn.ConnectionString = conStr;
            SqlCommand com = new SqlCommand(sql, conn);
            try
            {
                log.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :" + sql);
                OpenDataBase(conn);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds1);
                CloseDataBase(conn);
                if (ds1 != null)
                {
                    ds = ds1;
                    return true;
                }
                else
                {
                    ds = null;
                    return false;
                } 
            }
            catch (Exception ex)
            {
                log.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :" + ex.Message);
                ds = null;
                return false;
            }
        }
        /// <summary>
        /// 像数据库执行Insert，update语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static bool Post(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return false;
            }
            else
            {
                try
                {
                    log.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :" + sql);
                    conn = new SqlConnection();
                    conn.ConnectionString = conStr;
                    OpenDataBase(conn);
                    SqlCommand sqlCmd = conn.CreateCommand();
                    sqlCmd.CommandText = sql;
                    int backnum = sqlCmd.ExecuteNonQuery();
                    CloseDataBase(conn);
                    if (backnum >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :" + ex.Message);
                    return false;
                }
                
            }
        }
    }
}