using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class TrackCalculator
    {
        private TrackCalculator calculator;

        // structs for testing as i don't yet know how the Track works

        public void CalculateVelocity(Track oldTrack, Track newTrack)
        {
            var distanceSquared = Math.Pow(oldTrack.X - newTrack.X, 2) + Math.Pow(oldTrack.Y - newTrack.Y, 2);
            var distance = Math.Sqrt(distanceSquared);

            var timeDifference = oldTrack.Timestamp - newTrack.Timestamp;

            var velocity = (timeDifference.Seconds/distance) * 3.6;

            Console.WriteLine($"Velocity: {velocity}");

        }

        public void CalculateCompassCourse(Track oldTrack, Track newTrack)
        {

            var angle = Math.Atan2(oldTrack.Y - newTrack.Y, oldTrack.X - newTrack.X);
            var compassCourse = angle * 180 / Math.PI;

            Console.WriteLine($"Compass course: {compassCourse}");
        }
    }
}
