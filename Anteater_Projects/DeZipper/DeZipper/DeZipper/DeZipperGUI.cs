using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZipper
{
    class DeZipperGUI : DeZipperUI, IOpenable
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public DeZipperGUI(string zipPath, string tgPath) : base(zipPath, tgPath)
        {

        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delist(string path)
        {
            throw new NotImplementedException();
        }

        public override void PrintList()
        {
            throw new NotImplementedException();
        }

        public void OpenZip(string zipPath, string tgPath)
        {

        }
        #endregion
    }
}
