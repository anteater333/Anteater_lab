using System;

namespace _10886
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = Int32.Parse(Console.ReadLine());
            int cute = 0;
            for (int i = 0; i < length; i++)
            {
                if (Int32.Parse(Console.ReadLine()) == 1)
                    cute++;
            }

            Console.WriteLine((cute > length - cute) ? "Junhee is cute!" : "Junhee is not cute!");
        }
    }
}