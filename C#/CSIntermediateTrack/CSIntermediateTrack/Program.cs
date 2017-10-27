/**************************************************
                * C# 중급과정 *
***************************************************/

/***************************************************
* 날짜 : 2017.10.16
* 목표 : 중급과정 시작. Indexer, 접근 제한자, 상속.
             ******* 코멘트 *******
참고자료 http://www.csharpstudy.com/
중급과정. 기존 언어에서 배웠던 부분도 물론 있겠지만, C#만의 기능이라던가, OOP의 중요 개념이라던가.
Console에다가 출력하는것보단, 실행해서 아무것도 보이는게 없다하더라도 개념적으로 제대로 구현해보도록 하자.
편할진 몰라도 꼭 눈으로 봐야만 이해하는건 아니잖아? 괜히 오버헤드가 될 것 같기도 하고.
괜히 switch문 만들진 않도록 하자.

Indexer
클래스 객체의 데이터를 배열 형태로 인덱스를 써서 엑세스 할 수 있게 해줌.
백문이 불여일견
MyClass cls = new MyClass();
cls[0] = "First";
즉, 인덱스로 클래스 내의 특정 필드 데이터에 접근하는것.
정의법은 Property로써 this[] 정의하는것. 즉, this[] 에다가 getter, setter를 정의하면 된다.
public int this[int index]
{
    ...
}
string같은 클래스도 이걸로 만든게 아닐까.

접근 제한자 (Access Modifier)
계속해서 써왔던 바로 그것. 클래스 멤버들로 접근을 제한할 때 사용하는 바로 그것.
**public
모든 외부 혹은 파생 클래스에서 접근가능.
**internal
동일한 Assembly 내에 있는 다른 클래스들이 접근 가능.
**protected
파생 클래스에서 접근가능.
**private
동일 클래스 내의 멤버만 접근 가능.
internal이 애매하지만 필기순서대로 제한이 강해진다고 생각하면 될듯함.

상속 (Inheritance)
개념적으론 적당히 알고 있다고 자부하지만, 중요한 개념이니까 복습한다는 느낌으로.
C# 상속 문법도 배우고...
상속이란, 부모 클래스(Base Class)에서 새로운 자식 클래스(Derived Class)를 만드는것.
자식 클래스는 부모 클래스의 데이터와 메서드를 물려받고, 부모 클래스의 protected 멤버에 접근이 가능하게 된다.
C#에서 상속을 하려면 Colon(:) 기호를 사용하면 된다.
주의점! 자식 클래스는 2개 이상의 부모 클래스를 가질 수 없다.
namespace MyWorld를 참고하자.

추상 클래스
abstract 키워드. 객체를 직접 생성할 수 없는 클래스.
class 선언 시 class 키워드 앞에 abstract 키워드를 붙이고, abstract 멤버를 가지고 있다.
abstract 멤버는 자식 클래스에서 반드시 구현해 주어야 한다.
abstract 멤버를 자식 클래스에서 구현할땐 override 키워드를 사용한다.

연산자 as, is
as 연산자는 객체를 지정된 클래스 타입으로 변환하는데 사용된다.
쉽게말해 캐스팅. 변환에 실패하면 null을 리턴한다. 재미있는 특성.
참고로 기존에 내가 알던 캐스팅 연산자 (TYPE)는 변환이 실패하면 Exception을 발생시킨다. Catch 안하면 프로그램 중지.
is 연산자는 객체가 특정 클래스 타입이나 인터페이스를 갖고 있는지 확인하는데 사용된다. 그러니까 bool 타입을 반환.
***************************************************/

/***************************************************
* 날짜 : 2017.10.17
* 목표 : static, generics, interface
             ******* 코멘트 *******
static
정적 멤버를 만들 때 사용하는 키워드.
말인 즉슨, 클래스의 인스턴스를 생성하지 않고 사용하는 클래스 멤버.
MyClass.method1(); 이런 식으로 호출하는거. 대표적인 정적 메소드가 바로 Main().
! 정적 메소드 내부에서 클래스 인스턴스 객체 멤버를 참조해서는 안된다.
당연한 이야기. 내부에서 this.(non-static member) 이렇게 호출하면 에러가 뜨겠지.
static 메소드 뿐만 아니라 static 프로퍼티와 필드도 있을 수 있다.
non-static 필드는 인스턴스가 생성될때 메모리에 할당되지만,
static 필드는 프로그램 로딩시 단 한 번 클래스 내에 생성되고 동일 메모리를 계속 사용한다.
MyWorld의 클래스에 정적 메소드를 한번 만들어보자.
참고로 추상 클래스가 정적 메소드를 가져도 문제 없다. 인스턴스가 없어도 사용가능한 메소드기 때문.
근데 그 추상 클래스의 정적 메소드 내부에서 추상클래스 인스턴스를 만드려하면... 당연히 안되지...

static class
모든 클래스 멤버가 static 멤버인 클래스로, 다음과 같이 선언하고 정의한다.
public static class MyClass
{
    ...
    static MyClass()
    {
        ...
    }
    ...
}
static 클래스는 public 생성자를 가질 수 없다. 당연히 인스턴스를 생성할 수 없으니까.
하지만 static 생성자를 가질 수는 있다. static 생성자는 보통 static 필드들을 초기화 할 때 사용된다.

Generics
C++에서 배운 템플릿을 기억해보자.
제네릭은 클래스가 Type Parameter를 가질 수 있게 만들어준다. 이때까지 많이 봐 왔던 <T>가 바로 그것.
클래스 멤버들의 타입을 T로 지정해놓고, Runtime에서 T를 결정하도록 하는 기능.
사칙 연산에 관련된 클래스가 있다고 가정해보자.
이 클래스가 데이터 타입에 대해 유연성을 가지려면, 클래스의 필드와 파라미터가 특정 타입을 가지면 안된다.
예를들어 int로 정해놓으면 double에 대한 사칙 연산을 할 수 없어지니까.
그래서 제네릭으로 Type을 매개변수처럼 받아내는것.

Generics 타입 제약
T에다가 덮어놓고 막 넣다보면 필시 에러를 마주하게 될 것이다. 아니면 몹시 귀찮아 지던가.
따라서 [where T : 제약조건] 키워드로 타입을 제한하는 방법이 있다.
ex.
class MyClass<T> where T : struct       // Value 타입
class MyClass<T> where T : class        // Reference 타입
class MyClass<T> where T : new()        // 디폴트 생성자를 가져야 함
class MyClass<T> where T : MyBase       // MyBase의 파생 클래스
class MyClass<T> where T : IComparable  // ICompareble 인터페이스를 가져야함
참고로 쉼표로 구분해서 여러 제약을 걸 수도 있따.

.NET Generic 클래스
제네릭은 직접 구현하는데 쓰기도 하겠지만, .NET의 자료구조 클래스에 쓰인다는게 중요하지 않을까.
자료구조 클래스는 프로젝트 만들 때 자동으로 딸려온 System.Collections.Generic 네임스페이스에 있다. 
List<T>, Dictionary<T>, LinkedList<T> 같은것들. 유용하게 쓰도록 하자.
참고. Dictionary는 전에 파이썬 배울때 잠깐 봤는데, 일종의 해시값으로 접근하는 자료구조.
Dictionary<string, int> dic = new Dictionary<string, int>();
dic["길동"] = 100;
dic["태백"] = 90;

interface
인터페이스는 클래스처럼 메서드, 속성, 이벤트 등을 갖는다.
하지만 이를 직접 구현하지는 않고 prototype만 가진다. 그냥 선언만 해놓는다는것.
참고로 추상 멤버로만 구성된 추상 클래스는 Abstract base class라고 한다.
이것과 개념적으로 유사한것이 인터페이스.
중요한 점. 클래스는 오직 한 부모 클래스로부터만 상속받을 수 있지만, 여러 인터페이스를 가질 수 있다.
public class Dog : Animal, IBarkable, IRunnable
{
    ...
}
**인터페이스 정의
인터페이스를 정의할땐 interface 키워드를 사용해서 정의한다.
그리고 인터페이스 정의 시 내부 멤버들은 접근 제한자를 사용하지 않는다.
public interface IBarkable
{
    void Bark(int number);
}
**인터페이스 구현
일단 클래스가 인터페이스를 가지게 되면, 클래스는 인터페이스의 모든 멤버를 구현해야한다.
    ...
    public void Bark(int number)
    {
        ...
    }
    ...

인터페이스의 유용함을 알려주는 사례
public IDbConnection GetDbConnection()  // return이 인터페이스?
{
    IDbConnection dbConn = null;
    string cn = ConfigurationManager.AppSettings["Connection"];
    switch (ConfigurationManager.AppSettings["DbType"])
    {
        case "SQLServer":
            dbConn = new SqlConnection(cn);
            break;
        case "Oracle":
            dbConn = new OracleConnection(cn);
            break;
        case "OleDB":
            dbConn = new OleDbConnection(cn);
            break;
    }
    return dbConn;  // return은 IDbConection 인터페이스를 가지는 클래스!
}

public void Run()
{
    IDbConnection dbCon = GetDbConnection(); // DB 종류에 상관없이 인터페이스로 가져옴.
    dbCon.Open();
    if (dbCon.State == ConnectionState.Open)
    {
        dbCon.Close();
    }
}
***************************************************/

/***************************************************
* 날짜 : 2017.10.18
* 목표 : delegate
             ******* 코멘트 *******
참고 사이트를 보면 delegate 하나만 4 페이지에 걸쳐서 설명함.

delegate(v. 위임하다)가 뭔가?
메서드를 다른 메서드의 파라미터로써 전달하는것.
void Run(MyDelegate method1) { ... }
MyDelegate는 deleate type으로, 어떤 메서드 method1를 Run() 메서드에 전달하는 것.

delegate 정의법
delegate 키워드를 통해 정의한다.
delegate int MyDelegate(string s);
! 만약 어떤 메서드가 델리게이트 메서드 원형과 일치하다면,
! 즉, 입력 파라미터 타입 및 갯수, 리턴 타입이 동일하다면
! 그 메서드는 해당 델리게이트에서 사용될 수 있다.
! 따라서 델리게이트 정의에서입력 파라미터들과 리턴 타입이 중요하다!
보이기엔 메서드를 그냥 선언하는 것 처럼 보이지만, 내부적으로 이 선언식은 컴파일러에 의해 클래스로 변환된다.

delegate 사용
int StringToInt(string s) { ... }
...
MyDelegate m = new MyDelegate(StringToInt);
Run(m);
클래스를 다루듯이 생성하고, 생성된 객체를 메서드 호출 파라미터에 넣으면 된다.
(풀어서 해석하자면 메서드 정보를 갖는 Wrapper 클래스의 객체를 파라미터로 전달하는것.)

전달받은 메서드 호출법
앞서 인스턴스 변수 m으로 메서드를 받았는데, 이를 실행해야한다.
1) m.Invoke() 메서드를 호출한다.
2) m을 메서드처럼 사용한다. m();
메서드의 매개변수는 Invoke를 쓰던, m을 그대로 쓰던 뒤에 있는 괄호에다가 넣으면 된다.

delegate 개념
정리하자면, delegate는 함수 포인터와 비슷한 개념이다.
동일한 파라미터와 리턴 타입을 가진 메서드를 서로 호환해서 불러 쓸 수 있는 기능이다.
따라서 delegate 클래스타입 변수엔 함수의 레퍼런스를 대입할 수 있다.
리턴 타입과 파라미터가 서로 같은 두 함수 RunThis와 RunThat이 있다고 가정하면,
MyDelegate run = new RunDelegate(RunThis);  // 이렇게 인스턴스를 생성했다가.
run = RunThat;                              // 이렇게 대입할 수 있다.

delegate 응용. 메서드 파라미터로 전달.
Sort 함수에 매개변수로 CompareDelegate 변수를 넣는다고 하자.
public static void Sort(int[] arr, CompareDelegate comp)
{
    ...
}
여기서 comp에 넣는 함수에 따라 오름차순, 내림차순을 정할 수 있다.
int AscendingCompare(int i1, int i2) { (오름차순 비교) }
int DecendingCompare(int i1, int i2) { (내림차순 비교) }

delegate 필드/프로퍼티
delegate는 여러모로 복잡해 보이지만 결국엔 내부적으론 클래스이고, Ref type 변수로 쓰일 수 있다.
따라서 당연히 어떤 클래스의 필드나 프로퍼티로써 사용될 수도 있다.
클래스 내부 필드로써 Delegate 변수를 놔두고 내부 동작에 따라 할당된 메서드를 바꾼다던가...

Multicast Delegate
delegate에 메서드를 여러개 할당하고 싶다면 += 연산자를 쓰면 된다.
내부적으로 .NET MulticastDelegate 클래스에서 메서드들을 리스트로 관리하게 된다. (InvocationList)
그리고 delegate가 실행될 때 리스트에서 메서드를 하나씩 가져와 실행한다.

delegate 단점
delegate는 문제를 야기할 수 있다.
! 첫 번째로, delegate 필드에 추가 연산(+=)을 하지 않고 할당 연산(=)을 할 경우,
할당 연산은 기존에 가입된 모든 메서드 리스트를 지워버리고 마지막으로 할당된 메서드 1개만 리스트에 넣게 한다.
즉, 누구든 할당 연사자를 한번 사용하면 기존에 가입받은 모든 메서드 호출요구를 삭제하는 문제가 발생한다.
! 두 번째로, delegate는 해당 클래스 내부 뿐 아니라 외부에서도 누구라도 메서드를 호출하듯 호출할 수 있다는 점이다.
예를 들어 area 클래스가 delegate 필드나 프로퍼티를 가지고 있다고 가정했을 때,
Main에서 area 객체를 만들고
area.MyDelegate(null);
이런 식으로 호출할 수 있게 된다.

Delegate와 Event
event는 delegate의 문제점을 해결한 특별한 형태의 delegate이다.
event는 할당 연산자를 사용할 수 없고, +=/-=을 통해 이벤트 추가/삭제만 가능하다.
또한 해당 클래스 외부에서는 직접 호출할 수 없다.
이벤트 선언 예
public delegate void ClickEvent(object sender);
public event ClickEvent MyClick;
void MyAreaClicked()
{
    if (MyClick != null)
    {
        MyClick(this);
    }
}

Delegate VS 함수포인터
참고용. 몰라도 프로그래밍은 할 수 있을듯.
1)  C에서는 클래스 개념이 없다. 따라서 함수 포인터는 말 그대로 함수에 대한 주소값만을 가진다.
    delegate는 객체의 인스턴스 메서드에 대한 레퍼런스를 갖기 위해 그 객체의 주소와 메서드 주소를 함께 가지고 있다.
    C++에서는 Pointer to member function, 한 클래스의 멤버 함수에 대한 포인터가 있는데, delegate와 비슷하다.
    단 Pointer to member는 함수 포인터 선언 시 특정 클래스를 지정해주기 때문에 한 클래스에 대해서만 사용할 수 있다.
2)  함수 포인터는 하나의 함수 포인터를 갖는데, delegate는 하나 이상의 메서드 레퍼런스들을 가질 수 있다. Multicast가 가능.
3)  함수 포인터는 type safety를 보장하지 않는다. delegate는 type safety를 엄격하게 보장한다.
Type-Safe?
참고자료 http://egloos.zum.com/jerrypop/v/3409768
어떠한 연산도 정의되자 않은 결과를 내놓지 않는것. 예측불가능한 결과를 내지 않는 것.
1 + 1 = 2. 이는 쉽게 알 수 있지만,
1 + "1" = ???
Type safe한 언어에서는 이를 에러처리 하고
그렇지 않은 언어(ex. Javascript)는 이것을 계산한다. "11" 이라던지.
C# / java는 일반적으로 type safe.
***************************************************/

/***************************************************
* 날짜 : 2017.10.19
* 목표 : 무명 메서드, 람다식, 익명 타입, 확장 메서드
             ******* 코멘트 *******
무명 메서드
C# 2.0에서부터 지원된 기능. 어제 배운 delegate는 모두 이미 정의된 메서드를 가리키고 있었다.
어떤 메서드가 일회용으로 단순한 문장들로만 구성되어 있다면 굳이 별도의 메서드를 정의하지 않아도 된다.
이것이 바로 무명 메서드.
delegate(Params) { Executions };
아래 예를 보면 더 이해가 쉬울것이다.
this.button2.Click += new System.EventHandler(this.button1_Click); // 메서드명 지정. 기존 방식
this.button2.Click += delegate(object s, EventArgs e)
{
    MessageBox.Show("버튼2 클릭");
};  // 무명 메서드

! delegate 키워드는 Delegate 타입을 정의할 때도 사용되고, 무명 메서드를 정의할 때도 사용된다.
Delegate 타입을 사용해야 하는 곳에 무명메서드를 집어넣는경우 컴파일 에러가 발생할 수 있다.
예를들어, Invoke()나 BeginInvoke() 메서드는 Delegate 타입을 파라미터로 받아들인다.
this.Invoke(delegate {Button1.Text = s;} ); // 컴파일 에러 발생. 참고로 무명메서드에 파라미터가 없으면 생략 가능.

MethodInvoker mi = new MethodInvoker(delegate() { button1.Text = s; });
this.Invoke(mi);    // 맞는 표현

this.Invoke((MethodInvoker) delegate { button1.Text = s; });    // 축약된 표현

람다식. =>
람다식은 무명 메서드와 비슷하게 무명 함수를 표현하는데 사용된다.
람다식은 아래와 같이 입력 파라미터(0~N개)를 => 연산자 왼쪽에, 실행 문장들을 오른쪽에 둔다.
람다 Syntax : (입력 파라미터) => { 문장블럭 };
문자열을 받아 메세지 박스를 띄우는 람다식 예.
str => { MessageBox.Show(str); }
람다식 파라미터의 입력 타입은 일반적으로 컴파일러가 알아서 찾아내지만, 애매한 경우 명시할 수 있다.
(string s, int i) => Write(s, i);

delegate의 축약 과정
===================================================
다음과 같은 delegate 사용 코드가 있다.

this.button1.Click += new System.EventHandler(button1_Click);
private void button1_Click(object sender, EventArgs e)
{
    ((Button)sender).BackColor = Color.Red;
}

여기서 new System.EventHandler(button1_Click)은 간단히 button1_Click 메서드명만 사용하여 줄일 수 있다.

this.button1.Click += button1_Click;
private void button1_Click(object sender, EventArgs e)
{
    ((Button)sender).BackColor = Color.Red;
}

여기에 무명 메서드를 사용해 더 축약한다.

this.button.Click += delegate(object sender, EventArgs e)
{
    ((Button)sender).BackColor = Color.Red;
};

그리고 람다식을 사용해 코드를 한줄로 줄일 수 있다.

this.button1.Click += (sender, e) => ((Button)sender).BackColor = Color.Red;    // 실행 블럭이 한 문장일땐 {} 생략 가능

===================================================

(참고) 람다식이 대체 뭐야?
람다식 람다식 말만 많이 들어봤고 솔직히 우리학교 프로그래밍 언어론 수업이 몹시... 안좋았기 때문에
그냥 개념에 대해 하나도 모르겠다.
람다식이 그냥 delegate를 더 효율적으로 만든다는건 결과론적인 이야기 같고.
소위 말하는 트렌디한 언어들이 어떤 근거로 람다식을 채용하는건지 알 필요가 있지 않을까.
그래서 준비했다
자바에 왜 람다식이 필요한가?
Reference https://dzone.com/articles/why-we-need-lambda-expressions
기존 for문의 문제점을 알려주는 일화를 알아보자
나 : "Sofia, 이 장난감들을 치우자꾸나. 지금 바닥에 어떤 장난감들이 있지?"
Sofia : "네. 공이 있어요."
나 : "좋아. 그 공을 상자에 넣자. 다른 장난감이 남아 있니?"
Sofia : "네. 인형이 있어요."
나 : "좋아. 그 인형을 상자에 넣자. 다른게 더 남아있니?"
Sofia : "네. 책이 있어요."
나 : "좋아. 그 책을 상자에 넣자. 다른게 더 남아있니?"
Sofia : "아니요. 다 넣었어요."
나 : "그래, 다했구나."
기존 반복문의 사이클을 되짚어보면, 루프를 한번 실행하고 조건을 확인하고... 이 과정을 반복한다.
즉, 각각의 요소들을 하나하나 일일이 검증하며 순차적으로 값을 확인하여 조건절이 끝날 때 까지 진행된다.
이런 작동보다 더 효율적인건 그냥
"바닥에 있는 장난감들을 모두 치우렴"
이라고 한 번 말하는거다.
그래서 반복문과 람다식이 무슨 관련인가?
람다식은 익명 함수를 지칭하는 용어이다. 따라서 앞서 제기한 반복문의 문제점을 해결해 다음과 같은 코드
for (int i = 0; i < 10; i++)
{
    System.Console.Write(i);
}
를
Enumerable.Range(0, 10).ToList().ForEach((int i) => System.Console.Write(i));
이렇게 바꿀 수 있는거지.
0에서 9까지 하나씩 출력하렴에서,
0에서 9까지 있는 리스트를 그냥 싹 다 출력하렴으로.
코드가 간략해지고 따라서 이해도 쉽고.

익명 타입
어떤 클래스를 사용하기 위해서는 일반적으로 먼저 클래스를 정의한 후 사용한다. 당연한 이야기.
익명타입이란 클래스를 미리 정의하지 않고 사용할 수 있는 타입이다.
var t = new { Name="홍길동", Age=20 };
var 키워드는 컴파일러가 타입을 추론해서 찾아내도록 할 때 사용된다.
익명 타입 객체를 변수에 할당할 때는 특별히 타입명이 없으므로 var를 사용한다.
주의점은, 익명 타입은 읽기 전용이므로 속성값을 갱신할 수 없다는것.
익명 타입은 어디에 쓰나?
익명 타입은 간단히 말해 임시 타입이다. 다음과 같은 예제를 보자.
var v = new[] {
    new { Name="Lee", Age=33 },
    new { Name="Kim", Age=25 },
    new { Name="Park", Age=37 }
};
이렇게 만들어낸 객체는 LINQ(Language Intergrated Query)로 DB에 보내는데 사용한다던가.
어쨌든 여러 방면으로 쓸 수 있겠지 뭐.

확장 메서드
확장 메서드는 특수한 종류의 static 메서드로서 마치 다른 클래스의 인스턴스 메서드인 것처럼 사용된다.
확장 메서드는 메서드가 사용될 클래스명을 첫번째 파라미터로 지정한다.
마치 해당 클래스가 확장메서드를 인스턴스 메서드로 갖는것과 같은 효과를 낸다.
이해가 잘 안가지만 백문이 불여일견
정의
        public static bool Found(this String str, char ch)
        {
            int position = str.IndexOf(ch);
            return position >= 0;
        }
사용
string s = "This is a Test";
bool found = s.Found('z');
밑에 코드를 보면 Found() 메서드는 ExClass 클래스의 static 멤버이다.
그런데 ExClass.Found() 가 아니라 s.Found() 로 호출된다.

***************************************************/

/***************************************************
* 날짜 : 2017.10.20
* 목표 : partial, dynamic, async await. 중급과정 끝.
             ******* 코멘트 *******
Partial Type
파일 여러 개에 걸쳐 정의할 수 있는 타입(클래스, 구조체, 인터페이스).
Code Generator가 만든 코드와 사용자가 만드는 코드를 분리하기 위해 만들어졌다.
예를 들면, 윈폼에서는 Form UI 디자인과 관련된 Form1.designer.cs 파일과 사용자가 쓰는 Form1.cs 파일에
동일한 클래스명을 두고 이를 partial로 선언한다. 그리고 나중에 컴파일러가 이를 합쳐 하나의 클래스로 만든다.
정의방법은 간단함.
partial class Class1
{
    public void Run() { }
}
partial class Class1
{
    public void Get() { }
}

Partial Method
C# 3.0에서 추가된 기능. partial 기능을 타입이 아닌 메서드에 적용.
! Partial Method는 반드시 private 메서드여야 하고, 리턴값이 void, 즉 없어야 한다.
사용법은 마치 C에서 헤더 파일을 만들듯이 한 파일에서 메서드 몸체를 생략하고 선언부만 적은 다음,
구현 파일에서 실제 메서드를 구현하는것.
// file 1
    ...
    partial void DoThis();
    ...
// file 2
    partial void DoThis()
    {
        Log(DateTime.Now);
    }
만약 코드 상에 메서드 구현 부분이 생략되고 선언부만 있게 된다면,
C# 컴파일러가 메서드 전체를 생략해버린다.
즉, 특정 메서드가 다른 파일에서 구현되었는가의 여부에 따라 메서드 전체를 생략할 수 있는 기능.

Static Language vs Dynamic Language
컴파일시 Type Checking을 진행하는 경우 Static Language.
런타임시 Type을 판별하는 경우 Dynamic Language.
C#은 Static Language이지만, C# 4.0에서 추가된 dynamic 키워드와,
.NET Framework 4.0에 추가된 DLR(Dynamic Language Runtime)으로 Dynamic Language가 갖는 기능을 갖추고 있다.
즉, 프레임워크가 DLR을 사용함으로써 다른 Dynamic Language를 함께 사용하는것이 가능해졌다.
Ruby-Python 과 C#을 같이 사용할 수 있게 됐다는 말.

dynamic 키워드
컴파일러에게 변수의 Type을 체크하지 않도록 하고 런타임시 까지 해당 타입을 알 수 없음을 표시하는 키워드.
내부적으로 dynamic 타입은 object 타입을 사용한다. 따라서 dynamic 변수는 중간에 다른 타입의 값을 가질 수 있다.
물론 object와 다른 점도 있음.
object o = 10;
o = o + 20; // Error!
o = (int)o + 20;
object는 casting이 필요하지만 dynamic은 casting이 필요없다.
dynamic d = 10;
d = d + 20;
이렇게 해도 된다는 말.
코드를 작성하면서 봤듯이 dynamic은 런타임에 와서야 타입을 알 수 있기 때문에
다이나믹 변수 하고 .을 타이핑 해도 비주얼스튜디오가 메서드들을 불러올 수 없다.

익명타입과 dynamic
dynamic은 컴파일러에게는 그냥 정적 Type중 하나로 인식된다. 따라서 다음과 같이 파라미터로써 지정할 수도 있다.
    public void Run(dynamic o)
    {
        Console.WriteLine(o.Name);
        Console.WriteLine(o.Age);
    }
그리고 dynamic t = new { Name = "Kim", Age = 25 };
이렇게 변수 하나 만들어서 인자로 전달이 가능하다.
문제는 익명타입 자체가 한번 생성 후 새로운 속성을 갖지 못하고,
메서드 이벤트 등은 아예 가질 수 없기 때문에 이러한 멤버를 동적으로 할당해서 dynamic 타입에서 추가할 수 없다는것.
또한 메서드 정의가 있는 클래스와 메서드를 호출한 클래스가 서로 다른 어셈블리에 놓인다면
object does not contain a definition for Name 에러를 발생시킨다.
dynamic 타입이 믹명 타입인데, 다른 어셈블리에서는 이 익명타입을 볼 수없기 때문.

ExpandoObject
이러한 문제점을 해결하기 위해 DLR 네임스페이스 System,Dynamic에 ExpandoObject 클래스가 있다.
ExpandoObject 클래스는 개발자가 dynamic 타입에속성, 메서드, 이벤트를 동적으로 쉽게 할당할 수 있게 도와주는 클래스이다.
아래 코드에 예제를 직접 작성해보자. (MyDyClass)
ExpandoObject 클래스는 동적으로 추가되는 멤버들을 내부 해시테이블에 저장하고 있다.
필요한 경우 이 정보를 IDictionary<String, Object> 인터페이스를 통해 쉽게 엑세스할 수 있다.
말인즉슨 ExpandoObject 클래스 자체가 IDictionary<String, Object> 인터페이스를 구현하고 있고,
클래스 캑체를 IDictionary<String, Object> 인터페이스로 캐스팅해서 내부 멤버 데이터에 엑세스 할 수 있다는것.
    var dict = (IDictionary<string, object>)d;

    foreach (var m in dict)
    {
        Console.WriteLine("{0}: {1}", m.Key, m.Value);
    }

async / await
C# 5.0 부터 추가된 키워드로, 비동기 프로그래밍을 보다 손쉽게 지원하기 위해 추가되었다.
async는 컴파일러에게 해당 메서드가 await을 가지고 있음을 알려주는 역할을 한다.
async라고 표시된 메서드는 await을 1개 이상 가질 수 있는다. 하나도 없을때에도 컴파일이 가능하긴 하지만 warning이 뜸.
await은 보통 Task/Task<T>객체, GetAwaiter()라는 메서드를 갖는 클래스와 함께 사용된다. await은 해당 클래스 객체가 완료되기를 기다리는데,
!! UI 쓰레드가 정지되지 않고 메시지 루프를 계속 돌 수 있도록 필요한 코드를 컴파일러가 await 키워드를 만났을때 자동으로 추가한다.
즉 마우스 클릭이라던가 키보드 입력같은 처리를 계속 할 수 있다는 것.
await은 항상 특별한 Background Thread를 필요로 하지 않는다. (물론 대부분 Background Thread가 있음)
즉, await은 해당 Task가 끝날 때 까지 기다렸다가 완료되면 바로 다음 실행문부터 실행을 이어가는데,
해당 Task가 Worker Thread에서 돌 수도 있고, UI Thread에서 돌 수도 있다는 것.
!! await은 Task가 어떤 스레드에서 돌던지 상관없이 Task 완료 후 await 이후의 실행문들을 디폴트로
!! 원래 await을 실행하기 전의 스레드에서 실행하도록 보장한다.

async, await 부분은 멀티쓰레딩 파트를 배우면 다시 제대로 보자.
***************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using MyWorld;

namespace MyWorld
{
    public interface IBarkable
    {
        void Bark(int number);
    }

    public abstract class Animal
    {
        protected string Name { get; set; }
        protected int Age { get; set; } // getter setter 몹시 간단하게 작성.

        public abstract void Jump();
    }

    public class Dog : Animal, IBarkable
    {
        public Dog(int age, string name)
        {
            this.Age = age;
            this.Name = name;
        }
        public override void Jump()
        {
            Console.WriteLine("{0}({1}세), 뛰다!", this.Name, this.Age);
        }

        public static int Animalize(out object obj, int age, string name)
        {
            obj = new Dog(age, name);
            return 1;
        }

        public void Bark(int number)
        {
            for (int i = 0; i < number; i++)
                Console.Write("멍! ");
            Console.WriteLine();
        }
    }

    public class Bird : Animal
    {
        public Bird(int age, string name)
        {
            this.Age = age;
            this.Name = name;
        }
        public override void Jump()
        {
            Console.WriteLine("{0}({1}세), 날다!", this.Name, this.Age);
        }

        public static int Animalize(out object obj, int age, string name)
        {
            obj = new Bird(age, name);
            return 1;
        }
    }
}
namespace CSIntermediateTrack
{
    class MyClass
    {
        #region Fields
        private const int MAX = 10;
        private string name;

        private int[] data = new int[MAX];  // 정수 배열
        #endregion

        #region Properties
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= MAX)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return data[index];
                }
            }
            set
            {
                if (!(index < 0 || index >= MAX))
                {
                    data[index] = value;
                }
            }
        }
        #endregion
    }

    class MyDyClass
    {
        public void M1()
        {
            // ExpandoObject를 통해 dynamic 타입 생성.
            dynamic person = new ExpandoObject();

            // 속성
            person.Name = "Kim";
            person.Age = 10;

            // 메서드. 람다식을 통한 delegate로 지정.
            person.Display = (Action)(() =>
            {
                Console.WriteLine("{0} {1}", person.Name, person.Age);
            });

            person.ChangeAge = (Action<int>)((age) =>
            {
                person.Age = age;
                if (person.AgeChanged != null)
                {
                    person.AgeChanged(this, EventArgs.Empty);
                }
            });

            // 이벤트 초기화
            person.AgeChanged = null; // dynamic 이벤트는 먼저 null로 초기화 한다고 한다.
            // 이벤트핸들러
            person.AgeChanged += new EventHandler(OnAgeChanged);

            // 타 메서드에 파라미터로 전달
            M2(person);
        }

        private void OnAgeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Age Changed");
        }

        // dynamic 파라미터를 전달받는 메서드
        public void M2(dynamic d)
        {
            // dynamic 타입 메서드 호출
            d.Display();
            d.ChangeAge(20);
            d.Display();
            Console.WriteLine("=======================");
            var dict = (IDictionary<string, object>)d;

            foreach (var m in dict)
            {
                Console.WriteLine("{0}: {1}", m.Key, m.Value);
            }
        }
    }

    public static class ExClass
    {
        public static string ToChangeCase(this String str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                if (ch >= 'A' && ch <= 'Z')
                    sb.Append((char)('a' + ch - 'A'));
                else if (ch >= 'a' && ch <= 'x')
                    sb.Append((char)('A' + ch - 'a'));
                else
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        public static bool Found(this String str, char ch)
        {
            int position = str.IndexOf(ch);
            return position >= 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*MyClass cls = new MyClass();

            cls[9] = 512;

            Console.WriteLine(cls[9]);*/

            /*Bird eagle = new Bird(5, "독수리");
            //eagle.Age = 3; // protected이기에 접근 불가.
            eagle.Jump();

            Dog husky = new Dog(3, "허스키");
            husky.Jump();

            if (husky is Animal)
                Console.WriteLine("허스키는 동물입니다.");
            if (eagle is Dog)
                Console.WriteLine("독수리는 개입니다.");

            Console.WriteLine("멍멍아 짖어!");
            husky.Bark(3);*/

            //new Program().DelTest();

            /*
            Enumerable.Range(0, 10).ToList().ForEach((int i) => System.Console.Write(i));

            var v = new[]
            {
                new { Name="Lee", Age=33 },
                new { Name="Kim", Age=25 },
                new { Name="Park", Age=37},
            };
            var under30 = v.FirstOrDefault(p => p.Age > 30);
            if (under30 != null)
                Console.WriteLine(under30.Name);
            */

            /*string s = "This is a Test";

            string s2 = s.ToChangeCase();

            bool found = s.Found('z');*/

            /*dynamic v = 1;
            Console.WriteLine(v.GetType());

            v = "string";
            Console.WriteLine(v.GetType());*/

            MyDyClass MDC = new MyDyClass();

            MDC.M1();
        }

        #region DELEGATE
        delegate int MyDelegate(string s);

        void DelTest()
        {
            MyDelegate m = new MyDelegate(StringToInt);

            Run(m);
        }

        int StringToInt(string s)
        {
            return int.Parse(s);
        }

        void Run(MyDelegate m)
        {
            int i = m("123");

            Console.WriteLine(i);
        }
        #endregion
    }
}
