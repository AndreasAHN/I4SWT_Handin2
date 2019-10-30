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
    public class TransponderReceiverClient_Track_Test
    {
        // Fra det udleverede materiale
        private ITransponderReceiver _fakeTransponderReceiver;
        private TransponderReceiverClient _uut;
        private List<string> _testData;
        [SetUp]
        public void Setup()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
            _testData = new List<string>();

            _testData.Add("TWM378;88244;50602;10200;20191024005234115");
            _testData.Add("GVC241;37594;77966;10900;20191024005308490");
            _testData.Add("WIA512;17357;24364;2100;20191024005318802");

            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(_testData));
        }


        [Test]
        public void Test_Tag_Insert()
        {
            var track = _uut.Tracks;
            StringAssert.Contains("TWM378", track[0].Tag);
            StringAssert.Contains("GVC241", track[1].Tag);
            StringAssert.Contains("WIA512", track[2].Tag);
        }

        [Test]
        public void Test_X_Insert()
        {
            var track = _uut.Tracks;
            Assert.AreEqual(88244, track[0].X);
            Assert.AreEqual(37594, track[1].X);
            Assert.AreEqual(17357, track[2].X);
        }

        [Test]
        public void Test_Y_Insert()
        {
            var track = _uut.Tracks;
            Assert.AreEqual(50602, track[0].Y);
            Assert.AreEqual(77966, track[1].Y);
            Assert.AreEqual(24364, track[2].Y);
        }

        [Test]
        public void Test_Z_Insert()
        {
            var track = _uut.Tracks;
            Assert.AreEqual(10200, track[0].Z);
            Assert.AreEqual(10900, track[1].Z);
            Assert.AreEqual(2100, track[2].Z);
        }

        [Test]
        public void Test_Timestamp_Insert()
        {
            var track = _uut.Tracks;
            DateTime a = DateTime.ParseExact("20191024005234115", "yyyyMMddHHmmssfff", null);
            DateTime b = DateTime.ParseExact("20191024005308490", "yyyyMMddHHmmssfff", null);
            DateTime c = DateTime.ParseExact("20191024005318802", "yyyyMMddHHmmssfff", null);
            
            Assert.AreEqual(a, track[0].Timestamp);
            Assert.AreEqual(b, track[1].Timestamp);
            Assert.AreEqual(c, track[2].Timestamp);
            Assert.AreNotEqual(a, track[1].Timestamp);
        }
    }
}
