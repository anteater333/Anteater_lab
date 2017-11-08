using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZipper
{
    class DeZipperMain
    {
        static void Main(string[] args)
        {
            new ZipTest(@"D:/Temp/_Zip\audacity-win-2.1.0.zip").ZipDeleteTest(@"D:\Temp\_Zip\TG\", true);
        }
    }
}
