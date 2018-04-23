using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace TOS.UnitTest
{
    [TestFixture()]
    class ConsoleViewUnitTest
    {
        private IRender _uut;
        private IOutput _output;

        private int _width;
        private int _height;

        private List<ITrack> _validTracks;
        private CollisionPairs _collisionPairs;
        private List<CollisionPairs> _validCollisionPairs;

        [SetUp]
        public void SetUp()
        {
            // The faked IOutpus must return a screen size to ConsoleViews ctor.
            _output = Substitute.For<IOutput>();
            _output.When(x => x.GetLargetsScreenSize(out int w, out int h)).Do(x =>
            {
                x[0] = 80;
                x[1] = 40;

                //ConsoleView.SetUp subtracks some from width and height cause cosmetics 
                _width = 80 - 15;
                _height = 40 - 10;
            });

            _uut = new ConsoleView(_output);

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
            _validCollisionPairs = new List<CollisionPairs> { _collisionPairs };
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesGetLargetsScreenSizeCall()
        {
            _output.Received(1).GetLargetsScreenSize(out var w, out var h);
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesSetWindowSizeCall()
        {
            _output.ReceivedWithAnyArgs(1).SetWindowSize(0,0);
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesVerticalLineHeightTimes()
        {
            _output.Received(_height).OutputLine("║");
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesLine2TimesTwoThirdsWidth()
        {
            _output.Received(_width - _width / 3).OutputLine("═");
        }

        [Test]
        public void Ctor_CallsSetUpConsole_OutputReceivesCornerCharOnce()
        {
            _output.Received(1).OutputLine("╣");
        }

        [Test]
        public void PrintTrackData_ClearIsCalled_OutputReceivesEmptyStringTwoThirdsOfHeightTimes()
        {
            _uut.PrintTrackData(_validTracks);
            var clearHeight = (_height - _height / 3);
            var clearWidth = (_width - _width / 3);
            _output.Received(clearHeight).OutputLine(new string(' ', clearWidth));
        }

        [Test]
        public void PrintTrackData_RenderMapIsCalledWithListCount3_OutputReceivesCharXThreeTimes()
        {
            _uut.PrintTrackData(_validTracks);
            _output.Received(3).OutputLine("x");
        }

        [Test]
        public void PrintTrackData_ValidList_OutputReceivesCursorPositionChange()
        {
            _uut.PrintTrackData(_validTracks);
            _output.ReceivedWithAnyArgs().SetCursorPosition(int.MaxValue, int.MaxValue);
        }

        [Test]
        public void PrintTrackData_RenderTrackData_OutputReceivesAllTrackTags()
        {
            _uut.PrintTrackData(_validTracks);

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
            // SetUpConsole subtracts 15 from width.
            Assert.That(_width, Is.EqualTo(80 - 15));
        }

        [Test]
        public void SetUpConsole_OutputReceivedGetLargestScreenSize_ViewHeightIs40()
        {
            // ConsoleView ctor is called in SetUp().
            // SetUpConsole subtracts 10 from height.
            Assert.That(_height, Is.EqualTo(40 - 10));
        }

        [Test]
        public void SetUpConsole_ArgHeigtAndWidth_SetWindowSizeWithWidthAndHeight()
        {
            // ConsoleView ctor is called in SetUp().
            _output.Received(1).SetWindowSize(_width, _height);
        }

        [Test]
        public void PrintCollisionTracks_OutputReceivesSetCursorPosition()
        {
            _uut.PrintCollisionTracks(_validCollisionPairs);
            _output.ReceivedWithAnyArgs().SetCursorPosition(0, 0);
        }

        [Test]
        public void PrintCollisionTracks_ValidCollisionsListCount1_OutputReceivesCurrentTrackTag()
        {
            _uut.PrintCollisionTracks(_validCollisionPairs);
            _output.Received(1).OutputLine(Arg.Is<string>(x => x.Contains(_validCollisionPairs[0].currentTrack.Tag)));
        }

        [Test]
        public void PrintCollisionTracks_ValidCollisionsListCount1_OutputReceivesotherTrackTag()
        {
            _uut.PrintCollisionTracks(_validCollisionPairs);
            _output.Received(1).OutputLine(Arg.Is<string>(x => x.Contains(_validCollisionPairs[0].otherTrack.Tag)));
        }

        [Test]
        public void PrintCollisionTracks_EmptyListArg2IsTrue_CollisionWindowIsCleared()
        {
            _uut.PrintCollisionTracks(_validCollisionPairs, true);
            _output.Received().OutputLine(new string(' ', _width - _width / 3));
        }

    }
}
