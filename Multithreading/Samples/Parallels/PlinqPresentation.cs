using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Multithreading.Samples.Parallels
{
    internal class PlinqPresentation : ISample
    {
        public void Run()
        {
            //var task = Task.Factory.StartNew(ProcessIntData);
            ProcessIntData();
            Console.ReadKey();
        }

        private void ProcessIntData()
        {
            // Get a very large array of integers.
            int[] source = Enumerable.Range(1, 10_000_000).ToArray();
            var stopwatch = new Stopwatch();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            stopwatch.Start();
            var  dividesByThree = source.Where(n => n % 3 == 0).OrderByDescending(n => n).ToArray();
            Console.WriteLine(string.Format("Found {0} numbers synchronously that match query in {1}ms!",
            dividesByThree.Count(), stopwatch.ElapsedMilliseconds));

            Console.WriteLine(string.Format("Found {0} numbers synchronously that match query in {1}ms!",
            dividesByThree.Count(), stopwatch.ElapsedMilliseconds));

            Console.WriteLine(string.Format("Found {0} numbers synchronously that match query in {1}ms!",
            dividesByThree.Count(), stopwatch.ElapsedMilliseconds));

            stopwatch.Restart();
            OrderedParallelQuery<int> dividesByThreeAsync = null;
            try
            {
            bool islocked = false;
            dividesByThreeAsync = source
                .AsParallel()
                //
                //
                //.AsSequential()
                //.WithDegreeOfParallelism(8)
                .Where(n => {
                    
                    if(n % 10_000_000 == 0)
                        throw new ApplicationException();

                    return n % 3 == 0; })
                //.AsParallel() //why not at this place
                .OrderByDescending(n => n);
            }
            catch (Exception ex )
            {
                Console.WriteLine("Exception caught in method main thread.");
                Console.WriteLine(ex);
            }

            Console.WriteLine(string.Format("Found {0} numbers parallel that match query in {1}ms!",
            dividesByThreeAsync.Count(), stopwatch.ElapsedMilliseconds));
            stopwatch.Stop();
        }
    }
}
