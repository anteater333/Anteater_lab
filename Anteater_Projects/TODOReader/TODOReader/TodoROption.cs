using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

namespace TODOReader
{
    /// <summary>
    /// 프로그램 옵션 관리 모듈. app.config 파일 사용.
    /// </summary>
    public class TodoROption
    {
        private string todoUrl;
        private string splitter;
        private string dateFormat;
        private bool preStartupState;

        #region 시작 프로그램 등록 관련 변수
        // 시작 프로그램 레지스트리
        private string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private Microsoft.Win32.RegistryKey startupKey;
        private string appName = System.AppDomain.CurrentDomain.FriendlyName;
        private string makeStartUp = @".\MakeStartup.exe";
        #endregion

        /// <summary>
        /// 어플리케이션의 시작 프로그램 등록 유무
        /// </summary>
        public bool IsStartup { get => CheckStartup(); }
        /// <summary>
        /// TODO.txt가 위치한 URL
        /// </summary>
        public string TodoUrl { get => todoUrl; set => todoUrl = value; }
        /// <summary>
        /// TODO.txt에서 각 날짜별 내용을 구분할 때 사용할 구분자
        /// </summary>
        public string Splitter { get => splitter; set => splitter = value; }
        /// <summary>
        /// TODO.txt에서 오늘 내용을 찾을 때 사용할 날짜 형식
        /// </summary>
        public string Format { get => dateFormat; set => dateFormat = value; }

        public TodoROption()
        {
            this.preStartupState = CheckStartup();
            ReadOptions();
        }

        /// <summary>
        /// 시작 프로그램 등록
        /// </summary>
        public int AddStartup()
        {
            string arg = appName + " " + Application.ExecutablePath.ToString() + " " + "a";
            return RunProcess(makeStartUp, arg);
        }
        /// <summary>
        /// 시작 프로그램 해제
        /// </summary>
        public int RemoveStartup()
        {
            string arg = appName + " " + Application.ExecutablePath.ToString() + " " + "r";
            return RunProcess(makeStartUp, arg);
        }

        /// <summary>
        /// 어플리케이션이 시작 프로그램인지 확인
        /// </summary>
        private bool CheckStartup()
        {
            startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);
            return !(startupKey.GetValue(appName) == null);
        }

        /// <summary>
        /// App,config에 옵션을 적용함
        /// </summary>
        public void WriteOptions(string url, string splitter, string format)
        {
            Properties.Settings.Default.url = url;
            Properties.Settings.Default.seperator = splitter;
            Properties.Settings.Default.dateformat = format;
            Properties.Settings.Default.Save();

            ReadOptions();
        }

        /// <summary>
        /// App.config로부터 옵션을 가져옴
        /// </summary>
        private void ReadOptions()
        {
            todoUrl = Properties.Settings.Default.url;
            splitter = Properties.Settings.Default.seperator;
            dateFormat = Properties.Settings.Default.dateformat;
        }

        /// <summary>
        /// 시작 프로그램 등록용 프로세스 실행 메소드
        /// </summary>
        private int RunProcess(string fileName, string args)
        {
            Process process = new Process();

            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = args;

            process.Start();
            process.WaitForExit();

            return process.ExitCode;
        }
    }
}