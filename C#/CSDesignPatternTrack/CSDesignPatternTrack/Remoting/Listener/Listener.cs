using System;
using System.Runtime.Remoting;

class Listener
{
    public static void Main()
    {
        RemotingConfiguration.Configure("Listener.exe.config", false);
        Console.WriteLine("Listening for requests. Press enter to exit...");
        Console.ReadLine();
    }
}
