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

        public void TrackCalculate(Track track)
        {
            //track.Velocity = CalculateVelocity();
            //track.CompassCourse = CalculateCompassCourse();

            //List <Track> = TrackCalculator.calculate(List <Track> tracks);

        }

        // structs for testing as i don't yet know how the Track works

        public int CalculateVelocity(Track newTrack, Track oldTrack)
        {
            var distanceSquared = Math.Pow(newTrack.X - oldTrack.X, 2) + Math.Pow(newTrack.Y - oldTrack.Y, 2);
            var distance = Math.Sqrt(distanceSquared);

            var timeDifference = (newTrack.Timestamp - oldTrack.Timestamp).Milliseconds;

            var velocity = distance / timeDifference * 1000;

            return (int) velocity;

            //Console.WriteLine($"Velocity: {velocity}");

        }





        public int CalculateCompassCourse(Track newTrack, Track oldTrack)
        {

            var angle = Math.Atan2(newTrack.Y - oldTrack.Y, newTrack.X - oldTrack.X);
            var compassCourseAngle = angle * 180 / Math.PI;

            if (compassCourseAngle < 0)
            {
                compassCourseAngle = compassCourseAngle + 360;
            }

            return (int) compassCourseAngle;

            //Console.WriteLine($"Compass course: {compassCourse}");
        }
    }
}
