using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using ATM.ClassLib;

namespace ATM.UnitTests
{
    [TestFixture]
    public class Class1
    {
        private ITransponderReceiver _transponderReceiver;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
        }

        [Test]
        public void Test1()
        {
            var store = new TrackStore();
            var uut = new Receiver(store);
            //liste af strengs
            var args = new RawTransponderDataEventArgs();

        }
        

    }
}
