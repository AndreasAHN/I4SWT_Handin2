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
    public class Airspace_Test
    {
        private List<Track> _trackData;
        private Airspace _uut;
        private TrackCalculator _trackCalculator;


        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test_OutofBounds()
        {
            _trackData = new List<Track>();
            _trackData.Add(new Track { Tag = "GPJ740", X = 90000, Y = 90000, Z = 90000, Timestamp = DateTime.Now });
        }



        
    }
}
