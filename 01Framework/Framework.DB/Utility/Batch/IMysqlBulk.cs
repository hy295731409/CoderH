using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DB.Utility.Batch
{
    public interface IMysqlBulk : ISqlBulk
    {
        Task InsertAsync<T>(string csvPath, string tableName = "") where T : class;
    }
}
