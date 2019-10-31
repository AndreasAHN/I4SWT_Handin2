using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Screen : IScreen
    {
        public Screen()
        {

        }

        public void printTracks(List<Track> tracks)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");

            if (tracks.Count != 0)
            {
                for (int i = 0; i < tracks.Count; i++)
                {
                    Console.WriteLine("{0}:\tTag: {1}\tCoordinates: ({2}, {3}, {4})\tSpeed: {5}\tBearing: {6}\tTime: {7}",
                        i, tracks[i].Tag, tracks[i].X, tracks[i].Y, tracks[i].Z, tracks[i].Velocity, tracks[i].CompassCourse, tracks[i].Timestamp);
                }
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
        }

        public void printConflict(Track conflictTrack1, Track conflictTrack2)
        {
            Console.WriteLine("\n" + "!WARNING-SEPERATION! {0} and {1}, at: {2}",
                conflictTrack1.Tag, conflictTrack2.Tag, conflictTrack1.Timestamp);
        }
    }
}
