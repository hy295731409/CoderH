using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.算法
{
    public static class MinWindow
    {
        /// <summary>
        /// 最小覆盖子串
        /// </summary>
        /// <param name="S">ADOBECODEBANC</param>
        /// <param name="T">ABC</param>
        /// <returns></returns>
        public static string MINSTRING(string S= "ADOBECODEBANC", string T= "ABC")
        {
            int start = 0;
            int MinLen = S.Length;
            //左索引
            int LEFT = 0;
            //右索引
            int Right = 0;

            //windows存储的是窗口子串，needs存储的是目标串，字典中存储了对应的子串和目标串的各个字母的个数
            Dictionary<char, int> windows = new Dictionary<char, int>();
            Dictionary<char, int> needs = new Dictionary<char, int>();

            //初始化目标串
            for (int i = 0; i < T.Length; i++)
            {
                if (!needs.ContainsKey(T[i]))
                {
                    needs.Add(T[i], 1);
                }
                else { needs[T[i]] = needs[T[i]] + 1; }
            }
            int match = 0;
            //遍历S中元素
            while (Right < S.Length)
            {
                char c1 = S[Right];
                if (needs.Keys.Contains(c1))
                {
                    //记录在目标串中存在的字母和数目
                    if (windows.ContainsKey(c1))
                    {
                        windows[c1] = windows[c1] + 1;
                    }
                    else { windows.Add(c1, 1); }
                    if (windows[c1] == needs[c1]) { match++; }
                }
                //对r进行自增操作
                Right++;
                while (match == needs.Keys.Count)
                {
                    //如果满足条件，就移动窗口左侧
                    if (Right - LEFT < MinLen)
                    {
                        //更新左侧
                        start = LEFT;
                        MinLen = Right - LEFT;
                    }
                    char c2 = S[LEFT];
                    if (needs.Keys.Contains(c2))
                    {
                        //更新了其中的内容
                        windows[c2] = windows[c2] - 1;
                        if (windows[c2] < needs[c2])
                        {
                            match--;
                        }

                    }
                    //对l进行自增操作
                    LEFT++;
                }

            }
            return MinLen == T.Length ? "" : S.Substring(start, MinLen);
        }
    }
}
