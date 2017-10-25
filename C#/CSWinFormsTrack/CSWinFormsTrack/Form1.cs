﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CSWinFormsTrack
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private int timerCount = 0;

        private BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();

            /*
            // ProgressBar테스트용
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            */

            // BackgroundWorker 예제
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            timer.Stop();
            timer.Start();
            timerCount = 0;

            progressBar1.Value = 0;
            progressBar2.Value = 0;
            */

            // BackgroundWorker 예제
            // 비동기 실행
            worker.RunWorkerAsync();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // 한 스텝 이동
            progressBar1.PerformStep();
            progressBar2.PerformStep();

            // 타이머 중지 조건
            if (++timerCount == 10)
            {
                timer.Stop();
                progressBar3.Enabled = false; // 딱히 바뀌는건 없는데.
            }
        }

        /// <summary>
        /// Form 호출 시 이벤트. form의 디폴트 이벤트.
        /// 속성 수정 시 Form1.Designer.cs의 Initialize를 수정하기보단 이 메서드를 통해 수정하자.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            // progressBar2 속성 변경
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 90;  // 기본값 100
            progressBar2.Step = 5;      // 기본값 10
            progressBar2.Value = 0;

            // 타이머 시작
            timer.Start();
            */
        }

        // Worker 쓰레드에서 실제 하는 일.
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string srcDir = @"D:\Temp\_Src";
            string destDir = @"D:\Temp\_Dest";

            DirectoryInfo di = new DirectoryInfo(srcDir);
            FileInfo[] fileInfos = di.GetFiles();
            int totalFiles = fileInfos.Length;
            int counter = 0;
            int pct = 0;
            foreach (var fi in fileInfos)
            {
                string destFile = Path.Combine(destDir, fi.Name);
                File.Copy(fi.FullName, destFile);

                // 진행상황 전달 이벤트 발생
                pct = ((++counter * 100) / totalFiles);
                worker.ReportProgress(pct);
            }
        }

        // Progress Changed.
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage; // UI에 그려지는 진행상황 값 변경
        }

        // 작업 완료시.
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Error check
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error");
                return;
            }
            MessageBox.Show("성공적으로 완료되었습니다.");
        }
    }
}
