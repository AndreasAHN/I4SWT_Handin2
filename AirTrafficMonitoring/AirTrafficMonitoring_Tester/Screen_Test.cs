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
    public class Screen_Test
    {
        private List<Track> _testData;

        [SetUp]
        public void Setup()
        {
            _testData = new List<Track>();

            _testData.Add(new Track { Tag = "GPJ740", X = 46155, Y = 16263, Z = 3500, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "QRM275", X = 31268, Y = 57982, Z = 600, Timestamp = DateTime.Now });
            _testData.Add(new Track { Tag = "ONC788", X = 28636, Y = 26560, Z = 500, Timestamp = DateTime.Now });
        }


        [Test]
        public void Test_SeperationFound()
        {
            Screen screen = new Screen();

            //StringAssert.Contains("TWM378", track[0].Tag);
            //StringAssert.Contains("GVC241", track[1].Tag);
            //StringAssert.Contains("WIA512", track[2].Tag);
        }

        public void Test_SeperationNoFound()
        {
            Condition condition = new Condition();
        }
    }
