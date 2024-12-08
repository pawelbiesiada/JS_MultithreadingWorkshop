using System;
using System.Threading;

namespace Multithreading.Threads
{
    internal class BackgroundThread
    {
        private static void Main(string[] args)
        {
            var backgroundThread = new Thread(LongRunningMethod) { IsBackground = true };
         
            Console.WriteLine("Running method asynchronously as background thread.");
            backgroundThread.Start();
            Console.WriteLine("We are in main method, executing next statements...");
            backgroundThread.Join();
        }

        private static void LongRunningMethod()
        {
            Thread.Sleep(5000);
            Console.WriteLine($"LongRunningMethod completed execution on threadId {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
