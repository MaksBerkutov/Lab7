using System;
using System.Threading;

namespace Lab7
{
    public static class Task1
    {
        private static Mutex block = new Mutex();
        private static readonly int count = 10;
        public static void Run()
        {
            Thread th1 = new Thread(ThOne);
            Thread th2 = new Thread(ThTwo);
            th1.Start();
            th2.Start();
            th2.Join();
        }
        private static void ThOne()
        {
            block.WaitOne();
            for (int i = 1; i <= count; i++) Console.WriteLine(i);
            block.ReleaseMutex();
        }
        private static void ThTwo()
        {
            block.WaitOne();
            for (int i = count; i != 0; i--) Console.WriteLine(i);
            block.ReleaseMutex();
        }
    }
}
