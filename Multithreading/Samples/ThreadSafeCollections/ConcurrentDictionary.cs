using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Multithreading.Samples.ThreadSafeCollection
{
    internal class ConcurrentDictionary : ISample
    {
        public void Run()
        {
            var list = new ConcurrentQueue<int>();
            var stack = new ConcurrentStack<int>();

            var dict = new ConcurrentDictionary<int, string>();

            var d = new Dictionary<int, string>();

            d.Add(0, "zero");


            dict.GetOrAdd(0, (i) => { return i.ToString(); } );

            Action<int, string> printStatus = 
                (key, oldValue) => Console.WriteLine("key = {0}    oldValue = \"{1}\"", key, oldValue);
            //dict.AddOrUpdate()
            dict.AddOrUpdate(1, "one", (key, addingValue) => { printStatus(key, addingValue); return "one from update"; });
            dict.AddOrUpdate(2, "two", (key, addingValue) => { printStatus(key, addingValue); return "two from update1"; });
            dict.AddOrUpdate(2, "anoter two", 
                (key, addingValue) => 
                { 
                    printStatus(key, addingValue); 
                    var currentVal = dict[key]; 
                    return "two from update2"; 
                });
            
            //with a delegate even to add new value
            dict.AddOrUpdate(3, (valueToAdd) => "three", (key, existingValue) => { printStatus(key, existingValue); return "three from update"; });
            
            foreach (var item in dict)
            {
                Console.WriteLine("key={0}\tvalue={1}", item.Key, item.Value);
            }

            var val = String.Empty;
            var getSuccess = dict.TryGetValue(2, out val);
            if (getSuccess)
            {
                Console.WriteLine("Getting value from dictionary: {0}", val);
            }

            //This is is not thread safe.
            //if (dict.ContainsKey(2))
            //{
            //    Console.WriteLine("Getting value from dictionary: {0}", dict[2]);
            //}

            Console.ReadKey();
        }
    }
}
