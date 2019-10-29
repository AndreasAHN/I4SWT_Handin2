using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    public class TransponderReceiverClient
    {
        private readonly ITransponderReceiver _receiver;

        public event EventHandler DataReceivedEvent;

        public Track[] Tracks { get; private set; }

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            this._receiver = receiver;
            this._receiver.TransponderDataReady += HandleTransponderDataReady;
            Console.WriteLine("Started transponder reciver");
        }

        private void HandleTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Tracks = new Track[e.TransponderData.Count];

            string[] tokens;
            char[] separator = {';'};

            int trackIndex = 0;

            foreach (var data in e.TransponderData)
            {
                tokens = data.Split(separator);
                Tracks[trackIndex] = new Track()
                {
                    Tag = tokens[0],
                    X = int.Parse(tokens[1]),
                    Y = int.Parse(tokens[2]),
                    Z = int.Parse(tokens[3]),
                    Timestamp = DateTime.ParseExact(tokens[4], "yyyyMMddHHmmssfff", null)
                };
                trackIndex++;
            }

            Program.airSpace.SetTracks(Tracks.ToList());

            DataReceivedEvent?.Invoke(null, null);
        }
    }
}
