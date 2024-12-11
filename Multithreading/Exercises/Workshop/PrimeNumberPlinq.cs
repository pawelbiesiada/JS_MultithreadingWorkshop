using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Exercises.Workshop
{
    internal class PrimeNumberPlinq
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

            //var primeNumbers = new Task<List<int>>(() => 
            //{
            //    return numbers
            //    .AsParallel()
            //    .WithCancellation(cts.Token)
            //    //.Where(x => IsPrimeNumber(x))
            //    .Where(IsPrimeNumber)
            //    .ToList();
            //}, cts.Token);

            var primeNumbers = numbers
                .AsParallel()
                .WithCancellation(cts.Token)
                //.Where(x => IsPrimeNumber(x))
                .Where(IsPrimeNumber)
                .ToList();

            var sum = primeNumbers.Sum();
            var count = primeNumbers.Count();

            Console.WriteLine($"Suma: {sum} count: {count}");

            //print to console sum of prime numbers found
            //print to console what numbers were prime numbers
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
              //  Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
               // token.ThrowIfCancellationRequested();
                if (number % j == 0)
                    return false;
            }

            return true;
        }
    }
}
