using System;
using System.Threading;

namespace Multithreading.Samples
{
    internal class SerialVsAsynchronousProcessing : ISample
    {
        public void Run()
        {
            Console.WriteLine("This sample is to present time difference between serial and asynchronous execution.");
            Console.ReadKey();

            var longRunningMethodDelegate = new Action(LongRunningMethod);

            Console.WriteLine("Running method normally on threadId {0}", Thread.CurrentThread.ManagedThreadId);
            longRunningMethodDelegate.Invoke();
            Console.WriteLine("We are in main method, executing next statements...");

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Running method asynchronously.");
            var asyncResult = longRunningMethodDelegate.BeginInvoke(null, null);
            Console.WriteLine("We are in main method, executing next statements...");

            //longRunningMethodDelegate.EndInvoke(asyncResult);
            Console.WriteLine("Now we are synchronized back again in threadId {0}", Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
        }        

        private void LongRunningMethod()
        {
            double result;
            for (int i = 0; i < 50000000; i++)
            {
                result = Math.Acos(Math.Asin(i));
            }
            Console.WriteLine("LongRunningMethod completed execution on threadId {0}.", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
