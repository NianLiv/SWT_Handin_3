using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT8_ConsoleViewOutput
    {
        private IOutput _output;
        private ConsoleView _consoleView;
        private CollisionPairs _collisionPairs;
        private List<ITrack> _validTracks;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _output.When(x => x.GetLargetsScreenSize(out int w, out int h)).Do(x =>
            {
                x[0] = 80;
                x[1] = 40;
            });

            _consoleView = new ConsoleView(_output);

            _collisionPairs = new CollisionPairs(
                new Track
                {
                    Tag = "AAA111",
                    PositionX = 25000,
                    PositionY = 20000,
                    Altitude = 900
                },
                new Track
                {
                    Tag = "BBB222",
                    PositionX = 27000,
                    PositionY = 23000,
                    Altitude = 1100
                }, 
                DateTime.Now
            );

            _validTracks = new List<ITrack>
            {
                new Track
                {
                    Tag = "AAA111",
                    PositionX = 25000,
                    PositionY = 20000,
                    Altitude = 900
                },
                new Track
                {
                    Tag = "BBB222",
                    PositionX = 75000,
                    PositionY = 20000,
                    Altitude = 1900
                },
                new Track
                {
                    Tag = "CCC333",
                    PositionX = 12500,
                    PositionY = 38000,
                    Altitude = 2900
                }
            };
        }

        [Test]
        public void PrintTrackData_ClearIsCalled_OutputRecievesEmptyStringTwoThirdsOfHeightTimes()
        {
            _consoleView.PrintTrackData(_validTracks);
            var clearHeight = (_consoleView.Height - _consoleView.Height / 3);
            var clearWidth = (_consoleView.Width - _consoleView.Width / 3);
            _output.Received(clearHeight).OutputLine(new string(' ', clearWidth));
        }

        [Test]
        public void PrintTrackDate_RenderMapIsCalledWithListCount3_OutputRecievesCharXThreeTimes()
        {
            _consoleView.PrintTrackData(_validTracks);
            _output.Received(3).OutputLine("x");
        }


    }


   
}
