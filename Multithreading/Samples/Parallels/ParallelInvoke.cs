using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Samples.Parallels
{
    internal class ParallelInvoke : ISample
    {
        public void Run()
        {
            var arr = new Action[20];
            for (int i = 0; i < 20; i++)
            {
                var j = i;
                arr[i] = new Action(() => PrintMessage(j));
            }

            Parallel.Invoke(arr);
            
            Console.ReadKey();
        }

        private void PrintMessage(int number)
        {
            Console.WriteLine("Executed iteration {0}. CurrentThread {1}", number, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
