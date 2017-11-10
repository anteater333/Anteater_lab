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
        private bool DEBUG = true;
        #endregion

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// 삭제 작업 시 콘솔창에 작업 상황을 출력할지 정합니다. false일 경우 출력됩니다.
        /// </summary>
        public bool Silenced { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 경로를 지정합니다.
        /// </summary>
        /// <param name="zipPath">ZIP 파일 경로</param>
        /// <param name="tgPath">타겟 디렉토리 경로</param>
        public DeZipperCMD(string zipPath, string tgPath) : base(zipPath, tgPath)
        {
            base.deZipper = new DeZipper(base.zipPath, base.tgPath);
            base.Options = DeleteOptions.None;
        }

        /// <summary>
        /// ZIP 파일이 포함하는 파일들의 리스트를 출력합니다.
        /// </summary>
        public override void PrintList()
        {
            foreach (KeyValuePair<string, ZipArchiveEntry> entry in deZipper.Entries)
            {
                if (entry.Value.Name.Equals(""))
                    PrintMsg("Folder : " + entry.Value.FullName, MSG);
                else
                    PrintMsg("File : " + entry.Value.FullName, MSG);
            }
        }

        /// <summary>
        /// 삭제할 ZIP 파일 리스트에서 특정 파일을 제외합니다.
        /// </summary>
        /// <param name="path">제외할 파일의 ZIP 파일 내부 경로</param>
        public override void Delist(string path)
        {
            try
            {
                ZipArchiveEntry zipEntry = deZipper.Delist(path);
                if (zipEntry == null)
                {
                    PrintMsg("File " + path + " not found.", ERR);
                }
                else
                {
                    PrintMsg("Excluded " + zipEntry.FullName, MSG);
                }
            }
            catch (Exception e)
            {
                PrintException(e);
            }
        }

        /// <summary>
        /// ZIP 파일 리스트에 따라 파일을 삭제합니다.
        /// </summary>
        public override void Delete()
        {
            int delCount = 0;

            deZipper.Options = base.Options;


            foreach (string deleted in deZipper.ExecuteDelete())
            {
                try
                {
                    PrintMsg("Deleted " + deleted, MSG);
                    delCount++;
                }
                catch (DirectoryNotEmptyException e)
                {
                    PrintMsg(e.DirectoryPath + " is not empty.", MSG);
                }
                catch (FileNotFoundException e)
                {
                    PrintMsg(e.FilePath + " does not exist.", MSG);
                }
                catch (DirectoryNotFoundException e)
                {
                    PrintMsg(e.Message, ERR);
                }
                catch (Exception e)
                {
                    PrintException(e);
                }
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
                Console.WriteLine("ERR : " + msg);
            }
        }

        /// <summary>
        /// 예측하지 못한 예외 발생 시 콘솔 창에 오류를 출력합니다.
        /// </summary>
        /// <param name="e">예외</param>
        private void PrintException(Exception e)
        {
            PrintMsg("Unexpected Error!", ERR);
            Console.WriteLine("Massage :");
            Console.WriteLine(e.Message);
            if (DEBUG)
            {
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
        }
        #endregion
    }
}
