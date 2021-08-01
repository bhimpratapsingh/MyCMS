using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControlPanel.Models
{
    public class CommonRepository
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public static List<T> Query<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    con.Open();
                    return con.Query<T>(query, param, commandType: commandType).ToList();
                }
                catch (Exception ex)
                {
                    logger.Error($"ErrorMessage: {ex.Message} \n InnerException: {ex.InnerException} \n StackTrace: {ex.StackTrace}");
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static dynamic Execute(string query, object param = null)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    con.Open();
                    return con.Execute(query, param);
                }
                catch (Exception ex)
                {
                    logger.Error($"ErrorMessage: {ex.Message} \n InnerException: {ex.InnerException} \n StackTrace: {ex.StackTrace}");
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static dynamic ExecuteScalar<T>(string query, object param = null)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    con.Open();
                    return con.ExecuteScalar<T>(query, param);
                }
                catch (Exception ex)
                {
                    logger.Error($"ErrorMessage: {ex.Message} \n InnerException: {ex.InnerException} \n StackTrace: {ex.StackTrace}");
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static dynamic ExecuteReader(string query, object param = null)
        {
            using (var con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    con.Open();
                    return con.ExecuteReader(query, param);
                }
                catch (Exception ex)
                {
                    logger.Error($"ErrorMessage: {ex.Message} \n InnerException: {ex.InnerException} \n StackTrace: {ex.StackTrace}");
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}