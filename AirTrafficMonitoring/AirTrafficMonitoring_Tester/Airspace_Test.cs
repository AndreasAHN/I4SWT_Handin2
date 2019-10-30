using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring_Tester
{
    [TestFixture]
    public class Airspace_Test
    {
        //private IAirspace _fakeAirspace;
        private ITransponderReceiverClient _fakeTransponderReceiverClient;
        //der skulle oprettes en fake af TransponderReceiver grundet constructoren i TransponderReceiverClient
        //altså bruges TransponderReceiver ikke her
        private ITransponderReceiver _faketransponderReceiver;

        private List<Track> _trackData;
        private TransponderReceiverClient _uut;
        private DataReceivedEventArgs _receivedEventArgs;


        [SetUp]
        public void Setup()
        {
            //_fakeAirspace = Substitute.For<IAirspace>();
            _fakeTransponderReceiverClient = Substitute.For<ITransponderReceiverClient>();

            _uut = new TransponderReceiverClient(_faketransponderReceiver);
            _trackData = new List<Track>();


            _trackData.Add(new Track { Tag = "GPJ740", X = 30000, Y = 30000, Z = 30000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "GPJ740", X = 30000, Y = 30000, Z = 30000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "GPJ740", X = 30000, Y = 30000, Z = 30000, Timestamp = DateTime.Now });

            _fakeTransponderReceiverClient.DataReadyEvent += Raise.EventWith(this, new DataReceivedEventArgs(_trackData));
        }



        [Test]
        public void Test_OutOfBounds()//et fly der ikke bevæger sig inden for Airspace´s rammer
        {
            var track = _uut.Tracks;
           
        }

        [Test]
        public void GetTracks()
        {

        }

        [Test]
        public void CLearTracks()
        {

        }


        
    }
}
