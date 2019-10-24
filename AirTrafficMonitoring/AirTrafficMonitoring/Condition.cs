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

        public bool TooClose(new Track ownTrack, new Track tracks[] )
        {
            Seperation = false;
            for(int i = 0 ; i<tracks.Count() ; i++)
            {
                int distanceVertical = 0;
                int distanceHorizontal = 0;

                distanceVertical = ((ownTrack.x - tracks[i].x) * (ownTrack.x - tracks[i].x) + (ownTrack.y - tracks[i].y) * (ownTrack.y - tracks[i].y));
                distanceHorizontal = ownTrack.z - tracks[i].z;

                if(distanceVertical < 5000 || distanceHorizontal < 300)
                {
                    conflictTrack = tracks[i];
                    conflicDistanceVertical = distanceVertical;
                    conflicDistacneHorizontal = distanceHorizontal;

                    Sepration = true;
                }
            }

            return Sepration;
        }

        public bool GetSeperation()
        {
            return Sepration;
        }

        public Track GetConflictAirplain()
        {
            return ConflictTrack;
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
