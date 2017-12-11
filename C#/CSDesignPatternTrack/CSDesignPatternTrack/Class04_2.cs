/*****************************
* 2017.12.11 디자인 패턴
* 목표 : Chapter 04 - Factory 패턴 (2)
     ******* 코멘트 *******
지난 시간에 뭘 배웠었지.

바뀌는 부분을 캡슐화하라.
객체를 생성하는 코드를 캡슐화하라.
구상 클래스의 인스턴스를 만드는 코드가 있다면 그 부분은 쉽게 바뀔 수 있다.
팩토리는 인스턴스를 만드는 행동을 캡슐화하는 테크닉이다.
팩토리를 씀으로써,
객체 생성 코드를 한 객체 또는 메소드에 넣음으로써 코드 중복을 제거할 수 있고,
클라이언트 입장에서는 객체 인스턴스를 만들 때 필요한 구상 클래스가 아닌 인터페이스만 필요로 하게 된다.
그 덕분에 인터페이스를 바탕으로 프로그래밍을 할 수 있고, 유연성과 확장성이 증가한다.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
오늘은 이번 챕터의 두 번째 패턴을 배우려 한다.

우선 그 전에, 객체 의존성에 대해서 살펴보자.
객체 인스턴스를 직접 만들면 구상 클래스에 의존해야한다.
이번 챕터에서 제일 처음으로 팩토리를 써서 만든 코드를 생각해보자.
모든 피자 객체를 팩토리에 맡겨서 만들지 않고 PizzaStore.CreatePizza() 메소드에서 직접 만들었다.
즉 PizzaStore는 직접적으로 모든 피자 객체들에게 의존하게 된다. 정확히는 Pizza 클래스 구현에 의존한다.
의존 당하는 클래스의 구현이 변경되면 의존 하는 클래스까지 고쳐야 할 가능성이 생긴다.
이 경우엔 PizzaStore가 심하게 의존적이고, 새로운 피자 종류를 추가하면 의존성은 더 증가한다.
쉬운 이해를 원한다면 p.176의 그림을 보면 된다.

디자인 원칙. 의존성 뒤집기 원칙.
==============================
추상화된 것에 의존하도록 만들어라.
구상 클래스에 의존하도록 만들지 않도록 한다.
==============================

이 원칙은 고수준 구성요소가 저수준 구성요소에 의존하면 안된다는 것을 뜻한다.
항상 추상화에 의존하도록 만들어야 한다.
여기서 고수준 구성요소는 다른 저수준 구성요소에 의해 정의되는 행동이 들어있는 구성요소를 뜻한다.
예를 들어, PizzaStore의 행동은 피자에 의해 정의되기 때문에, PizzaStore는 고수준 구성요소이다.

이 원칙이 적용된 다이어그램은 p.178에서 확인할 수 있다.
Pizza 라는 추상 클래스를 만들어 고수준 구성요소인 PizzaStore와 저수준 구성요소인 피자 객체들이 모두
추상 클래스인 Pizza에 의존하게 된다.

의존성 뒤집기 원칙을 지키는 디자인을 설계한다는 것은,
객체지향 디자인을 할 때 일반적으로 생각하는 방법과는 반대로, 뒤집어서 생각해서 설계한다는 것을 의미한다.

다시 PizzaStore를 예로 들어보자.
PizzaStore, 고수준 구성요소를 먼저 생각하면서 위에서 아래로 구상 클래스까지 쭉 만들어간다.
하지만 앞서 확인했듯 피자 가게에서 구상 피자 형식을 직접 알고 있게 하면 문제가 생길 수 있다. 구상 클래스에 의존하게된다.
여기서 순서를 뒤집어 Pizza에 대해서 먼저 생각해보고, 어떤 것을 추상화시킬 수 있을지 생각한다.

이 원칙을 지키는 데 있어 도움이 될 가이드라인.
 - 어떤 변수에도 구상 클래스에 대한 레퍼런스를 저장하지 않는다.
 - 구상 클래스에서 유도된 클래스를 만들지 않는다.
 - 베이스 클래스에 이미 구현되어 있던 메소드를 오버라이드하지 않는다.

다만 지키기를 권장할 뿐, 불가능한 경우도 있다. String 같은 객체는 그냥 만들어서 쓰잖아. 그렇다고 그게 해로운 것도 아니고.
******************************/

/*****************************
피자 가게로 돌아와서...
새로운 프레임워크를 통해 PizzaStore는 성공을 거두었지만,
몇몇 분점에서 자잘한 재료를 더 싼 재료로 바꿔서 원가를 줄이고 마진을 올린다는 소식이 들려왔다.
브랜드에 타격을 줄 수 있는 이번 사항을 해결하기 위해 조치를 취하려고 한다.

어떻게 분점에서 좋은 재료를 사용하도록 관리할 수 있을까?
원재료를 생산하는 공장(팩토리)을 만들고 분점까지 재료를 배달하도록 한다.
하지만 문제점이 하나 있다. 분점이 서로 멀리 떨어져 있고, 뉴욕의 레드 소스와 시카고의 레드소스는 서로 다르다는것.
그래서 뉴욕으로 배달되는 재료들과 시카고로 배달되는 재료들이 서로 달라진다.
분점마다 서로 다른 종류의 재료들을 제공하기 위해 "원재료군"을 처리할 방법을 생각해야한다.
ex.
시카고의 원재료군 [FrozenClams, PlumTomatoSauce, ThickCrustDough, MozzarellaCheese]
뉴욕의 원재료군   [FreshClams, MarinaraSauce, ThinCrustDough, ReggianoCheese]

원재료를 생산하기 위한 공장을 만들어 보자.
******************************/

namespace Pizza02
{
    #region Ingredients
    public class Dough { }
    public class Sauce { }
    public class Cheese { }
    public class Veggies { }
    public class Pepperoni { }
    public class Clams { }
    #endregion
    public interface IPizzaIngredientFactory
    {
        Dough CreateDough();
        Sauce CreateSauce();
        Cheese CreateCheese();
        Veggies[] CreateVeggies();
        Pepperoni CreatePepperoni();
        Clams CreateClam();
    }
}

/*****************************
여기서 할 일들.
1. 지역별로 팩토리를 만든다. 각 생성 메소드를 구현하는 PizzaIngredientFactory 클래스를 만들어야한다.
2. ReggianoCheese, RedPeppers, ThickCrustDough와 같이 팩토리에서 사용할 원재료 클래스들을 구현한다.
3. 새로 만든 원재료 공장을 PizzaStore 코드에서 사용하도록 함으로써 모든 것을 하나로 묶어준다.
******************************/

namespace Pizza02
{
    #region Ingredients
    public class ThinCrustDough : Dough { }
    public class MarinaraSauce : Sauce { }
    public class ReggianoCheese : Cheese { }
    #region Veggies
    public class Garlic : Veggies { }
    public class Onion : Veggies { }
    public class Mushroom : Veggies { }
    public class RedPepper : Veggies { }
    #endregion
    public class SlicedPepperoni : Pepperoni { }
    public class FreshClams : Clams { }
    #endregion

    public class NYPizzaIngredientFactory : IPizzaIngredientFactory
    {
        public Cheese CreateCheese()
        {
            return new ReggianoCheese();
        }

        public Clams CreateClam()
        {
            return new FreshClams();
        }

        public Dough CreateDough()
        {
            return new ThinCrustDough();
        }

        public Pepperoni CreatePepperoni()
        {
            return new SlicedPepperoni();
        }

        public Sauce CreateSauce()
        {
            return new MarinaraSauce();
        }

        public Veggies[] CreateVeggies()
        {
            Veggies[] veggies = {new Garlic(), new Onion(), new Mushroom(), new RedPepper()};
            return veggies;
        }
    }
}

/*****************************
이제 피자 클래스를 변경하자. 시카고 재료공장을 직접 구현하는건 사실상 무의미한 반복같으니 대충 복붙해보고.
그리고 더이상 피자마다 지역별로 클래스를 따로 만들 필요가 없다.
지역별로 다른 점은 원재료 공장에서 커버가 된다.
******************************/

namespace Pizza02
{
    public abstract class Pizza
    {
        public string Name { get; set; }
        protected Dough dough;
        protected Sauce sauce;
        protected Veggies[] veggies;
        protected Cheese cheese;
        protected Pepperoni pepperoni;
        protected Clams clam;

        public abstract void Prepare();

        public virtual void Bake()
        {
            Console.WriteLine("Bake for 25 minutes at 350");
        }
        public void Cut()
        {
            Console.WriteLine("Cutting the pizza into diagonal slices");
        }
        public virtual void Box()
        {
            Console.WriteLine("Place pizza in official PizzaStore box");
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class CheesePizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;

        public CheesePizza(IPizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + Name);

            dough = ingredientFactory.CreateDough();    /////////////////////////////
            sauce = ingredientFactory.CreateSauce();    // 팩토리가 작동하는 부분. //
            cheese = ingredientFactory.CreateCheese();  /////////////////////////////
        }
    }

    public class ClamPizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;

        public ClamPizza(IPizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            Console.WriteLine("Preparing " + Name);

            dough = ingredientFactory.CreateDough();
            sauce = ingredientFactory.CreateSauce();
            cheese = ingredientFactory.CreateCheese();
            clam = ingredientFactory.CreateClam();
        }
    }

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

    public class NYPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string type)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory = new NYPizzaIngredientFactory(); // 뉴욕 재료공장

            if (type.Equals("cheese"))
            {
                pizza = new CheesePizza(ingredientFactory)  // 이제 각 피자를 만들 때 사용할 재료를 만들기 위해 쓸 팩토리를
                {                                           // 각 피자 객체에 전달해준다.
                    Name = "New York Style Cheese Pizza"
                };
            }
            else if (type.Equals("clam"))
            {
                pizza = new ClamPizza(ingredientFactory)
                {
                    Name = "New York Style Clam Pizza"
                };
            }

            return pizza;
        }
    }
}

/*****************************
지금까지 한 걸 정리해보자.
"추상 팩토리"라고 부르는 새로운형식의 팩토리를 도입해서,
서로 다른 피자에서 필요로 하는 원재료군을 생산하기 위한 방법을 구축했다.
추상 팩토리를 통해 제품군을 생성하기 위한 인터페이스를 제공하고,
이를 이용하는 코드를 만들어 코드를 제품을 생산하는 실제 팩토리와 분리할 수 있다. 끝없이 나오는 인터페이스에 맞춘 구현.
코드가 실제 제품과 분리되어 있으므로 다른 공장을 사용해서 다른 결과를 얻을 수 있다.

이제 주문 과정을 테스트해보자.
******************************/

namespace CSDesignPatternTrack
{
    using Pizza02;
    partial class Class04
    {
        private void Section02()
        {
            Console.WriteLine(" = Section #02 = ");
            /// 태초의 뉴욕에... 피자가게가 있으라.
            PizzaStore nyPizzaStore = new NYPizzaStore();

            /// 치즈 피자를 주문한다.
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
        }
    }
}

/*****************************
Pattern #05 Abstract Factory
추상 팩토리 패턴에서는 인터페이스를 이용하여 서로 연관된,
또는 의존하는 객체를 구상 클래스를 지정하지 않고도 생성할 수 있다.

간단히 말해 제품군을 만들 때 쓸 수 있는 패턴.
추상 팩토리 패턴을 사용하면 클라이언트에서 추상 인터페이스를 통해서 일련의 제품들을 공급받을 수 있다.
이때, 실제로 어떤 제품이 생산되는지 클라이언트는 전혀 알 필요가 없다.
따라서 클라이언트와 팩토리에서 생산되는 제품을 분리시킬 수 있다.
다이어그램은 지나치게 복잡하기 때문에 p.194 참고.

끝으로,
추상 팩토리 패턴 뒤에는 팩토리 메소드 패턴이 숨어있는 것인가?
추상 팩토리 패턴에서 메소드가 팩토리 메소드로 구현되는 경우도 있다.
추상 팩토리는 원래 일련의 제품들을 생성할 때 쓰일 인터페이스를 정의하기 위해 만들어진 것이기 때문.
그 인터페이스에 있는 각 메소드는 구상 제품을 생산하는 일을 맡고 있고,
추상 팩토리의 서브클래스를 만들어서 각 메소드의 구현을 제공한다.
따라서 추상 팩토리 패턴에서 제품을 생산하기 위한 메소드를 구현하는데 있어서 팩토리 메소드를 사용하는것은
너무나도 자연스러운 일이라고 한다. 그냥 그렇다고 한다...

이번 챕터에서 배운 두 패턴을 잘 비교해보고, 클래스 다이어그램도 살펴보자.
지금은 피곤해서 내가 과연 할지는 모르겠지만, 아무튼 그러자. 아무튼.
******************************/
