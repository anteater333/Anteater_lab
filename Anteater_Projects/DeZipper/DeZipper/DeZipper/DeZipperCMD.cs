using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace DeZipper
{
    /// <summary>
    /// 콘솔 입출력을 담당하는 클래스입니다.
    /// </summary>
    public class DeZipperCMD : DeZipperUI
    {
        #region Options for PrintMsg
        private const bool ERR = true;
        private const bool MSG = false;
        #endregion
        #region DEBUG
#if DEBUG
        private bool DEBUG = true;
#else
        private bool DEBUG = false;
#endif
        #endregion

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// 삭제 작업 시 콘솔창에 작업 상황을 출력할지 정합니다.
        /// 기본 값은 false이며, false인 경우 출력합니다.
        /// </summary>
        public bool Silenced { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 경로를 지정합니다.
        /// </summary>
        /// <param name="zipPath">ZIP 파일 경로</param>
        /// <param name="tgPath">타겟 디렉토리 경로</param>
        public DeZipperCMD(string zipPath, string tgPath)
        {
            try
            {
                base.ZipDirectory = zipPath;
                base.TargetDirectory = tgPath;
                base.deZipper = new DeZipper(base.ZipDirectory, base.TargetDirectory);
                base.Options = DeleteOptions.None;
                this.Silenced = false;
            }
            catch (Exception e)
            {
                PrintException(e);
            }
        }

        /// <summary>
        /// ZIP 파일이 포함하는 파일들의 리스트를 출력합니다.
        /// </summary>
        public override void PrintList()
        {
            int count = 0;
            foreach (KeyValuePair<string, ZipArchiveEntry> entry in deZipper.Entries)
            {
                if (entry.Value.Name.Equals(""))
                    PrintMsg("Folder\t: " + entry.Value.FullName, MSG);
                else
                    PrintMsg("File\t: " + entry.Value.FullName, MSG);
                count++;
            }
            PrintMsg("Total : " + count, MSG);
        }

        /// <summary>
        /// 삭제할 ZIP 파일 리스트에서 특정 파일을 제외합니다.
        /// </summary>
        /// <param name="path">제외할 파일의 ZIP 파일 내부 경로</param>
        /// <returns>파일 제외 성공 여부</returns>
        public override bool Delist(string path)
        {
            try
            {
                ZipArchiveEntry zipEntry = deZipper.Delist(path);
                if (zipEntry == null)
                {
                    PrintMsg("File " + path + " not found.", ERR);
                    return false;
                }
                else
                {
                    PrintMsg("Excluded " + zipEntry.FullName, MSG);
                    return true;
                }
            }
            catch (Exception e)
            {
                PrintException(e);
                return false;
            }
        }

        /// <summary>
        /// ZIP 파일 리스트에 따라 파일을 삭제합니다.
        /// </summary>
        public override void Delete()
        {
            int delCount = 0;
            deZipper.Options = base.Options;

            try
            {
                foreach (string entry in deZipper.ExecuteDelete())
                {
                    if (entry.Contains(DeZipper.FIlE_NOT_FOUND))
                        PrintMsg(entry.Replace(DeZipper.FIlE_NOT_FOUND, "") + " does not exist.", MSG);
                    else if (entry.Contains(DeZipper.DIR_NOT_EMPTY))
                        PrintMsg(entry.Replace(DeZipper.DIR_NOT_EMPTY, "") + " is not empty.", MSG);
                    else if (entry.Contains(DeZipper.DIR_NOT_FOUND))
                        PrintMsg(entry.Replace(DeZipper.DIR_NOT_FOUND, "") + " does not exist.", MSG);
                    else
                    {
                        delCount++;
                        PrintMsg("Deleted " + entry, MSG);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                PrintMsg("Cannot find " + e.Message, ERR);
            }
            catch (Exception e)
            {
                PrintException(e);
            }
            PrintMsg("Deleted " + delCount + " File(s).", MSG);
        }

        /// <summary>
        /// readme.txt 파일을 출력합니다.
        /// </summary>
        public static void PrintHelp()
        {
            try
            {
                string current = Environment.CurrentDirectory;
                using (StreamReader readme = new StreamReader(current + @"\readme.txt"))
                {
                    while(readme.Peek() >= 0)
                    {
                        Console.WriteLine(readme.ReadLine());
                    }
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("readme.txt를 찾을 수 없습니다.");
                Console.WriteLine("(usage : DeZipper.exe [source_zip] [target_dir])");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 메세지를 콘솔창에 출력합니다.
        /// 에러 메세지가 아니라면 Silenced 옵션에 따라 출력 여부가 결정됩니다.
        /// </summary>
        /// <param name="msg">출력할 메세지</param>
        /// <param name="isErr">에러 메세지 여부</param>
        private void PrintMsg(string msg, bool isErr)
        {
            if (!isErr)
            {
                if (!Silenced)
                    Console.WriteLine(msg);
            }
            else
            {
                Console.WriteLine("Error : " + msg);
            }
        }

        /// <summary>
        /// 예측하지 못한 예외 발생 시 콘솔 창에 오류를 출력하고 프로그램을 종료합니다.
        /// </summary>
        /// <param name="e">발생한 예외</param>
        private void PrintException(Exception e)
        {
            PrintMsg("Unexpected Error!", ERR);
            Console.WriteLine("==>" + e.Message);
            if (DEBUG)
            {
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
            Environment.Exit(1);
        }
        #endregion
    }
}
