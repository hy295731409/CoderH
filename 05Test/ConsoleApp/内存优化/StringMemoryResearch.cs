using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.String
{
    public class StringMemoryResearch
    {
        /// <summary>
        /// 102M内存分配给string
        /// </summary>
        public void UsingString()
        {
            var source = Enumerable.Range(0, 10)
                .Select(x => x.ToString())
                .ToArray();
            var re = string.Empty;
            for (int i = 0; i < 10_000; i++)
            {
                re += source[i % 10];
            }
        }

        /// <summary>
        /// 0.3M内存分配给string
        /// </summary>
        public void UsingStringBuilder()
        {
            var source = Enumerable.Range(0, 10)
                .Select(x => x.ToString())
                .ToArray();
            var sb = new StringBuilder();
            for (var i = 0; i < 10_000; i++)
            {
                sb.Append(source[i % 10]);
            }

            var _ = sb.ToString();
        }

        public void StringTest()
        {
            unsafe
            {
                // *&t1: 0x04bb11c8
                string t1 = string.Empty;

                //*&t2: 0x04bb11c8
                string t2 = "";

                //*&t3: 0x00000000 只分配内存，不分配空间
                string t3 = null;

                List<int> t4 = new List<int>();
                //List<int>* _t4 = &t4;

                char letter = 'A';
                char* pointerToLetter = &letter;
            }
            
        }
    }
}
