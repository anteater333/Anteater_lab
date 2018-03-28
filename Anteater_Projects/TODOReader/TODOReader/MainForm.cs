using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TODOReader
{
    public partial class MainForm : MetroForm
    {
        private TodoRRequester todoRequest;
        private TodoROption option;
        /// <summary>
        /// TODOs.txt가 위치하는 주소
        /// </summary>
        private string todoUrl;

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
