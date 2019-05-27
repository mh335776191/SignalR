using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caipiao
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> numDicCounts = new Dictionary<string, int>();
            Console.WriteLine("是否开始循环选号？（是：y 否：其他）");
            var isloop = Console.ReadKey().KeyChar == 'y';
            int loopTimes = 1;
            if (isloop)

            {
                Console.WriteLine("请输入循环次数");
                loopTimes = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("选号中...");
            while (true)
            {
                if (loopTimes <= 0)
                {
                    SumTimes(numDicCounts);
                    break;
                }
                var red = new int[] { 8, 9, 11, 24, 9, 1, 7, 11, 17, 4, 23 };
                var bule = new int[] { 8, 9, 11, 1, 7, 11, 4 };
                var redNums = GetNum(red, 6);
                var blueNums = GetNum(bule, 1);
                Console.WriteLine("红球:" + string.Join("  ", redNums));
                Console.WriteLine("蓝球:" + string.Join("  ", blueNums));
                var key = string.Join("  ", redNums) + "---" + string.Join("  ", blueNums);
                if (numDicCounts.ContainsKey(key))
                {
                    numDicCounts[key] = numDicCounts[key] + 1;
                }
                else
                {
                    numDicCounts.Add(key, 1);
                }
                if (!isloop)
                {
                    break;
                }
                loopTimes--;

            }
            Console.ReadKey();
        }
        /// <summary>
        /// 统计出现次数
        /// </summary>
        /// <param name="numDicCounts"></param>
        private static void SumTimes(Dictionary<string, int> numDicCounts)
        {
            Console.Clear();
            var timsDesc = numDicCounts.OrderByDescending(m => m.Value);
            foreach (var t in timsDesc)
            {
                Console.WriteLine(string.Format("次数:{0} 号码:{1}", t.Value, t.Key));
            }
        }
        /// <summary>
        /// 获取球
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="selectedNumCount"></param>
        /// <returns></returns>
        private static List<int> GetNum(int[] nums, int selectedNumCount)
        {
            HashSet<int> selectedNum = new HashSet<int>();
            Random rnd = new Random();
            while (true)
            {
                int index = rnd.Next(selectedNumCount);
                nums = nums.OrderBy(d => Guid.NewGuid()).ToArray();//随机排序 
                selectedNum.Add(nums[index]);
                if (selectedNum.Count == selectedNumCount)
                    break;
            }
            return selectedNum.OrderBy(m => m).ToList();
        }
    }
}
