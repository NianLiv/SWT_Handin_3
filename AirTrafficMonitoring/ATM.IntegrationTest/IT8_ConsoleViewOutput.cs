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

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
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


        }
    }


   
}
