using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NUnit.Framework;

namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    public class TrackCalculator_Test
    {
        private TrackCalculator _uut;
        private Track _oldTrack;
        private static Track _newTrack;

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackCalculator();

            _oldTrack = new Track()
            {
                Tag = "JYG338",
                X = 5258,
                Y = 57189,
                Z = 5000,
                Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null)
        };

            _newTrack = new Track()
            {
                Tag = "JYG338",
                X = 5387,
                Y = 57076,
                Z = 5000,
                Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null)

            };
        }

        // testing actual functions but only one set of new/old Tracks
        [Test]
        public void Test_Velocity_Calc()
        {
            var test = _uut.CalculateVelocity(_newTrack, _oldTrack);
            Assert.AreEqual(249, test);
        }

        [Test]
        public void Test_Course_Calc()
        {
            var test = _uut.CalculateCompassCourse(_newTrack, _oldTrack);
            Assert.AreEqual(318,test);
        }


        // tests replicating functions because i suck - testing many track values
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
        [TestCase(34, 3456, 214, 936, 258)]
        public void Test_Math_Compass(int newY, int oldY, int newX, int oldX, double result)
        {
            var angle = Math.Atan2(newY - oldY, newX - oldX);
            var compassCourseAngle = angle * 180 / Math.PI;

            if (compassCourseAngle < 0)
            {
                compassCourseAngle = compassCourseAngle + 360;
            }

            Assert.AreEqual(result, (int) compassCourseAngle);
        }


    }
}