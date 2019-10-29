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
        private List<Track> _oldTrack = new List<Track>();

        public List<Track> TrackCalculate(List<Track> track)
        {
            List<Track> buffTrack = new List<Track>();

            if (track.Count() >= 1)
            {
                for (int i = 0; i < track.Count; i++)
                {
                    bool found = false;

                    for (int x = 0; x < _oldTrack.Count; x++)
                    {
                        if (_oldTrack[x].Tag == track[i].Tag)
                        {
                            buffTrack.Add(track[i]);
                            buffTrack[buffTrack.Count - 1].Velocity = CalculateVelocity(track[i], _oldTrack[x]);
                            buffTrack[buffTrack.Count - 1].CompassCourse = CalculateCompassCourse(track[i], _oldTrack[x]);
                            found = true;
                        }
                        else if ((x < _oldTrack.Count) && (found == false))
                        {
                            buffTrack.Add(track[i]);
                        }
                    }
                }

                //_oldTrack.Clear();
                _oldTrack = track;

                return buffTrack;
            }
            else
            {
                _oldTrack = track;
                return track;
            }

        }


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
