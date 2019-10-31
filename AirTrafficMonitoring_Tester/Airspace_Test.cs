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
        private Airspace _uut;
        private DataReceivedEventArgs _receivedEventArgs;


        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiverClient = Substitute.For<ITransponderReceiverClient>();

            _uut = new Airspace(new TrackCalculator());
            _trackData = new List<Track>();

            _fakeTransponderReceiverClient.DataReadyEvent += _uut.HandleDataReadyEvent;
        }



        [Test]
        public void Test_OutOfBounds()
        //et fly der ikke bevæger sig inden for Airspace´s rammer, kommer ikke ind
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 79999, Y = 79999, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 80000, Y = 80000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 80001, Y = 80001, Z = 8000, Timestamp = DateTime.Now });

            _fakeTransponderReceiverClient.DataReadyEvent += Raise.EventWith(this, new DataReceivedEventArgs(_trackData));

            Assert.AreEqual(2, _uut.GetTracks().Count);

            
            /*
             Her forventes der 3 fly, men der kommer 0 ind, da alle tre fly har et track, der overstiger de grænser, 
             der er sat for det overvågede luftrummet            
             */
        }

        [Test]
        public void Test_InBounds()
        //et fly der bevæger sig inden for Airspace´s rammer, kommer ind
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 70000, Y = 70000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 70000, Y = 70000, Z = 8000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 70000, Y = 70000, Z = 8000, Timestamp = DateTime.Now });


            _fakeTransponderReceiverClient.DataReadyEvent += Raise.EventWith(this, new DataReceivedEventArgs(_trackData));
            Assert.AreEqual(3, _uut.GetTracks().Count);

            /*
             Her forventes 3 fly at komme ind det overvågede luftrummet, da alle tre fly har et track, der 
             holder sig inden fir luftrummets grænser
             */
        }


        [Test]
        public void no_lower_than_500()
            //tjekker at et fly ikke bevæger sig ind i det overvågede luftrum, hvis det har en Z værdi lavere end 500
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 70000, Y = 70000, Z = 499, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 70000, Y = 70000, Z = 500, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 70000, Y = 70000, Z = 501, Timestamp = DateTime.Now });

            _fakeTransponderReceiverClient.DataReadyEvent += Raise.EventWith(this, new DataReceivedEventArgs(_trackData));

            Assert.AreEqual(2, _uut.GetTracks().Count);
        }
        [Test]
        public void no_higher_than_20000()
            //tjekker at et fly ikke bevæger sig ind i det overvågede luftrum, hvis det har en Z værdi højere end 20000
        {
            _trackData.Add(new Track { Tag = "AAA111", X = 70000, Y = 70000, Z = 19999, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "BBB222", X = 70000, Y = 70000, Z = 20000, Timestamp = DateTime.Now });
            _trackData.Add(new Track { Tag = "CCC333", X = 70000, Y = 70000, Z = 20001, Timestamp = DateTime.Now });

            _fakeTransponderReceiverClient.DataReadyEvent += Raise.EventWith(this, new DataReceivedEventArgs(_trackData));

            Assert.AreEqual(2, _uut.GetTracks().Count);

        }
 
    }
}
