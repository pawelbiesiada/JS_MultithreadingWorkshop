using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadPriority : ISample
    {
        public void Run()
        {
            var priorityTest = new PriorityTest();
            var thread1 = new Thread(new ThreadStart(priorityTest.ThreadMethod)) { Priority = System.Threading.ThreadPriority.Lowest };
            var thread2 = new Thread(new ThreadStart(priorityTest.ThreadMethod)) { Priority = System.Threading.ThreadPriority.Normal };
            var thread3 = new Thread(new ThreadStart(priorityTest.ThreadMethod)) { Priority = System.Threading.ThreadPriority.Highest };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Thread.Sleep(2000);
            priorityTest.LoopSwitch = false;
            Console.ReadKey();
        }
    }

    internal class PriorityTest
    {
        static bool loopSwitch;
        [ThreadStatic] static long threadCount = 0;

        public PriorityTest()
        {
            loopSwitch = true;
        }

        public bool LoopSwitch
        {
            set { loopSwitch = value; }
        }

        public void ThreadMethod()
        {
            while (loopSwitch)
            {
                threadCount++;
            }
            Console.WriteLine("ThreadId {0} with {1} priority has a count = {2,13}", 
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.Priority,
                threadCount.ToString("N0"));
        }
    }
}
