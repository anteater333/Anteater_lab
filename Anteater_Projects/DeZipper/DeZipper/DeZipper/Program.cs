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
            // 테스트용 임시 코드!!!
            DeZipperCMD tmp = new DeZipperCMD(@"D:\Temp\_Zip\audacity-win-2.1.0.zip", @"D:\Temp\_Zip\TG");
            tmp.PrintList();
        }
    }
}
