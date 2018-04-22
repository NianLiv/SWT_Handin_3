using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Lib;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT4_TrackStorage
    {
        private Track _track;
        private Track _track1;
        private Track _track2;
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
            _track2 = new Track()
            {
                Tag = "GFC2",
                Altitude = 1500,
                Course = 0,
                PositionX = 40000,
                PositionY = 35000,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
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

        [Test]
        public void Remove_TrackOnStorage2Tracks_1TrackInStorage()
        {
            _trackStorage.Add(_track);
            _trackStorage.Add(_track2);

            _trackStorage.Remove(_track2);

            Assert.That(_trackStorage.Contains(_track2).Equals(false));

        }

        [Test]
        public void GetTrackByTag_1TrackInStorage_ReturnsTrack()
        {
            _trackStorage.Add(_track);

           // var tracktag = _trackStorage.GetTrackByTag("FTZ7");
            
            Assert.That(_trackStorage.GetTrackByTag("FTZ7"), Is.EqualTo(_track));
            
        }


        //Test Update?
    }
}
