using System;
using AirTrafficMonitoring.Lib;
using NSubstitute;
using NUnit.Framework;

namespace TOS.UnitTest
{
    [TestFixture]
    class TrackUnitTest
    {
        private ITrack _uut;
        private ITrack _newDataTrack;
        private TrackCalculator _trackCalculator;


        [SetUp]
        public void SetUp()
        {
            _uut = new Track
            {
                Tag = "AAA111",
                Altitude = 1900,
                Course = 180,
                PositionX = 28756,
                PositionY = 78562,
                Timestamp = DateTime.Now,
                Velocity = 218
            };
            _newDataTrack = Substitute.For<ITrack>();
            _newDataTrack.Tag = _uut.Tag;
            _trackCalculator = Substitute.For<TrackCalculator>();
        }

        [Test]
        public void ToString_TrackWithData_ReturnStringContainsAllButTimeProperties()
        {
            Assert.That(_uut.ToString(), Contains.Substring(_uut.Tag.ToString())
                                                .And.Contains(_uut.Altitude.ToString())
                                                .And.Contains(_uut.Course.ToString())
                                                .And.Contains(_uut.PositionX.ToString())
                                                .And.Contains(_uut.PositionY.ToString())
                                                .And.Contains(_uut.Velocity.ToString())
            );
        }

        [Test]
        public void Update_TrackWithNewAltitude_AltitudePropertiIsUpdated()
        {
            _newDataTrack.Altitude = 800;
            _uut.Update(_newDataTrack);
            Assert.That(_uut.Altitude, Is.EqualTo(_newDataTrack.Altitude));
        }

        [Test]
        public void Update_TrackWithNewPositionX_PositionXPropertiIsUpdated()
        {
            _newDataTrack.PositionX = 80000;
            _uut.Update(_newDataTrack);
            Assert.That(_uut.PositionX, Is.EqualTo(_newDataTrack.PositionX));
        }

        [Test]
        public void Update_TrackWithNewPositionY_PositionYPropertiIsUpdated()
        {
            _newDataTrack.PositionY = 12000;
            _uut.Update(_newDataTrack);
            Assert.That(_uut.PositionY, Is.EqualTo(_newDataTrack.PositionY));
        }

        [Test]
        public void Update_TrackWithNewTime_TimestampPropertiIsUpdated()
        {
            _newDataTrack.Timestamp = DateTime.Now;
            _uut.Update(_newDataTrack);
            Assert.That(_uut.Timestamp, Is.EqualTo(_newDataTrack.Timestamp));
        }

        // HOW TO NSUBSTITUDE .Recieved ON STATIC METHOD

        [TestCase(15237, 78562)]
        [TestCase(28756, 48720)]
        [TestCase(89999, 89000)]
        public void Update_TrackWithNewPositionAndTime_VelocityIsNotOldValue(int x, int y)
        {
            var oldVelocity = _uut.Velocity;

            _newDataTrack.PositionX = x;
            _newDataTrack.PositionY = y;
            _newDataTrack.Timestamp = DateTime.Now.AddSeconds(2);
            _uut.Update(_newDataTrack);

            Assert.That(_uut.Velocity, Is.Not.EqualTo(oldVelocity));
        }

        [TestCase(45237, 18562)]
        [TestCase(28756, 48720)]
        [TestCase(89999, 89000)]
        public void Update_TrackWithNewPosition_CourseIsNotOldValue(int x, int y)
        {
            var oldCourse = _uut.Course;

            _newDataTrack.PositionX = x;
            _newDataTrack.PositionY = y;
            _uut.Update(_newDataTrack);

            Assert.That(_uut.Course, Is.Not.EqualTo(oldCourse));
        }

        ///////////////////////////////////////////////////

    }
}
