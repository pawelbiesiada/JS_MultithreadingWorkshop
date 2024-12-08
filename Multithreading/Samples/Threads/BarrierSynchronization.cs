using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class BarrierSynchronization : ISample
    {
        private const int ThreadsCount = 8;
        private Barrier _barrier;

        public void Run()
        {
            _barrier = new Barrier(ThreadsCount); // +-1 What will happen?
            var threads = new Thread[ThreadsCount];
            for (int i = 0; i < ThreadsCount; ++i)
            {
                threads[i] = new Thread(ThreadMethod);
                threads[i].Start();
            }

            Console.ReadLine(); 
        }

        private void ThreadMethod()
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.Write(i.ToString());
                _barrier.SignalAndWait();
            }
            for (int i = 5; i < 10; ++i)
            {
                Console.Write(i.ToString());
                //_barrier.SignalAndWait();
            }
        }
    }
}
