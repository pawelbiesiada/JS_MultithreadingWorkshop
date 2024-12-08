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
                thread.Join();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private  void LongRunningMethod()
        {
            Thread.Sleep(1000);
            throw new ApplicationException();
        }
    }
}
