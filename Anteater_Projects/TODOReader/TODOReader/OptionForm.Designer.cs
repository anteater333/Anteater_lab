namespace TODOReader
{
    partial class OptionForm
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
            this.confirmButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.urlTextbox = new System.Windows.Forms.TextBox();
            this.urlLabel = new MetroFramework.Controls.MetroLabel();
            this.splitterLable = new MetroFramework.Controls.MetroLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.startUpToggle = new MetroFramework.Controls.MetroToggle();
            this.startUpLable = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.BackColor = System.Drawing.Color.Black;
            this.confirmButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.confirmButton.DisplayFocus = true;
            this.confirmButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.confirmButton.Location = new System.Drawing.Point(320, 320);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(99, 48);
            this.confirmButton.Style = MetroFramework.MetroColorStyle.Black;
            this.confirmButton.TabIndex = 5;
            this.confirmButton.Text = "확    인";
            this.confirmButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.confirmButton.UseSelectable = true;
            this.confirmButton.UseStyleColors = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Black;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.DisplayFocus = true;
            this.cancelButton.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(440, 320);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(99, 48);
            this.cancelButton.Style = MetroFramework.MetroColorStyle.Black;
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "취    소";
            this.cancelButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.cancelButton.UseSelectable = true;
            this.cancelButton.UseStyleColors = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // urlTextbox
            // 
            this.urlTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTextbox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.urlTextbox.Font = new System.Drawing.Font("나눔고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.urlTextbox.ForeColor = System.Drawing.SystemColors.Window;
            this.urlTextbox.Location = new System.Drawing.Point(24, 96);
            this.urlTextbox.Name = "urlTextbox";
            this.urlTextbox.Size = new System.Drawing.Size(512, 39);
            this.urlTextbox.TabIndex = 1;
            this.urlTextbox.Text = "https://";
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.urlLabel.Location = new System.Drawing.Point(24, 72);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(42, 20);
            this.urlLabel.TabIndex = 0;
            this.urlLabel.Text = "URL :";
            // 
            // splitterLable
            // 
            this.splitterLable.AutoSize = true;
            this.splitterLable.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.splitterLable.Location = new System.Drawing.Point(24, 152);
            this.splitterLable.Name = "splitterLable";
            this.splitterLable.Size = new System.Drawing.Size(61, 20);
            this.splitterLable.TabIndex = 7;
            this.splitterLable.Text = "구분자 :";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Font = new System.Drawing.Font("나눔고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(24, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(512, 39);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "======================================";
            // 
            // startUpToggle
            // 
            this.startUpToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startUpToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.startUpToggle.AutoEllipsis = true;
            this.startUpToggle.AutoSize = true;
            this.startUpToggle.DisplayStatus = false;
            this.startUpToggle.Location = new System.Drawing.Point(24, 248);
            this.startUpToggle.Name = "startUpToggle";
            this.startUpToggle.Size = new System.Drawing.Size(50, 50);
            this.startUpToggle.Style = MetroFramework.MetroColorStyle.Blue;
            this.startUpToggle.TabIndex = 3;
            this.startUpToggle.Text = "Off";
            this.startUpToggle.UseSelectable = true;
            // 
            // startUpLable
            // 
            this.startUpLable.AutoSize = true;
            this.startUpLable.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.startUpLable.Location = new System.Drawing.Point(24, 232);
            this.startUpLable.Name = "startUpLable";
            this.startUpLable.Size = new System.Drawing.Size(137, 20);
            this.startUpLable.TabIndex = 8;
            this.startUpLable.Text = "시작 프로그램 설정";
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(24F, 40F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 380);
            this.ControlBox = false;
            this.Controls.Add(this.startUpLable);
            this.Controls.Add(this.startUpToggle);
            this.Controls.Add(this.splitterLable);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.urlTextbox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Font = new System.Drawing.Font("굴림", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.Padding = new System.Windows.Forms.Padding(60, 160, 60, 53);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "설정";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton confirmButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        private System.Windows.Forms.TextBox urlTextbox;
        private MetroFramework.Controls.MetroLabel urlLabel;
        private MetroFramework.Controls.MetroLabel splitterLable;
        private System.Windows.Forms.TextBox textBox1;
        private MetroFramework.Controls.MetroToggle startUpToggle;
        private MetroFramework.Controls.MetroLabel startUpLable;
    }
}