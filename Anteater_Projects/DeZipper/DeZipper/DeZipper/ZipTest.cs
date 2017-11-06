using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;    // Zip-classes

namespace DeZipper
{
    class ZipTest
    {
        private string zipPath;
        private ZipArchive zipAchv;

        public ZipTest(string path)
        {
            zipPath = path;
            zipAchv = ZipFile.Open(zipPath, ZipArchiveMode.Update);

            foreach (ZipArchiveEntry entry in zipAchv.Entries)
            {
                if (entry.FullName == @"Audacity/wxmsw28u_html_vc_custom.dll")
                {
                    Console.WriteLine(entry.FullName + " ==> Deleted!!");
                    entry.Delete();
                    
                    break;
                }
                else
                    Console.WriteLine(entry.FullName);
            }
        }
    }
}
