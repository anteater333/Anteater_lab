﻿/**************************************************
         * DeZipper. Command Line Version *
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