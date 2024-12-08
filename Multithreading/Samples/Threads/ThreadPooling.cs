using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadPooling : ISample
    {
        //ThreadPool is used by:
        // - Task and Task<TResult>
        // - Timer
        public void Run()
        {
            Console.WriteLine("This Sample presents ThreadPool class and its properties.");
            int maxThreads, maxIOThreads;
            int availableThreads, availableIOThreads;
            ThreadPool.GetMaxThreads(out maxThreads, out maxIOThreads);
            ThreadPool.GetAvailableThreads(out availableThreads, out availableIOThreads);

            Console.WriteLine("Maximum threads number running on this machine.");
            Console.WriteLine("\tworker threads {0} \t IO threads {1}", maxThreads, maxIOThreads);
            Console.WriteLine("Number of currently available threads running on this machine.");
            Console.WriteLine("\tworker threads {0} \t IO threads {1}", availableIOThreads, availableIOThreads);
            Console.WriteLine();
            Console.ReadKey();

            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMethod));
            
            Console.WriteLine("Main threadId {0} is running. IsBackground = {1}", 
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);
            Thread.Sleep(1000);
            Console.WriteLine("Main threadId {0} is about to complete.", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static void ThreadMethod(object obj)
        {
            Console.WriteLine("ThreadMethod completed execution on threadId {0}. IsBackground = {1}", 
                Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);
        }
    }
}
