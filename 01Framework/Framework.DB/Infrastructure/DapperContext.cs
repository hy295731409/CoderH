using Framework.DB.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.DB.Infrastructure
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectStr;
        private readonly string DefaultConStr = "server=172.18.3.190;database=dockerdemo;uid=medicom;pwd=medic0m;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;";
        public DapperContext()
        {
            _connectStr = DefaultConStr;
        }
        public DapperContext(string connStr)
        {
            _connectStr = connStr;
        }
        public bool InTransaction { get; set; }
        public IDbConnection Connection
        {
            get
            {
                if (InTransaction)
                {
                    var key = "mysql_connection_" + typeof(DapperContext).FullName;
                    if (CallContext.GetData(key) is IDbConnection dbConnection)
                        return dbConnection;

                    dbConnection = new MySqlConnection(_connectStr);
                    //dbConnection = new SqlConnection(_connectStr);
                    CallContext.SetData(key, dbConnection);//放入线程数据槽

                    return dbConnection;
                }
                else
                {
                    return new MySqlConnection(_connectStr);
                    //return new SqlConnection(_connectStr);
                }
            }
        }

        
    }
}
