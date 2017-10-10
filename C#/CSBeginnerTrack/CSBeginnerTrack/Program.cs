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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBeginnerTrack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("안녕하세요, 여러분!");
            Console.Write("다음?");
            Console.ReadLine();
            Console.WriteLine();

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
            Console.Write("다음?");
            Console.ReadLine();
            Console.WriteLine();

        }
    }
}
