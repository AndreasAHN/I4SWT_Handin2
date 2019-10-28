using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    class Airspace
    {
       private List<Track> tracks = new List<Track>();
          
       private Track ownTrack;

        public Airspace()
        {

        }


        public void AirspaceChange()
        {

        }



        //1 - define a delegate

        public delegate void AirSpaceChangedEventHandler(object source, EventArgs args);

        //2 - define an event based on that delegate

        public event AirSpaceChangedEventHandler AirspaceChanged;

        //3 - raise the event

        protected virtual void OnAirspaceChanged(Track ownTrack)
        {

        }

        //metoder
        public void SetTracks(List<Track> bufTracks)
        {
            clearTracks();

            ownTrack = bufTracks[0];

            //funktion til sortering af tracks
            for (int i = 1; i < tracks.Count(); i++)
            {
                int vertical = 0;
                int horizontal = 0;

                vertical = ((ownTrack.X - tracks[i].X) * (ownTrack.X - tracks[i].X) + (ownTrack.Y - tracks[i].Y) * (ownTrack.Y - tracks[i].Y));
                horizontal = ownTrack.Z - tracks[i].Z;

                if ((vertical >= 500 || vertical <= 20000) &&  horizontal <= 8000)
                {
                    tracks.Add(bufTracks[i]);
                }

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
