using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Samples.Parallels
{
    internal class ParallelForeach : ISample
    {
        public void Run()
        {
            var arr = new List<string>(20);
            for (int i = 0; i < 20; i++)
            {
                arr.Add(i.ToString());
            }
            //arr.ForEach(PrintMessage);
            //var loopResults = Parallel.ForEach(arr, PrintMessage);

            var obj = new object();
            var count = 0;
            Parallel.ForEach(arr, //source
            () => { Console.WriteLine("Init {0}", Thread.CurrentThread.ManagedThreadId); return -2; }, // localInit
            (el, po, localState) => { Console.WriteLine("Body {0} value {1}", Thread.CurrentThread.ManagedThreadId, el); return 1; }, //body
            (localState) => { Console.WriteLine("finish {0}", Thread.CurrentThread.ManagedThreadId); lock (obj) { count += localState; } }//localfinally
            );

            Console.WriteLine($"Total number of threads ran in Parallel.ForEach {count}");
            Console.ReadKey();
        }

        private void PrintMessage(string number)
        {
            Console.WriteLine("Executed iteration {0}. CurrentThread {1}", number, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
