using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class MonitorLocking : ISample
    {
        private object _objectLock = new object();
        private int _valueLock = -1;

        public void Run()
        {
            var t1 = new Thread(PrintNumbers);
            var t2 = new Thread(PrintNumbers);



            //var t1 = new Thread(PrintNumbersWithLock);
            //var t2 = new Thread(PrintNumbersWithLock);
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.ReadKey();
        }

        private void PrintNumbers()
        {
            // object _lockedObject = new object(); - this is not valid, why???
            //Monitor.Enter(_valueLock); - why this is not valid???
            
            Monitor.Enter(_objectLock);
            try
            {
                Console.WriteLine("ThreadId {0} is executing PrintNumbers()",
                Thread.CurrentThread.ManagedThreadId);
                Console.Write("Your numbers: ");
                for (int i = 0; i < 20; i++)
                {
                    Random r = new Random();
                    Thread.Sleep(10 * (r.Next(5) + 1));
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(_objectLock);                
            }
        }

        private void PrintNumbersWithLock()
        {
            lock (_objectLock)
            {
                Console.WriteLine("ThreadId {0} is executing PrintNumbers()",
                Thread.CurrentThread.ManagedThreadId);
                Console.Write("Your numbers: ");
                Random r = new Random();
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100 * (r.Next(5) + 1));
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
        }
    }
}
