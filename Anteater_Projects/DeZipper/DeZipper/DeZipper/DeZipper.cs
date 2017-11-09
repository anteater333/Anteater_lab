using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace DeZipper
{
    /// <summary>
    /// 프로그램 핵심 기능을 담당하는 클래스입니다.
    /// </summary>
    class DeZipper
    {
        #region Fields
        private string zipPath;
        private string tgPath;
        private Dictionary<string, ZipArchiveEntry> entries;
        #endregion

        #region Properties
        /// <summary>
        /// 삭제시 옵션을 설정할 수 있습니다.
        /// </summary>
        public DeleteOptions Options { get; set; }
        /// <summary>
        /// ZIP 파일에 있는 파일들의 리스트를 HashTable 형태로 가져옵니다.
        /// </summary>
        public Dictionary<string, ZipArchiveEntry> Entries { get { return this.entries; } }
        /// <summary>
        /// 삭제 작업을 수행할 디렉토리 경로를 설정할 수 있습니다. 기호 '\' 는 자동으로 '/' 로 변환됩니다.
        /// </summary>
        public string TargetDirectory
        {
            set
            {
                if (value[value.Length - 1].Equals('\\') || value[value.Length - 1].Equals('/'))
                    tgPath = value;
                else
                    tgPath = value + "/";
                tgPath.Replace('\\', '/');
            }
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// ZIP 파일을 엽니다.
        /// </summary>
        /// <param name="zipPath">ZIP 파일 경로</param>
        /// <param name="tgPath">타겟 디렉토리 경로</param>
        public DeZipper(string zipPath, string tgPath)
        {
            this.zipPath = zipPath;
            this.zipPath.Replace('\\', '/');
            this.TargetDirectory = tgPath;
            this.Options = DeleteOptions.None;

            using (ZipArchive zipAchv = ZipFile.Open(this.zipPath, ZipArchiveMode.Update))
            {
                this.entries = new Dictionary<string, ZipArchiveEntry>(zipAchv.Entries.Count);
                foreach (ZipArchiveEntry entry in zipAchv.Entries)
                    this.Entries.Add(entry.FullName, entry);
            }
        }
        /// <summary>
        /// ZIP 파일을 열고 삭제 옵션을 지정합니다.
        /// </summary>
        /// <param name="zipPath">ZIP 파일 경로</param>
        /// <param name="tgPath">타겟 디렉토리 경로</param>
        /// <param name="options">삭제 옵션</param>
        public DeZipper(string zipPath, string tgPath, DeleteOptions options)
        {
            this.zipPath = zipPath;
            this.zipPath.Replace('\\', '/');
            this.TargetDirectory = tgPath;
            this.Options = options;

            using (ZipArchive zipAchv = ZipFile.Open(this.zipPath, ZipArchiveMode.Update))
            {
                this.entries = new Dictionary<string, ZipArchiveEntry>(zipAchv.Entries.Count);
                foreach (ZipArchiveEntry entry in zipAchv.Entries)
                    this.Entries.Add(entry.FullName, entry);
            }
        }

        /// <summary>
        /// 삭제할 ZIP 파일 리스트에서 특정 파일을 제외합니다.
        /// </summary>
        /// <param name="path">제외할 파일의 ZIP 파일 내부 경로</param>
        /// <returns>제외한 파일 엔트리. 해당 파일이 없을 경우 null을 반환합니다.</returns>
        public ZipArchiveEntry Delist(string path)
        {
            ZipArchiveEntry val;
            if (Entries.TryGetValue(path, out val))
            {
                Entries.Remove(path);
                return val;
            }
            else
                return null;
        }

        /// <summary>
        /// 파일 삭제를 실행합니다.
        /// </summary>
        /// <returns>삭제한 파일들의 전체 경로.</returns>
        public IEnumerable<string> ExcuteDelete()
        {
            Stack<string> zipDirPaths = new Stack<string>();
            int dirCount = 0;

            foreach (KeyValuePair<string, ZipArchiveEntry> entry in Entries)
            {
                if (entry.Value.Name == "")
                {
                    zipDirPaths.Push(entry.Value.FullName);
                    dirCount++;
                }
                else
                {
                    File.Delete(tgPath + entry.Value.FullName);
                    yield return tgPath + entry.Value.FullName;
                }
            }

            if ((Options & DeleteOptions.DeleteEmptyDirectory) != 0)
            {
                string strTmp;
                for (int i = 0; i < dirCount; i++)
                {
                    strTmp = zipDirPaths.Pop();
                    Directory.Delete(tgPath + strTmp);
                    yield return tgPath + strTmp;
                }
            }

            if ((Options & DeleteOptions.DeleteSourceZipFile) != 0)
            {
                File.Delete(zipPath);
                yield return zipPath;
            }
        }
        #endregion
    }

    /// <summary>
    /// 삭제 시 옵션을 지정합니다.
    /// </summary>
    [Flags]
    enum DeleteOptions
    {
        None = 0x0,                     // 000
        ToRecycleBin = 0x1,             // 001
        DeleteEmptyDirectory = 0x2,     // 010
        DeleteSourceZipFile = 0x4       // 100
    }
}
