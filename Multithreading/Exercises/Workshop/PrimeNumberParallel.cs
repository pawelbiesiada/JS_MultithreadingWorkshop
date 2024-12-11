using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Exercises.Workshop
{
    internal class PrimeNumberParallel
    {
        public void Test()
        {
            var cts = new CancellationTokenSource();
            var numbers = new List<int>();
            var rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                numbers.Add(rand.Next(10,300));
            }

            //run IsPrimeNumber paralelly     // new Task   Task.Run ()     Wait
            var primeNumbersResults = new List<(int, Task<bool>)>();

            foreach (var item in numbers)
            {
                primeNumbersResults.Add(new (item, Task.Run(() => IsPrimeNumber(item))));
            }

            //foreach (var result in primeNumbersResults)
            //{
            //    result.Item2.Wait();
            //}

            //Task.WaitAll(primeNumbersResults.Select(x => x.Item2));
            //Task.WhenAll(primeNumbersResults.Select(x => x.Item2)).Wait();


            var sum = primeNumbersResults.Where(x => x.Item2.Result).Sum(x => x.Item1);
            var count = primeNumbersResults.Count(x => x.Item2.Result);

            Console.WriteLine($"Suma: {sum} count: {count}");

            //print to console sum of prime numbers found
            //print to console what numbers were prime numbers


            var timeout = Task.Delay(1000, cts.Token);

           // System.IO.File.ReadAllTextAsync("C:\\Temp");

            var tasks = primeNumbersResults.Select(x => (Task)x.Item2).ToList();
            tasks.Add(timeout);

            var resultAny = Task.WhenAny(tasks);

            resultAny.Wait();

            var any = resultAny.Result;

            if(any == timeout)
            {
                //anything when operation timed out cts.Cancel()
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsPrimeNumber(int number, int millisecondsTimeout = 30_000 )
        {
            var cts = new CancellationTokenSource(millisecondsTimeout); 

            return IsPrimeNumber(number, cts.Token);
        }

        public bool IsPrimeNumber(int number, CancellationToken token)
        {
            for (int j = 2; j < number; j++)
            {
                Thread.Sleep(1);
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                token.ThrowIfCancellationRequested();
                if (number % j == 0)
                    return false;
            }

            return true;
        }
    }
}
