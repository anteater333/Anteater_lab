/**************************************************
                * C# 기초학습 *
***************************************************/

/***************************************************
* 날짜 : 2017.10.09
* 목표 : 프로젝트 시작 및 콘솔 입출력
             ******* 코멘트 *******
참고자료 http://www.csharpstudy.com/
물론 다른 사이트도 여러군데 참고하면서 할 예정. 교재는 따로 없음.
기존에 사용하던 언어들과 문법이 크게 차이나지 않기 때문에 적당히 거를건 거르면서 해야함.

Console (System.Console) 클래스의 Read / Write 메소드를 통해 콘솔 입출력.
자바의 System.in / System.out 이 합쳐진거라고 생각.

메소드 Read는 반환형이 int
메소드 ReadLine은 반환형이 string
ReadKey라는 메소드도 있다. 말 그대로 키를 입력받는다. 즉 방향키도 입력가능하고, 엔터키를 누를 필요도 없다.
반환형은 ConsoleKeyInfo라는 클래스.
***************************************************/

/***************************************************
* 날짜 : 2017.10.10
* 목표 : 데이터 타입, 배열, 문자열, 열거형, 연산자
             ******* 코멘트 *******
자료형은 기존에 쓰던것과 거의 동일.
곁다리로 DateTime 구조체를 써보자.. class가 아니라 struct다.

Nullable Type
정수나 날짜같이 null을 가질 수 없는 타입을 null을 가질수 있도록 만들어줌.
사용법
int? i = null;
Nullable<int> j = null;
(참고) null을 가질수있는 타입은 레퍼런스 타입. 대표적으로 string.

int.MaxValue 같은것도 가능.

상수
상수는 const 키워드를 이용해 선언한다.
readonly 키워드로 읽기전용 필드를 만들 수도 있음.
readonly int Max;
필드 선언부나 클래스 생성자에서 값을 지정할 수 있음. 런타임시 값이 결정.

배열
선언 방법
string[] players = new string[10]; // 10개짜리 string 배열
string[] regions = { "서울", "경기", "대구" }; // 선언과 초기화.
다차원 고정 크기 배열은 콤마로 표현.
Depts[0,0];
가변 배열을 만들수도 있다.
int[][] A = new int[3][]; // 물론 1차 배열크기는 고정
A[0] = new int[2];
A[1] = new int[3];
A[2] = new int[4];
!! 가변배열은 A[0][0]과 같은 방법으로 표현.
배열은 System.Array 의 메소드, 프로퍼티를 사용할 수 있다. 객체지향!

문자열(string)
string은 Immutable type.
string s;
s = "C#";
s = "F#";
위 코드를 실행할 경우 "C#" 을 가지는 string 객체와 "F#"을 가지는 string 객체가 둘다 존재.
다만 s라는 string이 참조하는게 달라질뿐. C#에서 F#으로. 객체 하나 가지고 값을 계속 바꾸는게 아니란 말.
배열처럼 System.String 의 메소드, 프로퍼티를 사용할 수 있다.
string은 char[] 처럼 취급하기도 한다. s[0]으로 접근이 가능하다는 사실.

StringBuilder
System.Text.StringBuilder 클래스
Mutable type 문자열. 문자열 갱신이 많이 되는 경우에 사용.
루프문에서 문자열을 가지고 논다고 할때 쓰도록 하자.

Flag Enum... 참고 정도만.
비트 필드를 가지는 enum. (0, 1, 2, 4, 8, ...)
[Flags]
enum eFlags
{
    ...
}
위처럼 선언. AND나 OR연산 쓰기 편해짐.
eFlags e = eFlags.a | eFlags.b;
이렇게 하면 다중값을 가지게 된다는것.
잘쓰면 유용할듯.

?? 연산자
다른 연산자는 다 똑같고. 특이한 연산자 ??
Null-coalescing operator
왼쪽 피연산자의 값이 NULL인 경우 ?? 뒤의 피연사자 값을 리턴. 아니면 ?? 앞의 피연산자 값을 리턴.
**************************************************/

/***************************************************
* 날짜 : 2017.10.11
* 목표 : 제어문, yield, 예외 처리
             ******* 코멘트 *******
**조건문
if, switch-case 끝. 특별할거 없음.

**반복문
for 똑같음. while도 똑같음.

**foreach
배열이나 컬렉션의 각 요소를 하나씩 꺼내서 블럭 실행할 때 사용.
다차 배열을 처리할때 루프 한번으로 처리할 수 있다.
백문이 불여일견 코드 참고.

**yield
호출자에게 컬렉션 데이터를 하나씩 리턴할 때 사용. 또는 리턴을 중지하고 루프를 빠져나올때 사용.
yield return <expression>;
yield break;
IEnumerable과 같이 사용

    static IEnumerable<int> GetNumber()
    {
        yield return 10;  // 첫번째 루프에서 리턴되는 값
        yield return 20;  // 두번째 루프에서 리턴되는 값
        yield return 30;  // 세번째 루프에서 리턴되는 값
    }

    static void Main(string[] args)
    {
        foreach (int num in GetNumber())
        {
            Console.WriteLine(num);
            /* 출력형태는
             * 10
             * 20
             * 30
             * /
        }             
    }

왜 써야하나?
10만개 짜리 데이터를 리턴하는것 보다 필요할때마다 10개씩만 리턴하는게 더 효과적.
ex. UI에서 리스트에 한번에 10개까지만 출력 가능할경우. * On demand *

GetEnumerator() 메소드와 yield
public class MyList
{
    private int[] data = { 1, 2, 3, 4, 5 };
    
    public IEnumerator GetEnumerator()
    {
        int i = 0;
        while (i < data.Length)
        {
            yield return data[i];
            i++;                
        }
    }
}

var list = new MyList();

foreach (var item in list)  
{
    Console.WriteLine(item);
}

**yield 실행 순서
1. 호출자가 IEnumerable을 리턴하는 메서드를 호출
2. yield return문에서 값을 하나 리턴
3. 해당 메서드 위치 기억
4. 호출자가 다시 루프를 돌아 다음 값을 메서드에 요청
5. 메서드의 기억된 위치 다음 문장부터 실행하더 다음 yield문을 만나 값을 리턴
6. 반복...

예외처리
try-catch-finally
기본적인 사용법은 java와 흡사함.
throw방식은 알아봐둘 필요가 있음
try
    ...
catch (IndexOutOfRangeException ex) // throw #1
{
    throw new MyException("Invalid array index", ex); // 새로운 Exception을 생성하여 thorw
}
catch (FileNotFoundException ex) // throw #2
{
    bool success = CreateLog();
    if (!succcess)
    {
        throw ex; // 기존 Exception을 throw
    }
}
catch (Exception ex)
{
    Log(ex);
    thorw; // 발생된 Exception을 그대로 호출자에 throw
}
***************************************************/

/***************************************************
* 날짜 : 2017.10.12
* 목표 : 네임스페이스, 구조체, 클래스
             ******* 코멘트 *******
네임스페이스
넘쳐나는 클래스들을 충돌업이 편리하게 관리, 사용하기 위해서 사용.
namespace 키워드로 정의하고, 네임스페이스 안에서는 클래스가 정의된다.
물론 네임스페이스 없어도 정의가 되긴 되는데, 불편하구로 굳이 그럴 필요는 없겠다.
네임스페이스를 참조하는 키워드는 지금껏 계속 써온 using
자바에서 import 같은 느낌이라고 보면 될것같다.
참고로 System도 네임스페이스로, using System;을 안했다면
System.Console.WriteLine(); 이런 식으로 참조를 해야한다. 그냥 참고용.
using으로 불러오는건 다 네임스페이스. import와 다른점이 이거.
(.*) 이라던가, (.클래스) 가 필요없다.

구조체 struct
! struct는 Value Type. class는 Reference Type.
예를 들어 Int32는 struct. String은 class.
Value와 Reference의 차이점을 기억하자.
!! C#의 구조체는 클래스처럼 메서드와 프로퍼티가 있다. 비슷한 구조.
다만 상속은 할 수 없다. 인터페이스를 구현할수는 있다.

클래스 class
거의 핵심개념.
구성요소 - 메서드, 프로퍼티, 필드, 이벤트
**메서드
실제 행동을 일으키는 코드 블럭.
참고로 작명은 동사 혹은 동사+명사로 하자.
**프로퍼티
클래스의 내부 데이터를 외부에서 사용할 수 있게 하거나, 외부에서 클래스 내부의 데이터를 간단하게 설정할때 사용.
설명이 한줄 길지만 간단히 getter와 setter를 가지는 멤버변수를 생각하면 될듯.
다만 C#에선 사용법이 다르다. getter와 setter를 설정하는 법을 유심히 보도록 하자.
**필드
객체의 상태를 유지하는데 이용. 클래스의 내부 데이터를 저장.
멤버변수. 여러 과목에서 여러번 강조되었듯이 public으로 만드는것은 OOP에 어긋난다.
private 필드를 만들고 public 프로퍼티를 이용해 필드값을 외부로 전달하는 방식으로 만들어야 한다.
getter와 setter.
**이벤트
객체 내부의 특정 상태 혹은 말그대로 이벤트를 외부로 전달하는데 이용.
ex)버튼 클래스의 버튼이 클릭되었으면, 버튼클릭 이벤트에 가입한 모든 외부 객체들에게 사실을 casting하는거.

Partial 클래스
클래스 하나를 여러 파일에 나누어 정의할 수 있는 기능. 약간 심화내용같음.
지금은 알아만 두고 나중에 다시 나오니까 그때 제대로 배우자.
***************************************************/

/***************************************************
* 날짜 : 2017.10.13
* 목표 : 메서드, 이벤트, 전처리기. 기초학습 마무리.
             ******* 코멘트 *******
Nullable 원리 보충
기본 아이디어. 정수형 변수 i에 값이 설정되지 않은 상태를 할당하려한다. (알고리즘 시간때 많이 봤던 상황)
해결책은 정수 중 안쓸것같은 값을 따로 할당하던가, bool iHasValue; 같은 변수를 추가하는것.
Nullable 타입은 HasValue 기능을 가지고있는 구조체. (따라서 Value Type이면서 NULL을 가질 수 있다.)
참고로 System.Nullable 이라는 static 클래스로 Nullable 객체에 대한 메서드를 쓸 수도 있음. 비교한다던가. Value타입을 알아낸다던가.

메서드
어제 알아봤듯이, 그리고 계속 배워왔듯이 클래스 내에서 일련의 코드 블럭을 실행시키는 함수를 메서드라고 부른다.
! C#은 Pass by Value 방식을 따른다. Call by Value.
근데 C나 Java나 다 PbV인걸로 기억. 사실 조금 모호하긴 함. C에서도 포인터를 인수로 주면 PbR같은 느낌이 들었으니까.
! 근데 Pass by Reference도 가능하다.
대놓고 PbR을 하겠다는 키워드 ref가 있다. 참고로 ref로 전달되는 변수는 반드시 사전에 초기화되어져야 한다.
ex.
static double GetData(ref int a, ref double b)
{ return ++a *  ++b; }
ref와 비슷한 기능을 하는 키워드 out도 있다.
out으로 전달되는 변수는 메서드 내에서 그 값을 반드시 지정하여 전달하게 되어 있다.
마치 함수의 출력처럼 쓸 수 있다는 말. 그러므로 ref와 달리 사전에 초기화할 필요는 없다.

그 외의 특별한 매개변수들
** Named Parameter
명시된 매개변수. 원래는 메서드 선언 시 매개변수 위치에 따라 순차적으로 변수가 전달되는데,
이름을 명시해서 위치와 상관없이 변수를 전달할 수 있게 하는 매개변수.
ex.
Method1(name: "John", age: 10, score: 90);
**Optional Parameter
default값을 가지는 매개변수. 말 그대로 선택적으로 생략할수도 있는 매개변수.
참고로 Optional Parameter는 일반적인 매개변수 뒤에 위치해야한다.
사용법은 메소드 선언 시 매개변수에 값을 할당하면 됨.
ex.
int Calc(int a, int b, string calcType = "+")
    ...
** params
매개변수 개수를 미리 알 수 없는 경우에 쓰는 키워드. 가변적인 배열을 인수로 갖게 해줌.
! 매개변수들 중 params는 반드시 하나만 존재해야 하며, 마지막에 위치해야 한다.
ex.
int Calc(params int[] values)   // 선언
    ...
int s = Calc(1,2,3,4);          // 호출1
s = Calc(6,7,8,9,10,11);        // 호출2

참고로 Named Parameter는 가독성을 증가시키고, Optional Parameter화한 값을 어떤것으로 호출하는지 확인하기 좋게 해준다고한다.

전처리기
C계열 언어인 만큼 #로 표현.
자주 사용되는 전처리기는 #define과 #if ... #else ... #endif

#define
보통 symbol을 정의할때 사용. #define DEBUG라던가 #define RELEASE라던가.
symbol은 다른 전처리기 지시어에서 사용된다. ex) #if (DEBUG)
#undef로 symbol 해제가능

#region
코드 블럭을 논리적으로 묶을 때 사용한다.
Public 메소드들만 묶어서 [Public Methods]라고 명명한다던가,
Private 메소드를 묶어서 [Privates]라고 명명한다던가.
#region은 #endregion과 쌍을 이뤄서 영역을 형성한다.
Visual Studio가 #region을 인식하기때문에 가독성에 매우 좋아보인다.

기타 전처리기 지시어는 필요할때 찾아서 쓰자.
***************************************************/

#define DEBUG_TEST
//#define DEBUG_NOT_TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBeginnerTrack
{
    public class MyList
    {
        private int[] data = { 1, 2, 3, 4, 5 };

        public IEnumerator<int> GetEnumerator()
        {
            int i = 0;
            while (i < data.Length)
            {
                yield return data[i];
                i++;
            }
        }
    }

    public class MyCustomer // 초심자에게 유용한 문법이 많으니까 눈여겨 보자.
    {
        // Field
        private string name;
        private int age;

        // Event
        public event EventHandler NameChanged;

        // Constructor (생성자, 인스턴스 생성시 수행되는 메서드)
        public MyCustomer()
        {
            name = string.Empty;
            age = -1;
        }

        // Property
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    if (NameChanged != null)
                    {
                        NameChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        // Method
        public string GetCustomerData()
        {
            string data = string.Format("Name: {0} (Age: {1})", this.Name, this.Age);
            return data;
        }
    }

    class Program
    {
        struct MyPoint
        {
            public int X;
            public int Y;

            public MyPoint(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return string.Format("({0}, {1})", X, Y);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("몇일차?");
            string select = Console.ReadLine();
            #region switch
            switch (select)
            {
                case "1":
                    {
                        Console.WriteLine("안녕하세요, 여러분!");
                        break;
                    }
                case "2":
                    {
                        DateTime date = new DateTime(2017, 10, 10, 15, 23, 0);
                        Console.WriteLine(date.ToString());
                        string[] players = new string[10]; // 10개짜리 string 배열
                        string[] regions = { "서울", "경기", "대구" }; // 선언과 초기화.
                        string[,] Depts = { { "김과장", "경리부" }, { "이과장", "총무부" } }; // 2차원 배열 선언, 초기화
                        Console.WriteLine(Depts[0, 0]);
                        Console.WriteLine(players.Length);
                        StringBuilder sb = new StringBuilder("StringBuilder");
                        sb.Append("Append!");
                        Console.WriteLine(sb);
                        int? i = null;
                        i = i ?? 3;
                        Console.WriteLine(i);
                        break;
                    }
                case "3":
                    {
                        string[,,] array = new string[,,]
                        {
                            { {"1", "2"}, {"11", "22"} },
                            { {"3", "4"}, {"33", "44"} }
                        };
                        Console.WriteLine("foreach");
                        foreach (var s in array)
                        {
                            Console.WriteLine(s);
                        }
                        var list = new MyList();
                        Console.WriteLine("yield & IEnumerator");
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Struct");
                        MyPoint point1 = new MyPoint(5, 5);
                        Console.WriteLine(point1.ToString());
                        Console.WriteLine("Class");
                        MyCustomer customer1 = new MyCustomer();
                        customer1.Name = "이지훈";                      // setName
                        customer1.Age = 23;                             // setAge
                        Console.WriteLine(customer1.Name);              // getName
                        Console.WriteLine(customer1.Age);               // getAge
                        Console.WriteLine(customer1.GetCustomerData()); // 메소드 사용
                        break;
                    }
                case "5":
                    {
#if (DEBUG_NOT_TEST)
                        Console.WriteLine("이 문장이 나오면 안돼!");
#elif (DEBUG_TEST)
                        Console.WriteLine("안녕하세요?");
#else
                        Console.WriteLine("이 문장도 나오면 안돼!");
#endif
                        break;
                    }
                default:
                    Console.WriteLine("프로그램 끝");
                    break;
            }
            #endregion
        }
    }
}
