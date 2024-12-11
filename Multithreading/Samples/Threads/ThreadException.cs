using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadException : ISample
    {
        public void Run()
        {
            var thread = new Thread(LongRunningMethod);

            try
            {
                thread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            thread.Join();
        }

        private  void LongRunningMethod()
        {
            Thread.Sleep(1000);
            throw new ApplicationException();
        }
    }
}
