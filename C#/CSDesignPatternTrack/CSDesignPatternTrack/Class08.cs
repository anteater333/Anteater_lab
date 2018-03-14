/*****************************
* 2017.12.18 디자인 패턴
* 목표 : 복습
     ******* 코멘트 *******
오늘은 이때까지 배운 내용들을 복습해보자.
특별히 코드를 짤 건 없다.
0. 앞서 정리해온 내용들을 처음부터 훑어보면서
1. 지금까지 나온 디자인 원칙들을 살펴보고,
2. 각 패턴들의 역할들을 복습한 다음,
3. 패턴들의 클래스 다이어그램을 정리하자.
******************************/

/*****************************
1장 Strategy 패턴

1장에서 배운 첫 번째 디자인 원칙.
==============================
애플리케이션에서 달라지는 부분을 찾아내고,
달라지지 않는 부분으로부터 분리시킨다.
==============================
코드에 새로운 요구사항이 있을 때 마다 바뀌는 부분이 있다면,
그 행동을 바뀌지 않는 다른 부분으로부터 골라내서 분리해야 한다.
어떻게 캡슐화를 해야 하는가를 말해주는 원칙이다.
바뀌는 부분과 바뀌지 않는 부분을 분리.

==============================
구현이 아닌 인터페이스에 맞춰서 프로그래밍 한다.
==============================
실제 실행시에 쓰이는 객체가 코드에 의해서 고정되지 않도록,
상위 형식에 맞춰서 프로그래밍 해야한다.
여기서 인터페이스가 뜻하는게 진짜 interface 키워드를 말하는게 아니라 상위 형식을 말한다.
Dog dog = new Dog();
이것보다
Animal dog = new Dog();
이게 더 유연하다는 이야기.

==============================
상속보다는 구성을 활용한다.
==============================
구성이란 용어는 1장부터 지금까지 배운 7장까지 계속 나오더라.
앞으로도 나올것같다.
상속 관계는 간단히 말해 "A는 B이다." 로 설명할 수 있다.
구성은 이와 달리 "A에는 B가 있다." 로 설명할 수 있다.
만약 "Duck에는 IFlyBehavior가 있다." 라고 한다면,
Duck 클래스 내부에 IFlyBehavior 변수가 존재한다는 뜻.
이 IFlyBehavior 변수를 통해 IFlyBehavior의 메소드를 호출할 수 있다.

Pattern #01 Strategy
스트래티지 패턴에서는 알고리즘군을 정의하고 각각을 캡슐화하여 교환해서 사용할 수 있도록 만든다.
스트래티지 패턴을 활용하면 알고리즘을 사용하는 클라이언트와는 독립적으로 알고리즘을 변경할 수 있다.

스트래티지 패턴의 예제는 오리 시뮬레이터였다.
각 오리별로 Fly() 메소드의 기능이 달라지는데, 이걸 인터페이스, 추상메소드로 처리하면 코드 재사용이 불가능하고....
그래서 각 나는 행동을 나타내는 클래스들의 집합을 만들고,
그 집합의 클래스들이 구현하는 인터페이스의 변수를 Duck 클래스가 가지게 해서(구성),
각 오리마다 인터페이스에 알맞은 행동을 할당한다.
IFlyBehavior 인터페이스가 그 클래스 집합을 나타내는 인터페이스였고,
Duck을 상속받은 구체화된 오리 클래스에서 IFlyBehavior에 자신들의 행동을 할당했다.

http://www.mcdonaldland.info/2007/11/28/40/
여기서 23개 패턴의 클래스 다이어그램이 모두 정리된 아주 훌륭한 PDF 자료를 찾았다.
클래스 다이어그램은 여기서 참고.
******************************/

/*****************************
2장 Observer 패턴

옵저버는 이벤트 같은 기능을 하는 패턴. 사실 이게 이벤트 아닌가.

Pattern #02 Observer
옵저버 패턴에서는 한 객체의 상태가 바뀌면 그 객체에 의존하는 다른 객체들한테 연락이 가고
자동으로 내용이 갱신되는 방식으로 일대다(one-to-many) 의존성을 정의한다.

일대다 관계. 하나의 서브젝트가 여러 옵저버를 가진다.
서브젝트에서는 옵저버들을 등록, 삭제하고 옵저버들이 가지고 있는 update() 메소드를 호출한다.
옵저버에서는 update() 메소드를 구현해 서브젝트에서 얻은 정보를 가지고 각자 할일을 한다.

==============================
서로 상호작용을 하는 객체 사이에서는
가능하면 느슨하게 결합하는 디자인을 사용해야 한다.
==============================
느슨한 결합이란?
두 객체가 느슨하게 결합되어 있다는 것은, 그 둘이 상호작용을 하긴 하지만 서로에 대해 잘 모른다는 것.
옵저버가 서브젝트, 주제에게서 데이터를 받긴 하지만, 둘은 서로 상대방이 무슨 일을 하는지는 전혀 모른다. 내가 누구냐고? 알필요없다!

2장에서는 그리고 C#에서 제공하는 옵저버 인터페이스를 다루는 법도 배웠었다.
IObservable<T>
여기서 T는 전송할 데이터의 타입
******************************/

/*****************************
3장 Decorator 패턴

데코레이터 패턴은 말 그대로 객체를 꾸미는 패턴이다.
이 장에서 나온 예제는 커피숍이었다. 마치 커피를 주문할 때 시럽, 우유 등을 추가하는 것 처럼,
객체를 생성할 때 겉가지들을 추가하는 패턴.

==============================
클래스는 확장에 대해서는 열려 있어야 하지만
코드 변경에 대해서는 닫혀 있어야 한다.
==============================
Open-Closed Principle 이라고 하는 원칙으로,
기존 코드는 건드리지 않은 채로 확장을 통해서 새로운 행동을 간단하게 추가할 수 있도록 해야 한다.
앞서 봤던 상속보다는 구성을 활용한다는 원칙을 적용해 이 원칙을 지킬 수 있다.
행동을 상속받는 경우엔 컴파일 시간에 행동이 결정된다. static
구성을 통해 행동을 동적으로 설정할 수 있다.

Pattern #03 Decorator
데코레이터 패턴에서는 객체에 추가적인 요건을 동적으로 첨가한다.
데코레이터는 서브클래스를 만드는 것을 통해서 기능을 유연하게 확장할 수 있는 방법을 제공한다.

데코레이터 패턴은 쉽게 생각해서, 커피와 조미료로 이루어진다고 생각하면 된다.
최상위의 Component에서 파생된 구체적인 커피와 조미료,
조미료에서 파생된 구체적인 조미료.
어쨌든 제일 위에 있는건 Component이고, Component의 메소드 operation()을 다들 상속받는다.
            // 하우스블렌드, 모카x2, 휘핑 추가.
            Beverage beverage2 = new HouseBlend();
            beverage2 = new Mocha(beverage2);
            beverage2 = new Mocha(beverage2);
            beverage2 = new Whip(beverage2);
            Console.WriteLine(beverage2.Description + " $" + beverage2.Cost());
이 코드를 보면 한눈에 정리가 된다.
beverage2.Cost() =
Whip.Cost()(.10) +
Mocha.Cost()(.20) +
Mocha.Cost()(.20) +
HouseBlend.Cost()(.89)
******************************/

/*****************************
4장 Factory 패턴

비교적 최근에 했지만 앞서 배운 것 보다 기억이 덜 나는 신비의 패턴.
4장에서는 두 가지 팩토리 패턴을 배웠다

Pattern #04 Factory Method
팩토리 메소드 패턴에서는 객체를 생성하기 위한 인터페이스를 정의하는데,
어떤 클래스의 인스턴스를 만들지는 서브클래스에서 결정하게 만든다.
즉, 팩토리 메소드 패턴을 이용하면 클래스의 인스턴스를 만드는 일을 서브클래스에게 맡기게 된다.

Pattern #05 Abstract Factory
추상 팩토리 패턴에서는 인터페이스를 이용하여 서로 연관된,
또는 의존하는 객체를 구상 클래스를 지정하지 않고도 생성할 수 있다.

먼저, 팩토리 메소드 패턴에서는 서브클래스에서 어떤 클래스를 만들지 결정하게 함으로 객체 생성을 캡슐화한다.
팩토리 메소드 패턴은 제품 클래스와 생산자 클래스가 나눠져있으며,
둘은 서로 병렬적이다. 둘 다 추상 클래스로 시작하고, 그 클래스를 확장하는 구상 클래스들을 가지고 있다.

            PizzaStore nyStore = new NYPizzaStore();
            PizzaStore chicagoStore = new ChicagoPizzaStore();  // 인터페이스에 맞춰서 코딩하라!

            Console.WriteLine("Ethan ordered a New York Style Cheese Pizza.");
            Pizza pizza = nyStore.OrderPizza("cheese");
            Console.WriteLine();
            Console.WriteLine("Joel ordered a Chicago Style Cheese Pizza.");
            pizza = chicagoStore.OrderPizza("cheese");

            Section02();

피자집 예제에서, 클래스 다이어그램의 factoryMethod()에 해당하는 메소드는 CreatePizza(),
anOperation()에 해당되는 메소드는 OrderPizza();
여기서 우린 인터페이스에 맞춰서 코딩한다는 원칙을 따라서,
클라이언트가 OrderPizza()를 호출하도록 한다.
factoryMethod()는 객체 생성 그 자체를 담당하는 메소드,
anOperation()은 객체에 대한 후처리를 담당하는 메소드로 생각하면 어떨까.

두 번째 Abstract Factory 패턴은 다음 원칙에 기반하여 구현된다.
==============================
추상화된 것에 의존하도록 만들어라.
구상 클래스에 의존하도록 만들지 않도록 한다.
==============================
고수준 구성요소가 저수준 구성요소에 의존하면 안된다.
항상 추상화에 의존해야한다.

            nyPizzaStore.OrderPizza("cheese");

            /// at OrderPizza()
            /// Pizza pizza = createPizza("cheese");
                /// 
                /// at CreatePizza()
                /// Pizza pizza = new CheesePizza(nyIngredientFactory);
                    /// 
                    /// at CheesePizza.Prepare()
                    /// dough = factory.CreateDough();      // 씬 크러스트
                    /// sauce = factory.CreateSauce();      // 마리나라 소스
                    /// cheese = factory.CreateCheese();    // 레지아노 치즈

추상 팩토리 패턴에서 피자를 주문했을때 일어나는 일을 보자.
피자스토어 인터페이스의 OrderPizza를 호출하면
orderpizza 안의 createpizza가 호출된다.
createpizza는 "cheese"를 입력받았으니 치즈 피자를 만드는데,
치즈피자의 생성자에 어떤 공장을 쓸지 보내준다.
저 뉴욕재료공장을 피자 클래스에 있는 ingredientFactory 변수에다가 대입하고,
공장에 걸맞는 재료들을 생산해낸다.
여기서,
                    /// dough = factory.CreateDough();      // 씬 크러스트
                    /// sauce = factory.CreateSauce();      // 마리나라 소스
                    /// cheese = factory.CreateCheese();    // 레지아노 치즈
이 코드를 보면 알수 있듯, 구현은 인터페이스에 맞춰서 이루어져있다.

추상 팩토리의 클래스 다이어그램을 보자. Client가 추상팩토리와 추상제품의 인터페이스를 가지고있다.
이 인터페이스를 통해 제품의 구상 클래스를 지정하지 않고 제품을 생성할 수 있다.
******************************/

/*****************************
5장 Singleton 패턴

Pattern #06 Singleton
싱글턴 패턴은 해당 클래스의 인스턴스가 하나만 만들어지고,
어디서든지 그 인스턴스에 접근할 수 있도록 하기 위한 패턴이다.

유일한 인스턴스를 만드는 패턴.

Singleton
-Fields----------------
static uniqueInstance
-Methods---------------
static Instance()

클래스에 static 인스턴스를 저장할 필드를 만들고,
Instance() 메소드로 호출하면 된다.

겉보기엔 이렇게 쉬워보이지만, 사실 동기화를 따로 해줘야하는 패턴.
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
여기서 instanceLock은 세마포어라고 생각하면 될 것 같다.
******************************/

/*****************************
6장 Command 패턴

Pattern #07 Command
커맨드 패턴을 이용하면 요구 사항을 객체로 캡슐화 할 수 있으며,
매개변수를 써서 여러 가지 다른 요구 사항을 집어넣을 수도 있다.
또한 요청 내역을 큐에 저장하거나 로그로 기록할 수도 있으며, 작업취소 기능도 지원 가능하다.

            SimpleRemoteControl remote = new SimpleRemoteControl();     // 인보커
            Light light = new Light();                                  // 리시버
            LightOnCommand lightOn = new LightOnCommand(light);         // 커맨드

            remote.SetCommand(lightOn);     // 커맨드 객체를 인보커에 전달

            remote.ButtonWasPressed();      // 클라이언트가 버튼을 누름!

어휴 좀 어지러운데, 커맨드 패턴에서 커맨드가 실행되는 과정을 보자.
클라이언트는 ConcreteCommand(LightOnCommand)를 생성하고 커맨드의 Receiver(light)를 설정한다.
여기서 리시버는 요구 사항을 수행하기 위해 어떤 일을 처리해야 하는지 알고있는 객체.
Light 클래스에 On, Off 메소드가 있고 거기서 콘솔 출력을 한다.
인보커(SimpleRemoteControl)에는 커맨드가 들어있다.
커맨드의 execute()를 호출함으로 커맨드 객체에게 특정 작업을 수행해 달라는 요구를 한다.

참고로 이 챕터에서는 널 객체에 대해서도 배웠었다.
널 객체는 딱히 리턴할 객체는 없지만 클라이언트 쪽에서 null을 처리하지 않아도 되도록 하고 싶을 때 활용한다.
말 그대로 아무런 기능이 없는 객체이다.
******************************/

/*****************************
7장 Adapter 패턴과 Facade 패턴

먼저 어댑터.
어댑터는 간단히 말해 칠면조를 닭처럼 보이게 만드는 패턴이다.

Pattern #08 Adapter
한 클래스의 인터페이스를 클라이언트에서 사용하고자 하는 다른 인터페이스로 변환한다.
어댑터를 이용하면 인터페이스 호환성 문제 때문에 같이 쓸 수 없는 클래스들을 연결해서 쓸 수 있다.

Client  ------------> <<Interface>> Target
                      request()
                               ∧
                               │
                               │
                               │
                               │ (Implements)
                      Adapter               ------------> Adaptee
                      request()                           specificRequest()
                   
이 클래스 다이어그램을 보면 어댑터 패턴은 사실상 끝.
클라이언트에서는 그냥 타겟의 request()를 호출한다.
클라이언트 입장에서는 내가 호출하는게 진짜 닭인지, 닭모양 틀에 들어간 칠면조인지,
알 필요 없다. 알 수도 없고.

        void TestDuck(IDuck duck)
        {
            duck.Quack();
            duck.Fly();
        }
이 메소드에 MallardDuck을 넣든, TurkeyAdapter를 넣든 똑같다.
하지만 Turkey를 넣을 순 없다. 뭔말인지 알겠지.

Pattern #09 Facade
어떤 서브시스템의 일련의 인터페이스에 대한 통합된 인터페이스를 제공한다.
퍼사드에서 고수준 인터페이스를 정의하기 때문에 서브시스템을 더 쉽게 사용할 수 있다.

퍼사드는 간단히 말해서, 복잡한 여러 인터페이스를 묶어 하나의 쉬운 인터페이스로 만들어준다.
어찌보면 문자 그대로 "인터페이스"에 어울리는 패턴이 아닐까.

퍼사드 패턴으로 클라이언트가 복잡한 과정을 거칠 필요 없이,
WatchMovie() 한번으로 영화를 볼 수 있게 할 수 있다.

클라이언트가 다음과 같이 홈 시어터를 구성했다.
            Amplifier amp = new Amplifier();
            Tuner tuner = new Tuner();
            DvdPlayer dvd = new DvdPlayer();
            CdPlayer cd = new CdPlayer();
            Projector projector = new Projector();
            Screen screen = new Screen();
            TheaterLights lights = new TheaterLights();
            PopcornPopper popper = new PopcornPopper();
여기서 각 인스턴스의 메소드를 직접 호출할 필요 없이,
            HomeTheaterFacade homeTheater =
                new HomeTheaterFacade(amp, tuner, dvd, cd, projector, screen, lights, popper);
            homeTheater.WatchMovie("The Shawshank Redemption");
이렇게 영화를 본다는 것.
물론 저 클래스들은 퍼사드 객체를 구성한다.

퍼사드는 최소 지식 원칙을 지키게 해준다.
==============================
최소 지식 원칙 - 정말 친한 친구하고만 얘기하라.
==============================
이 원칙은 여러 클래스들이 복잡하게 얽혀서 시스템의 한 부분을 변경했을 때
다른 부분까지 줄줄이 고쳐야 되는 상황을 미리 방지해준다.

참고로 최소 지식 원칙을 지키기 위한 메소드 호출 규칙.
아래에 속하는 객체의 메소드만 호출한다.
 - 객체 자체
 - 메소드에 매개변수로 전달된 객체
 - 그 메소드에서 생성하거나 인스턴스를 만든 객체
 - 그 객체에 속하는 구성요소 ("A에는 B가 있다" 관계)
******************************/

// Anteater