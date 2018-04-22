using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
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
        private List<CollisionPairs> _validCollisionPairs;

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
            _validCollisionPairs = new List<CollisionPairs> {_collisionPairs};
        }

        [Test]
        public void PrintTrackData_ClearIsCalled_OutputReceivesEmptyStringTwoThirdsOfHeightTimes()
        {
            _consoleView.PrintTrackData(_validTracks);
            var clearHeight = (_consoleView.Height - _consoleView.Height / 3);
            var clearWidth = (_consoleView.Width - _consoleView.Width / 3);
            _output.Received(clearHeight).OutputLine(new string(' ', clearWidth));
        }

        [Test]
        public void PrintTrackData_RenderMapIsCalledWithListCount3_OutputReceivesCharXThreeTimes()
        {
            _consoleView.PrintTrackData(_validTracks);
            _output.Received(3).OutputLine("x");
        }

        [Test]
        public void PrintTrackData_ValidList_OutputReceivesCursorPositionChange()
        {
            _consoleView.PrintTrackData(_validTracks);
            _output.ReceivedWithAnyArgs().SetCursorPosition(int.MaxValue, int.MaxValue);
        }

        [Test]
        public void PrintTrackData_RenderTrackData_OutputReceivesAllTrackTags()
        {
            _consoleView.PrintTrackData(_validTracks);

            foreach (var track in _validTracks)
                _output.Received(1).OutputLine(Arg.Is<string>(x => x.Contains(track.Tag)));
        }

        [Test]
        public void SetUpConsole_OutputReceivesGetLargestScreenSize()
        {
            // ConsoleView ctor is called in SetUp().
            _output.Received(1).GetLargetsScreenSize(out var w, out var h);
        }

        [Test]
        public void SetUpConsole_OutputReceivedGetLargesScreenSize_ViewWidthIs80()
        {
            // ConsoleView ctor is called in SetUp().
            Assert.That(_consoleView.Width, Is.EqualTo(80));
        }

        [Test]
        public void SetUpConsole_OutputReceivedGetLargestScreenSize_ViewHeightIs40()
        {
            // ConsoleView ctor is called in SetUp().
            Assert.That(_consoleView.Height, Is.EqualTo(40));
        }

        [Test]
        public void SetUpConsole_ArgHeigtAndWidth_SetWindowSizeWithWidthAndHeight()
        {
            // ConsoleView ctor is called in SetUp().
            _output.Received(1).SetWindowSize(_consoleView.Width, _consoleView.Height);
        }

        [Test]
        public void PrintCollisionTracks_OutputReceivesSetCursorPosition()
        {
            _consoleView.PrintCollisionTracks(_validCollisionPairs);
            _output.ReceivedWithAnyArgs().SetCursorPosition(0,0);
        }

        [Test]
        public void PrintCollisionTracks_ValidCollisionsListCount1_OutputReceivesCurrentTrackTag()
        {
            _consoleView.PrintCollisionTracks(_validCollisionPairs);
            _output.Received(1).OutputLine(Arg.Is<string>(x => x.Contains(_validCollisionPairs[0].currentTrack.Tag)));
        }

        [Test]
        public void PrintCollisionTracks_ValidCollisionsListCount1_OutputReceivesotherTrackTag()
        {
            _consoleView.PrintCollisionTracks(_validCollisionPairs);
            _output.Received(1).OutputLine(Arg.Is<string>(x => x.Contains(_validCollisionPairs[0].otherTrack.Tag)));
        }

    }


   
}
