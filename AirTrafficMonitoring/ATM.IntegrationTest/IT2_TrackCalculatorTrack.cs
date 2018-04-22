using AirTrafficMonitoring.Lib;
using NUnit.Framework;
using System;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT2_TrackCalculatorTrack
    {
        private Track _track;

        [SetUp]
        public void SetUp()
        {
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
        public void Update_NewTrackPosition_CourseRecalculated()
        {
            var oldCourse = _track.Course;
            var newTrack = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };

            newTrack.PositionX = 45000;
            newTrack.PositionY = 15000;
            _track.Update(newTrack);

            Assert.That(_track.Course, Is.Not.EqualTo(oldCourse));
        }

        [Test]
        public void Update_TrackNotChanged_CourseRecalculatedToZero()
        {
            _track.Update(_track);

            Assert.That(_track.Course, Is.EqualTo(0));
        }

        [Test]
        public void Update_TrackPositionMultipliedBySameFactor_RecalculationDosentChangeCourse()
        {

            var newTrack = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };
           
            // Updates uut's course to real angle.
            newTrack.PositionY *= 2;
            newTrack.PositionX *= 2;
            _track.Update(newTrack);

            // Saves the course.
            var oldCourse = _track.Course;

            // Multiple the position with a factor of 2, and update the track. 
            newTrack.PositionY *= 2;
            newTrack.PositionX *= 2;
            _track.Update(newTrack);
            
            // Course should not change. 
            Assert.That(_track.Course, Is.EqualTo(oldCourse));
        }


        [Test]
        public void Update_NewTrackPositionAndTime_VelocityRecalculated()
        {
            var oldVelocity = _track.Velocity;
            var newTrack = new Track
            {
                Tag = "AAA111",
                PositionX = 15000,
                PositionY = 30000,
                Altitude = 850,
                Course = 0,
                Timestamp = DateTime.Now,
                Velocity = 0
            };

            newTrack.PositionX = 45000;
            newTrack.Timestamp = newTrack.Timestamp.AddSeconds(2);
            _track.Update(newTrack);

            Assert.That(_track.Velocity, Is.Not.EqualTo(oldVelocity));
        }

        [Test]
        public void Update_TrackNotChanged_VelocityRecalculatedToZero()
        {
            _track.Update(_track);

            Assert.That(_track.Velocity, Is.EqualTo(0));
        }
    }
}
