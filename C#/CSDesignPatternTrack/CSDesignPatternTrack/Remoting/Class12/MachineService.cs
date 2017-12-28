using System;
using System.Runtime.Remoting;

namespace Gumball05
{
    class MachineService
    {
        public static void Main()
        {
            RemotingConfiguration.Configure("MachineService.exe.config", false);
            GumballMachineRemote gumballMachine = new GumballMachine("Seattle", 500);
            Console.WriteLine("Location: " + gumballMachine.Location);
            Console.WriteLine("Count: " + gumballMachine.Count);
            RemotingServices.SetObjectUriForMarshal(gumballMachine, "Seattle.rem");
            RemotingServices.Marshal(gumballMachine);
            Console.ReadLine();
        }
    }
}
