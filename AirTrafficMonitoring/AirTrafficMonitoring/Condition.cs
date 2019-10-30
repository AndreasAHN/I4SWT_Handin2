using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Condition
    {
        public FileWriter fileWriter;
        public bool sepration = false;
        public List<Track> conflictTrack1;
        public List<Track> conflictTrack2;

        public Condition()
        {
            fileWriter = new FileWriter("AirLogger.txt");
            conflictTrack1 = new List<Track>();
            conflictTrack2 = new List<Track>();
        }

        public bool TooClose(List<Track> tracks )
        {
            List<Track> bufConflictTrack1 = conflictTrack1.ToList();
            List<Track> bufConflictTrack2 = conflictTrack2.ToList();
            conflictTrack1.Clear();
            conflictTrack2.Clear();

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
                                bool exist = false;
                                for (int c = 0; c < bufConflictTrack1.Count(); c++)
                                {
                                    if((bufConflictTrack1[c].Tag == bufTrack.Tag) && (bufConflictTrack2[c].Tag == tracks[i].Tag))
                                    {
                                        this.conflictTrack1.Add(bufConflictTrack1[c]);
                                        this.conflictTrack2.Add(bufConflictTrack2[c]);
                                        exist = true;
                                    }
                                }

                                if (exist == false)
                                {
                                    this.conflictTrack1.Add(bufTrack);
                                    this.conflictTrack2.Add(tracks[i]);
                                    fileWriter.WriteToLog(bufTrack, tracks[i]);
                                }

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
    }
}
