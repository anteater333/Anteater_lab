﻿using System;
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
    public partial class OptionForm : MetroForm
    {
        private TodoROption option;

        public OptionForm(TodoROption option)
        {
            this.option = option;

            InitializeComponent();
            startUpToggle.Checked = option.IsStartup;

            urlTextbox.Text = option.TodoUrl;
            splitTextbox.Text = option.Splitter;
            formatTextbox.Text = option.Format;
        }

        /// <summary>
        /// 취소 버튼, 옵션들을 적용하지 않음
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 확인 버튼, 설정한 옵션들을 적용함
        /// </summary>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            option.WriteOptions(urlTextbox.Text, splitTextbox.Text, formatTextbox.Text);
            this.Close();
        }

        private void startUpToggle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (startUpToggle.Checked && !option.IsStartup)
                {
                    option.AddStartup();
                    MessageBox.Show("시작 프로그램으로 등록되었습니다.", "알림");
                }
                else if (!startUpToggle.Checked && option.IsStartup)
                {
                    option.RemoveStartup();
                    MessageBox.Show("시작 프로그램 등록이 해제되었습니다.", "알림");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러");
            }
        }
    }
}