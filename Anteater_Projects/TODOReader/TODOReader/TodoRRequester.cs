using System;
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

        public TodoRRequester(string url)
        {
            todoRequest = WebRequest.CreateHttp(url);
        }

        public string Request()
        {
            throw new System.NotImplementedException();
        }
    }
}