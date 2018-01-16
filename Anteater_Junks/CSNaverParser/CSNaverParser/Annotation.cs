/*****************************
* 2018.01.15 C# HTML 파서 체험
* 목표 : HTML 파서 만들어보기
     ******* 코멘트 *******
오늘도 이어지는 이것저것 알아보기 시간. 저번주 금요일엔 Xamarin.forms를 해보려다가 안드로이드쪽 코드에서 뭐가 에러가 났는지 안되가지고 던짐.
확실히 마음가짐이 대충대충하자는 식이니까 훨씬 Guilt-free에 가까워진 것 같다. 좋은건 아닌데 지금 상황에서 딱히 나쁜것도 아닌지라. 뭐 그냥 그럼.

오늘은 가볍게, 네이버 실시간 급상승 검색어 파싱해오기!
사실 예전에 동아리에서 자바로 한번 배워본적 있긴 한데, 허허허... 기억이 잘...
그래도 나름 철두철미했던 2016년의 나 자신이 다 필기를 해놨다. 기본 골자는 그걸 참고해보도록 하자.

HTML 파서 중에서 제일 유명한건 아마 BeautifulSoup 인것 같다. 대충 찾아보니까 이게 자주 나오더라.
BeautifulSoup은 파이썬 라이브러리이고, 이걸 자바용으로 만든게 Jsoup, 이걸 예전에 동아리에서 배웠었다.

C#용으론 어떤 라이브러리가 있는지 한번 찾아보자.

두 가지 방법이 있다.
먼저, WinForm 컴포넌트 중에 웹브라우저라고 있었는데, 그걸로 URL을 통해 접속해 HTML을 가져올 수 있다.
다른 방법은, NuGet에서 HtmlAgilityPack 이라는 라이브러리를 설치해서 사용하면 된다.
웹브라우저는 좀 난잡해질거 같으니까, 깔끔하게 원하는대로 쓸 수 있을 것 같은 HtmlAgilityPack을 써보자.

참고자료 http://html-agility-pack.net/

그냥 메인 페이지에 떡 하고 예제가 나와있다. 참고해보자.

흠... 폼은 어떻게 구성할까...

버튼하나랑... 출력을 어디다가 하면 좋을까...
출력은 ListBox에다가. 뭐 이런건 중요한건 아님.

흠... Form1.cs의 파일명을 MainForm.cs로 바꿨는데 갑자기 Designer.cs에 있는 InitializeComponent를 인식하지 못한다.
뜬금없네. 아니 비주얼스튜디오 리펙터 이정도밖에 안되는 거였어?

일단은 다시 Form1으로 파일명을 바꿨다가 다시 바꿔보니까 제대로 작동한다. 뭐... 원인을 알 수 없는 오류였던것 같음.

아오 귀찮아 파싱 제대로 시작하는건 내일로.
******************************/

/*****************************
* 날짜 : 2018.01.16
* 목표 : HTML 파서 진짜로 만들어보기
     ******* 코멘트 *******
일단은 http://html-agility-pack.net/from-web 여기 코드를 따라해보자.

XPath?
XPath는 XML같은 마크업 랭귀지에서 원하는 태그의 노드만 가져올 수 있는 표현방법인것 같다.

    var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

이 메소드의 입력으로 주어진 문자열이 바로 XPath 표현식이다.
위의 코드를 통해 <head> 노드 안의 <title> 노드를 가져왔다.
SelectSingleNode() 메소드는 조건에 부합하는 첫 번째 노드, 즉 노드 하나만 불러오는 메소드고,

    var nodes = htmlDoc.DocumentNode.SelectNodes("//head");

이렇게 조건에 부합하는 노드 묶음을 가져올 수도 있는 것 같다.

    wordList.Items.Add(node.Name);              // title
    wordList.Items.Add(node.OuterHtml);         // <title>NAVER</title>
    wordList.Items.Add(node.Id);                // (아무 내용 없음)
    wordList.Items.Add(node.InnerHtml);         // NAVER
    wordList.Items.Add(node.InnerText);         // NAVER
    wordList.Items.Add(node.OriginalName);      // title
    wordList.Items.Add(node.XPath);             // /html[1]/head[1]/title[1]

이렇게 한번 node가 어떤 정보를 가지고 있는지 출력해봤다.
인기검색어 리스트는 어떻게 불러와야할까 그럼...
검색어 까지 찾아들어가기엔 XPath 경로가 지나치게 길어질 것 같은데,

크으으 파이어폭스의 개발자 도구가 다 했다.
/html/body/div[2]/div[1]/div[2]/div[2]/div[1]/div
XPath 복사 기능이 있을 줄이야.

흐으으음.
HtmlNodeCollection 타입이 이터레이터를 지원하지 않는건가. foreach문 돌리니까 계속 하나만 반복해서 출력한다.
아아아아 이터레이터를 지원하지 않는게 아니라, 바보같이 XPath를 절대경로로 돌려서 그런 것 같다.
XPath 쓰는 법을 좀 제대로 알아봐야 할듯.
XPath는 기본적으로 디렉토리 찾아들어가는 방식과 비슷하다.
상대경로를 표현하려면, [.]을 쓰면 된다. 현재 경로를 나타내는 기호.

일단 경로 문제는 해결했다. 인기검색어 20개 딱 맞게 나옴.
소름돋게 20개가 설정해놓은 ListBox 컴포넌트 크기에 딱 맞는다.

근데 이게 원래 이렇게 인기검색어가 자주 안변하는건가.
처음 실행후 5~10분 기다렸다가 다시 버튼을 눌러봐야겠음.

아주 잘됨. 오늘 내용 끗. 사실 아이템을 클릭하면 바로 네이버 검색창에다가 보내주는것도 해볼까 했는데, 귀찮다.
그냥 방법만 알아두자.
    System.Diagnostics.Process.Start(target);
마법의 메소드. target에 URL을 넣으면 알아서 기본 브라우저로 열어준다.
https://search.naver.com/search.naver?&query=검색어
이렇게 경로를 만들어서 보내주면 되지 않을까.
에라이 그냥 해보자.

무슨이벤트를 쓰면 좋을까요오오오오, 빨리 하고 교양 쌓으러 가야지이이이이이
하필이면 지금 학교에서 입영통지서 올리라고 전화도 오고오오오말이야아아아아
참고자료 http://blog.nnoco.com/91

이제 진짜 끝 다했다.
******************************/
