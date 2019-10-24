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
        private Track _newTrack;
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

        [Test]
        public void CalculateVelocity()
        {
            
        }





    }
}
