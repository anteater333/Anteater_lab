using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace DeZipper
{
    /// <summary>
    /// WinForm GUI 에서 DeZipper 클래스를 호출하고,
    /// DeZipper의 연산 결과를 출력하는데 사용하는 클래스입니다.
    /// </summary>
    class DeZipperGUI : DeZipperUI, IOpenable
    {
        #region Fields
        #endregion

        #region Properties
        public TreeView EntryTree { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        public DeZipperGUI()
        {
            base.deZipper = new DeZipper();
        }

        public override bool Delist(string path)
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TreeView 컴포넌트에 파일 리스트를 출력합니다.
        /// </summary>
        public override void PrintList()
        {
            EntryTree.BeginUpdate();
            EntryTree.Nodes.Clear();
            
            List<string> dirList = new List<string>();
            TreeNodeCollection branch = EntryTree.Nodes;

            foreach (KeyValuePair<string, ZipArchiveEntry> entry in deZipper.Entries)
            {
                string path = entry.Value.FullName;
                path = path.TrimEnd('/');
                path = path.Remove(path.LastIndexOf('/') + 1);

                if (entry.Value.Name.Equals(""))    // Folder
                {
                    string name = entry.Value.FullName.Split('/')[entry.Value.FullName.Split('/').Length - 2];

                    dirList.Add(entry.Value.FullName);

                    if (dirList.Contains(path))
                        branch.Find(path, true)[0].Nodes.Add(entry.Value.FullName, name, 1, 1);
                    else
                        branch.Add(entry.Value.FullName, name, 1, 1);
                }
                else                                // Files
                {
                    if (dirList.Contains(path))
                        branch.Find(path, true)[0].Nodes.Add(entry.Value.FullName, entry.Value.Name);
                    else
                        branch.Add(entry.Value.FullName, entry.Value.Name);

                }
            }

            EntryTree.EndUpdate();
            EntryTree.ExpandAll();
        }

        /// <summary>
        /// 새 ZIP 파일을 엽니다.
        /// </summary>
        /// <param name="zipPath">ZIP 파일 경로</param>
        /// <param name="tgPath">타겟 디렉토리 경로</param>
        public void OpenZip(string zipPath, string tgPath)
        {
            deZipper.TargetDirectory = tgPath;
            deZipper.NewZip(zipPath);
        }
        #endregion
    }
}
