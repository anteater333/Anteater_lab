/*****************************
* 2017.12.05 디자인 패턴
* 목표 : Chapter 01 - 디자인 패턴 소개. Strategy 패턴
     ******* 코멘트 *******
 "누군가가 이미 여러분의 문제를 해결해 놓았습니다."

이 책은 시작부터 다짜고짜 디자인 패턴은 무엇이다 말해주지 않는다.
대신 오리 시뮬레이터 게임 SimUDuck을 보여준다.
******************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDesignPatternTrack
{
/*****************************
 = SimUDuck 개발자의 이야기 = 
다음 클래스들을 살펴보자.
슈퍼클래스 Duck을 상속받아 MallardDuck을 만들었다.
당연히 모든 오리들은 헤엄을 칠 수 있고 꽥꽥 소리를 낼 수도 있으리라 생각했으므로,
Quack()과 Swim()은 슈퍼 클래스에서 구현했다.
******************************/
    abstract partial class Duck
    {
        protected string name;

        public void Quack()
        {
            Console.WriteLine(name + ": Quack Quack!");
        }

        public void Swim()
        {
            Console.WriteLine(name + ": Swim Swim!");
        }

        public abstract void Display();
    }

    class MallardDuck : Duck
    {
        public MallardDuck()
        {
            Display();
        }

        public override void Display()
        {
            name = "Mallard Duck";
            Console.WriteLine("This is a" + name);
        }
    }
/*****************************
이제 프로그램에 새로운 기능을 추가하려한다.
오리들이 날아다니도록 해야한다. 슈퍼 클래스에 Fly() 메소드를 추가하자.
******************************/
    abstract partial class Duck
    {
        public void Fly()
        {
            Console.WriteLine(name + ": Fly Fly!");
        }
    }
/*****************************
여기서 문제가 발생했다. 오리들 중 날 수 없는 오리가 있던 것.
고무 오리와 나무 오리가 화면을 날아다니게 되었다.
나무 오리가 소리까지 내잖아!
******************************/
    class RubberDuck : Duck
    {
        public RubberDuck()
        {
            Display();
        }

        public override void Display()
        {
            name = "Rubber Duck";
            Console.WriteLine("This is a " + name);
        }
    }
    class DecoyDuck : Duck
    {
        public DecoyDuck()
        {
            Display();
        }

        public override void Display()
        {
            name = "Decoy Duck";
            Console.WriteLine("This is a" + name);
        }
    }
}
/*****************************
거기에 고무 오리는 삑삑 소리를 내야하고... 나무 오리는 아무 소리도 안내야하고....
애초에 상속을 잘못한 것일까?

여기서 인터페이스를 쓴다면 어떻게 될까?
IFlyable.Fly(), IQuackable.Quack()
==> 문제는 인터페이스는 구현부가 없기 때문에 코드 재사용은 전혀 기대할 수 없게 된다.
마흔 다섯 마리 오리 중 마흔 마리가 날 수 있다면 수정 한 번에 코드 40개를 고쳐야 한다.
******************************/

/*****************************
여기서 눈여겨 봐야 할 디자인 원칙.
==============================
애플리케이션에서 달라지는 부분을 찾아내고,
달라지지 않는 부분으로부터 분리시킨다.
==============================

즉, 코드에 새로운 요구사항이 있을 때 마다 바뀌는 부분이 있다면,
그 행동을 바뀌지 않는 다른 부분으로부터 골라내서 분리해야 한다.
==> 바뀌는 부분을 따로 뽑아서 캡슐화.

책에서는 여기서 디자인 패턴의 목적을 이야기 해준다.
 "모든 패턴은 '시스템의 일부분을 다른 부분과 독립적으로 변화시킬 수 있는' 방법을 제공하기 위한 것이니까요."
******************************/

/*****************************
다시 SimUDuck으로 돌아와서...
지금까지 살펴본 바로는 fly나 quack 같은 행동과 관련된 문제를 제외하고 Duck 클래스가 잘 작동하고 있다.
Duck은 몇가지 수정 사항을 제외하고는 그대로 둬야한다.

오리마다 달라지는 부분은 Fly()와 Quack() 메소드들.
변화하는 부분과 그대로 있는 부분을 Duck과 별개로 분리하려면 클래스 집합을 만들어야한다.
나는 것과 관련된 집합, 소리를 내는 것과 관린된 집합.
각 클래스 집합에는 각각의 행동을 구현한 것을 전부 집어넣는다.
p.48 그림을 참고하면 아주 좋음.

그렇다면 클래스 집합은 어떻게 디자인해야할까?
우선 최대한 유연해야한다. Duck 인스턴스에 행동을 할당할 수 있어야 한다.
예를 들면, MallardDuck 인스턴스를 만든 다음 특정 형식의 나는 행동으로 초기화시키는 방법이 있다.
Duck 클래스에 행동과 관련된 프로퍼티(책에서는 자바 기준이니까 setter 메소드)를 포함시켜
동적으로 MallardDuck의 행동을 바꿀 수 있도록 만들 수도 있다.

그리고 여기서 디자인 원칙 하나 더.
==============================
구현이 아닌 인터페이스에 맞춰서 프로그래밍 한다.
==============================

각 행동은 인터페이스로 표현하고(FlyBehavior, QuackBehavior) 행동을 구현할 때 이런 인터페이스를 구현하도록 한다.
그리고 Duck 클래스에서 날거나 소리를 내는 행동을 구현하지 않는다.
대신 특정 행동만을 목적으로 하는 클래스의 집합을 만들고, 행동 인터페이스를 이러한 행동 클래스에서 구현한다.

이전 방법은 Duck에서 구체적으로 구현하거나, 서브클래스 자체에서 별도로 구현하는 방법이었다.
이는 항상 특정 구현에 의존하도록 만들었다. 특정 구현을 써야했으니까 행동을 변경하기도 어려웠다.
이런 새로운 디자인을 사용하면 Duck의 서브클래스에서는 인터페이스(FlyBehavior, QuackBehavior)로 표현되는 행동을 사용하게 된다.

참고.
인터페이스에 맞춰서 프로그래밍하라?
여기서 인터페이스란, 진짜 그 interface 키워드로 만들어지는 구조일 수도 있고,
말 그대로 인터페이스라는 개념을 뜻할 수도 있다.
중요한것은 실제 실행시에 쓰이는 객체가 코드에 의해서 고정되지 않도록,
어떤 상위 형식에 맞춰서 프로그래밍 해야한다는것.

다음 예제 코드를 잘 살펴보자.

Dog d = new Dog();
d.bark();
=> 구체적인 구현에 맞춰서 코딩을 해야만 한다.

Animal animal = new Dog();
animal.makeSound();
=> 다형성을 이용. Animal에 대한 레퍼런스를 쓴다.

a = getAnimal();
a.makeSound();
=> 아예 new로 인스턴스 생성 과정을 코드로 만드는 대신,
   구체적으로 구현된 객체를 실행시에 대입한다.

어느것이 더 유연한지는 뻔한 이야기.
******************************/

// Anteater