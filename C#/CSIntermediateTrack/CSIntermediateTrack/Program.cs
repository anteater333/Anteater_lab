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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            new Program().DelTest();
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
