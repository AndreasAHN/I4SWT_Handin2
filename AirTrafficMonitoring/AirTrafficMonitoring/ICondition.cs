using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring
{
    public interface ICondition
    {
        bool TooClose(List<Track> tracks);
        bool GetSeperation();
        List<Track> GetConflictAirplain1();
        List<Track> GetConflictAirplain2();
    }
}
