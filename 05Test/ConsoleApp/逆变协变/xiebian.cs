using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    /// <summary>
    /// 协变
    /// </summary>
    public class xiebian
    {
        
    }

    
    /// <summary>
    /// 协变的关键：out T 表示接口中T只能出现在返回参数中，不能出现在输入参数中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITest<out T>
    {
        T Get();
        //void Set(T t);
    }
    public class Test<T> : ITest<T>
    {
        public T Get()
        {
            return default(T);
        }
    }
}
