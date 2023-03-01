using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab7
{
    public static class Task2<T>
    {
        private static Mutex block = new Mutex();
        public static IEnumerable<T> res;
        public static void Run(IEnumerable<T> obj, Func<T> func)
        {
            res = obj;
            Console.WriteLine($"Input: {String.Join(" ", obj)} [{obj.ToList().Count}]");
             new Thread(ThOne).Start(func);
             new Thread(ThTwo).Start();
            //th1.Start(func);
        }
        private static void ThOne(object obj)
        {
            block.WaitOne();

            var func = obj as Func<T>;
            var addArr = new T[new Random().Next(10, 50)];
            for (int i = 0; i < addArr.Length; i++)
                addArr[i] = func();
            var tmp = res.ToList(); tmp.AddRange(addArr);
            res = tmp.Distinct();

            block.ReleaseMutex();
        }
        private static void ThTwo()
        {
            block.WaitOne();
            Console.WriteLine($"Result: {String.Join(" ", res)} [{res.ToList().Count}]");
            Console.WriteLine($"\nMax element is:  {res.ToList().Max()}");
            block.ReleaseMutex();
        }
    }

    internal class Program
    {
        public static Random Random = new Random();
        static void Main(string[] args)
        {
            Task1.Run();
            Console.ReadKey(true);
            Console.Clear();
            var arr = new int[Random.Next(1, 50)];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Random.Next(0, 10);
            Task2<int>.Run(arr, () => Random.Next(0, 100));
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine($"Result: {String.Join(" ", Task2<int>.res)} [{ Task2<int>.res.ToList().Count}]");
            Console.ReadKey(true);
            Console.Clear();
        }

    }
}
