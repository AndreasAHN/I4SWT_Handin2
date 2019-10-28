using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Condition
    {
        public bool sepration = false;
        public List<Track> conflictTrack1 = new List<Track>();
        public List<Track> conflictTrack2 = new List<Track>();
        public List<int> conflicDistanceVertical = new List<int>();
        public List<int> conflicDistacneHorizontal = new List<int>();

        public Condition()
        {

        }

        public bool TooClose(List<Track> tracks )
        {
            conflictTrack1.Clear();
            conflictTrack2.Clear();
            conflicDistanceVertical.Clear();
            conflicDistacneHorizontal.Clear();

            if (tracks.Count >= 2)
            {
                sepration = false;
                for (int x = 0; x < tracks.Count(); x++)
                {
                    Track bufTrack = tracks[x];
                    
                    for (int i = 0; i < tracks.Count(); i++)
                    {
                        if (i != x)
                        {
                            double distanceVertical = 0;
                            double distanceHorizontal = 0;

                            distanceVertical = Math.Sqrt((bufTrack.X - tracks[i].X) * (bufTrack.X - tracks[i].X) + (bufTrack.Y - tracks[i].Y) * (bufTrack.Y - tracks[i].Y));
                            distanceHorizontal = Math.Sqrt(bufTrack.Z - tracks[i].Z);

                            if (distanceVertical < 5000 && distanceHorizontal < 300)
                            {
                                this.conflictTrack1.Add(bufTrack);
                                this.conflictTrack2.Add(tracks[i]);
                                this.conflicDistanceVertical.Add(Convert.ToInt32(distanceVertical));
                                this.conflicDistacneHorizontal.Add(Convert.ToInt32(distanceHorizontal));
                                this.sepration = true;
                            }
                        }
                    }
                }
            }

            return sepration;
        }

        public bool GetSeperation()
        {
            return sepration;
        }

        public List<Track> GetConflictAirplain1()
        {
            return conflictTrack1;
        }

        public List<Track> GetConflictAirplain2()
        {
            return conflictTrack2;
        }

        public List<int> GetConflictDistanceVertical()
        {
            return conflicDistanceVertical;
        }

        public List<int> GetConflictDistanceHorizontal()
        {
            return conflicDistacneHorizontal;
        }
    }
}
