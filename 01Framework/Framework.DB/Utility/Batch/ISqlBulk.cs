﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DB.Utility.Batch
{
    public interface ISqlBulk
    {
        /// <summary>
        /// 批量导入数据
        /// </summary>
        /// <param name="table">数据源</param>
        void Insert(DataTable table);
        /// <summary>
        /// 批量导入数据
        /// </summary>
        /// <param name="table">数据源</param>
        Task InsertAsync(DataTable table);

        //void Insert<T>(IEnumerable<T> enumerable) where T : class;
        //Task InsertAsync<T>(IEnumerable<T> enumerable) where T : class;
    }
}
