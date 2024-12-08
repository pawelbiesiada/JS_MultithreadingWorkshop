using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    class ThreadStatusesInterrupt : ISample
    {
        public void Run()
        {
            Console.WriteLine("Main threadId {0} is in status {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.ThreadState);
            
            var thread2Sleep = new Thread(new ParameterizedThreadStart((ival) => { Method2Sleep(ival); }));
            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            thread2Sleep.Start(2000);

            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            Thread.Sleep(500);
            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            Thread.Sleep(2000);
            Console.WriteLine(" ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            Console.WriteLine();
            Console.ReadKey();

            thread2Sleep = new Thread(new ParameterizedThreadStart((ival) => { Method2Sleep(ival); }));
            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            thread2Sleep.Start(-1);

            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            Thread.Sleep(500);
            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            thread2Sleep.Interrupt(); //Interrupted sleeping thread.
            Thread.Sleep(10);
            Console.WriteLine("ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);

            Thread.Sleep(2000);
            Console.WriteLine(" ThreadId {0} is in status {1}", thread2Sleep.ManagedThreadId, thread2Sleep.ThreadState);
            Console.ReadKey();
        }

        private void Method2Sleep(object obj)
        {
            double result;
            for (int i = 0; i < 1000000; i++)
            {
                result = Math.Acos(Math.Asin(i));
            }

            try
            {
                var miliseconds = int.Parse(obj.ToString());
                Thread.Sleep(miliseconds);
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("ThreadId {0} was interrupted: {1}", Thread.CurrentThread.ManagedThreadId, ex.Message);
                Console.WriteLine("ThreadId {0} is in status: {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.ThreadState);
            }

            for (int i = 0; i < 10000000; i++)
            {
                result = Math.Acos(Math.Asin(i));
            }
        }
    }
}
