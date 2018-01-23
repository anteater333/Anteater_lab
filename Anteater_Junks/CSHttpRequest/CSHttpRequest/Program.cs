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
                Console.WriteLine("===============================");
                Console.WriteLine("===============================");
                CookieAnalysis(webRequest, webRequest.CookieContainer);
                           
                if (result.Contains("https://nid.naver.com/login/sso/finalize.nhn?url"))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        static private void CookieAnalysis(HttpWebRequest request, CookieContainer cookies)
        {
            foreach(Cookie cookie in cookies.GetCookies(request.RequestUri))
            {
                Console.WriteLine("Cookie:");
                Console.WriteLine("{0} = {1}", cookie.Name, cookie.Value);
                Console.WriteLine("Domain: {0}", cookie.Domain);
                Console.WriteLine("Path: {0}", cookie.Path);
                Console.WriteLine("Port: {0}", cookie.Port);
                Console.WriteLine("Secure: {0}", cookie.Secure);

                Console.WriteLine("When issued: {0}", cookie.TimeStamp);
                Console.WriteLine("Expires: {0} (expired? {1})",
                    cookie.Expires, cookie.Expired);
                Console.WriteLine("Don't save: {0}", cookie.Discard);
                Console.WriteLine("Comment: {0}", cookie.Comment);
                Console.WriteLine("Uri for comments: {0}", cookie.CommentUri);
                Console.WriteLine("Version: RFC {0}", cookie.Version == 1 ? "2109" : "2965");

                // Show the string representation of the cookieie.
                Console.WriteLine("String: {0}", cookie.ToString());
                Console.WriteLine("===============================");
            }
        }
    }
}
