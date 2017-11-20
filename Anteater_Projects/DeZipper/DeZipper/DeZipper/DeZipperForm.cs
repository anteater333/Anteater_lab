using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeZipper
{
    public partial class DeZipperForm : Form
    {
        private DeZipperGUI deZipCaller;

        public DeZipperForm()
        {
            InitializeComponent();
        }

        private void DeZipperForm_Load(object sender, EventArgs e)
        {
            zipEntryTreeView.ImageList = fileImageList;

            deZipCaller = new DeZipperGUI();
            deZipCaller.EntryTree = zipEntryTreeView;
        }

        private void zipPathButton_Click(object sender, EventArgs e)
        {

        }

        private void targetPathButton_Click(object sender, EventArgs e)
        {

        }

        private void excludeButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

        }

        private void zipPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cursor.Current = Cursors.WaitCursor;
                deZipCaller.OpenZip(zipPath.Text, tgPath.Text);
                deZipCaller.PrintList();
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
