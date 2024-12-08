using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Multithreading.Samples
{
    internal class SerialVsParallelProcessing : ISample
    {
        public void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Executing task in serial mode");
            for (int i = 0; i < 20; i++)
            {
                LongRunningMethod();
            }
            var serialMethodTime = stopwatch.ElapsedMilliseconds;


            stopwatch.Restart();
            Console.WriteLine("Executing task in parallel mode");
            Parallel.For(0, 20, (i) => { LongRunningMethod(); });
            var parallelMethodTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("LongRunningMethod time comparision:");
            Console.WriteLine("\tSerial processing: {0}ms", serialMethodTime);
            Console.WriteLine("\tParallel processing: {0}ms", parallelMethodTime);
            Console.WriteLine("\tImprovement ratio: {0}%", Math.Round((serialMethodTime * 100.0/parallelMethodTime *1.0) - 100.0));
            Console.ReadKey();
        }

        private void LongRunningMethod()
        {
            double result;
            for (int i = 0; i < 10000000; i++)
            {
                result = Math.Acos(Math.Asin(i));
            }
        }
    }
}
