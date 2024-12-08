using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadStatuses : ISample
    {
        public void Run()
        {
            Console.WriteLine("Main threadId {0} is in status {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.ThreadState);
            var thread = new Thread(new ThreadStart(LongRunningMethod));
            Console.WriteLine("ThreadId {0} is in status {1}", thread.ManagedThreadId, thread.ThreadState);
            thread.Start();
            Console.WriteLine("ThreadId {0} is in status {1}", thread.ManagedThreadId, thread.ThreadState);
            int i = 1;
            while (thread.IsAlive)
            {
                try
                {
                    if (i % 3 == 0)
                    {
                        if (thread.ThreadState == ThreadState.Running)
                            thread.Suspend();
                        else
                            thread.Resume();

                        if (i % 12 == 0) //9 for en exception
                        {
                            thread.Abort();
                        }
                    }
                    Console.WriteLine("ThreadId {0} in while is in status {1}", thread.ManagedThreadId, thread.ThreadState);
                    Thread.Sleep(500);
                    i++;
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Exception of type {0} raised.", exc.GetType().ToString());
                    Console.WriteLine(exc.Message);
                    Console.WriteLine(exc.StackTrace);
                    Console.WriteLine();
                }
            }
            Console.WriteLine(" ThreadId {0} is in status {1}", thread.ManagedThreadId, thread.ThreadState);
            
            Console.ReadKey();
        }

        private void LongRunningMethod()
        {
            try
            {
                double result;
                for (int i = 0; i < 100000000; i++)
                {
                    result = Math.Acos(Math.Asin(i));
                }
                Console.WriteLine("LongRunningMethod completed execution on threadId {0}.", Thread.CurrentThread.ManagedThreadId);

            }
            catch (ThreadAbortException exc)
            {
                Console.WriteLine("Thread exception of type {0} raised.", exc.GetType().ToString());
                Console.WriteLine(exc.Message);
                Console.WriteLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }        
    }
}
