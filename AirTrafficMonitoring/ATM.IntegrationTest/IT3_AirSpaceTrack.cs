using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT3_AirSpaceTrack
    {
        private Track _track;
        private AirSpace _airspace;

        [SetUp]
        public void SetUp()
        {
            _airspace = new AirSpace();
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

        [Test]
        public void IsInValidAirSpace_TrackIsInAirSpace_ReturnTrue()
        {
            Assert.That(_airspace.IsInValidAirSpace(_track), Is.EqualTo(true));
        }

        [Test]
        public void IsInValidAirSpace_TrackHorizontalPositionIsOutOfAispace_ReturnFalse()
        {
            _track.PositionX = 90001;
            Assert.That(_airspace.IsInValidAirSpace(_track), Is.EqualTo(false));
        }

        [Test]
        public void IsInValidAirSpace_TrackVerticalPositionIsOutOfAirSpace_ReturnFalse()
        {
            _track.Altitude = 20001;
            Assert.That(_airspace.IsInValidAirSpace(_track), Is.EqualTo(false));
        }
    }
}
