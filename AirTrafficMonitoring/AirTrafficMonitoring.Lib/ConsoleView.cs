using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using AirTrafficMonitoring.Lib.Interfaces;

namespace AirTrafficMonitoring.Lib
{
    public class ConsoleView : IRender
    {
        public int Height { get; set; }
        public int Width { get; set; }
        private IOutput _output;


        public ConsoleView(IOutput output)
        {
            Height = Console.LargestWindowHeight - 10;
            Width = Console.LargestWindowWidth;

            _output = output;

            SetUpConsole();
        }

        private void SetUpConsole()
        {
            _output.SetWindowSize(Width, Height);
            
                
            for (int i = 0; i < Height; i++)
            {
                _output.SetCursorPosition(Width - Width / 3, i);
                _output.OutputLine("║");
            }

            for (int i = 0; i < Width - Width / 3; i++)
            {
                _output.SetCursorPosition(i, Height - Height / 3);
                _output.OutputLine("═");
            }

            _output.SetCursorPosition(Width - Width / 3, Height - Height / 3);
            _output.OutputLine("╣");
        }

        public void PrintTrackData(List<ITrack> tracks)
        {
            RenderMap(tracks);
            RenderTrackData(tracks);
        }

        public void PrintCollisionTracks(List<CollisionPairs> pairs, bool clearArea = false)
        {
            int lineNum = 0;
            if(clearArea) ClearCollisionArea();

            foreach (var pair in pairs)
            {
                _output.SetCursorPosition(0, (Height - Height / 3) + 1 + lineNum);
                _output.OutputLine(pair.ToString());
                lineNum++;
            }
        }

        private void ClearCollisionArea()
        {
            Clear(0, (Height - Height / 3) + 1, Width - Width / 3, Height);
        }

        private void RenderTrackData(List<ITrack> tracks)
        {
            //Clear((Width - Width / 3) + 1, 0, Width - 1, Height);

            int lineNum = 0;
            foreach (var track in tracks)
            {
                _output.SetCursorPosition((Width - Width / 3) + 1, lineNum);
                _output.OutputLine(track.ToString());
                lineNum++;
            }
        
        }

        private void RenderMap(List<ITrack> tracks)
        {
            Clear(0, 0, Width - Width / 3, Height - Height / 3);
            var aspectW = 80000 / (Width - Width / 3);
            var aspectH = 80000 / (Height - Height / 3);

            foreach (var track in tracks)
            {
                //_output.SetCursorPosition(track.PositionX % (Width - Width / 3), track.PositionY % (Height - Height / 3));
                _output.SetCursorPosition((track.PositionX-9999) / aspectW, (track.PositionY-9999) / aspectH);
                _output.OutputLine("x");
            }
        }

        private void Clear(int startX, int startY, int endX, int endY)
        {
            _output.SetCursorPosition(startX, startY);
            for (int y = startY; y < endY; y++)
            {
                _output.SetCursorPosition(startX, y);
                for (int x = startX; x < endX; x++)
                    _output.OutputLine(" ");
            }
        }
    }
}
