using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using AirTrafficMonitoring.Lib;

namespace TOS.UnitTest
{
    [TestFixture]
    public class AirTrafficMonitoringTest
    {
        private ITransponderReceiver _transponderReceiver;
        private Tos _uut;

       [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Tos(_transponderReceiver);
        }

        //Test to check if the Track object is correctly stored.
        [Test]
        public void Objectify_Track_OnEventRecieved()
        {
            var TrackList = new List<string>();
            TrackList.Add("ATR423;39045;12932;14000;20151006213456789");
            var args = new RawTransponderDataEventArgs(TrackList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //Using multiple asserts to ensure every property is stored correct.
            Assert.That(_uut.RecievedTracks[0].PositionX, Is.EqualTo(39045));
            Assert.That(_uut.RecievedTracks[0].PositionY, Is.EqualTo(12932));
            Assert.That(_uut.RecievedTracks[0].Altitude, Is.EqualTo(14000));
            Assert.That(_uut.RecievedTracks[0].Timestamp, Is.EqualTo(new DateTime(2015, 10, 06, 21, 34, 56, 789)));
        }

       
        //Test to check if track is updated correctly.
        [Test]
        public void EventHandler_DifferentTrack_DictionaryCountEquals2()
        {
            var TrackList = new List<string>();
            //Track position with tag
            TrackList.Add("ATR423;39045;12932;14000;20151006213456789");

            //Track new position with same tag
            TrackList.Add("BBB425;38045;12332;14100;20151006213456789");
            var args = new RawTransponderDataEventArgs(TrackList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //Only one instance of the object is stored.
            Assert.That(_uut.RecievedTracks.Count, Is.EqualTo(2));
        }

        //Test to check for wrong input
        [Test]
        public void WrongInput_DictionaryCountEquals0()
        {
            var TrackList = new List<string>();

            //Wrong string
            TrackList.Add("Wierd string");
            var args = new RawTransponderDataEventArgs(TrackList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //The wierd string is not identified as a track, therefor not added to dictionary.
            Assert.That(_uut.RecievedTracks.Count, Is.EqualTo(0));
        }

        [Test]
        public void Notify_UpdateOnObserverCalled()
        {
            var _observer = Substitute.For<AirTrafficMonitoring.Lib.IObserver<AirTrafficController>>();
            var TrackList = new List<string>();

            //Track position with tag
            TrackList.Add("ATR423;39045;12932;14000;20151006213456789");
            var args = new RawTransponderDataEventArgs(TrackList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //_observer.Received().Update();
        }

    }
}
