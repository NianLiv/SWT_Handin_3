using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using System;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT5_TrackTos
    {
        private Track _track;
        private Tos _tos;

        [SetUp]
        public void SetUp()
        {
            //_tos = new Tos();
            _track = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
        }

    }
}
