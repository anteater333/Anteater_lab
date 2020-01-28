using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace CSNaverParser
{
    public partial class MainForm : Form
    {
        private HtmlWeb naverWeb;
        private string html;
        private HtmlAgilityPack.HtmlDocument htmlDoc;
        private bool isSearchable = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            html = @"https://datalab.naver.com/keyword/realtimeList.naver";   // HTML을 가져올 URL
            naverWeb = new HtmlWeb();               // HTTP로 HTML 문서를 가져오는데 쓸 객체
            //htmlDoc = naverWeb.Load(html);      // type : HtmlDocument, 주소로 부터 가져온 HTML 문서.
            // Load는 당연히 파싱 할때마다.

            /* DEBUG 정의하기 귀찮음
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");  // type : HtmlNode, XPath 표현방법 사용.

            wordList.Items.Add(node.Name);
            wordList.Items.Add(node.OuterHtml);
            wordList.Items.Add(node.Id);
            wordList.Items.Add(node.InnerHtml);
            wordList.Items.Add(node.InnerText);
            wordList.Items.Add(node.OriginalName);
            wordList.Items.Add(node.XPath);
            */

            GetKeywords();
        }

        /// <summary>
        /// https://www.naver.com/ 으로부터 실시간 급상승 검색어 리스트를 가져와 출력하는.
        /// 가져오는 행위와 출력하는 행위가 분리되지 않음. 대충한 설계의 폐해.
        /// </summary>
        public void GetKeywords()
        {
            wordList.Items.Clear();

            htmlDoc = naverWeb.Load(html);
            var wordNodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[1]/div/div[2]/div[2]/div[2]/div/ul");

            try
            {
                foreach (var child in wordNodes)
                {
                    var childNodes = child.SelectNodes("./li");

                    foreach (var word in childNodes)
                    {
                        string item =
                            word
                            .SelectSingleNode("./div/span[1]")
                            .InnerText + ". " +
                            word
                            .SelectSingleNode("./div/span[2]/span[1]")
                            .InnerText;
                        wordList.Items.Add(item);
                    }
                }

                isSearchable = true;
            }
            catch (Exception e)
            {
                wordList.Items.Add("Error: " + e.Message);
                Console.WriteLine(e);
                isSearchable = false;
            }
        }

        private void parseButton_Click(object sender, EventArgs e)
        {
            GetKeywords();
        }

        private void wordList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void wordList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isSearchable)
            {
                int selected = -1;

                Point point = e.Location;

                // 현재 마우스 위치에 있는 아이템의 인덱스를 가져옴.
                // 리스트 박스의 빈 공간을 더블클릭 했을때에도 이벤트가 발생하는것을 방지
                // 하긴 하는데 사실 이 프로그램에서는 필요없긴 하나 배워두면 좋겠다 싶어서.
                selected = wordList.IndexFromPoint(point);

                if (selected != -1)
                {
                    string keyword = wordList.Items[selected] as string;
                    if (keyword.Length > 0)
                    {
                        int i = keyword.IndexOf(' ') + 1;
                        keyword = keyword.Substring(i);
                    }

                    string url = "https://search.naver.com/search.naver?where=nexearch&ie=utf8&query=" + keyword;

                    System.Diagnostics.Process.Start(url);
                }
            }
        }
    }
}
