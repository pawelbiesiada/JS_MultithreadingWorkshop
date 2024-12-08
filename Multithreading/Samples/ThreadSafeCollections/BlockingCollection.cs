using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class BlockingCollection : ISample
    {
        public void Run()
        {
            var cts = new CancellationTokenSource();

            //var addingPace = 200; var removingPace = 1000;       //Producer produces faster than consumer consumes
            var addingPace = 500; var removingPace = 600;      //Producer produces slower than consumer consumes

            var blockingCollection = new BlockingCollection<int>(4);
            blockingCollection.Add(-2);
            //thread waits on Add if max capacity is reached;
            blockingCollection.Add(-1);
            
            //this casues exception
            //blockingCollection.CompleteAdding();
            //blockingCollection.Add(0);

            var addTask = Task.Run(() => 
            {
                int i = 1;
                while (!blockingCollection.IsAddingCompleted)
                {
                    blockingCollection.Add(i);
                    Console.WriteLine("\tAdded item {0}", i);
                    if (i >= 10)
                    {
                        blockingCollection.CompleteAdding();                        
                    }
                    i++;
                    Thread.Sleep(addingPace);
                }
            });

            var removeTask1 = GetTaskWithEnumerable(blockingCollection, cts, removingPace);
            var removeTask2 = GetTaskWithEnumerable(blockingCollection, cts, removingPace);

            //var removeTask1 = GetTaskWithTryTake(blockingCollection, cts, removingPace);
            //var removeTask2 = GetTaskWithTryTake(blockingCollection, cts, removingPace);
            
            Thread.Sleep(8000);
            cts.Cancel();
            Console.WriteLine("DONE.");

            Console.ReadKey();
        }


        private static Task GetTaskWithEnumerable(BlockingCollection<int> blockingCollection, CancellationTokenSource cts, int removingPace)
        {
            return Task.Run(() =>
            {
                foreach (var element in blockingCollection.GetConsumingEnumerable())
                {
                    Console.WriteLine("{0} task read: {1}", Thread.CurrentThread.ManagedThreadId, element);

                    Thread.Sleep(removingPace);
                }
                Console.WriteLine("{0} task reading done.", Thread.CurrentThread.ManagedThreadId);

            }, cts.Token);
        }

        private static Task GetTaskWithTryTake(BlockingCollection<int> blockingCollection, CancellationTokenSource cts, int removingPace)
        {
            return Task.Run(() =>
            {
                while (!blockingCollection.IsAddingCompleted && !cts.IsCancellationRequested)
                {
                    var isSuccess = blockingCollection.TryTake(out int element);
                    if (isSuccess)
                    {
                        Console.WriteLine("{0} task read: {1}", Thread.CurrentThread.ManagedThreadId, element);
                    }
                    Thread.Sleep(removingPace);
                }

            }, cts.Token);
        }
    }
}
