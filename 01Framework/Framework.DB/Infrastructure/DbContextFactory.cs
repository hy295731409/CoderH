using Framework.DB.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DB.Infrastructure
{
    public class DbContextFactory
    {
        public static IDapperContext GetDbContext()
        {
            var key = typeof(IDapperContext).Name + "_" + typeof(IDapperContext).FullName;
            if (CallContext.GetData(key) is IDapperContext dbContext)
                return dbContext;

            //dbContext = IocManager.Resolve<IDapperContext>();
            dbContext = new DapperContext();
            CallContext.SetData(key, dbContext);//放入线程数据槽

            return dbContext;
        }
    }
}
