/*****************************
* 2017.12.08 디자인 패턴
* 목표 : Chapter 04 - Factory 패턴 (1)
     ******* 코멘트 *******
 "객체지향 빵 굽기"

오늘의 목표는 느슨한 결합을 이용하는 객체지향 디자인이라고 한다.
챕터의 제목은 바로 팩토리 패턴!
살짝 목차를 엿본 결과, 분량이 장난이 아니란걸 알게 되었다.
하루종일 이것만 하겠는데. 껄껄껄.

단순히 새로운 연산자를 만드는 것 보다는 객체를 만드는 과정이 더 중요하다.
객체의 인스턴스를 만드는 작업이 항상 공개될 필요는 없다.
모든 것을 공개했다가는 결합과 관련된 문제가 생길 수 있다.
******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
new 키워드에 대해 생각해보자.
지금껏 계속해서 특정 구현을 바탕으로 프로그래밍 하는 것이 위험하다고 했는데,
new를 쓸 때마다 결국은 특정 구현을 사용하게 된다.

new가 뜻하는 것은 뭘까?
new 키워드는 구상 클래스의 인스턴스를 만들어준다.
아래 코드를 보자.
Duck duck = new MallardDuck();
인터페이스를 써서 코드를 유연하게 만들려했지만,
결국엔 new로 구상 클래스의 인스턴스를 만들어야한다.
그러니까 여러 종류의 구상 클래스 중 하나를 만들어야 할 때,
아래와 같이 코드를 짤 수 밖에 없게될 수 있다는것.
Duck duck;
if (picnic)
    duck = new MallardDuck();
else if (hunting)
    duck = new DecoyDuck();
else if (inBathTub)
    duck = new RubberDuck();
딱 봐도 변경, 확장에 불리해보인다.

근데 이게 new 자체의 문제라곤 할 순 없다.
사실 new를 어떻게 쓰느냐의 문제.
첫 번째 디자인 원칙을 다시 생각해보자.
"바뀔 수 있는 부분을 찾아내서 바뀌지 않는 부분과 분리해야한다."
즉 구상 클래스의 인스턴스를 만드는 부분을
애플리케이션의 나머지 부분으로부터 분리/캡슐화 시켜야 한다.
******************************/

/*****************************
오늘의 예제는 바로 피자. 공교롭게도 오늘 아침은 피자를 먹었다.
어쨌든, 다음과 같이 피자 주문을 나타내는 코드를 만들었다.

Pizza orderPizza()
{
    Pizza pizaa = new Pizza();

    pizza.prepare();
    pizza.bake();
    pizza.cut();
    pizza.box();
    return pizza;
}

당연히 피자 종류는 여러가지가 있다. 예를 들면 하와이안 피자 같은거.

Pizza orderPizza(string type)
{
    Pizza pizaa;

    if (type.equals("cheese"))
        pizza = new CheesePizza();
    else if (type.equals("greek"))
        pizza = new GreekPizza();
    else if (type.equals("hawaiian"))
        pizza = new HawaiianPizza();

    pizza.prepare();
    pizza.bake();
    pizza.cut();
    pizza.box();
    return pizza;
}

점점 답이 없어지는 느낌이 든다.
저기에 페퍼로니 피자를 추가하려면
else if (type.equals("pepperoni"))
    pizza = new PepperoniPizza();
이런 식으로 코드를 추가해야한다.

문제가 많은 객체 생성 부분을 캡슐화하자.
객체 생성 코드만 따로 빼서 피자 객체를 만드는 일만 전담하는 다른 객체에 집어넣도록 한다.
객체의 이름에는 팩토리라는 이름을 붙이도록 한다. SimplePizzaFactory.
orderPizza() 메소드는 이제 더 이상 어떤 피자를 만들어야 하는지 고민하지 않아도 되고,
새로 만든 객체의 클라이언트가 된다. 그냥 피자 하나 만들어 주세요 하면 된다는것.
물론 그 캡슐화한 부분을 어떻게 구현할지는 또 생각해야할 문제지만,
우선은 간단하게 클래스를 먼저 구현해보고 생각하자.
******************************/

namespace PizzaProto
{
    class Pizza
    {
        public void Prepare() { }
        public void Bake() { }
        public void Cut() { }
        public void Box() { }
    }
    class CheesePizza : Pizza { }
    class GreekPizza : Pizza { }
    class HawaiianPizza : Pizza { }
    
    class PizzaStore
    {
        SimplePizzaFactory factory;

        public PizzaStore(SimplePizzaFactory factory)
        {
            this.factory = factory;
        }

        public Pizza OrderPizza(string type)
        {
            Pizza pizza;

            pizza = factory.CreatePizza(type);  // 이제 new는 팩토리에서만.

            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }
    }

    class SimplePizzaFactory
    {
        public Pizza CreatePizza(String type)
        {
            Pizza pizza = null;

            // OrderPizza에 있던 코드를 그대로 가져왔다.
            if (type.Equals("cheese"))
                pizza = new CheesePizza();
            else if (type.Equals("greek"))
                pizza = new GreekPizza();
            else if (type.Equals("hawaiian"))
                pizza = new HawaiianPizza();

            return pizza;
        }
    }
}

/*****************************
물론 이런 간단한 팩토리를 디자인 패턴이라고 말할 수는 없다.
그냥 프로그래밍에 자주 사용되는 관용구 정도?

p.155의 클래스 다이어그램을 참고해서 피자가 생성되는 과정을 확인해보자.
******************************/

/*****************************
지금까진 워밍업이었다.
간단한 팩토리를 도입한 PizzaStore가 큰 성공을 거둬 프랜차이즈 사업을 시작하기로 하였다.
앞서 써왔던 팩토리 코드를 다른 지점에서도 쓸 수 있겠지만,
지역별로 조금씩 다른 차이점을 적용하고싶다.
SimplePizzaFactory를 응용해서 서로 다른 팩토리를 만든 다음,
PizzaStore에서 적당한 팩토리를 사용하도록 하면 분점에서도 전혀 문제 없이 슬 수 있을것이다.
NYPizzaFactory nyFactory = new NYPizzaFactory();    // 뉴욕 피자공장
PizzaStore nyStore = new PizzaStore(nyFactory);
nyStore.order("Veggie");

흠... 그런데 분점과 본점의 공장이 서로 다르니까...
본점이 지 멋대로 피자를 만들어 버릴수도 있는데?
피자 가게와 피자 제작 과정 전체를 하나로 묶어주는 프레임워크를 만들 필요가 있다.
물론 유연성을 챙길수도 있어야하고.
팩토리를 만들기 전의 코드를 보자.
피자를 만드는 코드가 PizzaStore와 직접 연결되어 있긴 했지만, 유연성이 부족했다.
어떻게하면 그 둘을 모두 챙길 수 있을까?

피자 가게 프레임워크.
피자를 만드는 활동 자체는 전부 PizzaStore 클래스에 국한시키면서,
분점마다 고유의 스타일을 살릴 수 있도록 하는 방법이 있다.
CreatePizza() 메소드는 PizzaStore에 다시 넣도록 한다. 대신에 이번엔 추상 메소드로 선언한다.
그렇게 각 지역마다 고유의 스타일에 맞게 PizzaStore의 서브클래스를 만들도록 한다.
******************************/

namespace Pizza
{
    public abstract class PizzaStore
    {
        public Pizza OrderPizza(string type)
        {
            Pizza pizza = CreatePizza(type);

            pizza.Prepare();
            pizza.Bake();
            pizza.Cut();
            pizza.Box();

            return pizza;
        }

        public abstract Pizza CreatePizza(string type);
    }
}

/*****************************
이제 각 분점을 위한 서브클래스가 필요하다.
주문 시스템은 이미 PizzaStore의 OrderPizza() 메소드에 잘 갖춰져 있다.
주문 자체는 모든 분점에서 똑같이 진행되어야 하고, 분점마다 달라져야 할 것은 피자의 스타일 뿐이다.
달라지는 점들을 CreatePizza() 메소드에 집어넣고 그 메소드에서 해당 스타일의 피자를 만드는 것을 모두 책임지도록 한다.

OrderPizza()를 눈여겨 보자. OrderPizza는 추상 클래스인 Pizza 객체를 가지고 작업을 한다.
OrderPizza()는 실제로 어떤 구상 클래스에서 작업이 처리되고 있는지 전혀 알 수 없다.
즉, PizzaStore와 Pizza는 완전히 분리되어있다.
OrderPizza()에서 CreatePizza()를 호출하면 PizzaStore의 서브클래스가 그 호출을 받아서 피자를 만든다.
따라서 어떤 종류의 피자가 나올지는 피자 가게의 종류가 결정하게된다.

어쨌든 PizzaStore를 만들어보자.
******************************/

namespace Pizza
{
    public class NYPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string type)
        {
            if (type.Equals("cheese"))
                return new NYStyleCheesePizza();
            else
                return null;
        }
    }
    public class ChicagoPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string type)
        {
            if (type.Equals("cheese"))
                return new ChicagoStyleCheesePizza();
            else
                return null;
        }
    }
    public class CaliPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string type)
        {
            if (type.Equals("cheese"))
                return new CaliStyleCheesePizza();
            else
                return null;
        }
    }
}

/*****************************
이제 Pizza 인스턴스를 만드는 일은 팩토리 역할을 하는 메소드, CreatePizza()에서 맡아서 처리한다.

팩토리 메소드는 객체 생성을 처리하며, 팩토리 메소드를 이용하면 객체를 생성하는 작업을 서브클래스에서 캡슐화시킬 수 있다.
이로써 수퍼클래스에 있는 클라이언트 코드와 서브클래스에 있는 객체 생성 코드를 분리시킬 수 있다.
일반화 시키면 다음과 같은 형태를 띈다.
    abstract Product factoryMethod(String type)

피자가 주문되는 과정을 한번 정리해보자.
1. 뉴욕스타일 피자를 하나 시키려한다. NYPizzaStore가 필요하다.
   PizzaStore nyPizzaStore = new NYPizzaStore();
2. 피자 가게가 확보됐으니까 주문을 받을 수 있다.
   nyPizzaStore.OrderPizza("cheese");
3. OrderPizza() 메소드에서 CreatePizza() 메소드를 호출한다. (type.Equals("cheese"))
4. OrderPizza() 메소드에서 피자 작업을 마무리한다.
   prepare, bake, cut, box

Pizza 클래스를 완성시키자.
******************************/

namespace Pizza
{
    #region Pizza
    public abstract class Pizza
    {
        protected string name;
        protected string dough;
        protected string sauce;
        public string Name
        { get { return this.name; } }
        protected ArrayList toppings = new ArrayList();

        public virtual void Prepare()
        {
            Console.WriteLine("Preparing " + Name);
            Console.WriteLine("Tossing dough...");
            Console.WriteLine("Adding sauce...");
            Console.WriteLine("Adding toppings: ");
            for (int i = 0; i < toppings.Count; i++)
                Console.WriteLine("    " + toppings[i]);
        }
        public virtual void Bake()
        {
            Console.WriteLine("Bake for 25 minutes at 350");
        }
        public virtual void Cut()
        {
            Console.WriteLine("Cutting the pizza into diagonal slices");
        }
        public virtual void Box()
        {
            Console.WriteLine("Place pizza in official PizzaStore box");
        }
    }
    public class CheesePizza : Pizza { }
    public class GreekPizza : Pizza { }
    public class HawaiianPizza : Pizza { }

    public class NYStyleCheesePizza : Pizza
    {
        public NYStyleCheesePizza()
        {
            name = "NY Style Sauce and Cheese Pizza";
            dough = "Thin Crust Dough";
            sauce = "Marinara Sauce";
            toppings.Add("Grated Regginano Cheese");
        }
    }
    public class ChicagoStyleCheesePizza : Pizza
    {
        public ChicagoStyleCheesePizza()
        {
            name = "Chicago Style Deep Dish Ceese Pizza";
            dough = "Extra Thick Crust Dough";
            sauce = "Plum Tomato Sauce";
            toppings.Add("Shredded Mozzarella Cheese");
        }

        public override void Cut()   // 각 지점, 피자마다 준비 과정에 또 다른 부분이 있을 수 있다.
        {                            // Pizza의 메소드들을 virtual로 선언하는게 좋아보인다.
            Console.WriteLine("Cutting the pizza into square slices");  // 시카고 피자는 사각형으로 잘라준다 하더라.
        }
    }
    public class CaliStyleCheesePizza : Pizza { }
    #endregion
}

/*****************************
완성된 Pizza 클래스를 가지고 테스트해보자.
******************************/

namespace CSDesignPatternTrack
{
    using Pizza;
    class Class04
    {
        public Class04()
        {
            PizzaStore nyStore = new NYPizzaStore();
            PizzaStore chicagoStore = new ChicagoPizzaStore();

            Console.WriteLine("Ethan ordered a New York Style Cheese Pizza.");
            Pizza pizza = nyStore.OrderPizza("cheese");
            Console.WriteLine();
            Console.WriteLine("Joel ordered a Chicago Style Cheese Pizza.");
            pizza = chicagoStore.OrderPizza("cheese");
        }
    }
}

/*****************************
이번 챕터에서 배울 패턴은 사실 2개다.

팩토리 패턴에서는 객체 생성을 캡슐화한다.
팩토리 메소드 패턴에서는 서브클래스에서 어떤 클래스를 만들지를 결정하게 함으로써 객체 생성을 캡슐화한다.
p.169에 있는 클래스 다이어그램을 보자.
이 패턴은 생산자(Creator) 클래스와 제품(Product) 클래스로 이루어져있다.
여기서 생산자는 PizzaStore와 그 서브클래스들,
제품은 Pizza와 서브클래스들을 뜻한다.

둘은 서로 병렬적으로 구성되어있다. 둘 다 추상 클래스로 시작하고, 그 클래스를 확장하는 구상 클래스들을 가지고 있다.
그리고 각 분점들에 대한 구체적인 구현은 구상 클래스들이 책임지고 있다.

그래서 이번 챕터의 첫 번째 패턴,

Pattern #04 Factory Method
팩토리 메소드 패턴에서는 객체를 생성하기 위한 인터페이스를 정의하는데,
어떤 클래스의 인스턴스를 만들지는 서브클래스에서 결정하게 만든다.
즉, 팩토리 메소드 패턴을 이용하면 클래스의 인스턴스를 만드는 일을 서브클래스에게 맡기게 된다.

팩토리 메소드 패턴에서 Creator 추상 클래스는 객체를 만들기 위한 메소드를 제공한다. (factoryMethod())
Creator 클래스에 구현되어 있는 다른 메소드에서는 팩토리 메소드에 의해 생산된 제품을 가지고 필요한 작업을 처리한다. (anOperation())
하지만 실제 팩토리 메소드를 구현하고 제품을 만들어내는 일은 서브클래스에서만 할 수 있다.

팩토리 메소드의 정의에서 결정한다는 표현을 쓰는 이유에 대해서 알아보자.
결정한다는 말이 이 패턴을 사용할 때 서브클래스에서 실행중에 어떤 클래스의 인스턴스를 만들지를 결정한다는 뜻이 아니라,
생산자 클래스 자체가 실제 생산될 제품에 대한 사전 지식이 전혀 없이 만들어진다는 뜻이다.
즉, 더 정확한 정의는 "사용하는 서브클래스에 따라 생산되는 객체 인스턴스가 결정된다"는 표현이 적합하다.

끝으로 p.173은 가볍게 한번 읽어보길 바라고, 오늘은 여기까지.
이번 챕터는 하루만에 끝내긴 어려울 것 같다. 내일이 주말이라 다음주로 넘어가는게 아쉽긴 하지만 별수없지.
******************************/