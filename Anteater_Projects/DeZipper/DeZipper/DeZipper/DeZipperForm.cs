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
        private bool zipLoaded;

        public DeZipperForm()
        {
            zipLoaded = false;
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
            DialogResult dResult = zipOpenDialog.ShowDialog();

            if (dResult == DialogResult.OK)
            {
                zipPath.Text = zipOpenDialog.FileName;
                ZipLoad();
            }
            else
            {
                zipPath.ResetText();
            }
        }

        private void targetPathButton_Click(object sender, EventArgs e)
        {
            DialogResult dResult = folderBrowserDialog.ShowDialog();

            if (dResult == DialogResult.OK)
            {
                tgPath.Text = folderBrowserDialog.SelectedPath;
                deZipCaller.TargetDirectory = tgPath.Text;
            }
            else
            {
                tgPath.ResetText();
            }
        }

        private void excludeButton_Click(object sender, EventArgs e)
        {
            TreeNode selected = zipEntryTreeView.SelectedNode;
            if (selected == null || !zipLoaded)
            {

            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                deZipCaller.Delist(selected.Name);
                UpdateTreeViewCount();
                Cursor.Current = Cursors.Default;
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!zipLoaded)
            {

            }
            else
            {
                if (toRecycleBin.Checked)
                    deZipCaller.Options |= DeleteOptions.ToRecycleBin;
                if (deleteEmptyDirectory.Checked)
                    deZipCaller.Options |= DeleteOptions.DeleteEmptyDirectory;
                if (deleteSourceZipFile.Checked)
                    deZipCaller.Options |= DeleteOptions.DeleteSourceZipFile;


            }
        }

        private void zipPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ZipLoad();
            }
        }

        private void UpdateTreeViewCount()
        {
            // # File(s), # Folder(s)
            treeViewCount.Text = deZipCaller.CountFiles + " File(s), ";
            treeViewCount.Text += deZipCaller.CountDirs + " Folder(s)";
        }

        private void ZipLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            deZipCaller.OpenZip(zipPath.Text, tgPath.Text);
            deZipCaller.PrintList();
            UpdateTreeViewCount();
            Cursor.Current = Cursors.Default;

            zipLoaded = true;
        }
    }
}
