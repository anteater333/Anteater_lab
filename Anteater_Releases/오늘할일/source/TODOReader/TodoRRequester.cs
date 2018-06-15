using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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

        public TodoRRequester(TodoROption option)
        {
            todoRequest = WebRequest.CreateHttp(option.TodoUrl);
            todoRequest.Method = "GET";
            todoRequest.Timeout = 10 * 1000;

            dtToday = DateTime.Now;

            this.splitter = new string[] { option.Splitter };
            this.format = option.Format;
        }

        /// <summary>
        /// url에서 TODO.txt를 읽어옴
        /// </summary>
        public string Request()
        {
            string todoToday = string.Empty;
            try
            {
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
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// TODO.txt에서 오늘 할 일을 찾음
        /// </summary>
        private string FindToday(string todoes)
        {
            string[] todoList = todoes.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            string today = dtToday.ToString(format);

            string todoToday = Array.Find(todoList, (todo) =>
            {
                bool found = false;
                found = todo.Contains("\n" + today + "\n") || todo.Contains(today + "\n");  // format이 ddd처럼 한글자인 경우를 해결할 수 있음.
                                                                                            // 무조건 날짜 표시는 한 줄에, 날짜를 나타내는 문자들만 있도록 제약.
                return found;
            }); // 오늘에 해당하는 문자열 찾음

            if (todoToday == null)
                todoToday = GenerateHolidayMsg();

            return todoToday;
        }

        /// <summary>
        /// 쉬는날 메세지 랜덤 생성
        /// </summary>
        private string GenerateHolidayMsg()
        {
            string[] messages =
            {
                "오늘 할 일이 없습니다.",
                "오늘 날짜에 등록된 일이 없습니다."
            };

            string[] jokeMessages =
            {
                "오늘은 쉬는날?",
                "주말인가요?",
                "오늘 할 일: 휴식",
                "오늘 날짜에 적힌 일은 없지만 스스로 찾아보시길 바랍니다.",
                "오늘 딱히 할 일은 없습니다.",
                "할일 없음. 알아서 하시오."
            };
            Random random = new Random();

            int rVal = random.Next(50);

            if (rVal >= 6)
                return messages[rVal % 2];
            else
                return jokeMessages[rVal];
        }
    }
}