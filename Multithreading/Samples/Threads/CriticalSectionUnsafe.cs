using System;
using System.Threading;

namespace Multithreading.Samples.Threads
{
    class CriticalSectionUnsafe : ISample
    {
        int _commonCounter;
        int _thread1Counter;
        int _thread2Counter;

        public void Run()
        {
            _commonCounter = 0;
            _thread1Counter = 0;
            _thread2Counter = 0;

            var t1 = new Thread(Increment1);
            var t2= new Thread(Increment2);
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("Thread1 executed the loop {0} times", _thread1Counter.ToString("N0"));
            Console.WriteLine("Thread2 executed the loop {0} times", _thread2Counter.ToString("N0"));
            Console.WriteLine("Both Thread1 & Thread2 executed the loop {0} times", (_thread1Counter + _thread2Counter).ToString("N0"));
            Console.WriteLine("CommonCounter was increased {0} times", _commonCounter.ToString("N0"));
            Console.ReadKey();
        }

        private void Increment1()
        {
            while (_commonCounter < 10000000)
            {
                _commonCounter++;
                _thread1Counter++;
            }
        }
        private void Increment2()
        {
            while (_commonCounter < 10000000)
            {
                _commonCounter++;
                _thread2Counter++;
            }
        }
    }
}
