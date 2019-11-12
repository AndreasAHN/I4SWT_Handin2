using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Condition : ICondition
    {
        private IFileWriter _fileWriter;
        private bool sepration = false;
        private List<Track> conflictTrack1;
        private List<Track> conflictTrack2;

        public Condition(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
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
                            distanceHorizontal = Math.Abs(bufTrack.Z - tracks[i].Z);

                            if (distanceVertical <= 5000 && distanceHorizontal <= 300)
                            {
                                bool exist = false;
                                for (int c = 0; c < bufConflictTrack1.Count(); c++)
                                {
                                    if ((conflictTrack1.Count() > c) && ((!(conflictTrack1[c].Tag == bufTrack.Tag) && !(conflictTrack2[c].Tag == tracks[i].Tag) || (!(conflictTrack2[c].Tag == bufTrack.Tag) && !(conflictTrack1[c].Tag == tracks[i].Tag)))))
                                    {
                                        exist = true;
                                        break;
                                    }
                                    else if (((bufConflictTrack1[c].Tag == bufTrack.Tag) || (bufConflictTrack2[c].Tag == tracks[i].Tag)))
                                    {
                                        this.conflictTrack1.Add(bufConflictTrack1[c]);
                                        this.conflictTrack2.Add(bufConflictTrack2[c]);
                                        exist = true;
                                        break;
                                    }
                                }

                                if ((conflictTrack1.Count() != 0) && ((!(conflictTrack1[0].Tag == bufTrack.Tag) && !(conflictTrack2[0].Tag == tracks[i].Tag) || (!(conflictTrack2[0].Tag == bufTrack.Tag) && !(conflictTrack1[0].Tag == tracks[i].Tag)))))
                                {
                                    exist = true;
                                }

                                if (exist == false)
                                {
                                    bufTrack.Timestamp = DateTime.Now;
                                    Track bufTrack2 = tracks[i];
                                    bufTrack2.Timestamp = bufTrack.Timestamp;
                                    this.conflictTrack1.Add(bufTrack);
                                    this.conflictTrack2.Add(bufTrack2);
                                    _fileWriter.WriteToFile(bufTrack, bufTrack2);
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
