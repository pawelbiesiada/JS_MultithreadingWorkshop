using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class MonitorPulse : ISample
    {
        private int _counter;
        private readonly object _theLock = new object();

        public void Run()
        {
            _counter = 0;
            var thread1 = new Thread(ThreadMethod1);
            var thread2 = new Thread(ThreadMethod2);
            thread1.Start();
            thread2.Start();

            Console.ReadKey();
        }

        private void ThreadMethod1()
        {
            lock (_theLock) 
            {
                for (int i = 1; i <= 10; ++i)
                {
                    Console.WriteLine("Thread1, Counter is {1}, Thread {2}.", i, ++_counter, Thread.CurrentThread.ManagedThreadId);
                    Monitor.Pulse(_theLock);
                    Thread.Sleep(1000);
                    Monitor.Wait(_theLock, 4000);
                }
            }
        }

        private void ThreadMethod2()
        {
            lock (_theLock) 
            {
                for (int i = 0; i < 10; ++i)
                {
                    Console.WriteLine("Thread2, Counter is {1}, Thread {2}.", i, ++_counter, Thread.CurrentThread.ManagedThreadId);
                    Monitor.Pulse(_theLock);
                    Thread.Sleep(1000);
                    Monitor.Wait(_theLock, 4000);
                }
            }
        }
    }
}
