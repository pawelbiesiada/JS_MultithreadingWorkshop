using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadPresentation : ISample
    {
        public void Run()
        {
            Console.WriteLine($"Main threadId {Thread.CurrentThread.ManagedThreadId} is in status {Thread.CurrentThread.ThreadState}");
            var thread = new Thread(() => LongRunningMethod());
            //thread.IsBackground
            Console.WriteLine($"ThreadId {thread.ManagedThreadId} is in status {thread.ThreadState}");
            thread.Start();          
            while (thread.IsAlive)
            {
                Console.WriteLine($"ThreadId {thread.ManagedThreadId} in while is in status {thread.ThreadState}");
                Thread.Sleep(500);
            }
            Console.WriteLine($"ThreadId {thread.ManagedThreadId} is in status {thread.ThreadState}");
            Console.ReadKey();

            var parametrizedThread = new Thread((i) => LongRunningMethod(i));
            parametrizedThread.Start(2000);
            parametrizedThread.Join();
            parametrizedThread = new Thread((i) => LongRunningMethod(i));
            parametrizedThread.Start();
            Console.ReadKey();

        }

        private void LongRunningMethod()
        {
            Thread.Sleep(5000);
            Console.WriteLine($"LongRunningMethod completed execution on threadId {Thread.CurrentThread.ManagedThreadId}.");
        }


        private void LongRunningMethod(object delay)
        {
            Thread.Sleep((int)delay);
            Console.WriteLine($"LongRunningMethod completed execution on threadId {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
