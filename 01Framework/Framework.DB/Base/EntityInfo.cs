using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WeShare.PropertyWrapper;

namespace Framework.DB.Base
{
    public class EntityInfo
    {
        public PropertyInfo PropertyInfo { get; set; }

        public string FieldName { get; set; }

        public IGetValue GetMethod { get; set; }
        public object Get(object obj)
        {
            return this.GetMethod.Get(obj);
        }
    }
}
