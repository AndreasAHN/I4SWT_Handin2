using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    class Screen
    {
        public Screen()
        {

        }

        public void printTracks(Track ownTrack, List<Track> tracks)
        {
            Console.Clear();
            Console.WriteLine("Our Airplain: Track: {0} Cordinates: {1} , {2} , {3}  Speed: {4} Direction: {5} Time: {6}",
                    ownTrack.Tag, ownTrack.X, ownTrack.Y, ownTrack.Z, ownTrack.Velocity, ownTrack.CompassCourse, ownTrack.Timestamp);

            //if (tracks.Count != 0)
            //{
            //    for (int i = 0; i > (tracks.Count - 1); i++)
            //    {
            //        Console.WriteLine("{0}: Track: {1} Cordinates: {2} , {3} , {4}  Speed: {5} Direction: {6} Time: {7}",
            //            i, tracks[i].Tag, tracks[i].X, tracks[i].Y, tracks[i].Z, tracks[i].Velocity, tracks[i].CompassCourse, tracks[i].Timestamp);
            //    }
            //}
        }

        public void printConflict(Track ownTrack, Track conflictTrack, int vertical, int horizontal)
        {
            Console.WriteLine("\n" + "!WARNING-SEPERATION! {0} and {1} are too close, distance vertical: {2} and horizontal {3}", 
                ownTrack.Tag, conflictTrack.Tag, vertical, horizontal);
        }
    }
}
