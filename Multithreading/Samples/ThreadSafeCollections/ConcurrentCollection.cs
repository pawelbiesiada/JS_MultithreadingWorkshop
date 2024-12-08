using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class ConcurrentCollection : ISample
    {
        public void Run()
        {
            var queue = new ConcurrentQueue<string>(); //Enqueue, TryPeek, TryDequeue 
            var stack = new ConcurrentStack<int>();    //Push, PushRange, TryPeek, TryPop, TryPopRange
            var bag = new ConcurrentBag<string>();     //Add, TryPeek, TryTake

            var cts = new CancellationTokenSource();

            var enqueuedObjectCount = 0;
            var dequeuedObjectCount = 0;

            //var queue = new Queue<string>();

            var t1 = Task.Run(() => 
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    queue.Enqueue(enqueuedObjectCount.ToString("c"));
                    enqueuedObjectCount++;
                }
            });
            var t2 = Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested || queue.Count > 0)
                {
                    if(queue.TryDequeue(out var element))
                    {
                        dequeuedObjectCount++;
                    }
                }
            });

            Thread.Sleep(2000);
            cts.Cancel();
            Task.WaitAll(t1, t2);
            Console.WriteLine($"Enqued elements: {enqueuedObjectCount.ToString("D")} dequeued elements: {dequeuedObjectCount.ToString("D")}");

            Console.ReadKey();
        }
    }
}
