using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TODOReader
{
    /// <summary>
    /// 프로그램 옵션 관리 모듈. app.config 파일 사용.
    /// </summary>
    public class TodoROption
    {
        private bool isStartup;
        private string todoUrl;
        private string splitter;

        /// <summary>
        /// 어플리케이션이 시작 프로그램으로 등록되어 있는지를 확인.
        /// </summary>
        public bool IsStartup { get => isStartup; }
        public string TodoUrl { get => todoUrl; set => todoUrl = value; }
        public string Splitter { get => splitter; set => splitter = value; }

        public TodoROption()
        {

        }

        public void AddStartup()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStartup()
        {
            throw new System.NotImplementedException();
        }

        private bool CheckStartup()
        {
            throw new System.NotImplementedException();
        }
    }
}