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
    class IT4_TrackStorage
    {
        private Track _track;
        private Track _track1;
        private TrackStorage _trackStorage;

        [SetUp]
        public void SetUp()
        {
            _trackStorage = new TrackStorage();
            _track = new Track()
            {
                Tag = "FTZ7",
                Altitude = 1000,
                Course = 0,
                PositionX = 20000,
                PositionY = 25000,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
            _track1 = new Track();
        }


        [Test]
        public void Add_TrackAddedToStorage_NewTrack()
        {
            _trackStorage.Add(_track);

            Assert.That(_trackStorage.Contains(_track));
        }

        [Test]
        public void Add_InvalidTrack_Fails()
        {
            _trackStorage.Add(_track1);

            Assert.That(_trackStorage.Contains(_track1).Equals(false));
        }

    }
}
