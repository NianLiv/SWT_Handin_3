using System;
using TransponderReceiver;
using AirTrafficMonitoring.Lib;

namespace AirTrafficMonitoring.App
{
    internal class Program
    {
        private static void Main()
        {
            new Tos(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Console.ReadLine();
        }
    }  
}
