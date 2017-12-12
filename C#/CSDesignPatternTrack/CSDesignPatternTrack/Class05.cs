/*****************************
* 2017.12.12 디자인 패턴
* 목표 : Chapter 05 - Singleton 패턴
     ******* 코멘트 *******
 "세상에서 단 하나뿐인 특별한 객체"

싱글턴 패턴은 내가 디자인 패턴 공부를 시작하기 전부터 의미를 알고 있던 패턴이다.
실은 소프트웨어 공학 시간때 패턴을 배운 적이 있는데, 그 중에 유일하게 기억나는게 이 싱글턴 패턴.
기능이 워낙 씸플하다보니까, 그냥 "객체가 딱 하나만 필요한 경우에 쓰면 된다." 이렇게 외웠음.

이제 싱글턴 패턴에 대해 제대로 알아보자.
싱글턴 패턴은 인스턴스가 하나 뿐인 특별한 객체를 만들 수 있게 해 주는 패턴이다.
싱글턴 패턴은 앞서 내가 느꼈듯, 클래스 다이어그램으로 나타낸 구조만 보면 몹시 간단하다.
하지만 구현에 있어서는 적지 않는 난관이 기다리고 있다더라.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*****************************
싱글턴 패턴은 어떤 상황에서 써야할까?
스레드 풀, 캐시, 대화상자, 사용자 설정/레지스트리 설정을 처리하는 개체,
로그 기록용 객체, 디바이스 드라이버 등등...
근데 이게 그렇게 만들기 어려운 것일까?
좀 더 자세히 말해 싱글턴 패턴은 특정 클래스에 대해서 객체 인스턴스가 하나만 만들어질 수 있도록 하는 패턴이다.
그냥 쉽게 생각해서 전역 변수를 쓰면 되지 않을까 할 수 있지만, 전역 변수 그 자체엔 또 단점이 있다.
전역 변수에 객체를 대입하면 애플리케이션이 시작될 때 객체가 생성된다.(일반적으로)
만약 애플리케이션이 끝날 때까지 그 객체를 한 번도 쓰지 않는다면 그 객체는 괜히 자원만 잡아먹는 객체가 되버린다.
그렇다면 대체 어떻게 만들어야 하는지는... 이제부터 알아보자.
******************************/

/*****************************
private 생성자에 대해 들어본 적 있습니까?
public으로 선언되지 않은 클래스라면 같은 패키지(namespace) 안에 있는 클래스에서만 인스턴스를 만들 수 있다.
그렇다면 이건 어떨까?
public MyClass
{ private MyClass() { } }
즉, MyClass 안에서만 호출할 수 있는 생성자.
근데 그냥 저것만 있어선 MyClass의 생성자를 호출할 방법이 없다.
public MyClass
{
    private MyClass() { }
    public static MyClass Instance()
    {
        return new MyClass();
    }
}
이렇게 하면 슬슬 싱글턴을 어떻게 구현할지 아이디어가 떠오를 수 있게 되었다.
******************************/

namespace OldSingleton
{
    public class Singleton
    {
        private static Singleton uniqueInstance;    // 클래스의 유일한 인스턴스를 저장하기 위한 정적 변수

        private Singleton() { } // private 생성자

        public static Singleton Instance() 
        {
            if (uniqueInstance == null)
                uniqueInstance = new Singleton();   // Lazy instantiation
            return uniqueInstance;
        }
    }
}

/*****************************
다했다! 오늘 내용 끝! ...?

오늘의 예제를 살펴보도록 하자.
요즘은 거의 모든 초콜릿 공장에서 초콜릿을 끓이는 보일러를 컴퓨터로 제어한다.
이 보일러에서는 초콜릿과 우유를 받아서 끓이고 초코바를 만드는 단계로 넘겨준다.
오늘의 예제는 초코홀릭(Choc-O-Holic)사의 최신형 초콜릿 보일러를 제어하기 위한 클래스.
******************************/

namespace ChocOHolicProto
{
    public class ChocolateBoiler
    {
        public Boolean Empty { get; private set; }
        public Boolean Boiled { get; private set; }

        public ChocolateBoiler()
        {
            Empty = true;
            Boiled = false;
        }

        public void Fill()
        {
            if (Empty)
            {
                Empty = false;
                Boiled = false;
            }
        }

        public void Drain()
        {
            if (!Empty && Boiled)
                Empty = true;
        }
        
        public void Boil()
        {
            if (!Empty && !Boiled)
                Boiled = true;
        }
    }
}

/*****************************
이 코드의 ChocolateBoiler 클래스를 싱글턴으로 업그레이드 해보자.
******************************/

namespace ChocOHolicProto
{
    public class ChocolateBoilerSingleton
    {
        public Boolean Empty { get; private set; }
        public Boolean Boiled { get; private set; }

        private static ChocolateBoilerSingleton uniqueInstance;

        private ChocolateBoilerSingleton()
        {
            Empty = true;
            Boiled = false;
        }

        public static ChocolateBoilerSingleton Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ChocolateBoilerSingleton();
            return uniqueInstance;
        }

        public void Fill()
        {
            if (Empty)
            {
                Empty = false;
                Boiled = false;
            }
        }

        public void Drain()
        {
            if (!Empty && Boiled)
                Empty = true;
        }

        public void Boil()
        {
            if (!Empty && !Boiled)
                Boiled = true;
        }
    }
}

/*****************************
지금까지 싱글턴 패턴의 구현법을 배웠다. 싱글턴 패턴은 다음과 같이 정의된다.

Pattern #06 Singleton
싱글턴 패턴은 해당 클래스의 인스턴스가 하나만 만들어지고,
어디서든지 그 인스턴스에 접근할 수 있도록 하기 위한 패턴이다.

싱글턴 패턴을 적용하려면 클래스에서 자신의 단 하나뿐인 인스턴스를 관리하도록 만든다.
그리고 다른 어떤 클래스에서도 자신의 인스턴스를 추가로 만들지 못하도록 해야한다.
인스턴스가 필요하다면 반드시 클래스 자신을 거치도록 만든다.
그리고 어디서든 그 인스턴스에 접근할 수 있도록 만들어야 한다.
다른 객체에서 이 인스턴스가 필요하면 언제든지 클래스한테 요청을 할 수 있게 만들고,
요청이 들어오면 그 하나뿐인 인스턴스를 건네주도록 만들어야 한다.
또한 싱글턴이 '게으르게' 생성되도록 구현할 수 있다. 앞에서 만든 static 메소드 Instance() 처럼.
클래스의 객체가 자원을 많이 잡아먹는 경우에는 이런 생성 방법을 사용하는 것이 유용하다.

클래스 다이어그램도 별 거 없음.
Singleton
-Fields----------------
static uniqueInstance
-Methods---------------
static Instance()

진짜 오늘 내용 끝...?
******************************/

/*****************************
Hershey, we have a problem.

누군가 다중 스레드를 사용해서 초콜릿 보일러 컨트롤러를 최적화하려고 했다고 가정해보자.
컴파일러의 입장에서 생각해본다.
두 개의 스레드에서 다음 코드를 실행시킨다.
ChocolateBoilerSingleton boiler = ChocolateBoilerSingleton.Instance();
아래와 같이 두 스레드가 서로 다른 보일러 객체를 사용하게 될 가능성이 있다.
Thread01                                            Thread02
public static ChocolateBoiler Instance()
                                                    public static ChocolateBoiler Instance()
    if (uniqueInstance == null)
                                                        if (uniqueInstance == null)
        uniqueInstance = new ChocolateBoiler;
        return uniqueInstance;
                                                        uniqueInstance = new ChocolateBoiler;
                                                        return uniqueInstance;

해결책은 무엇인가?
Instance()를 동기화 시켜줘야 한다. java에서는 synchronized 키워드를 사용한다.
synchronized는 지정된 영역의 코드를 한 번에 하나의 쓰레드가 수행하도록 보장한다.
C#에선 어떤 키워드를 써야하지?
참고자료 https://stackoverflow.com/questions/541194/c-sharp-version-of-javas-synchronized-keyword
[MethodImpl(MethodImplOptions.Synchronized)]
아니면 직접 세마포어를 구현해도 될듯. 귀찮긴 하겠지만.
******************************/

namespace ChocOHolicSync
{
    public class ChocolateBoilerSingleton
    {
        public Boolean Empty { get; private set; }
        public Boolean Boiled { get; private set; }

        private static ChocolateBoilerSingleton uniqueInstance;

        private ChocolateBoilerSingleton()
        {
            Empty = true;
            Boiled = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ChocolateBoilerSingleton Instance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ChocolateBoilerSingleton();
            return uniqueInstance;
        }

        public void Fill()
        {
            if (Empty)
            {
                Empty = false;
                Boiled = false;
            }
        }

        public void Drain()
        {
            if (!Empty && Boiled)
                Empty = true;
        }

        public void Boil()
        {
            if (!Empty && !Boiled)
                Boiled = true;
        }
    }
}

/*****************************
하지만 동기화에도 단점이 있다.
동기화가 필요한 시점은 이 메소드가 시작되는 때 뿐이다.
일단 uniqueInstance 변수에 Singleton 인스턴스를 대입하고 나면 굳이 이 메소드를 동기화된 상태로 유지시킬 필요가 없다.
최초 과정만 제외하면 동기화는 불필요한 오버헤드가 된다.


해결책? (참고로 최소한 싱글턴이 다중 스레드 환경에서 돌아갈 수 있도록 해야한다.)
1. Instance()의 속도가 그리 중요하지 않다면 그냥 둔다.
애플리케이션에서 Instance() 메소드가 병목으로 작용하지 않는다면 굳이 바꿀 필요는 없다.
그냥 synchronized를 사용한다.
2. 인스턴스를 필요할 때 생성하지 말고, 처음부터 만들어 버린다.
Lazy instantiation을 포기한다.
애플리케이션에서 반드시 해당 인스턴스를 생성하고, 그 인스턴스를 항상 사용한다면,
또는 인스턴스를 실행중에 수시로 만들고 관리하기가 성가시다면 다음과 같은 식으로 처음부터 인스턴스를 만들어버린다.
private static Singleton uniqueInstance = new Singleton();
public static Singleton Instance()
{ return uniqueInstance; }
3. DCL(Double-checking locking)을 써서 Instance()에서 동기화되는 부분을 줄인다.
DCL을 사용하면 일단 인스턴스가 생성되어 있는지 확인한 다음, 생성되어 있지 않았을 때만 동기화를 할 수 있다.
아무래도 이 방법이 최선이 아닐까. volatile 이라는 어렵고 비교적 신식 문물에 해당되는 키워드를 사용한다.
private volatile static Singleton uniqueInstance;
private object instanceLock = new object();
public static Singleton Instance()
{
    if (uniqueInstance == null)
    {
        lock (instanceLock)
        {
            if (uniqueInstance == null)
                uniqueInstance == new Singleton();
        }
    }
    return uniqueInstance;
}

끝으로 ChocolateBoiler클래스를 위에서 알아본 사항에 맞춰 수정하고 테스트해보자.
******************************/

namespace ChocOHolic
{
    public class ChocolateBoiler
    {
        public Boolean Empty { get; private set; }
        public Boolean Boiled { get; private set; }

        private volatile static ChocolateBoiler uniqueInstance;
        private static object instanceLock = new object();

        private ChocolateBoiler()
        {
            Empty = true;
            Boiled = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ChocolateBoiler Instance()
        {
            if (uniqueInstance == null)
            {
                lock (instanceLock)
                {
                    if (uniqueInstance == null)
                        uniqueInstance = new ChocolateBoiler();
                }
            }
            return uniqueInstance;
        }

        public void Fill()
        {
            if (Empty)
            {
                Empty = false;
                Boiled = false;
                Console.WriteLine("보일러를 가득 체웠습니다.");
            }
            else
                Console.WriteLine("보일러에 이미 초콜릿이 있습니다.");
        }

        public void Drain()
        {
            if (!Empty && Boiled)
            {
                Empty = true;
                Console.WriteLine("다 끓인 초콜릿을 다음 공정으로 넘겼습니다.");
            }
            else if (Empty)
                Console.WriteLine("보일러가 비어있습니다.");
            else if (!Boiled)
                Console.WriteLine("초콜릿이 아직 끓지 않았습니다.");
        }

        public void Boil()
        {
            if (!Empty && !Boiled)
            {
                Boiled = true;
                Console.WriteLine("초콜릿을 끓입니다.");
            }
            else if (Empty)
                Console.WriteLine("보일러가 비어있습니다.");
            else if (Boiled)
                Console.WriteLine("초콜릿이 이미 끓고 있습니다.");
        }
    }
}

namespace CSDesignPatternTrack
{
    using ChocOHolic;
    class Class05
    {
        public Class05()
        {
            Thread thread1 = new Thread(() =>
            {
                ChocolateBoiler boiler = ChocolateBoiler.Instance();
                boiler.Fill();
                boiler.Boil();
                boiler.Drain();
            });

            Thread thread2 = new Thread(() =>
            {
                ChocolateBoiler boiler = ChocolateBoiler.Instance();
                boiler.Fill();
                boiler.Boil();
                boiler.Drain();
            });

            Thread thread3 = new Thread(() =>
            {
                ChocolateBoiler boiler = ChocolateBoiler.Instance();
                boiler.Fill();
                boiler.Boil();
                boiler.Drain();
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }
    }
}
