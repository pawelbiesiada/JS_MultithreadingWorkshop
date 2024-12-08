using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class ConcurrentBagPresentation : ISample
    {
        public void Run()
        {

            var bag = new ConcurrentBag<int>();
            var list = new List<int>(10000);

            Parallel.For(0, 10000, (i) => {
                bag.Add(i);
                list.Add(i);
            });

            Console.WriteLine($"ConcurrentBag has elements: {bag.Count}");
            Console.WriteLine($"List has elements: {list.Count}");

            Console.ReadKey();
        }
    }
}
