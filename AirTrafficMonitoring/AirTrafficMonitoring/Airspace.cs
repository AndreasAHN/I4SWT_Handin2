using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public class Airspace
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


        public void SetTracks(List<Track> bufTracks)
        {
            clearTracks();

            if (bufTracks.Count != 0)
            {
                for (int i = 0; i < bufTracks.Count(); i++)
                {
                    if (((500 < bufTracks[i].Z) || (bufTracks[i].Z < 20000)) && (bufTracks[i].X < 80000) && (bufTracks[i].Y < 80000))
                    {
                        tracks.Add(bufTracks[i]);
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
