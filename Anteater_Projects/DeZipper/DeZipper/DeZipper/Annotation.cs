﻿/**************************************************
                   * DeZipper *
***************************************************/

/***************************************************
* 날짜 : 2017.11.02
* 목표 : 프로젝트 생성
             ******* 코멘트 *******
부산 다녀오느라 휴가였음. 실은 어제 수요일에 시작했어야하나 너무 피곤했던 관계로 오늘 시작.
할일은 https://github.com/anteater333/Anteater_lab/projects/1 여기에 정리돼있음. github 짱짱.

일단은 CMD 창에서 실행할 수 있는 프로그램을 만드는 거 부터 하자. 그 편이 쉬우니까.
프로그램 다 작성 후에 WinForms. GUI에 연결하자.

그리고 이제 이런 프로젝트는 Anteater_Projects 폴더에서 작업.
완성 후엔 이런 주석같은 군더더기 제거해서 Anteater_Releases 폴더에 복사.

앞으로 해야할 일
시나리오 작성.
ZipFile 클래스 공부.
***************************************************/

/***************************************************
* 날짜 : 2017.11.06
* 목표 : Zip 관련 클래스 학습
             ******* 코멘트 *******
ZipTest.cs 에서 작업.

ZipFile 클래스는 정적 클래스다. 인스턴스 만들 일 없다. Zip 관련된 static 메소드를 제공한다.
zip 파일을 나타내는 객체는 ZipArchive 클래스의 인스턴스로 나타낸다.

ZipArchive 클래스 예제에서 using문을 사용하는 것을 봤다.
using문은 메모리 할당-해제를 프로그래머가 예측 가능하도록 만든다.
using(객체선언)
{
    코드
}
using문과 함께 선언된 객체는 해당 블록이 끝날때 메모리가 해제된다.
불확실한 GC의 단점을 커버하는 방법인듯.

참고로 메모리를 할당하는 모든 객체는 기본적으로 IDispossable이라는 클래스를 상속받는다.
이 IDispossable이라는 객체가 바로 메모리를 해제하는 역활을 한다.
출처: http://hackss.tistory.com/942 [동동이의 블러그]

1)ZipFile의 정적 메소드로 읽어
2)ZipArchive로 반환받은 zip파일의 각 파일들은
3)ZipArchiveEntry 클래스 타입을 가진다. (Entries 속성)
ZipArchiveEntry는 컬렉션으로 foreach문이 사용가능하다.

그렇다면 ZipArchive의 프로퍼티 Entries 에서 특정 엔트리를 하나 삭제하면 실제 zip 파일에도 영향을 줄까?

우선 특정 엔트리에서 delete 메소드를 호출해서 삭제할 수 있다.
이 경우 zip 파일을 update 모드로 열어야하고, 실제 zip파일에 영향을 주지 않는다.
줄것같은데 안준다 어쨌든.

(참고) foreach문에선 컬렉션을 수정해선 안된다. 에러난다. 그 경우 for 문으로 바꿔줘야함.
***************************************************/

/***************************************************
* 날짜 : 2017.11.07
* 목표 : 기능구현
         ******* 코멘트 *******
Entries 프로퍼티에서 얻은 경로를 수정해 파일을 삭제해보자.
사실상 이번 프로젝트의 핵심 기능.

우선 entry.FullName은 경로를 포함한 파일이름을 가져온다.
entry.Name은 폴더일 경우 아무 값도 가지지 않고(아마 NULL인듯), 파일일 경우 파일명만 가져온다.
==> entry.Name == null 로 비교하면 될 줄 알았지만 안됨. entry.Name == "" 로 비교하니까 됨.

entries는 DFS로 zip의 파일을 하나씩 넣은듯. 폴더 파일 구분은 안됐고 그냥 알파벳순으로.
해당 폴더의 파일을 돌다가 또다른 폴더가 있으면 그 폴더로 먼저 들어가서 탐색.

일단 오늘은 폴더는 그대로 놔두고, 파일만 삭제하는 기능을 구현해보자.

생각해볼 문제.
파일 삭제는 쉬움. 근데 폴더를 만났을때, 그걸 구별하는건 일단 할 수 있겠는데,
폴더가 비었는지를 판단하는걸 어떻게 하지.
예외처리는 천천히 핵심 기능을 완성시키고 하자. 일종의 마감처리같이.

참고로 File.Delete() 메소드는
If the file to be deleted does not exist, no exception is thrown.
라고 한다. 로그에 뭐라고 적을지 생각을 해보자.
***************************************************/

/***************************************************
* 날짜 : 2017.11.08
* 목표 : 기능구현. 폴더 삭제
         ******* 코멘트 *******
아이디어1
entries에서 폴더를 찾으면 해당 경로로 Directory 클래스 메소드를 사용하면 되지 않을까
https://msdn.microsoft.com/ko-kr/library/62t64db3(v=vs.110).aspx
Directory.Delete(string)
빈 디렉토리를 삭제.

아이디어2
먼저 파일들을 다 삭제하고,
다시 탐색을 시작해서 폴더들을 삭제.
구현하기 편하긴 할듯. 효율은 모르겠지만...

그리고 휴지통 보내기의 경우
 Microsoft.VisualBasic.FileIO
이 네임스페이스에 있는 클래스 FileSystem의 메소드를 사용하면 된다고한다.
근데 별로 마음에 들진 않는데...
차라리 휴지통 기능을 없애는건 어떨까.

갑자기 생각남.
삭제시 옵션은 Flag Enum으로 만들면 될듯.

아이디어3
파일 삭제하면서 폴더들을 스택에 넣음.
그럼 제일 하위 폴더부터 삭제할수 있다!

일단 써먹을 수 있는 정도는 만들어진듯하다.
파일 폴더 깔끔하게 삭제됨.

테스트는 이정도로 하고 CMD용 프로그램 구현 + 예외처리.
***************************************************/

/***************************************************
* 날짜 : 2017.11.09
* 목표 : CMD 실행 가능 프로그램 만들기
         ******* 코멘트 *******
이때까지 ZipTest에서 했던 내용을 토대로 실제 프로그램 구현하기.
코드 자체는 뭐 크게 변할거 있나 싶다만, 좀 깔끔하게.

한 번 생각해볼 점은, GUI 버전과 CMD 버전 둘이 같은 클래스의 메소드를 사용하도록 코드를 만드는게 좋지 않을까.

따라서 호출관계
DeZipperMain(프로그램 메인) -> DeZipperCMD || DeZipperGUI(UI담당 클래스) -> DeZipper(알고리즘 담당 클래스)

DeZipper 클래스에서는 파일 열기, 삭제만 하는거.
예외처리를 throw 하고 그걸 UI 담당 클래스에서 catch. 예외처리.
DeZipperCMD와 DeZipperGUI는 DeZipperUI 클래스에서 상속받도록 하는게 좋지 않을까.

Entries에서 항목 하나를 제외할때, 리스트를 순차적으로 탐색해서 삭제하면 비효율적일것같은데
효과적인 접근방법이 없을까. 이름으로 접근한다던가.
생성자에서 Entries를 Dictionary 타입으로 만들면 어떨까, 비효율적일까?

삭제와 출력을 따로하려니까 머리가 아프다.
삭제부분에선 for문을 돌리면서 yield return을 하고,
출력부분에선 foreach문을 돌리면 될까.

삭제 후 원본 zip 파일 삭제 기능을 만들다가 든 생각.
보아하니 DeZipper 인스턴스 생성 후 ZipArchive 필드가 계속 zip 파일을 열어두는데,
그럴 필요가 있나?
그냥 열어서 파일 리스트만 뽑아오고 닫는게 더 좋지 않을까?
어차피 생성자 말고 zipAchv 변수를 쓰는곳이 없다.

DeZipper 클래스는 어느정도 완성됨.
참고로 ExcuteDelete() 메소드는 yield return이 들어간 IEnumerable<string> 타입이니까
foreach문에서 호출해야한다.
***************************************************/

/***************************************************
* 날짜 : 2017.11.10
* 목표 : DeZipperUI, DeZipperCMD 구현.
         ******* 코멘트 *******
우선 DeZipper 클래스에 새로운 파일을 여는 메소드를 추가했다.
CMD 버전에선 쓸일 없을것같지만, GUI는 삭제 후에도 응용 프로그램 창이 계속 남아있으니까..

DeZipperMain(프로그램 메인) -> DeZipperCMD || DeZipperGUI(UI담당 클래스) -> DeZipper(알고리즘 담당 클래스)
어제 계획한 호출관계에 따라
DeZipper 클래스에선 유저와 어떤 통신도 불가능하다. 출력도 없고, 직접 입력을 넣을수도 없고.
예외처리도 안되있음. 이건 뭐 thorw 추가하긴 해야하는데, 요는 유저에게 직접 오류를 띄워주는 역할을 하지 않는다는거지.
어쨌든 유저에게 입력을 받고 메세지를 띄워주는 역할을 하는 클래스가 오늘 만들 DeZipperCMD : DeZipperUI 클래스다.
정확히 말하자면 CMD 버전에선 입력은 DeZipperMain의 args를 통해 받는다. 잡담이 좀 있네.

우선 DeZipperUI는 추상 클래스로 구현해야겠다.
UI가 해야할 일.
입출력.
입력을 받아서 DeZipper 인스턴스로 넘겨주고
Entries 프로퍼티를 가져와서 ZIP 내부 파일 리스트를 출력하거나,
삭제를 실행하고 삭제된 파일들을 하나하나 출력한다거나.

DeZipperUI에는 CMD, GUI 둘이 공통적으로, 필수적으로 포함해야하는 멤버들이 있어야한다.

휴지통기능은 일단 빼자. 미구현상태로 놔두자.

CMD버전 명령어를 생각해봐야겠다.
기본 사용법
DeZipper.exe [source_zip]
 : [source_zip] 에서 파일 리스트를 읽어옴
DeZipper.exe [source_zip] [target_dir]
 : [source_zip] 에서 파일 리스트를 읽어서 [target_dir] 에서 삭제
DeZipper.exe [source_zip] [target_dir] [options]
 : [options] 에 따라 옵션 설정, [options] 는 띄어쓰기로 구분
옵션. 옵션은 파일 삭제시에만 사용 가능.
-s, -silence
 : 출력 없음.
-e, -empty
 : 삭제 후 빈 폴더 삭제
-z, -zip
 : 삭제 후 원본 ZIP 파일 삭제
-r, -recycle
 : 휴지통으로 보냄
-ex [file], -exclude [file]
 : 파일 리스트에서 [file] 을 제외. 다중 옵션 사용 시 해당 옵션은 마지막에 위치해야함.



CMD버젼 구현할땐 CMD버젼 기능만 생각하자. GUI에서 어떻게 할지는 GUI 구현하면서.
따라서 GUI에선 DeZipper 인스턴스를 UI 클래스 생성자에서 생성하는게 골치아픈 일이지만,
한번 돌고 종료되는 CMD에선 그렇게 해도 상관이 없다.
그러므로 DeZipperCMD는 OpenZip 메소드가 필요가 없음.

리스트 출력 알고리즘에 대해 생각해보자.
지금까진 그냥 대충... 순차적으로 뱉었는데, 아무래도 깔끔하게 폴더와 디렉토리를 좀 구분해서 정렬할 필요가 있다.
예를 들어 한 폴더 내의 파일 엔트리를 출력한다고 하면,
먼저 해당 폴더의 파일들을 먼저 출력.
그 다음 해당 폴더 내부의 폴더를 출력.

그냥 순차적으로 출력할경우엔, 파일-폴더 구분도 없고
출력하다가 폴더를 만나면 바로 그 폴더로 들어가서
하위 폴더의 파일들을 먼저 다 출력하고 난 다음에야 상위 폴더의 파일들이 나오게 됨.

목표는 해당 폴더의 파일들을 모두 다 출력하고 하위 폴더의 파일들을 출력하는것.
머리아픈데 그냥 대충 출력할까...
DeZipper에서 가져오는 Entries가 뭐 트리 구조를 가지고있는게 아니라...
그냥 이미 정렬된 상태로 순차적인 리스트(정확히는 해쉬테이블이긴 하지만)로 저장된거라서.
일단은 그냥 출력하자. 가독성이 좀 별로긴함...

생각해보니 폴더는 리스트에서 삭제했는데 하위 파일들은 리스트에서 삭제가 안될수도 있음.
일장일단이 있다 이경우는.
-e 옵션에서 특정 폴더를 살리고 싶을때 사용할수 있지만...
폴더 하나를 리스트에서 날리기엔 좀 불편할듯.

아 File.Exist 메소드로 파일이 존재하지 않을때 상황을 처리하면 될 듯.

11/10 11:57 PM
현재 당면한 문제,
foreach문의 조건에서 예외가 발생하면 어떻게 예외를 catch할것인가?
참고자료 https://stackoverflow.com/questions/12522870/catch-exception-thrown-in-foreach-condition
다행이도 나랑 똑같은 사람이 있다.
위 링크의 질문자가 한 질문이랑 똑같은 상황
foreach문의 밖에 try-catch문을 넣으니 예외가 발생하면 한 번만 예외를 잡고 그냥 프로그램이 끝나버린다.
foreach문의 안에 try-catch문을 넣으니 예외를 잡지 못한다. 조건에서 발생한 예외니까.
일단... foreach문 대신 수동으로 반복문을 돌리는 방법이 있고... C# 문법 배우면서 본 그거... IEnumrator...

TryForEach라는 메소드를 구현하는 방법이 있다.
    public static IEnumerable<T> TryForEach<T>(this IEnumerable<T> sequence, Action<Exception> handler)
    {
        if (sequence == null)
        {
            throw new ArgumentNullException("sequence");
        }

        if (handler == null)
        {
            throw new ArgumentNullException("handler");
        }

        var mover = sequence.GetEnumerator();
        bool more;
        try
        {
            more = mover.MoveNext();
        }
        catch (Exception e)
        {
            handler(e);
            yield break;
        }

        while (more)
        {
            yield return mover.Current;
            try
            {
                more = mover.MoveNext();
            }
            catch (Exception e)
            {
                handler(e);
                yield break;
            }
        }
    }
이해하려는데 아주...
확장 메서드, 람다식, yield...
그냥 위 코드 쓰는대신 foreach 말고 while로 하자...

https://social.msdn.microsoft.com/Forums/vstudio/en-US/d4f6b528-f271-4a72-b2a1-57b02ca8628a/is-it-possible-to-resume-a-ienumerator-after-exception?forum=netfxbcl
IEnumrator에서 예외가 throw되면 열거가 끝난것으로 간주한다.
폭망... 코드 대폭 수정해야할듯.
그렇다면 예외를 발생시키는게 아니라 yield return에 특별한 오류 메세지를 string으로 반환해야하나...
윈도우 시스템상 파일명에 들어갈 수 없는 기호로 구분해서.
그럼 DeZipperException.cs 는 결국 필요없는 짓이 된다.
오류 메세지는 < > 기호로 묶어서 구분하자
DeZipperCMD 쪽에서 반환값을 받았을때 이게 에러코드인지, 삭제한 파일경로인지 알아낼 수 있어야 하니까.
흠... 잠을 못자서 머리가 안돌아간건가.
return할 문자열에 에러 메세지를 넣어버리면 출력할때 이상하잖아...

11/11 02:38 AM
얼추 다 한 것 처럼 보인다.
이제 CMD 버전에서 남은건 Main에서 args로 입력하는것만 남았다.
그건 다음주에.
***************************************************/

/***************************************************
* 날짜 : 2017.11.13
* 목표 : Main함수 args를 통해 입력받기
         ******* 코멘트 *******
흑흑 재밌었다. 오늘 철권 파이날은

저번주 금요일날 정리한 명령어들
기본 사용법
DeZipper.exe [source_zip]
 : [source_zip] 에서 파일 리스트를 읽어옴
DeZipper.exe [source_zip] [target_dir]
 : [source_zip] 에서 파일 리스트를 읽어서 [target_dir] 에서 삭제
DeZipper.exe [source_zip] [target_dir] [options]
 : [options] 에 따라 옵션 설정, [options] 는 띄어쓰기로 구분
옵션. 옵션은 파일 삭제시에만 사용 가능.
-s, -silence
 : 출력 없음.
-e, -empty
 : 삭제 후 빈 폴더 삭제
-z, -zip
 : 삭제 후 원본 ZIP 파일 삭제
-r, -recycle
 : 휴지통으로 보냄
-ex [file], -exclude [file]
 : 파일 리스트에서 [file] 을 제외. 다중 옵션 사용 시 해당 옵션은 마지막에 위치해야함.
-h, -help
 : readme.txt 파일을 출력합니다.

오늘은 좀 가벼운 내용이라 장난기 가득 넣어서.
***************************************************/

/***************************************************
* 날짜 : 2017.11.14
* 목표 : CMD 버전 테스트 및 오류 수정.
         ******* 코멘트 *******
테스트도 하고, Entries를 트리형식으로 정렬하는 알고리즘도 생각해보자.
테스트는 Windows PowerShell에서 수행.

첫번째로 발견한 에러.
IndexOutofRange. Array.Length 프로퍼티는 배열의 길이를 반납한다.
당연히 아무것도 없으면 0, 하나가 있으면 1.
그러니까 Length가 0이라고 array[0]이 있단 소리가 아니다!
근데 args[0]이면 DeZipper.exe가 들어있을줄 알았는데 그게 아니였네.
args엔 그냥 명령줄 인수만 들어감.

두번째로 발견한 에러.
예외를 발생시키진 않음. 근데 경로를 못찾는다. 이경우는 코드 내부에서 \를 /로 대체시켜주면 될듯.
그리고 이렇게 경로를 못찾은 경우에도 그냥 삭제 진행해버리는데 이러면 큰 문제가 발생할수있음.
중요 파일을 삭제해버리면 안되지.
그런 의미로 -recycle 옵션을 썼을때에도 삭제가 진행되지 않도록 임시조치.

세번째 수정사항.
-z 옵션 없으면 어차피 리스트만 읽어오는데, 읽기 전용으로 열어도 되지 않나?
이렇게 수정함으로써 이제 zip파일을 열고있는 상태에도 파일 삭제가 가능하다.
하지만 -z 옵션을 쓰면 예외가 발생하겠지. 이건 뭐 원래도 그랬던 거고 그래야만 하는거니까.
그리고 -z 옵션에서 예외가 발생해도 타겟 디렉토리에서의 삭제는 제대로 작동됨. 이건 딱히 바꿀 필요 없어보임.

이젠 원하는대로 잘 작동한다고 판단됨.

./DeZipper.exe D:\Temp\_Zip\audacity-win-2.1.0.zip D:\Temp\_Zip\TG\ -s -e -z -ex Audacity\audacity.exe Audacity\Plug-Ins\beat.ny

이게 파워쉘에서 테스트한 명령어.

아오씨 네번째 에러.
./DeZipper.exe D:\Temp\_Zip\audacity-win-2.1.0.zip
위 명령어를 테스트 했더니 예외발생 DeZipper.cs 47번째줄
Main에서 발생한 예외는 아닌걸로 봐서 args문제는 아닌것같다.
                if (value[value.Length - 1].Equals('\\') || value[value.Length - 1].Equals('/'))
에러는 여기서 발생.
메세지가
인덱스가 배열 범위를 벗어났습니다.
인걸로 봐서 IndexOutofRangeException으로 보임.
왜벗어났지?
아... tgPath를 ""으로 줘서... index가 -1이 된것같다.
귀찮지만 깔끔한 해결책은 DeZipper클래스와 DeZipperUI,CMD 클래스에 생성자를 오버로드하는거같고
그냥 대충 처리하는법은 "" 대신 "."을 주는거.
***************************************************/

/***************************************************
* 날짜 : 2017.11.15
* 목표 : 릴리즈 방법 조사.
     ******* 코멘트 *******
현재 생각중인 이상적인 방향은 GUI.exe랑 CMD.exe를 같은 경로에 두는 방식인데,
안타깝게도 내가 만든 프로그램을 어떻게 릴리즈하는지를 모르는 상태라...

일단 이걸 어떤식으로 할지 해결해야 DeZipperGUI 클래스를 어떤식으로 구현할지도 정할 수 있을듯.

참고자료 http://h5bak.tistory.com/188

전처리기를 좀 알아볼까
#if DEBUG
이런거처럼

프로젝트 속성에서 [빌드]-[조건부 컴파일 기호]를 써보자.

이제 현재 프로젝트에서 WinForm창을 띄울 수 있게 되었다. 이제 내일 GUI 구현하자.
***************************************************/

/***************************************************
* 날짜 : 2017.11.16
* 목표 : GUI 컴포넌트 배치
     ******* 코멘트 *******
저번에 WinForm 배우면서 미리 만들어본 GUI를 참고하도록 하자.

당연한 이야기지만 CLI 버전에서 지원하는 기능은 모두 지원해야함.

창 크기를 바꾸면 컴포넌트 배치도 거기에 따라 바꼈으면 좋겠는데.
TableLayoutPanel 컴포넌트를 쓰도록 하자.

너무 어렵다. 깔끔한 디자인이 안나온다.

프로그램 내부적으로 도움말을 표시해야할까?
귀찮은데

디자인은 얼추 괜찮게 된것같고.
이제 DeZipperGUI 구현하고 이벤트 연결하면 될거같다.
***************************************************/

/***************************************************
* 날짜 : 2017.11.17
* 목표 : DeZipperGUI : DeZipperUI, ZipArchiveEntries to Tree Algorithm
     ******* 코멘트 *******
오늘 할 일은 DeZipperGUI 클래스를 구현하기.

GUI 버전의 호출관계를 정리해보자.
DeZipperMain -> DeZipperForm -> DeZipperGUI -> DeZipper
DeZipperForm은 GUI 자체를 구성하는 클래스.
DeZipper는 프로그램의 연산 작업을 수행하는 클래스.
DeZipperGUI는 DeZipper를 호출하고,
연산 결과에 따른 데이터를 얻어 Form에 적용시키는 클래스.

DeZipperForm이랑 DeZipperGUI가 좀 모호하다.
Form에서 바로 DeZipper를 호출하면 안되나?
그러니까 DeZipperForm을 DeZipperGUI로 rename하면 어떨까.
근데 또 DeZipperForm은 DeZipperUI를 상속받지 못함. Form을 이미 상속받고있으니까.
DeZipperGUI가 중계자 역할을 하려하니 메소드들의 리턴값이 다 void네...
Form이랑 완전히 분리해서 어떻게 나타낼 수 있을까.
모듈화를 너무 과하게 한건 아닐까. 배우기 위해 만든다곤 하지만.

DeZipperGUI의 프로퍼티를 통해 어떻게 해결할 수 있을 것 같다.

중간에 추가된 DeZipperForm을 얼마나 깔끔하게 다루는지 중요할듯.
정확히는 form에서 DeZipperGUI를 얼마나 깔끔하게 호출할지.

다만 그 전에 Entries를 tree로 표현하는 방법을 생각하자. 오늘은 반드시.
이 부분은 DeZipper에서 엔트리를 만들 때 부터 트리 구조를 따를 수 있도록 해보자.
목표는 폴더를 탐색하면 폴더 내부에 있는 파일들을 우선적으로 출력하고, 하위 폴더를 탐색하도록.
여기서 파일들을 우선적으로 출력할 때, 폴더도 같이 출력할 필요는 없어보인다.

아무리 생각해도 계속 시간복잡도가 우주를 초월할거같은데.
일단은 침착하고.
원형큐가 작동하는 방식으로 만들어보자.

PM 04:15
다 집어치워!
파일을 담는 List,
폴더를 담는 Stack,
Dictionary에 역순으로 정렬해서 담기위해 사용한 Stack 하나 더 써서,
깔끔하게 정렬됐다. 시간복잡도는 O(n^2) 정도 되는 것 같다. 그리 효율적이진 않아보임 군더더기 반복문이 많아서.
일단 폴더, 파일 각각 자료구조에 담고,
폴더를 하나씩 pop 하면서 FullName에 이 폴더의 FullName을 포함하는 파일들을 찾아서 Entries에 담는다.
실은 Entries에 바로 담진 않았고 앞서 말했듯 Stack 하나 더 만들어서 거기다가 넣었다.
처음엔 폴더에 스택 대신 큐를 썼는데 원하는대로 안됨.
예를 들어
Audacity/TEMP/temp.exe
는
Audacity/TEMP/
가 DQ됐을때 나와야하는데
Audacity/
가 DQ됐을때 나오게 되니까.

어쨌든 GUI부분 구현은 다음주로 넘어가야겠다. 다음주에도 골치아플 예정.
***************************************************/

/***************************************************
* 날짜 : 2017.11.20
* 목표 : DeZipperGUI 구현
 ******* 코멘트 *******
흠..................................................
아무리 생각해도 DeZipperForm에서 DeZipper를 호출해야될것같은데. 답안나오네.

일단 DeZipperGUI를 구현하는쪽으로 도전이라도 해보자.

DeZipperGUI클래스의 인스턴스는 DeZipperForm이 로드됐을때 생성된다.
DeZipperGUI가 가지고있는 DeZipper 클래스 인스턴스는 Zip path가 입력됐을때 생성되야한다.

ProgressBar를 표시하는 새 Form을 만들었다. 이걸 어떻게 다룰지는 앞으로 생각해봐야할 문제.

DeZipperForm의 컴포넌트를 프로퍼티로 DeZipperGUI에게 넘기는게 옳은것일까. 흠...

우선은 PrintList()를 구현 중.
문제는 Entries를 어떻게 TreeView에 그리느냐인데... 어려움.
대충 만들어보긴 했는데 확인할 길이 당장에 없다.

간단하게 이벤트 등록하고 해서 테스트해본 결과.
폴더가 그냥 계속 하위폴더로 들어감. 역시 너무 대충만들었어.
그래도 일단 그려진다는것이 중요하다.
아 그리고 폴더명이 fullname으로 저장되는것도 문제.

또 알고리즘 짤려니까 머리 터지겠음.
애초에 ZipFile 계열 클래스가 엔트리를 트리구조로 뽑아줬으면 편했을건데.

오늘은 이쯤하고 내일 하루종일 알고리즘 생각해보자.
***************************************************/

/***************************************************
* 날짜 : 2017.11.21
* 목표 : Entries to TreeView
 ******* 코멘트 *******
오늘은 간단하게.
의욕이 안난다... 알고리즘 짜기 어렵다.

아 몇 시간을 붙잡았는데 아이디어가 딱히 안떠오름.

으아아아아아 인간승리!

Find(path, true)[0].Node.Add()

이 코드 한줄로 해결한것같다.
path를 통해서 해당 파일의 상대경로를 찾음.
DeZipper에서 미리 정렬을 어느정도 해놨기 때문에
항상 엔트리에는 상위 폴더가 들어가있다.

솔직히 효율 하나도 신경안씀. 일단 깔끔한게 중요하다고 생각했기 때문에.
코드도 개판 이건 지금 바로 깔끔하게 수정해야겠다.

오늘 구현한 방법이 마음에 드는게 나중에 DeList 구현할때도 편할거같다.
폴더들도 Key가 남아있게 돼서.
***************************************************/

/***************************************************
* 날짜 : 2017.11.22
* 목표 : DeZipperGUI.Delist()
 ******* 코멘트 *******
일단 Delete 자체는 DeZipper 클래스의 필드 entries를 통해 수행된다.
Delist는 TreeView 컴포넌트에서 선택한 아이템을 Entries에서 찾아서 지워야한다.
그냥 파일 하나를 선택해서 제외한 경우는 뭐 쉽겠는데,
폴더를 선택한 경우엔 하위 폴더, 파일까지 싹 날려줘야한다.

근데 Remove 메소드 쓰면 알아서 날려주지 않을까?
다만 문제는 Entries에서도 직접 지워야하는데...

Entries에서 path로 전달받은 스트링을 포함하는 모든 엔트리들을 지우면 되지 않을까
예를들어
audacity/language/ca/
를 전달받으면 이 텍스트를 경로에 포함하는 모든 아이템을 지우기.

아 EntryTree.Nodes.RemoveByKey 해서 지우려니까
최상위 노드의 파일/폴더만 지울 수 있다.
Find를 해야할듯.
어쨌든 깔끔하게 지웠다.

TreeView가 다중선택을 지원하지 않는다. 의외다.

오늘은 이것 저것 구현함.
너무 중구난방으로 해서 조금 헷갈리긴 함.
계획은
Delete 기능은 내일 구현.
예외처리는 금요일.
이번주에 실제 사용 가능한 수준까지 만들도록 하자.
***************************************************/

/***************************************************
* 날짜 : 2017.11.23
* 목표 : DeZipperGUI.Delete()
 ******* 코멘트 *******
DeZipperProgressForm을 호출해야한다.
ShowDialog() 메소드는 부모 창을 접근할 수 없게 만든다.

흠... CMD에서 만든 코드 참고해서 일사천리로 진행했는데,
삭제가 안되네?

Delete 버튼 눌렀을때 타겟 디렉토리를 안정해줬었다.
그건 그거고 수정했는데도 삭제가 안되네?

아 좀 프로그램이 꼬여가지고 헷갈림.
DeZipperForm에서 GUI의 TargetDirectory 프로퍼티를 바꿨고
그걸로 다시 GUI에서 DeZipper의 TargetDirectory를 바꿔야한다.

이번엔 삭제는 됐는데 progressForm이 전혀 작동하지 않았다.

아 ShowDialog() 때문인가.

Show로 바꿨는데 이번엔 프로그래스바 작동이 이상하다.
Maximum이 100을 넘으면 안되는건가?
디버거 돌려본 결과 Value가 Maximum까지 도달하지 못함.
너무 빨라서 그런가?
아아아아아아아아아아아아아아아아아아아아아아아아아아아아
빈 폴더 삭제 옵션을 안켜놨는데
폴더 수 까지 보내놨으니까 맥스값이 잘못된거.

이제 삭제 후 메인 폼을 초기화하면 되겠다.
흠... 트리뷰를 지웠는데 생각보다 별로인데. 그냥 놔둬야겠음.

DirectoryNotFoundException을 어떻게 처리해야할까.
foreach에서 예외를 계속 받을수도 없고.
예외로 보내지 말고 Directory.Exists() 메소드를 써보자.

Delete 전에 경고 메세지를 띄우는게 어떨까?
***************************************************/

/***************************************************
* 날짜 : 2017.11.24
* 목표 : GUI 예외처리, 사소한 수정
             ******* 코멘트 *******
App.config에서 디폴트 타겟 디렉토리를 설정하자.

예외가 DeZipper에서 throw 되면 GUI에서 throw해서 Form이 받아서 메세지를 출력하는걸로.
몹시 귀찮음.

예외처리는 안하고 계속 딴짓한다.
버튼 활성화 비활성화랑... 드래그앤 드롭이랑... App.config도 했고...
히히히... TreeView에 노드 추가되면 자동으로 스크롤 내려가는것도 바꿔야지...

예외처리 빨리 끝내고 동노가자.
대충하자 대충.
***************************************************/

/***************************************************
* 날짜 : 2017.11.27
* 목표 : 이번 주의 할 일은 무엇인가.
         ******* 코멘트 *******
프로젝트가 마무리단계에 도달함과 동시에 의욕 저하가 발생.
오늘은 이번 주 어떻게 프로젝트를 마무리 할 지 생각을 해보자.

우선 아이콘 만들기. 포토샵으로 대충 만들어보자.
다음은 readme.txt 만들기. 노잼... 귀찮음...
그리고 릴리즈에 대해서. 어떻게 DeZipper.exe와 DeZipper-GUI.exe를 같이 놔둘지 고민.
***************************************************/

/***************************************************
* 날짜 : 2017.11.28
* 목표 : readme.txt
         ******* 코멘트 *******
readme.txt를 만들자.
사실 readme.md 라고 마크다운식으로 된거 좋은거 있긴 한데.
뭐 굳이 그렇게까지 만들 그럴 필요 있나.

흠.......... 일단은 Lazzzy에서 만들었던 readme 형식을 따라서 만들자. md는 뭐... 귀찮음...
readme.txt에선 CLI 버전의 사용법만 적는게 좋겠다.
GUI는 애초에 직관적인 UI니까 텍스트로 사용법을 적는게 좀 뭣허다.
***************************************************/

/***************************************************
* 날짜 : 2017.11.29
* 목표 : 리소스 제작
     ******* 코멘트 *******
아이콘을 만들자.
근데 호기롭게 한번 만들어보자! 나서긴 했는데
이건 진짜 생각이 안난다. 어떻게 만들어야할지.
일단 포토샵 켜서 생각해보자.

참고자료 http://starclusters.tistory.com/66

아이콘 뭐 큰 의미는 없고. 그냥 색깔이 이쁘게 만듬.

내일은 DeZipper 0.1 버전 배포를 하도록 한다.
지금 문제는 그냥 bin 폴더의 release를 복사해서 다른 경로에서 프로그램을 실행해보니
App.config를 읽지 못함. 제대로된 배포 방식을 찾아서 이런 문제를 해결하도록 하자.
***************************************************/

/***************************************************
* 날짜 : 2017.11.30
* 목표 : 오류 해결, 0.1 릴리즈
     ******* 코멘트 *******
어제 발견한 오류는 2개.
App.config 인식 문제랑, 파일 갯수를 나타내는 label이 새 zip파일을 열었을때 초기화되지 않는문제.

label 초기화 문제는 DeZipperGUI.PrintList() 메소드에 cFiles, cDirs 필드를 초기화하는 코드를 넣음으로 해결.
App.config는 정식으로 배포하면 해결되지 않을까 생각중.

흠... 일단 release 폴더로 솔루션을 옮기고싶은데, 흠... 의미가 있나 모르겠네.

참고자료 http://eyshin05.tistory.com/entry/Making-Csharp-Portable-Exe-File

release 폴더로 대충 솔루션을 복사했고, 포터블 버전을 만들었다.
내 github repo에 들어갔을때 어떻게 나타나게 할지가 걱정인데... 일단은 그냥 소스를 올려두는걸로.
app.config가 여전히 인식이 안되는건 차차 알아낼 문제.
***************************************************/