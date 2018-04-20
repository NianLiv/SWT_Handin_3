using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT2_ConsoleView
    {
        private ConsoleView _uut;
        private IOutput _output;
        private ITrack _track;
       // private CollisionPairs _collisionPairs;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
           // _collisionPairs = Substitute.For<CollisionPairs>();
            _track = new Track { Tag = "BBB222", PositionX = 80000, PositionY = 12000, Altitude = 1700 };

            _uut = new ConsoleView(_output);
            
           
        }

        [Test]
        public void OnTrackPrintData_Received_RenderMap()
        {
            var tracklist = new List<ITrack>();
            tracklist.Add(_track);


            //sat for at clear kan kaldes.
            _uut.Height = 15;
            _uut.Width = 15;

            //sat ud fra kode.
            var aspectW = 80000 / (_uut.Width - _uut.Width / 3);
            var aspectH = 80000 / (_uut.Height - _uut.Height / 3);


            //test 
            _uut.PrintTrackData(tracklist);
            _output.Received().OutputLine("x");
            _output.Received().SetCursorPosition((_track.PositionX - 10001) / aspectW, (_track.PositionY - 10001) / aspectH);
        }


    }


   
}
