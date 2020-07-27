using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.DB.Infrastructure;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Framework.DB.Utility.Extension;

namespace Framework.DB.Utility.Batch
{
    public class SqlServerBulk : ISqlServerBulk
    {
        private DapperContext _context;

        public SqlServerBulk(DapperContext context)
        {
            _context = context;
        }

        public void Insert(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(table.TableName))
            {
                throw new ArgumentNullException("DataTable的TableName属性不能为空");
            }

            //var conn = (SqlConnection)_context.Database.GetDbConnection();
            var conn = (SqlConnection)_context.Connection;
            conn.Open();
            using (var bulk = new SqlBulkCopy(conn))
            {
                bulk.DestinationTableName = table.TableName;
                foreach (DataColumn column in table.Columns)
                {
                    bulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                bulk.WriteToServer(table);
            }
        }

        public async Task InsertAsync(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(table.TableName))
            {
                throw new ArgumentNullException("DataTable的TableName属性不能为空");
            }
            //var conn = (SqlConnection)_context.Database.GetDbConnection();
            var conn = (SqlConnection)_context.Connection;
            await conn.OpenAsync();
            using (var bulk = new SqlBulkCopy(conn))
            {
                bulk.DestinationTableName = table.TableName;
                foreach (DataColumn column in table.Columns)
                {
                    bulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                await bulk.WriteToServerAsync(table);
            }
        }

        #region EF
        //public void Insert<T>(IEnumerable<T> enumerable) where T : class
        //{
        //    //获取主键
        //    var keys = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        //    var primaryKey = "";
        //    if (keys.Count == 1)
        //    {
        //        primaryKey = keys[0].PropertyInfo.Name;
        //    }
        //    using (var table = enumerable.ToDataTable(primaryKey))
        //    {

        //        Insert(table);
        //        table.Clear();
        //        table.Dispose();
        //    }
        //}

        //public async Task InsertAsync<T>(IEnumerable<T> enumerable) where T : class
        //{
        //    //获取主键
        //    var keys = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        //    var primaryKey = "";
        //    if (keys.Count == 1)
        //    {
        //        primaryKey = keys[0].PropertyInfo.Name;
        //    }
        //    using (var table = enumerable.ToDataTable(primaryKey))
        //    {
        //        await InsertAsync(table);
        //        table.Clear();
        //        table.Dispose();
        //    }
        //} 
        #endregion

    }
}
