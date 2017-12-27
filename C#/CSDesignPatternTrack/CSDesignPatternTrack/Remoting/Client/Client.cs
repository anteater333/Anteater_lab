using System;
using System.Runtime.Remoting;

class Client
{
    public static void Main()
    {
        RemotingConfiguration.Configure("Client.exe.config", false);
        RemotableType remoteObject = new RemotableType();
        Console.WriteLine(remoteObject.SayHello());
    }
}