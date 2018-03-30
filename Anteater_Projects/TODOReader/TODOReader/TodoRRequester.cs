using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TODOReader
{
    /// <summary>
    /// Http request 처리 모듈.
    /// </summary>
    public class TodoRRequester
    {
        private HttpWebRequest todoRequest;
        private DateTime dtToday;
        private string[] splitter;
        private string format;

        public TodoRRequester(string url, string splitter, string format)
        {
            todoRequest = WebRequest.CreateHttp(url);
            todoRequest.Method = "GET";
            todoRequest.Timeout = 10 * 1000;

            dtToday = DateTime.Now;

            this.splitter = new string[] { splitter };
            this.format = format;
        }

        /// <summary>
        /// url에서 TODO.txt를 읽어옴
        /// </summary>
        public string Request()
        {
            string todoToday = string.Empty;
            using (HttpWebResponse todoResponse = (HttpWebResponse)todoRequest.GetResponse())
            {
                if (todoResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = todoResponse.GetResponseStream();
                    using (StreamReader strReader = new StreamReader(responseStream))
                    {
                        todoToday = FindToday(strReader.ReadToEnd());
                    }
                }
            }

            return todoToday;
        }

        /// <summary>
        /// TODO.txt에서 오늘 할 일을 찾음
        /// </summary>
        private string FindToday(string todoes)
        {
            string[] todoList = todoes.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            string today = dtToday.ToString(format);

            string todoToday = Array.Find(todoList, todo => todo.Contains(today));

            if (todoToday == null)
                todoToday = "오늘은 할 일이 없습니다.";

            return todoToday;
        }
    }
}