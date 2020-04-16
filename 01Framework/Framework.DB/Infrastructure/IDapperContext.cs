using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.DB.Infrastructure
{
    public interface IDapperContext
    {
        bool InTransaction { get; set; }
        IDbConnection Connection { get; }
    }
}
