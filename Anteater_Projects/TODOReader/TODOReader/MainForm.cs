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
        private OptionForm optionForm;

        public MainForm()
        {
            option = new TodoROption();
            InitializeComponent();

            // 테스트용 코드 ///////////////////////////////////////////////////////////////////////////////////////
            //option.TodoUrl = @"https://raw.githubusercontent.com/anteater333/Anteater_lab/master/Txt/TODOs.txt";//
            //option.Splitter = "======================================";                                         //
            //option.Format = "yyyy.MM.dd";                                                                       //
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// 옵션창 버튼
        /// </summary>
        private void settingButton_Click(object sender, EventArgs e)
        {
            optionForm = new OptionForm(option);
            optionForm.Owner = this;
            optionForm.ShowDialog();
            ReadTodo();
        }

        /// <summary></summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            FadeIn(this, 20);

            ReadTodo();
        }

        /// <summary>
        /// 새로고침 버튼
        /// </summary>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReadTodo();
        }

        /// <summary>
        /// TODO.txt를 읽어 화면에 출력
        /// </summary>
        private void ReadTodo()
        {
            todoRequest = new TodoRRequester(option.TodoUrl, option.Splitter, option.Format);

            todoTextbox.Lines = todoRequest.Request().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 투명도 서서히 감소
        /// </summary>
        private async Task FadeIn(Form o, int interval = 80)
        {
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }
            o.Opacity = 1.0;
        }

        /// <summary>
        /// 투명도 서서히 증가
        /// </summary>
        private async Task FadeOut(Form o, double min = 0.0, int interval = 80)
        {
            while (o.Opacity > min)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = min;
        }

        /// <summary>
        /// 메인 폼이 비활성화 됐을 때
        /// </summary>
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            FadeOut(this, 0.5, 20);
        }

        /// <summary>
        /// 메인 폼이 활성화 됐을 때
        /// </summary>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            FadeIn(this, 20);
        }

        /// <summary>
        /// 메인 폼 종료 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Opacity > 0)
            {
                e.Cancel = true;
                CloseWithFade(e);
            }
        }

        /// <summary>
        /// 비동기 함수 대기 후 종료
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task CloseWithFade(FormClosingEventArgs e)
        {
            await FadeOut(this, 0.0, 5);
            e.Cancel = false;
            Close();
        }
    }
}
