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

            Bird eagle = new Bird(5, "독수리");
            //eagle.Age = 3; // protected이기에 접근 불가.
            eagle.Jump();

            Dog husky = new Dog(3, "허스키");
            husky.Jump();

            if (husky is Animal)
                Console.WriteLine("허스키는 동물입니다.");
            if (eagle is Dog)
                Console.WriteLine("독수리는 개입니다.");

            Console.WriteLine("멍멍아 짖어!");
            husky.Bark(3);
        }
    }
}
