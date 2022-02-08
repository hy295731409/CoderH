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

      

        /// <summary>
        /// 归并排序（目标数组，子表的起始位置，子表的终止位置）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        private static void MergeSortFunction(int[] array, int first, int last)
        {
            if (first < last)   //子表的长度大于1，则进入下面的递归处理
            {
                int mid = (first + last) / 2;   //子表划分的位置
                MergeSortFunction(array, first, mid);   //对划分出来的左侧子表进行递归划分
                MergeSortFunction(array, mid + 1, last);    //对划分出来的右侧子表进行递归划分
                MergeSortCore(array, first, mid, last); //对左右子表进行有序的整合（归并排序的核心部分）
            }
        }

        /// <summary>
        /// 归并排序的核心部分：将两个有序的左右子表（以mid区分），合并成一个有序的表
        /// </summary>
        /// <param name="array"></param>
        /// <param name="first"></param>
        /// <param name="mid"></param>
        /// <param name="last"></param>
        private static void MergeSortCore(int[] array, int first, int mid, int last)
        {
            int indexA = first; //左侧子表的起始位置
            int indexB = mid + 1;   //右侧子表的起始位置
            int[] temp = new int[last + 1]; //声明数组（暂存左右子表的所有有序数列）：长度等于左右子表的长度之和。
            int tempIndex = 0;
            while (indexA <= mid && indexB <= last) //进行左右子表的遍历，如果其中有一个子表遍历完，则跳出循环
            {
                if (array[indexA] <= array[indexB]) //此时左子表的数 <= 右子表的数
                {
                    temp[tempIndex++] = array[indexA++];    //将左子表的数放入暂存数组中，遍历左子表下标++
                }
                else//此时左子表的数 > 右子表的数
                {
                    temp[tempIndex++] = array[indexB++];    //将右子表的数放入暂存数组中，遍历右子表下标++
                }
            }
            //有一侧子表遍历完后，跳出循环，将另外一侧子表剩下的数一次放入暂存数组中（有序）
            while (indexA <= mid)
            {
                temp[tempIndex++] = array[indexA++];
            }
            while (indexB <= last)
            {
                temp[tempIndex++] = array[indexB++];
            }

            //将暂存数组中有序的数列写入目标数组的制定位置，使进行归并的数组段有序
            tempIndex = 0;
            for (int i = first; i <= last; i++)
            {
                array[i] = temp[tempIndex++];
            }
        }
    }
}
