using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    internal class ThreadTimer : ISample
    {
        public void Run()
        {
            var msg = "Hi all!";
            TimerCallback timerCallback = (obj) => { Console.WriteLine(obj); };
            var timer = new Timer(timerCallback, msg, 2000, 500);

            Console.ReadKey();
        }
    }
}
