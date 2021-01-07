using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.内存优化
{
    class BoxingNoBoxing
    {
        /// <summary>
        /// 装箱内存使用24M
        /// </summary>
        public void Boxing()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                UseObject(i);//int =》 obj
            }
        }

        /// <summary>
        /// 不装箱内存使用0.9M
        /// </summary>
        public void NoBoxing()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                UseInt(i);
            }
        }

        public static void UseInt(int age)
        {
            // nothing
        }

        public static void UseObject(object obj)
        {
            // nothing
        }
    }
}
