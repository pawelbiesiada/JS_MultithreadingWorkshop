using System;
using System.Collections.Immutable;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class ImmutableCollection : ISample
    {
        public void Run()
        {
            var immutableStack = ImmutableStack<string>.Empty; //Push, Pop, Peek, PeekRef

            var immutableQueue = ImmutableQueue<string>.Empty;
            immutableQueue.Enqueue("first");
            var newImmutable = immutableQueue.Enqueue("second");
            var anotherImmutable = immutableQueue.Dequeue(out var dequedElement); //Peek, PeekRef

            //you can even modify the list while enumerating immutable collection
            foreach (var element in immutableQueue)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();
            foreach (var element in newImmutable)
            {
                Console.WriteLine(element);
            }
            Console.ReadKey();

            var immutableList = ImmutableList<int>.Empty;
            immutableList = immutableList.Add(1);
            immutableList = immutableList.Add(2);


            //ineffective way of enumerating using indexes
            for (int i = 0; i < immutableList.Count; i++)
            {
                //O(log N) operation
                var listElement = immutableList[i];
                Console.WriteLine(listElement);
            }

            foreach (var element in immutableList)
            {
                Console.WriteLine(element);
            }
            Console.ReadKey();
        }
    }
}
