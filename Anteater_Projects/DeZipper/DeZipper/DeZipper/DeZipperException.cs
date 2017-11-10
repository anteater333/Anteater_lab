using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZipper
{
    /// <summary>
    /// 삭제하려는 디렉토리가 빈 상태가 아닐 경우 발생하는 예외입니다.
    /// </summary>
    public class DirectoryNotEmptyException : Exception
    {
        private string dirPath;

        /// <summary>
        /// 예외가 발생한 디렉토리입니다.
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return dirPath;
            }
        }

        public DirectoryNotEmptyException()
        { }

        public DirectoryNotEmptyException(string path)
        {
            this.dirPath = path;
        }

        public DirectoryNotEmptyException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    /// <summary>
    /// 삭제하려는 파일이 존재하지 않을 경우 발생하는 예외입니다.
    /// </summary>
    public class FileNotFoundException : Exception
    {
        private string filePath;

        /// <summary>
        /// 예외가 발생한 파일입니다.
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        public FileNotFoundException()
        { }

        public FileNotFoundException(string path)
        {
            this.filePath = path;
        }

        public FileNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
