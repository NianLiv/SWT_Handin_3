using System;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class ConsoleOutput : IOutput
    {
        public void OutputLine(string line) => Console.Write(line);
        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);
        public void SetWindowSize(int width, int height) => Console.SetWindowSize(width, height);

        public void GetLargetsScreenSize(out int width, out int height)
        {
            width = Console.LargestWindowWidth;
            height = Console.LargestWindowHeight;
        }
    }
}
