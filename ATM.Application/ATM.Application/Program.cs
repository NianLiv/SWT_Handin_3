using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;
using ATM.ClassLib;

namespace ATM.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new TrackStore();
            var receiver = new Receiver(store);
            
            while (true)
            {
                for (int i = 0; i < store.trackObjects.Count; i++)
                {
                    Console.WriteLine(store.trackObjects[i].Tag);
                    Thread.Sleep(1000);
                }
            }


            Console.ReadKey();
        }
    }
}
