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

        //private Track _oldTrack;
        //private static Track _newTrack;

        [SetUp]
        public void SetUp()
        {
            _fakeTrackCalculator = Substitute.For<ITrackCalculator>();
            _uut = new TrackCalculator(_fakeTrackCalculator);

            _testTracksOld = new List<Track>();
            _testTracksNew = new List<Track>();

            var oldTrack = new Track()
            {
                Tag = "JYG338",
                X = 5258,
                Y = 57189,
                Z = 5000,
                Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null)
            };

            Track oldTrack1 = new Track()
            {
                Tag = "GVC241",
                X = 38594,
                Y = 77966,
                Z = 10900,
                Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null)
            };

            Track oldTrack2 = new Track()
            {
                Tag = "WIA512",
                X = 17357,
                Y = 24364,
                Z = 2100,
                Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null)
            };

            _testTracksOld.Add(oldTrack);
            _testTracksOld.Add(oldTrack1);
            _testTracksOld.Add(oldTrack2);

            Track newTrack = new Track()
            {
                Tag = "JYG338",
                X = 5387,
                Y = 57076,
                Z = 5000,
                Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null)

            };

            Track newTrack1 = new Track()
            {
                Tag = "GVC241",
                X = 38794,
                Y = 78066,
                Z = 10800,
                Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null)
            };

            Track newTrack2 = new Track()
            {
                Tag = "WIA512",
                X = 18357,
                Y = 25364,
                Z = 2000,
                Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null)
            };

            _testTracksNew.Add(newTrack);
            _testTracksNew.Add(newTrack1);
            _testTracksNew.Add(newTrack2);
        }

        // testing actual functions but only one set of new/old Tracks
        [Test]
        public void Test_Velocity_Calc()
        {
            var test0 = _uut.CalculateVelocity(_testTracksNew[0], _testTracksOld[0]);
            var test1 = _uut.CalculateVelocity(_testTracksNew[1], _testTracksOld[1]);
            var test2 = _uut.CalculateVelocity(_testTracksNew[2], _testTracksOld[2]);
            Assert.AreEqual(249, test0);
            Assert.AreEqual(325, test1);
            Assert.AreEqual(2055, test2);
        }

        [Test]
        public void Test_Course_Calc()
        {
            var test0 = _uut.CalculateCompassCourse(_testTracksNew[0], _testTracksOld[0]);
            var test1 = _uut.CalculateCompassCourse(_testTracksNew[1], _testTracksOld[1]);
            var test2 = _uut.CalculateCompassCourse(_testTracksNew[2], _testTracksOld[2]);
            Assert.AreEqual(318,test0);
            Assert.AreEqual(26, test1);
            Assert.AreEqual(45, test2);
        }






        // tests replicating functions for troubleshooting
        [TestCase(5387,5258,57076,57189,249)]
        [TestCase(368, 83555, 4527, 257, 121070)]
        [TestCase(896, 12385, 9864, 9, 22000)] // that result was random wtf
        [TestCase(24664, 5456258, 5703476, 54437189, 71272480)]
        public void Test_Math_Velocity(int newX, int oldX, int newY, int oldY, double result)
        {

            DateTime oldTime = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null);
            DateTime newTime = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null);

            var distanceSquared = Math.Pow(newX - oldX, 2) + Math.Pow(newY - oldY, 2);
            var distance = Math.Sqrt(distanceSquared);

            var timeDifference = (newTime - oldTime).Milliseconds;

            var velocity = (int) (distance / timeDifference * 1000);

            Assert.AreEqual(result, velocity);
        }

        [TestCase(34674, 6345, 213, 78698, 160)]
        [TestCase(78066, 77966, 38794, 38594, 26)]
        [TestCase(34, 3456, 214, 936, 258)]
        public void Test_Math_Compass(int newY, int oldY, int newX, int oldX, double result)
        {
            var angle = Math.Atan2(newY - oldY, newX - oldX);
            var compassCourseAngle = (angle * 180) / Math.PI;

            if (compassCourseAngle < 0)
            {
                compassCourseAngle = compassCourseAngle + 360;
            }

            Assert.AreEqual(result, (int) compassCourseAngle);
        }


    }
}