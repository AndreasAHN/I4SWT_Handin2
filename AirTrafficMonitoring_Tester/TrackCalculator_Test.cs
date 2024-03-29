﻿using System;
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
            _testTracksOld.Add(new Track { Tag = "WIA512", X = 17357, Y = 24364, Z = 2100, Timestamp = DateTime.ParseExact("20191024155401709", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });

            _testTracksNew.Add(new Track { Tag = "JYG338", X = 5387, Y = 57076, Z = 5000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksNew.Add(new Track { Tag = "GVC241", X = 38794, Y = 78066, Z = 10800, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });
            _testTracksNew.Add(new Track { Tag = "WIA512", X = 18357, Y = 25364, Z = 2000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 0, Velocity = 0 });



        }

        [Test]
        public void Test_TrackCalculate()
        {
            var test0 = _uut.TrackCalculate(_testTracksOld);
            var test1 = _uut.TrackCalculate(_testTracksNew);

            var resTrack = new List<Track>();
            resTrack.Add(new Track { Tag = "JYG338", X = 5387, Y = 57076, Z = 5000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 318, Velocity = 249 });
            resTrack.Add(new Track { Tag = "GVC241", X = 38794, Y = 78066, Z = 10800, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 26, Velocity = 325 }); 
            resTrack.Add(new Track { Tag = "WIA512", X = 18357, Y = 25364, Z = 2000, Timestamp = DateTime.ParseExact("20191024155402397", "yyyyMMddHHmmssfff", null), CompassCourse = 45, Velocity = 2055 });

            for (int i = 0; i < resTrack.Count; i++)
            {
                Console.WriteLine("Actual " + i +"\t" + test1[i].Tag + test1[i].X + test1[i].Y + test1[i].Z + test1[i].Velocity + test1[i].CompassCourse + "\t" + test1[i].Timestamp);
                Console.WriteLine("Expected " + i + "\t" + resTrack[i].Tag + resTrack[i].X + resTrack[i].Y + resTrack[i].Z + resTrack[i].Velocity + resTrack[i].CompassCourse + "\t" + resTrack[i].Timestamp);

                Assert.AreEqual(resTrack[i].CompassCourse, test1[i].CompassCourse);
                Assert.AreEqual(resTrack[i].Velocity, test1[i].Velocity);
            }
            Assert.AreEqual(resTrack[0].CompassCourse, test1[0].CompassCourse);
            Assert.AreEqual(resTrack[0].Velocity, test1[0].Velocity);
            Assert.AreEqual(resTrack[1].CompassCourse, test1[1].CompassCourse);
            Assert.AreEqual(resTrack[1].Velocity, test1[1].Velocity);
            Assert.AreEqual(resTrack[2].CompassCourse, test1[2].CompassCourse);
            Assert.AreEqual(resTrack[2].Velocity, test1[2].Velocity);

        }

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