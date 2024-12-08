using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    class SemaphoreLimit : ISample
    {
        public Semaphore _semaphore { get; set; }

        public void Run()
        {
            _semaphore = new Semaphore(5, 5);
            StartProcessing();

            Console.ReadKey();
        }

        private void StartProcessing()
        {
            for (int i = 1; i <= 40; i++)
            {
                //ThreadPool.QueueUserWorkItem(WorkToBeDone, i);
                var t = new Thread(WorkToBeDone);
                t.Start(i);
            }
        }

        private void WorkToBeDone(object args)
        {
            // Wait thread to start processing (a semaphore to be released).
            Console.WriteLine("ThreadId {0} number {1} is ready to start.", Thread.CurrentThread.ManagedThreadId, args);
            _semaphore.WaitOne();

            Console.WriteLine("ThreadId {0} number {1} is running.", Thread.CurrentThread.ManagedThreadId, args);
            Thread.Sleep(1000);

            _semaphore.Release();
            Console.WriteLine("ThreadId {0} number {1} is completed and is releasing some room.", Thread.CurrentThread.ManagedThreadId, args);
        }
    }
}
