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
                    if (option.AddStartup() == 0)
                        MessageBox.Show(this, "시작 프로그램으로 등록되었습니다.", "알림");
                    else
                    {
                        MessageBox.Show(this, "시작 프로그램 설정에 실패했습니다.", "에러");
                        startUpToggle.Checked = !startUpToggle.Checked;
                    }
                }
                else if (!startUpToggle.Checked && option.IsStartup)
                {
                    if (option.RemoveStartup() == 0)
                        MessageBox.Show(this, "시작 프로그램 등록이 해제되었습니다.", "알림");
                    else
                    {
                        MessageBox.Show(this, "시작 프로그램 설정에 실패했습니다.", "에러");
                        startUpToggle.Checked = !startUpToggle.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "에러");
                startUpToggle.Checked = !startUpToggle.Checked;
            }
        }
    }
}
