using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;   // HttpUtility

namespace CSHttpRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("ID : ");
            string ID = Console.ReadLine();
            Console.Write("PW : ");
            string PW = HttpUtility.UrlEncode(Console.ReadLine());


            if (NaverLogin(ID, PW))
            {
                Console.WriteLine("로그인 성공");
            }
            else
            {
                Console.WriteLine("로그인 실패");
            }
        }

        static private bool NaverLogin(string ID, string PW)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://nid.naver.com/nidlogin.login");

            /* Setting HttpWebRequest's Properties */
            webRequest.Method = "POST";
            webRequest.Referer = "https://nid.naver.com/nidlogin.login";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.CookieContainer = new CookieContainer();

            /* Post request */
            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write($"enctp=2&svctype=0&id={ID}&pw={PW}");
            streamWriter.Close();

            /* Get HTTP Response */
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                string result = reader.ReadToEnd();
                webResponse.Close();
                dataStream.Close();
                reader.Close();

                Console.WriteLine(result);

                if (result.Contains("https://nid.naver.com/login/sso/finalize.nhn?url"))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
