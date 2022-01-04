using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.算法
{
    /// <summary>
    /// 用自带的双链表
    /// </summary>
    public class LRUCache
    {
        /// <summary>
        /// 双向链表(插入复杂度为O(1))
        /// </summary>
        private LinkedList<KeyValuePair<int, int>> linkedList = new LinkedList<KeyValuePair<int, int>>();
        /// <summary>
        /// 哈希字典(查询复杂度为O(1))
        /// </summary>
        private Dictionary<int, LinkedListNode<KeyValuePair<int, int>>> keyValuePairs;

        private int _capacity = 0;
        public LRUCache(int capacity)
        {
            _capacity = capacity;
            keyValuePairs = new Dictionary<int, LinkedListNode<KeyValuePair<int, int>>>(capacity);
        }

        public int Get(int key)
        {
            if (!keyValuePairs.ContainsKey(key))
                return -1;
            var node = keyValuePairs[key].Value;
            Put(key, node.Value);
            return node.Value;
        }

        public void Put(int key, int val)
        {
            LinkedListNode<KeyValuePair<int, int>> newNode = new LinkedListNode<KeyValuePair<int, int>>(KeyValuePair.Create(key, val));
            if (keyValuePairs.ContainsKey(key))
            {
                linkedList.Remove(keyValuePairs[key]);
                linkedList.AddFirst(newNode);
                //更新dic key 中的值
                keyValuePairs[key] = newNode;
            }
            else
            {
                if (_capacity == linkedList.Count)
                {
                    LinkedListNode<KeyValuePair<int, int>> lastNode = linkedList.Last;
                    linkedList.RemoveLast();
                    keyValuePairs.Remove(lastNode.Value.Key);
                }
                linkedList.AddFirst(newNode);
                keyValuePairs.Add(key, newNode);
            }
        }
    }


    /// <summary>
    /// 自定义的双链表
    /// </summary>
    public class LRUCache2
    {
        /// <summary>
        /// 缓存容量
        /// </summary>
        private int _capacity = 0;
        /// <summary>
        /// 插入复杂度O(1)
        /// </summary>
        private DoubleLinkList cache;
        /// <summary>
        /// 查询复杂度O(1)
        /// </summary>
        private Dictionary<int, Node> dic;

        public LRUCache2(int capacity)
        {
            _capacity = capacity;
            cache = new DoubleLinkList(capacity);
            dic = new Dictionary<int, Node>(capacity);
        }
        public void Put(int key, int val)
        {
            var newNode = new Node(key, val);
            if (dic.ContainsKey(key))
            {
                cache.Delete(dic[key]);
                cache.AddFirst(newNode);
                dic[key] = newNode;
            }
            else
            {
                if(dic.Count == _capacity)
                {
                    var delKey = cache.DeleteLast();
                    dic.Remove(delKey);
                }
                cache.AddFirst(newNode);
                dic.Add(key, newNode);
            }
        }
        public int Get(int key)
        {
            if (!dic.ContainsKey(key))
                return -1;
            int val = dic[key].Val;
            Put(key, val);
            return val;
        }
    }

    /// <summary>
    /// 双向链表
    /// </summary>
    public class DoubleLinkList
    {
        private int _size;
        /// <summary>
        /// 链表首位节点
        /// </summary>
        public Node Head { get; set; }
        public Node Tail { get; set; }

        public DoubleLinkList(int size)
        {
            //表头(做为一个标志)为空
            Head = new Node(0, 0);
            Tail = new Node(0, 0);
            Head.Prev = Tail;
            Head.Next = Tail;
            Tail.Prev = Head;
            Tail.Next = Head;
            _size = size;
        }
        /// <summary>
        /// 在首位插入一个节点
        /// </summary>
        /// <param name="node"></param>
        public void AddFirst(Node node)
        {
            //新节点插入首位之前
            node.Prev = Head;
            node.Next = Head.Next;
            
            Head.Next.Prev = node;
            Head.Next = node;
        }
        /// <summary>
        /// 返回删除的key
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Delete(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
            return node.Key;
        }
        /// <summary>
        /// 返回删除的key
        /// </summary>
        /// <returns></returns>
        public int DeleteLast()
        {
            if (Head.Next == Tail) return -1;
            return Delete(Tail.Prev);
        }
    }
    /// <summary>
    /// 链表节点
    /// </summary>
    public class Node
    {
        public Node Prev { get; set; }
        public Node Next { get; set; }
        public int Key { get; set; }
        public int Val { get; set; }
        public Node(int key, int val)
        {
            Key = key;
            Val = val;
        }
    }
}
