using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.内存优化
{
    /// <summary>
    /// 结论：struct 不需要分配在堆上，但是，数组是引用对象，需要分配在堆上。
    /// List 自增的过程本质是扩张数组的特性在报告中也得到了体现。
    /// </summary>
    public class StructClassResearch
    {
        /// <summary>
        /// 34M
        /// </summary>
        public void UsingClass()
        {
            Console.WriteLine($"memory in bytes before execution: {GC.GetGCMemoryInfo().TotalAvailableMemoryBytes}");
            const int count = 1_000_000;
            var list = new List<Student>(count);
            for (var i = 0; i < count; i++)
            {
                list.Add(new Student
                {
                    Level = int.MinValue
                });
            }

            //list.Clear();

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            Console.WriteLine($"heap size: {gcMemoryInfo.HeapSizeBytes}");
            Console.WriteLine($"memory in bytes end of execution: {gcMemoryInfo.TotalAvailableMemoryBytes}");
        }

        /// <summary>
        /// 6.9M
        /// </summary>
        public void UsingStruct()
        {
            Console.WriteLine($"memory in bytes before execution: {GC.GetGCMemoryInfo().TotalAvailableMemoryBytes}");
            const int count = 1_000_000;
            var list = new List<Yueluo>(count);
            for (var i = 0; i < count; i++)
            {
                list.Add(new Yueluo
                {
                    Level = int.MinValue
                });
            }

            //list.Clear();

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            Console.WriteLine($"heap size: {gcMemoryInfo.HeapSizeBytes}");
            Console.WriteLine($"memory in bytes end of execution: {gcMemoryInfo.TotalAvailableMemoryBytes}");
        }

        public class Student
        {
            public int Level { get; set; }
        }

        public struct Yueluo
        {
            public int Level { get; set; }
        }
    }
}
