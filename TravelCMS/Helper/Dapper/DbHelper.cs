using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Dapper
{
    public static class DbHelper
    {
        public static SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection(DbConn.ConnectionString);
            conn.Open();
            return conn;
        }
        public async static Task<IEnumerable<T>> RunSqlWithModel<T>(string sql,object param=null,string isproc=null)
        {
            using (var conn=GetConn())
            {
                if(string.IsNullOrEmpty(isproc))
                {
                    var data = conn.Query<T>(sql, param);
                    return data;
                }
                else
                {
                    var data = await conn.QueryAsync<T>(sql, param,commandType:CommandType.StoredProcedure);
                    return data;
                }
            }
        }
        public async static Task<IEnumerable<dynamic>> RunSqlWithOutModel(string sql, object param = null, string isproc = null)
        {
            using (var conn = GetConn())
            {
                if (string.IsNullOrEmpty(isproc))
                {
                    var data = await conn.QueryAsync(sql, param);
                    return data;
                }
                else
                {
                    var data = conn.Query(sql, param, commandType: CommandType.StoredProcedure);
                    return data;
                }

            }
        }
        public static void HitData(string sql, object param = null, string isproc = null)
        {
            using (var conn = GetConn())
            {
                if (string.IsNullOrEmpty(isproc))
                {
                    conn.Execute(sql, param);
                }
                else
                {
                    conn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                }

            }
        }

    }
}
