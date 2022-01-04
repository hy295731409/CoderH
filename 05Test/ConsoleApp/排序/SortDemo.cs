using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.排序
{
    public static class SortDemo
    {
        /// <summary>
        /// 冒泡排序
        /// 优点：稳定
        /// 确定：慢
        /// </summary>
        /// <param name="array"></param>
        public static void BubbleSort(this int[] array)//321
        {
            for (int i = 0; i < array.Length; i++)//i=0=>213 , i=1 => 123
            {
                //减i是因为外层循环i次之后，最后的i个数已经排序好了
                //-1是因为最后两个数比较一次就行
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="array"></param>
        public static void InsertSort(this int[] array)//321
        {
            for (int i = 1; i < array.Length; i++)//
            {
                if (array[i] < array[i - 1])//2<3
                {
                    int temp = array[i];//2
                    int j = 0;
                    for (j = i - 1; j >= 0 && temp < array[j]; j--)
                    {
                        array[j + 1] = array[j];//
                    }
                    array[j + 1] = temp;
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                if(array[i] < array[i - 1])//若第 i 个元素大于 i-1 元素则直接插入；反之，需要找到适当的插入位置后在插入。
                {
                    int j = i - 1;//有序数组的最后一个元素index
                    int temp = array[i];//待插的值
                    while(j > -1 && temp < array[j])//采用顺序查找方式找到插入的位置，在查找的同时，将数组中的元素进行后移操作，给插入元素腾出空间
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = temp;//插入到正确位置
                }
            }
        }
        /// <summary>
        /// 选择排序
        /// 每轮循环选择剩下数中的最小数，第N轮确定第N个数，0-N下标的数已确定
        /// </summary>
        /// <param name="array"></param>
        public static void SelectSort(this int[] array)//321
        {
            int temp = 0;
            int minIndex = 0;//最小数的下标
            for (int i = 0; i < array.Length; i++)//第一轮=>132
            {
                minIndex = i;//假设第一个数就是最小数
                for (int j = i + 1; j < array.Length; j++)//将第一个数与后面剩下的数比较
                {
                    //如果有比假设的最小数小的，设置其下标为最小下标
                    if (array[minIndex] > array[j])
                        minIndex = j;
                }
                temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
            }
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="array"></param>
        public static int Sort(int[] array, int low, int high)
        {
            int key = array[low];//以第一个为基数
            while (low < high)
            {
                //从后向前搜索key小的值
                while (array[high] >= key && high > low)
                    --high;
                //比key小的放左边
                array[low] = array[high];//arr[0]=arr[2]->arr[0]=1
                                         //从前向后搜索比key大的值，放右边
                while (array[low] <= key && high > low)
                    ++low;
                //比key大的放右边
                array[high] = array[low];
            }
            //左边都比key小，右边都比key大
            //将key放在游标当前位置，此时low=high
            array[low] = key;
            Console.WriteLine(string.Join(",", array));
            Console.WriteLine();
            return high;
        }
        public static void QuickSort(this int[] array, int low, int high)
        {
            if (low >= high)
                return;
            //完成一次单元排序,得到的是基数在数组中的下标
            int index = Sort(array, low, high);
            //对左边的单元进行排序
            QuickSort(array, low, index - 1);
            //对右边的单元进行排序
            QuickSort(array, index + 1, high);
        }

    }
}
