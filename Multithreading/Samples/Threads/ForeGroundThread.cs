using System;
using System.Threading;

namespace Multithreading.Threads
{
    internal class ForegroundThread
    {
        private static void Main(string[] args)
        {
            var backgroundThread = new Thread(LongRunningMethod) { IsBackground = false };

            Console.WriteLine("Running method asynchronously as foreground thread.");
            backgroundThread.Start();
            Console.WriteLine("We are in main method, executing next statements...");
        }

        private static void LongRunningMethod()
        {
            Thread.Sleep(5000);
            Console.WriteLine($"LongRunningMethod completed execution on threadId {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
