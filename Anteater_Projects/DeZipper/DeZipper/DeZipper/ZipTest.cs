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

        /// <summary>
        /// DelZipper 구현 Test용 클래스.
        /// </summary>
        /// <param name="path">ZIP 파일 경로</param>
        public ZipTest(string path)
        {
            zipPath = path;
            zipAchv = ZipFile.Open(zipPath, ZipArchiveMode.Update);

            foreach (ZipArchiveEntry entry in zipAchv.Entries)
            {
                /*  11.06
                if (entry.FullName == @"Audacity/wxmsw28u_html_vc_custom.dll")
                {
                    Console.WriteLine(entry.FullName + " ==> Deleted!!");
                    entry.Delete();
                    
                    break;
                }
                else
                    Console.WriteLine(entry.FullName);
                */
                if (entry.Name == "")
                    Console.WriteLine(entry.FullName);
                else
                    Console.WriteLine(entry.Name);
            }
            Console.WriteLine("total : " + zipAchv.Entries.Count);
        }

        /// <summary>
        /// ZIP 파일에서 얻은 경로로 파일들을 삭제하는 메소드.
        /// </summary>
        /// <param name="dirPath">삭제 작업을 수행할 폴더 경로</param>
        public void ZipDeleteTest(string dirPath)
        {
            string delPath;

            /// 경로 마지막에 / 또는 \가 없는 경우를 정리.
            if (dirPath[dirPath.Length - 1].Equals("\\") || dirPath[dirPath.Length - 1].Equals("/"))
                delPath = dirPath;
            else
                delPath = dirPath + "/";

            int delCount = 0;   // 삭제한 파일 갯수

            foreach (ZipArchiveEntry entry in zipAchv.Entries)
            {
                if (entry.Name == "")
                    Console.WriteLine("Dir : " + delPath + entry.FullName);
                else
                {
                    File.Delete(delPath + entry.FullName);
                    Console.WriteLine("Deleted " + entry.Name);
                    delCount++;
                }
            }
            Console.WriteLine("total deleted : " + delCount);
        }
    }
}
