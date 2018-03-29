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

        public TodoRRequester(string url, string splitter)
        {
            todoRequest = WebRequest.CreateHttp(url);
            todoRequest.Method = "GET";
            todoRequest.Timeout = 10 * 1000;

            dtToday = DateTime.Now;

            this.splitter = new string[] { splitter };
        }

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
                        string[] todoList = strReader.ReadToEnd().Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                        todoToday = FindToday(todoList);
                    }
                }
            }

            return todoToday;
        }

        private string FindToday(string[] todoList)
        {
            string today = dtToday.ToString("yyyy.MM.dd");

            return Array.Find(todoList, todo => todo.Contains(today));
        }
    }
}