using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    class Program 
    //Main er vores AirTrafficController, da denne ikke har fået sin egen klasse
    {
        static void Main(string[] args)
        {
            AirTrafficController airTrafficController = new  AirTrafficController();


            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
