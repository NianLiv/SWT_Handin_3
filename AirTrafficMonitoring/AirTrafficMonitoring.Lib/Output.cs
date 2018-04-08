using System;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    class Output : IOutput
    {
        public void OutputLine(string line) => Console.WriteLine(line);
    }
}
