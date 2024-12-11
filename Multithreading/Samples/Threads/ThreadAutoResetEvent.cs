using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    public class ThreadAutoResetEvent : ISample
    {
        public int _counter = 0;

        private AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        public void Run()
        {
            _counter = 0;
            var thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();

            Thread.Sleep(10);
            Console.WriteLine("ThreadId {0} is in status {1}. The counter is {2}", thread.ManagedThreadId, thread.ThreadState, _counter);

            autoResetEvent.Set();
            //Thread.Sleep(10);
            Console.WriteLine("ThreadId {0} is in status {1}. The counter is {2}", thread.ManagedThreadId, thread.ThreadState, _counter);
            Thread.Sleep(100);
            Console.WriteLine("ThreadId {0} is in status {1}. The counter is {2}", thread.ManagedThreadId, thread.ThreadState, _counter);

            autoResetEvent.Set();
            Thread.Sleep(1000);
            Console.WriteLine("ThreadId {0} is in status {1}. The counter is {2}", thread.ManagedThreadId, thread.ThreadState, _counter);
            Console.ReadKey();
        }

        void ThreadMethod()
        {
            _counter++;
            autoResetEvent.WaitOne(); //set thread to WaitSleepJoin status
            LongRunningMethod();
            _counter++;
            autoResetEvent.WaitOne();
            _counter++;
        }
        
        private void LongRunningMethod()
        {
            for (int i = 0; i < 10000000; i++)
            {
                Math.Acos(Math.Asin(i));
            }
        }
    }
}
