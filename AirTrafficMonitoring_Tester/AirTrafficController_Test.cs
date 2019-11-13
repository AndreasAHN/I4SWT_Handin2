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
        private ITransponderReceiverClient _fakeTransponderReceiverClient;





        [SetUp]
        public void SetUp()
        {
            _fakeAirspace = new Airspace(new TrackCalculator());
            _fakeTransponderReceiverClient = Substitute.For<ITransponderReceiverClient>();

            _uut = new AirTrafficController();

            _fakeAirspace.AirSpaceChanged += _uut.air_ThresholdReached;

            _fakeTransponderReceiverClient.DataReadyEvent += _fakeAirspace.HandleDataReadyEvent;

        }

        [Test]
        public void EventOccured()
        //tests if an event from Airspace is received
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 70000, Y = 70000, Z = 499, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 70000, Y = 70000, Z = 500, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 70000, Y = 70000, Z = 501, Timestamp = DateTime.Now });

            
            var wasCalled = false;
            _fakeAirspace.AirSpaceChanged += (o, e) => wasCalled = true;




            _fakeAirspace.HandleDataReadyEvent(null, new DataReceivedEventArgs(_trackData));

            Assert.IsTrue(wasCalled);
        }

    }
}
