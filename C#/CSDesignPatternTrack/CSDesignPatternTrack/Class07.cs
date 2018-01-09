/*****************************
* 2017.12.14 디자인 패턴
* 목표 : Chapter 07 - Adapter 패턴과 Facade 패턴 (1)
     ******* 코멘트 *******
 "네모난 기둥을 동그란 구멍에 끼우기"

데코레이터 패턴은 객체를 래퍼로 감싸서 새로운 역할을 부여하는 패턴이였다.
이번 장에서는 다른 목적으로 객체를 감싼다. 실제와 다른 인터페이스를 가진 것처럼 보이도록 만드는것.
이를 통해 특정 인터페이스를 필요로 하는 디자인을 다른 인터페이스를 구현하는 클래스에 적응시킬 수 있게된다.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
어댑터?
흔히 생각할 수 있는 어댑터는 바로 전자제품에 달린 그것.
만약에 한국에서 쓰던 전자제품을 유럽에 가서 쓰려면 플러그 모양을 바꿔주는 어댑터를 쓰면 된다.
어댑터는 소켓의 인터페이스를 플러그에서 필요로 하는 인터페이스로 바꿔준다.

객체지향 어댑터?
어떤 소프트웨어 시스템이 있는데, 새로운 업체에서 제공한 클래스 라이브러리를 사용해야 한다.
만약 새로 채택한 업체에서 사용하는 인터페이스가 기존 업체에서 사용하던 인터페이스와 다른 경우,
그냥은 기존 시스템과 새 클래스를 연결할 수 없다. 둘 중 어느 하나의 코드를 바꾸지 않는 이상.
바로 여기서 어댑터를 사용해 기존 시스템과 새 클래스를 연결하면 된다.

어댑터는 클라이언트로부터 요청을 받아서
새로운 업체에서 제공하는 클래스에서 받아들일 수 있는 형태의 요청으로 변환시켜주는
중개인 역활을 한다. p.275의 그림을 보면 바로 이해가 될 것이다.
******************************/

/*****************************
오리처럼 걷고 꽥꽥거린다면 반드시 오리... 가 아니라 사실 오리 어댑터로 감싼 칠면조일 수도 있다!

1장에서 같이 놀았던 오리들을 데려오자. 약간 단순화 시켜서.
******************************/

namespace BirdAdapter
{
    public interface IDuck
    {
        void Quack();
        void Fly();
    }

    public class MallardDuck : IDuck
    {
        public void Quack()
        {
            Console.WriteLine("Quack");
        }
        public void Fly()
        {
            Console.WriteLine("I'm flying");
        }
    }
}

/*****************************
여기에 새로운 친구를 만나보자.
******************************/

namespace BirdAdapter
{
    public interface ITurkey
    {
        void Gobble();
        void Fly();
    }

    public class WildTurkey : ITurkey
    {
        public void Gobble()
        {
            Console.WriteLine("Gobble gobble");
        }
        public void Fly()
        {
            Console.WriteLine("I'm flying a short distance");
        }
    }
}

/*****************************
Duck 객체가 모자라서 Turkey 객체를 대신 사용해야 하는 상황이다.
당연히 인터페이스가 서로 다르기 때문에 바로 Turkey 객체를 사용할 수는 없다.
TurkeyAdapter를 만들어보자.
******************************/

namespace BirdAdapter
{
    public class TurkeyAdapter : IDuck  // 적응시킬 형식의 인터페이스를 구현한다.
    {                                   // 클라이언트에서 원하는 인터페이스를 구현해야 한다.
        ITurkey turkey;

        public TurkeyAdapter(ITurkey turkey)    // 원래 형식의 객체에 대한 레퍼런스가 필요하다.
        {
            this.turkey = turkey;
        }

        // 인터페이스에 들어있는 메소드들을 모두 구현해야 한다.
        // 이 경우는 몹시 간단.
        public void Quack()
        {
            turkey.Gobble();
        }

        public void Fly()
        {
            for (int i = 0; i < 5; i++)
            {
                turkey.Fly();   // 오리처럼 조금 더 오래날게끔.
            }
        }
    }
}

/*****************************
테스트.
******************************/

namespace CSDesignPatternTrack
{
    using BirdAdapter;
    partial class Class07
    {
        public Class07()
        {
            BirdAdapter.MallardDuck duck = new BirdAdapter.MallardDuck();

            WildTurkey turkey = new WildTurkey();
            IDuck turkeyAdapter = new TurkeyAdapter(turkey);    // Turkey는 TurkeyAdapter로 감싸서 Duck처럼 보이게 만든다.

            Console.WriteLine("The Turkey says...");
            turkey.Gobble();
            turkey.Fly();

            Console.WriteLine("\nThe Duck says...");
            TestDuck(duck);

            Console.WriteLine("\nThe TurkeyAdapter says...");
            TestDuck(turkeyAdapter);

            Section02();
        }
        
        void TestDuck(IDuck duck)
        {
            duck.Quack();
            duck.Fly();
        }
    }
}

/*****************************
오늘 배울 내용은 거의 다 했다고 보면 된다.
이제 어댑터가 전체적으로 어떤 식으로 돌아가는지 살펴보자.

먼저, 클라이언트는 타겟 인터페이스에 맞게 구현되어있다.
클라이언트에서는 평소 하던대로 request()를 호출한다.
어댑터는 타겟 인터페이스를 구현하며, 여기에는 어댑티(adaptee) 인스턴스가 들어있다.

여기서 클라이언트는 앞서 예제에서의 TestDuck() 메소드,
어댑터는 TurkeyAdapter,
어댑티는 Turkey.

클라이언트에서 어댑터를 사용하는 방법
1. 클라이언트에서 타겟 인터페이스를 사용하여 메소드를 호출함으로써 어댑터에 요청을 한다.
2. 어댑터에서는 어댑티 인터페이스를 사용하여 그 요청을 어댑티에 대한 (하나 이상의) 메소드 호출로 변환한다.
3. 클라이언트에서는 호출 결과를 받긴 하지만 중간에 어댑터가 껴 있는지는 전혀 알지 못한다.
클라이언트와 Adaptee는 서로 분리되어있다. 서로 상대방에 대해서 전혀 모른다.

Pattern #08 Adapter
한 클래스의 인터페이스를 클라이언트에서 사용하고자 하는 다른 인터페이스로 변환한다.
어댑터를 이용하면 인터페이스 호환성 문제 때문에 같이 쓸 수 없는 클래스들을 연결해서 쓸 수 있다.

클래스 다이어그램을 보자. 쉬워서 그릴 수 있겠다 이건.
Client  ------------> <<Interface>> Target
                      request()
                               ∧
                               │
                               │
                               │
                               │ (Implements)
                      Adapter               ------------> Adaptee
                      request()                           specificRequest()
클라이언트에서는 타겟 인터페이스만 볼 수 있다.
어댑터에서는 타겟 인터페이스를 구현한다.
어댑터(er)는 어댑티(ee)로 구성(composition)되어있다.
결과적으로 모든 요청은 어댑티에게 위임된다.

어댑티를 새로 바뀐 인터페이스로 감쌀 때 객체 구성을 사용함으로써,
어댑티의 어떤 서브클래스에 대해서도 어댑터를 쓸 수 있다.
또한 이 패턴에서는 클라이언트를 특정 구현이 아닌 인터페이스에 연결시킨다.
인터페이스를 기준응로 한 코딩으로 인해, 타겟 인터페이스만 제대로 지킨다면 나중에 다른 구현을 추가하는 것도 가능하다.
즉, 여러 디자인 원칙이 들어간 패턴이라는 것.
******************************/

/*****************************
사실 어댑터에는 두 종류가 있다. 객체 어댑터, 클래스 어댑터.
앞서 알아본 어댑터는 객체 어댑터에 해당된다.

클래스 어댑터는 사실 자세히 알아볼 순 없는데, 그이유는 바로 다중 상속 때문.
java나 C#이나 다중 상속을 지원하지 않는다...
클래스 어댑터에서는 어댑터를 만들 때 타겟과 어댑티 모두의 서브클래스로 만든다.
즉, 객체 어댑터에서는 어댑티를 적응시키는데 구성을 사용하고,
클래스 어댑터에서는 상속을 사용한다.

둘의 자세한 차이는 심심하면 읽어보도록 하자.
******************************/

/*****************************
어댑터 패턴을 실전에서 사용해보자.
평생 오리만 가지고 놀 순 없지.
지금부터는 java에 맞춰서 설명한다.

Enumeration 인터페이스는 java의 과거 버전부터 존재했던 인터페이스다.
java가 점점 업데이트 되면서 새로운 컬렉션 클래스가 나오고,
Enumeration과 마찬가지로 컬렉션에 있는 일련의 항목들에 접근할 수 있게 해주면서,
항목을 제거할수도 있게 해주는 Iterator 라는 인터페이스를 이용하기 시작했다.
Enumerator 인터페이스를 사용하는 구형 코드를 사용해야 하는 경우가 종종 있지만,
새로 만드는 코드에서는 Iterator만 사용할 계획이다. 여기에 어댑터 패턴을 적용한다.

<<Interface>> Iterator                  <<Interface>> Enumeration
hasNext()                 -- 대응 ->    hasMoreElements()
next()                    -- 대응 ->    nextElement()
remove()             -- 없는데..? ->
일단은 두 인터페이스를 비교해보고, 이에 맞춰 어댑터를 설계한다.

<<Interface>> Iterator
           ∧
           │
           │
           │
           │ (Implements)
EnumerationIterator     ----------------> <<Interface>> Enummeration
hasNext()
next()
remove()

문제는 remove() 메소드.
Enummeration에서는 remove()에 해당하는 기능을 제공하지 않는다. 읽기 전용 인터페이스이기 때문.
안타깝게도 어댑터 차원에서 완벽하게 작동하는 remove() 메소드를 구현하는 방법은 없다.
런타임 에러를 throw 할 수 밖에... 대신 java 제작자들의 선견지명으로 UnsupportedOperationException이라는 예외가 있다고 한다.

코드는 뭐... C# 에서 짜기엔 좀 뭣해서...
내일은 이번 챕터의 두 번째 패턴을 알아보도록 하자.
******************************/