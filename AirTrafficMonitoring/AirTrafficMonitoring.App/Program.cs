using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring.App
{
    internal class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            var tos = new Tos(TransponderReceiverFactory.CreateTransponderDataReceiver());
            var atm = new AirTrafficController(new CollisionDetector(), new TrackStorage(),  new AirSpace(), new ConsoleView(new ConsoleOutput()), null);

            tos.Attach(atm);

            Console.ReadLine();
        }
    }  
}
