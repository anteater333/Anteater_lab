namespace DeZipper
{
    partial class DeZipperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeZipperForm));
            this.deleteButton = new System.Windows.Forms.Button();
            this.zipEntryTreeView = new System.Windows.Forms.TreeView();
            this.optionPanel = new System.Windows.Forms.Panel();
            this.deleteEmptyDirectory = new System.Windows.Forms.CheckBox();
            this.deleteSourceZipFile = new System.Windows.Forms.CheckBox();
            this.toRecycleBin = new System.Windows.Forms.CheckBox();
            this.excludeButton = new System.Windows.Forms.Button();
            this.targetPath = new System.Windows.Forms.TextBox();
            this.zipPath = new System.Windows.Forms.TextBox();
            this.zipPathButton = new System.Windows.Forms.Button();
            this.targetPathButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.zipPathPanel = new System.Windows.Forms.Panel();
            this.targetPathPanel = new System.Windows.Forms.Panel();
            this.executionPanel = new System.Windows.Forms.Panel();
            this.buttonImageList = new System.Windows.Forms.ImageList(this.components);
            this.optionPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.zipPathPanel.SuspendLayout();
            this.targetPathPanel.SuspendLayout();
            this.executionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Verdana", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(8, 176);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(184, 55);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // zipEntryTreeView
            // 
            this.zipEntryTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zipEntryTreeView.Location = new System.Drawing.Point(203, 67);
            this.zipEntryTreeView.Name = "zipEntryTreeView";
            this.zipEntryTreeView.Size = new System.Drawing.Size(402, 470);
            this.zipEntryTreeView.TabIndex = 3;
            // 
            // optionPanel
            // 
            this.optionPanel.Controls.Add(this.toRecycleBin);
            this.optionPanel.Controls.Add(this.deleteSourceZipFile);
            this.optionPanel.Controls.Add(this.deleteEmptyDirectory);
            this.optionPanel.Location = new System.Drawing.Point(8, 80);
            this.optionPanel.Name = "optionPanel";
            this.optionPanel.Size = new System.Drawing.Size(184, 92);
            this.optionPanel.TabIndex = 2;
            // 
            // deleteEmptyDirectory
            // 
            this.deleteEmptyDirectory.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteEmptyDirectory.Location = new System.Drawing.Point(16, 0);
            this.deleteEmptyDirectory.Name = "deleteEmptyDirectory";
            this.deleteEmptyDirectory.Size = new System.Drawing.Size(168, 32);
            this.deleteEmptyDirectory.TabIndex = 1;
            this.deleteEmptyDirectory.Text = "빈 폴더 삭제";
            this.deleteEmptyDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteEmptyDirectory.UseVisualStyleBackColor = true;
            // 
            // deleteSourceZipFile
            // 
            this.deleteSourceZipFile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteSourceZipFile.Location = new System.Drawing.Point(16, 32);
            this.deleteSourceZipFile.Name = "deleteSourceZipFile";
            this.deleteSourceZipFile.Size = new System.Drawing.Size(168, 32);
            this.deleteSourceZipFile.TabIndex = 2;
            this.deleteSourceZipFile.Text = "원본 zip 파일 삭제";
            this.deleteSourceZipFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteSourceZipFile.UseVisualStyleBackColor = true;
            // 
            // toRecycleBin
            // 
            this.toRecycleBin.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toRecycleBin.Location = new System.Drawing.Point(16, 64);
            this.toRecycleBin.Name = "toRecycleBin";
            this.toRecycleBin.Size = new System.Drawing.Size(168, 32);
            this.toRecycleBin.TabIndex = 3;
            this.toRecycleBin.Text = "휴지통(미구현)";
            this.toRecycleBin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toRecycleBin.UseVisualStyleBackColor = true;
            // 
            // excludeButton
            // 
            this.excludeButton.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.excludeButton.Location = new System.Drawing.Point(8, 16);
            this.excludeButton.Name = "excludeButton";
            this.excludeButton.Size = new System.Drawing.Size(184, 55);
            this.excludeButton.TabIndex = 1;
            this.excludeButton.Text = "선택 파일 제외";
            this.excludeButton.UseVisualStyleBackColor = true;
            // 
            // targetPath
            // 
            this.targetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPath.Location = new System.Drawing.Point(0, 0);
            this.targetPath.Name = "targetPath";
            this.targetPath.Size = new System.Drawing.Size(367, 25);
            this.targetPath.TabIndex = 4;
            // 
            // zipPath
            // 
            this.zipPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zipPath.Location = new System.Drawing.Point(0, 0);
            this.zipPath.Name = "zipPath";
            this.zipPath.Size = new System.Drawing.Size(367, 25);
            this.zipPath.TabIndex = 5;
            // 
            // zipPathButton
            // 
            this.zipPathButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.zipPathButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.zipPathButton.FlatAppearance.BorderSize = 0;
            this.zipPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zipPathButton.ImageIndex = 0;
            this.zipPathButton.ImageList = this.buttonImageList;
            this.zipPathButton.Location = new System.Drawing.Point(367, 0);
            this.zipPathButton.Name = "zipPathButton";
            this.zipPathButton.Size = new System.Drawing.Size(25, 25);
            this.zipPathButton.TabIndex = 6;
            this.zipPathButton.UseVisualStyleBackColor = true;
            // 
            // targetPathButton
            // 
            this.targetPathButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.targetPathButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.targetPathButton.FlatAppearance.BorderSize = 0;
            this.targetPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.targetPathButton.ImageIndex = 1;
            this.targetPathButton.ImageList = this.buttonImageList;
            this.targetPathButton.Location = new System.Drawing.Point(367, 0);
            this.targetPathButton.Name = "targetPathButton";
            this.targetPathButton.Size = new System.Drawing.Size(25, 24);
            this.targetPathButton.TabIndex = 7;
            this.targetPathButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(69, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Zip File :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(69, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Target Directory :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.targetPathPanel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.zipEntryTreeView, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.zipPathPanel, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.executionPanel, 0, 2);
            this.tableLayoutPanel.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(608, 540);
            this.tableLayoutPanel.TabIndex = 10;
            // 
            // zipPathPanel
            // 
            this.zipPathPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zipPathPanel.Controls.Add(this.zipPath);
            this.zipPathPanel.Controls.Add(this.zipPathButton);
            this.zipPathPanel.Location = new System.Drawing.Point(203, 3);
            this.zipPathPanel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.zipPathPanel.Name = "zipPathPanel";
            this.zipPathPanel.Size = new System.Drawing.Size(405, 26);
            this.zipPathPanel.TabIndex = 1;
            // 
            // targetPathPanel
            // 
            this.targetPathPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPathPanel.Controls.Add(this.targetPath);
            this.targetPathPanel.Controls.Add(this.targetPathButton);
            this.targetPathPanel.Location = new System.Drawing.Point(203, 35);
            this.targetPathPanel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.targetPathPanel.Name = "targetPathPanel";
            this.targetPathPanel.Size = new System.Drawing.Size(405, 26);
            this.targetPathPanel.TabIndex = 2;
            // 
            // executionPanel
            // 
            this.executionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executionPanel.Controls.Add(this.excludeButton);
            this.executionPanel.Controls.Add(this.optionPanel);
            this.executionPanel.Controls.Add(this.deleteButton);
            this.executionPanel.Location = new System.Drawing.Point(3, 304);
            this.executionPanel.Name = "executionPanel";
            this.executionPanel.Size = new System.Drawing.Size(194, 233);
            this.executionPanel.TabIndex = 4;
            // 
            // buttonImageList
            // 
            this.buttonImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonImageList.ImageStream")));
            this.buttonImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonImageList.Images.SetKeyName(0, "zip_placeholder.ico");
            this.buttonImageList.Images.SetKeyName(1, "folder_placeholder.ico");
            // 
            // DeZipperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 553);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "DeZipperForm";
            this.Text = "DeZipper";
            this.optionPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.zipPathPanel.ResumeLayout(false);
            this.zipPathPanel.PerformLayout();
            this.targetPathPanel.ResumeLayout(false);
            this.targetPathPanel.PerformLayout();
            this.executionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TreeView zipEntryTreeView;
        private System.Windows.Forms.Panel optionPanel;
        private System.Windows.Forms.CheckBox deleteEmptyDirectory;
        private System.Windows.Forms.CheckBox toRecycleBin;
        private System.Windows.Forms.CheckBox deleteSourceZipFile;
        private System.Windows.Forms.Button excludeButton;
        private System.Windows.Forms.TextBox targetPath;
        private System.Windows.Forms.TextBox zipPath;
        private System.Windows.Forms.Button zipPathButton;
        private System.Windows.Forms.Button targetPathButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel targetPathPanel;
        private System.Windows.Forms.Panel zipPathPanel;
        private System.Windows.Forms.Panel executionPanel;
        private System.Windows.Forms.ImageList buttonImageList;
    }
}