using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class nibian
    {
    }

    /// <summary>
    /// 逆变的关键：in T 表示接口中T只能出现在输入参数中，不能出现在返回参数中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITest2<in T>
    {
        //T Get();
        void Set(T t);
    }
    public class Test2<T> : ITest2<T>
    {
        public T CurrentVal { get; set; }
        public void Set(T t)
        {
            CurrentVal = t;
        }
    }
}
