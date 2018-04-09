using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using AirTrafficMonitoring.Lib;

namespace TOS.UnitTest
{
    [TestFixture]
    public class Class1
    {
        private ITransponderReceiver _transponderReceiver;
        private Tos _uut;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Tos(_transponderReceiver);
        }

        [Test]
        public void Test()
        {
            var TrackList = new List<string>();
            TrackList.Add("ATR423;39045;12932;14000;20151006213456789");
            var args = new RawTransponderDataEventArgs(TrackList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_uut.RecievedTracks["ATR423"].PositionX, Is.EqualTo(39045));
        }

    }
}
