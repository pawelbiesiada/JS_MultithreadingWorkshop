using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Exercises.Workshop
{
    internal class ProducerConsument
    {
        public void StartProcessing()
        {
            var queue = new BlockingCollection<string>(10);

            var producent = 
                Task.Run(() => 
                {
                    while (!queue.IsAddingCompleted)
                    {
                        queue.Add("1");
                        Thread.Sleep(200);
                    }
                });

            var producent2 =
                Task.Run(() =>
                {
                    while (!queue.IsAddingCompleted)
                    {
                        queue.Add("2");
                        Thread.Sleep(200);
                    }
                });
            var consument = 
                Task.Run(() => 
                {
                    foreach (var item in queue.GetConsumingEnumerable())
                    {
                        Console.WriteLine("Fetched item with value: " + item + " Collection has elements: " + queue.Count );

                        Thread.Sleep(150);
                    }
                });


            Console.ReadLine();
            producent.Dispose();
            queue.CompleteAdding();

            Console.ReadLine();
        }
    }
}
