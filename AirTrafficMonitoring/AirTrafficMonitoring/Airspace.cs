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
            clearTracks();

            if (bufTracks.Count != 0)
            {
                ownTrack = bufTracks[0];

                if (bufTracks.Count >= 1)
                {
                    //funktion til sortering af tracks
                    for (int i = 1; i < bufTracks.Count(); i++)
                    {
                        double vertical = 0;
                        double horizontal = 0;

                        vertical = Math.Sqrt((ownTrack.X - bufTracks[i].X) * (ownTrack.X - bufTracks[i].X) + (ownTrack.Y - bufTracks[i].Y) * (ownTrack.Y - bufTracks[i].Y));
                        horizontal = Math.Sqrt(ownTrack.Z - bufTracks[i].Z);

                        if (((500 < vertical) || (vertical < 20000)) && (horizontal < 80000))
                        {
                            tracks.Add(bufTracks[i]);
                        }
                    }
                }   

                //tracks = Program.trackCalculator.calculate(tracks); //Når Marie er done, tilføjes denne metode

                AirTrafficController(EventArgs.Empty);
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
