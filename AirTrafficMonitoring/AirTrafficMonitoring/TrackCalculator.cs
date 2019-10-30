using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class TrackCalculator : ITrackCalculator
    {
        private List<Track> _oldTrack;

        public TrackCalculator()
        {
            _oldTrack = new List<Track>();
        }


        public List<Track> TrackCalculate(List<Track> track)
        {
            if (_oldTrack.Count != 0)
            {
                for (int i = 0; i < track.Count(); i++)
                {
                    for (int x = 0; x < _oldTrack.Count(); x++)
                    {
                        if (_oldTrack[x].Tag == track[i].Tag)
                        {
                            track[i].Velocity = CalculateVelocity(track[i], _oldTrack[x]);
                            track[i].CompassCourse = CalculateCompassCourse(track[i], _oldTrack[x]);
                        }
                    }
                }
            }

            _oldTrack = track.ToList();
            return track;
        }


        public int CalculateVelocity(Track newTrack, Track oldTrack)
        {
            var distanceSquared = Math.Pow(newTrack.X - oldTrack.X, 2) + Math.Pow(newTrack.Y - oldTrack.Y, 2);
            var distance = Math.Sqrt(distanceSquared);

            var timeDifference = (newTrack.Timestamp - oldTrack.Timestamp).Milliseconds;

            var velocity = distance / timeDifference * 1000;
            
            //Console.WriteLine("Velocity: {0} , {1}, {2}", velocity, newTrack.Tag, oldTrack.Tag);
            
            return (int) velocity;
        }





        public int CalculateCompassCourse(Track newTrack, Track oldTrack)
        {
            var angle = Math.Atan2(newTrack.Y - oldTrack.Y, newTrack.X - oldTrack.X);
            var compassCourseAngle = angle * 180 / Math.PI;

            if (compassCourseAngle < 0)
            {
                compassCourseAngle = compassCourseAngle + 360;
            }

            //Console.WriteLine("Compass course: {0}", compassCourseAngle);

            return (int) compassCourseAngle; 
        }
    }
}
