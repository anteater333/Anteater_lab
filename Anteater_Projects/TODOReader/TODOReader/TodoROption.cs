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
        public void AddStartup()
        {
            try
            {
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.SetValue(appName, Application.ExecutablePath.ToString());
                startupKey.Close();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 시작 프로그램 해제
        /// </summary>
        public void RemoveStartup()
        {
            try
            {
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.DeleteValue(appName, false);
                startupKey.Close();
            }
            catch
            {
                throw;
            }
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
        /// 관리자 권한인지 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}