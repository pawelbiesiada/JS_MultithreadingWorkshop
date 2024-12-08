using System;
using System.Collections;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class SemaphoreSynchronizing : ISample
    {
        private Random _random = new Random();
        public void Run()
        {
            //creates semaphore with 4 threads and starts them
            CrudeThreadPool pool = new CrudeThreadPool();
            Thread.Sleep(5000);
            for (int i = 0; i < 20; ++i)
            {
                pool.SubmitWorkItem(WorkFunction);
            }
            // Sleep to simulate this thread doing other work.
            Thread.Sleep(100000);
            pool.Shutdown();
        }

        private void WorkFunction()
        {
            var ms = _random.Next(2000, 5000);
            Console.WriteLine("WorkFunction() called on Thread {0} doing some work for {1}ms.",
                Thread.CurrentThread.ManagedThreadId, ms);
            Thread.Sleep(ms);
        }
    }

    public class CrudeThreadPool
    {
        private const int MaxWorkThreads = 4;
        private const int WaitTimeout = 5000;
        private Semaphore semaphore;
        private Queue workQueue;
        private Thread[] threads;
        private volatile bool stop;

        public delegate void WorkDelegate();

        public CrudeThreadPool()
        {
            stop = false;
            semaphore = new Semaphore(0, int.MaxValue);
            workQueue = new Queue();
            threads = new Thread[MaxWorkThreads];
            for (int i = 0; i < MaxWorkThreads; ++i)
            {
                threads[i] =
                new Thread(new ThreadStart(ThreadFunc));
                Console.WriteLine("Created ThreadId {0}", threads[i].ManagedThreadId);
                threads[i].Start();
            }
            Console.WriteLine();
        }
        private void ThreadFunc()
        {
            do
            {
                if (!stop)
                {
                    Console.WriteLine("Ready to acquire wait lock on threadId {0}", Thread.CurrentThread.ManagedThreadId);
                    WorkDelegate workItem = null;
                    if (semaphore.WaitOne(WaitTimeout))
                    {
                        // Process the item on the front of the queue
                        lock (workQueue)
                        {
                            workItem =
                            (WorkDelegate)workQueue.Dequeue();
                            Console.WriteLine("Dequed new element, total elements {0}.", workQueue.Count);
                        }
                        workItem();
                    }
                    else
                    {
                        Console.WriteLine("Wait timed out on threadId {0}", Thread.CurrentThread.ManagedThreadId);
                    }
                }
            } while (!stop);
        }
        public void SubmitWorkItem(WorkDelegate item)
        {
            lock (workQueue)
            {
                workQueue.Enqueue(item);
                Console.WriteLine("Enqued new element, total elements {0}.", workQueue.Count);
            }
            semaphore.Release();
        }

        public void Shutdown()
        {
            stop = true;
        }
    }
}
