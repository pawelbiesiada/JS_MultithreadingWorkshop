using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Samples
{
    internal class Volatile
    {
        //private bool flag = true;
        private volatile bool flag = true;
        private static void Main(string[] args)
        {
            var volatileClass = new Volatile();
            var thread = new Thread(() => { volatileClass.flag = false; });
            thread.Start();

            while (volatileClass.flag)
            {
                Console.WriteLine($"Executing at {DateTime.Now.ToString("HH:mm:ss.fffffff")}");
                Thread.Sleep(100);
            }

            //possible optimization used by compiler (Release)
            //if(volatileClass.flag)
            //{
            //    while(true)
            //    {
            //        Console.WriteLine($"Executing at {DateTime.Now.ToString("HH:mm:ss.fffffff")}");
            //        Thread.Sleep(100);
            //    }
            //}
        }
    }
}
