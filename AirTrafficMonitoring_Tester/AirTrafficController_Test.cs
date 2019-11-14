using System;
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
            _uut = new AirTrafficController();
        }

        [Test]
        public void EventOccured()
        //tests if an event from Airspace is received
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 5000, Y = 5000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 5000, Y = 5000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 5000, Y = 5000, Z = 8000, Timestamp = DateTime.Now });

            // Final destination test 
            _uut.air_ThresholdReached(this, new AirspaceChangedEventArgs{Tracks = _trackData});
        }
    }
}
