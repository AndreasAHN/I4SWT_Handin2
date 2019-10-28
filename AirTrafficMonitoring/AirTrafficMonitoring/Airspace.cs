using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    class Airspace
    {
       private static Mutex mutex = new Mutex();

       private static List<Track> tracks = new List<Track>();
          
       private static Track ownTrack;

        public Airspace()
        {

        }


        public event EventHandler AirSpaceChanged;
        protected virtual void AirTrafficController(EventArgs e)//Threshold
        {
            EventHandler handler = AirSpaceChanged;
            handler?.Invoke(this, e);
        }


        //metoder
        public void SetTracks(List<Track> bufTracks)
        {
            //mutex.WaitOne();
            clearTracks();

            Console.WriteLine("Called airspace change:" + bufTracks.Count);

            if (bufTracks.Count != 0)
            {
                ownTrack = bufTracks[0];
                tracks.Add(bufTracks[0]);

                if (bufTracks.Count > 1)
                {
                    //funktion til sortering af tracks
                    for (int i = 1; i > (tracks.Count() - 1); i++)
                    {
                        int vertical = 0;
                        int horizontal = 0;

                        vertical = ((ownTrack.X - tracks[i].X) * (ownTrack.X - tracks[i].X) + (ownTrack.Y - tracks[i].Y) * (ownTrack.Y - tracks[i].Y));
                        horizontal = ownTrack.Z - tracks[i].Z;

                        if ((vertical >= 500 || vertical <= 20000) && horizontal <= 8000)
                        {
                            //tracks.Add(bufTracks[1]);
                        }
                        Console.WriteLine("Number of airplains written:" + i);
                    }
                }   

                //tracks = Program.trackCalculator.calculate(tracks); //Når Marie er done, tilføjes denne metode

                AirTrafficController(EventArgs.Empty);
                //mutex.ReleaseMutex();
            }
        }


     
        public List<Track> GetTracks()
        {
            return tracks;
        }


        private void clearTracks()
        {
            tracks.Clear();
        }

        public Track GetOwnTrack()
        {
            return ownTrack;
        }

    }
}
