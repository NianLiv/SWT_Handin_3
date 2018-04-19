using AirTrafficMonitoring.Lib;
using AirTrafficMonitoring.Lib.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace TOS.UnitTest
{
    [TestFixture]
    class TrackStorageUnitTest
    {
        private ITrackStorage _uut;
        private ITrack _track1;
        private ITrack _track2;
        private ITrack _track3;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackStorage();
            _track1 = Substitute.For<ITrack>();
            _track2 = Substitute.For<ITrack>();
            _track3 = Substitute.For<ITrack>();
        }

        [Test]
        public void Add_ValidTrack_ContainsTheTrack()
        {
            _track1.Tag = "AAA111";
            _uut.Add(_track1);
            Assert.That(_uut.Contains(_track1), Is.True);
        }

        [Test]
        public void Add_InvalidKeyTrack_DosentContainTheTrack()
        {
            _track1.Tag = "";
            _uut.Add(_track1);
            Assert.That(_uut.Contains(_track1), Is.False);
        }

        [Test]
        public void Add_ValidTrackTwice_OnlyAddOnce()
        {
            _track1.Tag = "AAA111";
            _uut.Add(_track1);
            _uut.Add(_track1);
            Assert.That(_uut.GetAllTracks().Count, Is.EqualTo(1));
        }

        [Test]
        public void Remove_ValidTrack_DosentContainTrack()
        {
            _track1.Tag = "AAA111";
            _uut.Add(_track1);
            _uut.Remove(_track1);
            Assert.That(_uut.Contains(_track1), Is.False);
        }

        [Test]
        public void Remove_ValidTrackNotStored_ReturnsFalse()
        {
            _track1.Tag = "AAA111";
            Assert.That(_uut.Remove(_track1), Is.False);
        }

        [Test]
        public void Clear_ThreeTracksIsStored_CountIsZero()
        {
            _track1.Tag = "AAA111";
            _track2.Tag = "BBB222";
            _track3.Tag = "CCC333";

            _uut.Add(_track1);
            _uut.Add(_track2);
            _uut.Add(_track3);

            _uut.Clear();
            Assert.That(_uut.GetAllTracks().Count, Is.Zero);
        }

        [Test]
        public void Contains_ValidTrackIsStored_ReturnTrue()
        {
            _track1.Tag = "AAA111";
            _uut.Add(_track1);
            Assert.That(_uut.Contains(_track1), Is.True);
        }

        [Test]
        public void Contains_ValidTrackNotStored_ReturnFalse()
        {
            _track1.Tag = "AAA111";
            Assert.That(_uut.Contains(_track1), Is.False);
        }

        [Test]
        public void Contains_InvalidTrackKey_ReturnFalse()
        {
            _track1.Tag = "";
            Assert.That(_uut.Contains(_track1), Is.False);
        }

        [Test]
        public void GetTrackByTag_ValidTagIsStored_ReturnedTrackHasSameTag()
        {
            var targetTag = "AAA111";
            _track1.Tag = targetTag;
            _uut.Add(_track1);
            Assert.That(_uut.GetTrackByTag(targetTag).Tag, Is.EqualTo(targetTag));
        }

        [Test]
        public void GetTrackByTag_ValidTagNotStored_ReturnNull()
        {
            Assert.That(_uut.GetTrackByTag("AAA111"), Is.Null);
        }

        [Test]
        public void GetTrackByTag_InvalidTag_ReturnNull()
        {
            Assert.That(_uut.GetTrackByTag(""), Is.Null);
        }

        [Test]
        public void Update_ValidTrackIsStored_UpdateIsCalled()
        {
            _track1.Tag = "AAA111";
            _uut.Add(_track1);
            _uut.Update(_track1);
            _track1.Received().Update(_track1);
        }

        [Test]
        public void Update_InvalidTrack_UpdateNotCalled()
        {
            _track1.Tag = "";
            _uut.Update(_track1);
            _track1.DidNotReceive().Update(_track1);
        }

        [Test]
        public void Update_ValidTrackNotStored_UpdateNotCalled()
        {
            _track1.Tag = "AAA111";
            _uut.Update(_track1);
            _track1.DidNotReceive().Update(_track1);
        }

        // MANGLER TEST PÅ "GetAllTracks" HVORDAN TESTER MAN PÅ EN LIST?


    }
}
