using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading.Exercises.Workshop
{
    internal class PrimeNumberParallel
    {
        public void Test()
        {
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


            //print to console sum of prime numbers found
            //print to console what numbers were prime numbers


            var timeout = Task.Delay(1000);

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


        public bool IsPrimeNumber(int i)
        {
            for (int j = 2; j < i; j++)
            {
                if (j % i == 0)
                    return false;
            }

            return true;
        }
    }
}
