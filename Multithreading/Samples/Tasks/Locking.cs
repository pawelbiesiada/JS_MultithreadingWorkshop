using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Tasks
{
    class Locking
    {
        public void Run()
        {
            var t1 = Task.Run<LazySingleton>(() => LazySingleton.Instance);
            var t2 = Task.Run<LazySingleton>(() => LazySingleton.Instance);
            var t3 = Task.Run<LazySingleton>(() => LazySingleton.Instance);

            Task.WaitAll(t1, t2, t3);

            if(t1 != t2 || t1 != t3 || t2 != t3)
            {
                Console.WriteLine("It was not a thread safe singleton initialization");
            }            
        }
    }

    public class LazySingleton
    {
        private static LazySingleton _instance;

        private LazySingleton() { }

        //private static readonly object _lock = new object();
        //private static readonly SpinLock _lock = new SpinLock();


        static LazySingleton()
        {
            _instance = new LazySingleton();
        }

        public static LazySingleton Instance
        {
            get
            {

                

                return _instance;
            }
        }

        public int ConfigValue { get; set; }
    }   
}
