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

            // 테스트용 코드 /////////////////////////////////////////////////////////////////////////////////////
            option.TodoUrl = @"https://raw.githubusercontent.com/anteater333/Anteater_lab/master/Txt/TODOs.txt";//
            option.Splitter = "======================================";                                         //
            //////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            optionForm = new OptionForm(option);
            optionForm.Owner = this;
            optionForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadTodo();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ReadTodo();
        }

        private void ReadTodo()
        {
            todoRequest = new TodoRRequester(option.TodoUrl, option.Splitter);
            
            todoTextbox.Lines = todoRequest.Request().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
