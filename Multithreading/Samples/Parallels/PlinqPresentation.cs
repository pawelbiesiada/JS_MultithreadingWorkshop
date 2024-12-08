using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

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
            int[] source = Enumerable.Range(1, 100000000).ToArray();
            var stopwatch = new Stopwatch();
            // Find the numbers where num % 3 == 0 is true, returned
            // in descending order.
            stopwatch.Start();
            int[] dividesByThree = source.Where(n => n % 3 == 0).OrderByDescending(n => n).ToArray();
            Console.WriteLine(string.Format("Found {0} numbers synchronously that match query in {1}ms!",
            dividesByThree.Count(), stopwatch.ElapsedMilliseconds));

            stopwatch.Restart();
            try
            {

            int[] dividesByThreeAsync = source
                .AsParallel()
                //.WithDegreeOfParallelism(8)
                .Where(n => {
                    //throw new Exception();
                    return n % 3 == 0; })
                //.AsParallel() //why not at this place
                .OrderByDescending(n => n).ToArray();
            Console.WriteLine(string.Format("Found {0} numbers asynchronously that match query in {1}ms!",
            dividesByThreeAsync.Count(), stopwatch.ElapsedMilliseconds));
            }
            catch (Exception ex )
            {
                Console.WriteLine("Exception caught in method main thread.");
                Console.WriteLine(ex);
            }
            stopwatch.Stop();
        }
    }
}
