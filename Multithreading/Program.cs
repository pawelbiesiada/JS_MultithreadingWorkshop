﻿using System;
using System.Text;
using System.Collections.Generic;
using Multithreading.Samples;
using Multithreading.Samples.Threads;
using Multithreading.Samples.Tasks;
using Multithreading.Samples.Parallels;
using Multithreading.Samples.Rx;
using Multithreading.Samples.ThreadSafeCollection;

namespace Multithreading
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //var ex1 = new ParallelFolderRead();
            //ex1.Run();

            //Console.ReadLine();


            var repeat = true;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            while (repeat)
            {
                Console.Clear();
                RunSample();

                Console.WriteLine();
                Console.WriteLine("Repeat?");
                var line = Console.ReadLine();
                if (!line.StartsWith("t", StringComparison.InvariantCultureIgnoreCase) &&
                    !line.StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    repeat = false;
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private static void RunSample()
        {
            ISample sample = null;
            
            sample = new ThreadPresentation();
            //sample = new ThreadException();

            //sample = new TaskGeneric();
            //sample = new AsyncAwaitPresentation();

            //sample = new Multithreading.Samples.Threads.CriticalSectionUnsafe();
            //sample = new MonitorLocking();

            //sample = new TasksCancellationToken();

            //sample = new BeginEndInvoke(); //PlatformNotSupportedException - in .NET Core projects

            //sample = new Deadlock();
            //sample = new ForeachAsync();

            //sample = new ParallelFor();
            //sample = new ParallelForeach();
            //sample = new PlinqPresentation();

            //sample = new RxPresentation();
            //sample = new RxBuffer();

            //sample = new ConcurrentCollection();
            //sample = new ConcurrentDictionary();
            //sample = new SimpleDictionarySample();

            //sample = new BlockingCollection();

            sample.Run();
        }
    }
}