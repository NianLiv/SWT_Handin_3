using System;
using TransponderReceiver;
using AirTrafficMonitoring.Lib;

namespace AirTrafficMonitoring.App
{
    class Program
    {
        static void Main(string[] args)
        {
            new Tos(TransponderReceiverFactory.CreateTransponderDataReceiver());
            Console.ReadLine();
        }
    }  
}
