namespace TODOReader
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.todoTextbox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.settingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // todoTextbox
            // 
            this.todoTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.todoTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.todoTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.todoTextbox.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.todoTextbox.Location = new System.Drawing.Point(16, 72);
            this.todoTextbox.Multiline = true;
            this.todoTextbox.Name = "todoTextbox";
            this.todoTextbox.ReadOnly = true;
            this.todoTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.todoTextbox.Size = new System.Drawing.Size(416, 208);
            this.todoTextbox.TabIndex = 3;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Image = global::TODOReader.Properties.Resources.refresh_16x16;
            this.refreshButton.Location = new System.Drawing.Point(384, 280);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(26, 24);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // settingButton
            // 
            this.settingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.settingButton.FlatAppearance.BorderSize = 0;
            this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingButton.Image = global::TODOReader.Properties.Resources.gear_4_16x16;
            this.settingButton.Location = new System.Drawing.Point(408, 280);
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(26, 24);
            this.settingButton.TabIndex = 2;
            this.settingButton.UseVisualStyleBackColor = true;
            this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(450, 310);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.todoTextbox);
            this.Controls.Add(this.settingButton);
            this.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(18, 68, 18, 26);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.Text = "오늘 할 일";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button settingButton;
        private System.Windows.Forms.TextBox todoTextbox;
        private System.Windows.Forms.Button refreshButton;
    }
}

