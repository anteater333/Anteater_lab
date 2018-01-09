using System;
using System.Runtime.Remoting;

namespace Gumball05
{
    public class GumballMonitor
    {
        GumballMachineRemote machine;

        public GumballMonitor(GumballMachineRemote machine)
        {
            this.machine = machine;
        }

        public void Report()
        {
            Console.WriteLine("뽑기 기계 위치: " + machine.Location);
            Console.WriteLine("현재 재고: " + machine.Count + " 개");
            Console.WriteLine("현재 상태: " + machine.State);       // ToString()은 있다고 생각하자.
        }
        
        public static void Main()
        {
            RemotingConfiguration.Configure("GumballMonitor.exe.config", false);

            GumballMachineRemote gumballMachine =
                (GumballMachineRemote)RemotingServices.Connect(typeof(GumballMachineRemote), "http://localhost:8989/Seattle.rem");
            GumballMonitor remoteMonitor = new GumballMonitor(gumballMachine);
            remoteMonitor.Report();
        }
    }
}
