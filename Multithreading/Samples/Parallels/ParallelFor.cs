﻿using System;
using System.Threading.Tasks;
using System.Threading;

namespace Multithreading.Samples.Parallels
{
    internal class ParallelFor : ISample
    {
        public void Run()
        {
            var loopResults = Parallel.For(0, 20, 
                new ParallelOptions() {MaxDegreeOfParallelism = 8,  }, 
                PrintMessage);
            
            Console.ReadKey();
        }

        private void PrintMessage(int number)
        {
            Console.WriteLine("Executed iteration {0}. CurrentThread {1}", number, Thread.CurrentThread.ManagedThreadId);
        }
    }
}