using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.算法
{
    /// <summary>
    /// 循环队列，复用100个地址，首位相连，添加元素时，空间够加队尾，否则异常，获取时，队列不为空，返回第一个，否则抛异常，实现add，remove
    /// 注：循环队列就是用数组实现，当队列塞满时，队头队尾下标相等，这种方式无法判断队列当前队空还是队满，所以需要牺牲一个位置专门来标队尾
    /// </summary>
    public class LoopQueue<T>
    {
        /// <summary>
        /// 队列容量
        /// </summary>
        private int capacity { get; set; }
        /// <summary>
        /// 头下标
        /// </summary>
        private int headIndex { get; set; }
        /// <summary>
        /// 尾下标
        /// </summary>
        private int tailIndex { get; set; }
        private T[] data { get; set; }

        public LoopQueue(int _capacity)
        {
            capacity = _capacity;
            data = new T[capacity];
            headIndex = tailIndex = 0;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return headIndex == tailIndex;
        }
        /// <summary>
        /// 是否满员
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            //原理:队尾再往后一个就是队头，说明头尾相连了，满了
            return (tailIndex + 1) % capacity == headIndex;
        }

        /// <summary>
        /// 队尾添加
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            if (IsFull())
            {
                Console.WriteLine("队列满员，添加失败");
                return;
            }
                
            //在队尾添加
            data[tailIndex] = t;
            //队尾下标+1，指向下一个空的数组位置
            tailIndex = (tailIndex + 1) % capacity;
            Console.WriteLine($"添加成功，当前队尾下标{tailIndex}");
        }

        /// <summary>
        /// 队头取出
        /// </summary>
        public T Remove()
        {
            if (IsEmpty())
            {
                Console.WriteLine("队列为空,取出失败");
                return default(T);
            }
                
            T t = data[headIndex];
            data[headIndex] = default(T);
            //取出后重设队头下标
            headIndex = (headIndex + 1) % capacity;
            Console.WriteLine($"取出成功，当前队头下标{headIndex}");
            return t;
        }
    }
}
