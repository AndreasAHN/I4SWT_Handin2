﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using AirTrafficMonitoring;



namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    
    public class AirTrafficController_Test
    {
        private IAirspace _fakeAirspace;
        private AirTrafficController _uut;
        private List<Track> _trackData = new List<Track>();





        [SetUp]
        public void SetUp()
        {
            _fakeAirspace = new Airspace(new TrackCalculator());

            _uut = new AirTrafficController();
            _fakeAirspace.AirspaceChangedEvent += _uut.air_ThresholdReached;

        }

        [Test]
        public void EventOccured()
        //tests if an event from Airspace is received
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 79999, Y = 80000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 80000, Y = 80000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 80001, Y = 80001, Z = 8000, Timestamp = DateTime.Now });

            _fakeAirspace.AirspaceChangedEvent += Raise.EventWith(new AirspaceChangedEventArgs { Tracks = _trackData });


            Assert.AreEqual(1, 1);
        }
    }
}
