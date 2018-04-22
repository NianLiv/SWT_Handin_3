using System;
using NUnit.Framework;
using AirTrafficMonitoring.Lib;

namespace ATM.IntegrationTest
{
    [TestFixture]
    class IT1_PointTrackCalculator
    {
        private Point _point1;
        private Point _point2;

        [SetUp]
        public void SetUp()
        {
            _point1 = new Point(12000, 20000);
            _point2 = new Point(20000, 32000);
        }

        [Test]
        public void CalculateVelocity_CorrectVelocityFrom2Points_CalcCallsDistanceToInPointsCorrect()
        {
            var time1 = DateTime.Now;
            var time2 = time1.AddSeconds(5);
            var length = Math.Sqrt(Math.Pow(_point2.X - _point1.X, 2) + Math.Pow(_point2.Y - _point1.Y, 2));
            var deltaTime = time2 - time1;
            var correctVelocity = Math.Round(length / deltaTime.TotalSeconds, 3);

            var result = TrackCalculator.CalculateVelocity(_point1.X, _point1.Y, _point2.X, _point2.Y, time1, time2);

            // If asserts is true, calculator most call and use point.distanceTo() correct.
            Assert.That(result, Is.EqualTo(correctVelocity));
        }


    }
}
