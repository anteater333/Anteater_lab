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
                string msgboxText = "";
                string msgboxOptionText = "";
                int total = deZipCaller.CountFiles;
                deZipCaller.Options = DeleteOptions.None;
                if (toRecycleBin.Checked)
                {
                    deZipCaller.Options |= DeleteOptions.ToRecycleBin;
                    msgboxOptionText += " - 휴지통으로 보내기" + Environment.NewLine;
                }
                if (deleteEmptyDirectory.Checked)
                {
                    deZipCaller.Options |= DeleteOptions.DeleteEmptyDirectory;
                    total += deZipCaller.CountDirs;
                    msgboxOptionText += " - 빈 폴더 삭제" + Environment.NewLine;
                }
                if (deleteSourceZipFile.Checked)
                {
                    deZipCaller.Options |= DeleteOptions.DeleteSourceZipFile;
                    total += 1;
                    msgboxOptionText += " - 원본 zip 파일 삭제" + Environment.NewLine;
                }
                deZipCaller.TargetDirectory = this.tgPath.Text;

                msgboxText += "총 " + total + " 파일이 삭제됩니다." + Environment.NewLine;
                msgboxText += "선택한 옵션" + Environment.NewLine;
                msgboxText += msgboxOptionText + Environment.NewLine;
                msgboxText += "삭제하시겠습니까?";

                if (MessageBox.Show(msgboxText, "경고!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    deZipCaller.Delete();
                }
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
