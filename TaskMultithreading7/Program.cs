using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMultithreading7
{
    class Program
    {
        static void Main(string[] args)
        {
            //case 1 
            //Continuation task should be executed regardless of the result of the parent task.

            Task<int> t1 = new Task<int>(DummyMethod);

            t1.ContinueWith((t) =>
            {
                Console.WriteLine(t.Result);
            });
        
            t1.Start();


            //case 2
            //Continuation task should be executed when the parent task finished without success

            Task<int> t2 = new Task<int>(DummyMethod);

            t2.ContinueWith((t) =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Console.WriteLine("something bad happened :(((");
                }
                else
                {
                    Console.WriteLine(t.Result);
                }
            }, TaskContinuationOptions.NotOnRanToCompletion);

            t2.Start();


            //case 3
            //Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation

            Task<int> t3 = new Task<int>(DummyMethod);

            t3.ContinueWith((t) =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Console.WriteLine("something bad happened :(((");
                }
                else
                {
                    Console.WriteLine(t.Result);
                }
            }, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.AttachedToParent);

            t3.Start();
            

            //case 4
            //Continuation task should be executed outside of the thread pool when the parent task would be cancelled


            Task<int> t4 = new Task<int>(DummyMethod);

            t4.ContinueWith((t) =>
            {
                Console.WriteLine("parent task was cancelled.");
            }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            t4.Start();
            Console.ReadKey();
        }

        private static int DummyMethod()
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += i;
                Thread.Sleep(150);
            }
            return sum;
        }
    }
}
