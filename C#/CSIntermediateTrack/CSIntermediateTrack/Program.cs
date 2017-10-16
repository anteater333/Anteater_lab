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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWorld;

namespace MyWorld
{
    public abstract class Animal
    {
        protected string Name { get; set; }
        protected int Age { get; set; } // getter setter 몹시 간단하게 작성.

        public abstract void Jump();
    }

    public class Dog : Animal
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
        }
    }
}
