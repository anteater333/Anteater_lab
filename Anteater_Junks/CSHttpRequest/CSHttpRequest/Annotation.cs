/*****************************
* HttpWebRequest 탐구 * 
*****************************
* 날짜 : 2018.01.22
* 목표 : 네이버 로그인
     ******* 코멘트 *******
훈련소 입소 전의 비정규, 자유 학습과정.
오늘은 HttpWebRequest라는 클래스를 사용해 네이버에 로그인해보자.

내가 저번주에 인기검색어 가져오는걸 만들면서 느낀건데,
확실히 웹에 발을 들이니까 프로그램이 쓸만해진다.
컴퓨터 킬때마다 자연스럽게 인기검색어 확인하게 됨.
물론 DeZipper도 개인적인 용도로 쓰긴 하지만, 흠... 사용 빈도랑 접근성에서 확실히 차이가 남.
흠 사실 훈련소 들어가기 전에 Lazzzy나 다시 만들어 보는것도 나쁘진 않겠는데. 납땜도 날 추울때 해야지.
부품 새로 주문하고...

어쨌든, 오늘 배워볼 클래스는 HttpWebRequest 라고 하는데, 사실 HttpWebResponse라는 클래스도 있고,
URL로 Web에 접근해서 Http 요청/응답을 할 수 있게 만들어 주는 클래스로 보인다.
... 아... 집중안돼...
일단은 백문이 불여일견
참고자료 http://lstar2397.tistory.com/1
일단 HttpWebRequest클래스는 System.Net 네임스페이스에 있고,
WebRequest라는 추상 클래스에서 파생된걸로 보인다.
보니까 FtpWebRequest라는 클래스도 있다. 어-썸.

일단 NaverLogin 메소드를 작성하긴 했는데, 로그인이 안된다.
비밀번호에 들어간 &기호 때문인가?
&기호 때문 맞습니다. 정답! 하하히히히히허ㅏ허히ㅓ히ㅓ히ㅣㅎ
아버지 아이디로 로그인 해보니까 잘 된다.
역시 예전에 인벤에서 주워들은 정보가 맞았다. 비밀번호에 &나 %같은걸 넣으면 이렇게 프로그램에서 접근하기가 까다로워진다.
그럼 어떻게 처리해야 할까?
URL 인코딩에서 &기호는 %26에 해당된다. 일단은 프로그램은 조금 있다 고쳐보고, 테스트에서 한번 %26으로 입력해보자.
와우 로그인 성공해버렸자너?
URL 인코딩을 제공하는 메소드를 찾아보자.

System.Web 네임스페이스에 HttpUtility라는 클래스가 있다.
참고자료 https://msdn.microsoft.com/ko-kr/library/system.web.httputility(v=vs.110).aspx
대놓고 웹 요청을 처리할 때 URL 인코딩 및 디코딩을 위한 메서드를 제공한다고 적혀있다.
HttpUtility.UrlEncode() 메소드를 사용해서 인코딩하면 된다.
오늘은 일단 이렇게 로그인 해보고 끝.
******************************/

/*****************************
* 날짜 : 2018.01.23
* 목표 : HTTP Response 메세지 분석
     ******* 코멘트 *******
어제는 로그인 되자 마자 신나서 바로 끝내버렸다.
오늘은 다시 평온을 되찾고 코드와 로그인 시 받는 응답에 대해 분석해보자.

    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://nid.naver.com/nidlogin.login");

너무 앞에서부터 시작하는게 아닌가 싶지만, 짚고 넘어가야 할 부분이다.
HttpWebRequest 클래스의 객체를 생성하는 코드인데, new를 사용하지 않는다. 이거 어디서 많이 봤죠?
이게 바로 팩토리 패턴. 정확히는 팩토리 메소드 패턴.
조금 더 분석하자면, string으로 주어진 URL을 아래와 같이 분류해서,
    http://
    https://
    ftp://
    file://
어떤 ConcreteCreator가 객체를 생성할지 결정하고, 클라이언트는 어떤 객체가 생성되는지 알 필요 없도록 만들어준다.
신기방기. 역시 .NET은 허투루 설계된것이 아니다.

    webRequest.Method = "POST";
    webRequest.Referer = "https://nid.naver.com/nidlogin.login";
    webRequest.ContentType = "application/x-www-form-urlencoded";
    webRequest.CookieContainer = new CookieContainer();

여기는 webRequest 인스턴스가 가지는 프로퍼티들을 설정하는 부분.
Method 는 뭐 당연히 Http 메소드를 뜻하겠지. POST랑 GET이 있다.
참고자료 https://ko.wikipedia.org/wiki/HTTP, http://unius.tistory.com/535
조금 더 자세히 알기 위해 위키백과를 켜보자.
제일 유명한 메소드가 POST랑 GET. HTTP 무식자에 가까운 내가 알 정도니까.

GET은 특정 URL의 자원을 요청할 때 사용된다.
아 그리고 간단한 파라메터를 URL 상에 표현하여 서버에 넘겨줄 수도 있다.
저번에 검색 할때 URL에서 ? 뒤에 붙은 파라메터들이 바로 그것.
여기서 중요한 점은, 변수들이 그냥 URL에 훤히 드러난다는 점이다.
따라서 로그인 같은 과정을 GET으로 처리하면 큰일남.
결과적으로 GET 메소드는 일반적인 경우엔 서버의 자원을 요청할 때 사용되고, 서버의 상태를 변경하지 않는다.

POST 메소드는 변수가 URL상에 표현되지 않고, HTTP Body에 입력된다.
따라서 주소창에 변수가 노출될 걱정도 필요 없고, HTTP Body에 데이터가 붙으니까 데이터 크기에 제한도 없다.
그러니까 일반적으로 폼에 입력한 데이터를 서버에 전송하여 상태를 변경하는 작업에 사용된다.

Referer는 현재 페이지로 오기 전의 페이지 주소값이 담겨있는 환경변수이다.
이건 어제 찾아봤었다. 딱히 중요한건 없어보임. 그냥 HTTP 헤더에 들어감.

ContentType도 Referer와 같이 HTTP 헤더에 들어갈 속성으로, 문자 그대로 이 HTTP 메세지가 어떤 내용인가를 뜻하는 것 같다.
이건 뭔가 명확하게 적힌 자료가 잘 안보이네...
application/x-www-form-urlencoded 이 컨텐츠 타입은 간단하게 말해서,
"enctp=2&svctype=0&id={ID}&pw={PW}" 이런걸 전달할 때 쓰는 컨텐츠 타입이다.

쿠키 컨테이너는 말 그대로 쿠키를 저장하는 속성이다. 쿠키를 담아야 쿠키 컨테이너가 의미가 있지.
CookieContainer is null by default.
You must assign a CookieContainer object to the property
to have cookies returned in the Cookies property of the HttpWebResponse returned by the GetResponse method.
MSDN에 따르면, 기본값이 NULL이기 때문에 쿠키를 담으려면 이 프로퍼티에 쿠키 컨테이너 인스턴스를 할당해야한다고 한다.
그러면 GetResponse 메소드가 반환하는 리스폰스의 쿠키 프로퍼티가 쿠키 컨테이너에 담긴다고 한다.
쿠키에 대해서는 오늘 조금있다가 더 자세히 알아보자.

    StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
    streamWriter.Write($"enctp=2&svctype=0&id={ID}&pw={PW}");
    streamWriter.Close();

이 코드는 이제 HTTP 리퀘스트를 보내는 코드.
streamWriter 인스턴스를 생성하는 걸 봐라. 왠지 데코레이터가 생각나는데?
데코레이터가 아닐수도 있고... 아직 여기에 확신은 없는 초보 프로그래머라...
쨌든, 중요한것은 어떻게 동작하는가! StreamWriter는 입력 버퍼를 제공하는 객체.
생성자는 인수로 Stream을 받는데, 여기에 webRequest의 RequestStream을 넣었다.
이렇게 streamWriter를 생성함으로써, 우리는 입력버퍼에 스트링을 넣는 식으로 POST 리퀘스트를 보낼 수 있게 된다.
아 물론 Stream 객체가 제공하는 Write 메소드가 있으니까 그냥 GetRequestStream().Write()로 보내도 됨. 사용법은 조금 다름.
근데 StreamWriter를 쓰는 방식이 더 깔끔하고 편해보인다. 그리고 잊지말고 다 쓴 입력 스트림은 Close.

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

이 부분은 이제 POST 리퀘스트에 대한 응답을 받는 부분이다.
여기서 한가지 의문점. 리퀘스트는 RequestStream에 값이 쓰여질 때 이뤄질까 아니면 GetResponse() 메소드가 호출될 때 이뤄질까?
흠... 흠.... 뭐.... GetResponse() 될 때가 맞지 않을까...?
어쨌든 Response의 상태를 읽어서(아마 이건 HTTP 응답 코드), OK이면 메세지를 읽는다.
    Stream dataStream = webResponse.GetResponseStream();
    StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
    string result = reader.ReadToEnd();
이건 Request에 Write 할 때 처럼 뭐 줄여보자면
    StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
    string result = reader.ReadToEnd();
이렇게? 둘 사이에 큰 차이는 없을듯. 아니면 밑에 Close() 이거 때문에 그러나?
콘솔 출력은 내가 그냥 확인해보려고 넣은거.
result엔 Response 메세지가 통째로 들어있고,
거기에 https://nid.naver.com/login/sso/finalize.nhn?url 이 url이 들어있다면 로그인에 성공했다. 끝!

<html>
<script language=javascript>
location.replace("https://nid.naver.com/login/sso/finalize.nhn?url=http%3A%2F%2Fwww.naver.com&sid=bWC1bz7Gpn672Y32&svctype=1");
</script>
</html>

웹 브라우저에서 로그인 안했는데, 저 링크를 따라 들어가니까 로그인 된 상태로 네이버 메인 창이 뜬다.

이제 결과를 조금 더 분석해보자.
우선, 쿠키엔 뭐가 들어갈까?
참고자료 https://msdn.microsoft.com/ko-kr/library/system.net.httpwebrequest.cookiecontainer(v=vs.110).aspx

막 주르륵 주르륵 나오는데, 머리가 아프다.

흠... 일단은 오늘은 여기까지 하고. 내일은 로그인을 일단 했으니까, 그 후에 어떻게 더 활용할 수 있는지에 대해 생각해보자.
깔깔 자율학습 너무 편해요 깔깔깔
******************************/