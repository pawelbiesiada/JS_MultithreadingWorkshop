using System;
using System.Threading.Tasks;

namespace Multithreading.Samples.Tasks
{
    internal class TaskContinueWith : ISample
    {
        public void Run()
        {
            var task1 = new Task(LongRunningMethod);

            task1.ContinueWith((t) => 
            {
                Console.WriteLine(" task1 status {0}", t.Status);
                LongRunningMethod();
            });
            //task1.RunSynchronously();
            task1.Start();
            Console.WriteLine("Threads are running");
            Console.ReadKey();
        }

        private void LongRunningMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i.ToString());
                Task.Delay(1).Wait();
            }
        }
    }
}
