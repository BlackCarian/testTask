using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter valid arguments: name of process, allowable lifetime and interaval for check");
            }
            else
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;
                string processName = args[0];
                int lifetime;
                int interval;
                MonitorLogic monitorLogic;

                if (Int32.TryParse(args[1], out lifetime) && Int32.TryParse(args[2], out interval))
                {
                    monitorLogic = new MonitorLogic(processName, lifetime, interval);
                    monitorLogic.StartMonitor(token);
                }
                else
                {
                    Console.WriteLine("Allowable lifetime or interaval for check in not valid");
                }
            }
        }
    }
}
