using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    class TrackCalculator
    {
        private TrackCalculator calculator;
        //public Track oldTrack { get; set; }
        //public Track newTrack { get; set; }


        // structs for testing as i don't yet know how the Track works
        public struct oldTrack
        {
            public static int x = 95000;
            public static int y = 69547;
            public static long time = 20191023142125299;
        }

        public struct newTrack
        {
            public static int x = 94871;
            public static int y = 69517;
            public static long time = 20191023142125988;
        }

        public void CalculateVelocity()
        {
            var distanceSquared = Math.Pow(oldTrack.x - newTrack.x, 2) + Math.Pow(oldTrack.y - newTrack.y, 2);
            var distance = Math.Sqrt(distanceSquared);

            var timeDifference = oldTrack.time - newTrack.time;

            var velocity = distance / timeDifference;

            //Console.WriteLine($"Velocity: {velocity}");

        }

        public void CalculateCompassCourse()
        {

            var angle = Math.Atan2(oldTrack.y - newTrack.y, oldTrack.x - newTrack.x);
            var compassCourse = angle * 180 / Math.PI;

            //Console.WriteLine($"Compass course: {compassCourse}");
        }
    }
}
