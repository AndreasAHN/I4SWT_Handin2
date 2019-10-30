using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    public class TrackCalculator_Test 
    {
        private ITrackCalculator _fakeTrackCalculator;
        private TrackCalculator _uut;
        private List<Track> _testTracksOld;
        private List<Track> _testTracksNew;
        private List<Track> _testTracks;

        //private Track _oldTrack;
        //private static Track _newTrack;

        [SetUp]
        public void SetUp()
        {
            _fakeTrackCalculator = Substitute.For<ITrackCalculator>();
            _uut = new TrackCalculator(_fakeTrackCalculator);

            _testTracksOld = new List<Track>();
            _testTracksNew = new List<Track>();
            _testTracks = new List<Track>();

            _testTracksOld.Add(new Track { Tag = "JYG338", X = 5258, Y = 57189, Z = 5000, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksOld.Add(new Track { Tag = "GVC241", X = 38594, Y = 77966, Z = 10900, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksOld.Add(new Track { Tag = "WIA512",X = 17357, Y = 24364, Z = 2100, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });

            _testTracksNew.Add(new Track { Tag = "JYG338", X = 5387, Y = 57076, Z = 5000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksNew.Add(new Track { Tag = "GVC241", X = 38794, Y = 78066, Z = 10800, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksNew.Add(new Track { Tag = "WIA512", X = 18357, Y = 25364, Z = 2000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });

            _testTracks.Add(_testTracksOld[0]);
            _testTracks.Add(_testTracksNew[0]);
            _testTracks.Add(_testTracksOld[1]);
            _testTracks.Add(_testTracksNew[1]);
            _testTracks.Add(_testTracksOld[2]);
            _testTracks.Add(_testTracksNew[2]);

        }



        //[Test]
        //public void Test_TrackCalculate()
        //{
        //    foreach (var v in _testTracks)
        //    {
        //        var calc = _uut.TrackCalculate(_testTracks);
        //    }
        //    var test0 = _uut.TrackCalculate(_testTracks);

        //    var resTrack = new List<Track>();
        //    resTrack.Add(new Track { Tag = "JYG338", X = 5258, Y = 57189, Z = 5000, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
        //    resTrack.Add(new Track { Tag = "GVC241", X = 38594, Y = 77966, Z = 10900, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 325, Velocity = 26 }); 
        //    resTrack.Add(new Track { Tag = "WIA512", X = 17357, Y = 24364, Z = 2100, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 2055, Velocity = 45 });

        //    Console.WriteLine("Actual:   (" + test0[1].X + ", " + test0[1].Y + "). Vel: " + test0[1].Velocity + ". Compass: " + test0[1].CompassCourse);
        //    Console.WriteLine("Expected: (" + resTrack[1].X + ", " + resTrack[1].Y + "). Vel: " + resTrack[1].Velocity + ". Compass: " + resTrack[1].CompassCourse);

        //    Assert.AreEqual(resTrack[1], test0[1]);

        //}

        [Test]
        public void Test_CalculateVelocity()
        {
            var test0 = _uut.CalculateVelocity(_testTracksNew[0], _testTracksOld[0]);
            var test1 = _uut.CalculateVelocity(_testTracksNew[1], _testTracksOld[1]);
            var test2 = _uut.CalculateVelocity(_testTracksNew[2], _testTracksOld[2]);
            Assert.AreEqual(249, test0);
            Assert.AreEqual(325, test1);
            Assert.AreEqual(2055, test2);
        }

        [Test]
        public void Test_CalculateCompassCourse()
        {
            var test0 = _uut.CalculateCompassCourse(_testTracksNew[0], _testTracksOld[0]);
            var test1 = _uut.CalculateCompassCourse(_testTracksNew[1], _testTracksOld[1]);
            var test2 = _uut.CalculateCompassCourse(_testTracksNew[2], _testTracksOld[2]);
            Assert.AreEqual(318,test0);
            Assert.AreEqual(26, test1);
            Assert.AreEqual(45, test2);
        }
    }
}