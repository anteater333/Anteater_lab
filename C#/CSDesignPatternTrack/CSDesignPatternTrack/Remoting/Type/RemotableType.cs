using System;

public class RemotableType : MarshalByRefObject
{
    public string SayHello()
    {
        Console.WriteLine("RemotableType.SayHello() was called!");
        return "Hello, world!";
    }
}