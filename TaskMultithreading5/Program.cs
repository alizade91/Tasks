using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMultithreading5
{
    class Program
    {
        private static int InitialState = 10;
        static void Main(string[] args)
        {
            ThreadIncrementer(InitialState);
            Console.ReadKey();
        }

        private static Semaphore semaphoreObject = new Semaphore(initialCount: 1, maximumCount: 1);
        private static void ThreadIncrementer(int state)
        {
            if (state == 0) return;
            ThreadPool.QueueUserWorkItem((param) =>
            {
                semaphoreObject.WaitOne();
                state--;
                semaphoreObject.Release();
                Console.WriteLine(state);
                Thread.Sleep(150);
                ThreadIncrementer(state);
            });
        }
    }
}
