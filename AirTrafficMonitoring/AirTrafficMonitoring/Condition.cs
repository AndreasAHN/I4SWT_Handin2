using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    class Condition
    {
        public bool sepration = false;
        public Track conflictTrack;
        public int conflicDistanceVertical;
        public int conflicDistacneHorizontal;

        public Condition()
        {

        }

        public bool TooClose(Track ownTrack, List<Track> tracks )
        {
            sepration = false;
            for(int i = 0 ; i<tracks.Count() ; i++)
            {
                int distanceVertical = 0;
                int distanceHorizontal = 0;

                distanceVertical = ((ownTrack.X - tracks[i].X) * (ownTrack.X - tracks[i].X) + (ownTrack.Y - tracks[i].Y) * (ownTrack.Y - tracks[i].Y));
                distanceHorizontal = ownTrack.Z - tracks[i].Z;

                if(distanceVertical < 5000 || distanceHorizontal < 300)
                {
                    this.conflictTrack = tracks[i];
                    this.conflicDistanceVertical = distanceVertical;
                    this.conflicDistacneHorizontal = distanceHorizontal;

                    this.sepration = true;
                }
            }

            return sepration;
        }

        public bool GetSeperation()
        {
            return sepration;
        }

        public Track GetConflictAirplain()
        {
            return conflictTrack;
        }

        public int GetConflictDistanceVertical()
        {
            return conflicDistanceVertical;
        }

        public int GetConflictDistanceHorizontal()
        {
            return conflicDistacneHorizontal;
        }
    }
}
