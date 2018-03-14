/*****************************
* 2017.12.07 디자인 패턴
* 목표 : Chapter 03 - Decorator 패턴
     ******* 코멘트 *******
 "상속맨, 디자인에 눈을 뜨다."

좋은 것은 항상 좋은 것일까?
어제 공부를 끝내고 인터넷을 하다가 안티 패턴이라는 용어를 알게 되었다.
안티 패턴이란 간단히 말해 프로그램을 짜면서 자주 사용하게 되지만,
프로그램에 있어서는 해로운 영향을 주는 패턴을 말한다.
여러 가지가 있지만, 그 중에 하나 기억나는게 "상속을 과하게 하는 것" 이었다.
프로그래밍 뿐만 아니라 삶에 있어서도 과유불급이란 말이 있듯,
"난 OOP를 배웠으니까 상속할거야 상속!" 이런 태도는 좋지 않다는 거지.
OOP나, 디자인 패턴이나, 자바같은 고생산성 언어가 나오는 이유가 뭔지 잘 되새겨 보자.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
오늘의 예제는 바로 커피샵 스타버즈. 스타벅스에서 소송을 걸어도 할 말 없다고 생각한다.
어쨌든 스타벅ㅅ.. 스타버즈에서 다양한 음료들을 모두 포괄하는 주문 시스템을 이제서야 겨우 갖추려고 하고 있는 중이다.
최초에 만들어진 클래스들은 다음과 같은 식으로 구성되어 있다.
******************************/

namespace StarbuzzProto01
{
    /// <summary>
    /// 음료를 나타내는 추상 클래스.
    /// 커피샵에서 판매되는 모든 음료는 이 클래스의 서브클래스가 된다.
    /// </summary>
    abstract class Beverage
    {
        protected string description = "";
        public string Description
        { get { return this.description; } }

        public abstract int Cost();
    }

    /// <summary>
    /// 서브클래스 예. 하우스블렌드 커피
    /// </summary>
    class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            base.description = "House Blend";
        }

        public override int Cost()
        {
            return 5000;
        }
    }

    /// 기타 등등 커피들
}

/*****************************
일단은 괜찮아 보이지만, 커피 주문에 대해서 생각을 해보자.
참고자료 https://ko.wikihow.com/%EC%8A%A4%ED%83%80%EB%B2%85%EC%8A%A4%EC%97%90%EC%84%9C-%EC%A3%BC%EB%AC%B8%ED%95%98%EA%B8%B0
위의 링크를 들어가보면 아주 컴퓨터공학개론 교과서를 보는 기분이 든다.
저 과정들에서 나올 수 있는 모든 조합을 서브클래스로 만든다? 엄청나게 비효율적임.
그래서 Beverage 클래스를 다음과 같이 개선해본다.
******************************/

namespace StarbuzzProto02
{
    abstract class Beverage
    {
        protected string description = "";
        public string Description
        { get { return this.description; } }

        // 첨가물
        public bool Milk { get; set; } = false;
        public bool Soy { get; set; } = false;
        public bool Mocha { get; set; } = false;
        public bool Whip { get; set; } = false;

        public virtual int Cost()   // 추상 메소드가 더이상 아니다.
        {
            int cost = 0;
            if (Milk)
                cost += 500;
            if (Soy)
                cost += 700;
            if (Mocha)
                cost += 1000;
            if (Whip)
                cost += 1200;
            return cost;
        }
    }
    
    class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            base.description = "Dark Roast";
        }

        public override int Cost()
        {
            int cost = 5000;
            return cost + base.Cost();
        }
    }
}

/*****************************
해결이 된건가?
나중에 첨가물 가격이 바뀔 때는 어떻게 해야 할까?
새로운 첨가물이 생길 때 마다 새로운 프로퍼티를 만들고, Cost()도 고쳐야 하지 않을까?
아이스티 같은 음료가 Whip 프로퍼티를 상속받아 버릴수도 있는데? 아이스티 + 휘핑크림?
손님이 더블 모카를 주문하면 어떻게 해야하지?
******************************/

/*****************************
처음 했던 질문에 대해서 생각해보자.
상속은 물론 강력하지만 무조건 유연한, 관리하기 쉬운 디자인을 만들어주진 않는다.
그렇다면 상속 대신에 어떻게 재사용을 가능하게 할까?
Chapter 01에서 말했던 디자인 원칙,
"상속보다는 구성을 활용한다."
만약 위의 Starbuzz 예제 처럼 서브클래스를 만드는 방식으로 행동을 상속받으면,
그 행동은 컴파일 시간에 완전히 결정된다. 정적이다.
게다가 모든 서브클래스가 똑같은 행동을 상속받는다.
구성은 객체의 행동을 동적으로 설정할 수 있게 해준다.

디자인 원칙. OCP(Open-Closed Principle)
==============================
클래스는 확장에 대해서는 열려 있어야 하지만
코드 변경에 대해서는 닫혀 있어야 한다.
==============================

즉, 기존 코드는 건드리지 않은 채로 확장을 통해서 새로운 행동을 간단하게 추가할 수 있도록 해야 한다.
어떻게보면 모순된 말처럼 들린다. 하지만 이런걸 가능하게 해주는 것이 디자인 패턴.
******************************/

/*****************************
이제 이 원칙을 스타버즈의 사례에 추가하는 방법에 대해 생각해보자.
앞서 시도했던 두 방식은 서브 클래스가 너무 많아지거나,
서브 클래스가 불필요한 기능까지 상속받는 경우가 생겼다.

이번에 사용할 방법은 다음과 같다.
음료를 하나 선택하고, 이를 장식한다.
1. DarkRoast 객체를 가져온다.
2. Mocha 객체로 장식한다.
3. Whip 객체로 장식한다.
4. Cost() 메소드를 호출한다. 이때 첨가물의 가격을 계산하는 일은 해당 객체들에게 위임된다.

말은 쉬운데 그럼 어떻게 장식하고 위임할까?
p.127 참고
1. DarkRoast 객체에서 시작한다.
2. 손님이 모카를 주문했으니 Mocha 객체를 만들고 그 객체로 DarkRoast를 감싼다.
   여기서 Mocha 객체가 데코레이터이다. 이 객체의 형식은 이 객체가 장식하고 있는 객체를 반영한다.
   따라서 이 경우엔 Beverage를 반영한다.(== 같은 형식을 갖는다.)
   그러니까 Mocha 또한 Cost() 메소드를 가지고 있고, 다형성을 통해 Mocha가 감싸고 있는 것도 Beverage 객체로 간주할 수 있다.
3. 손님이 휘핑 크림도 같이 주문했으니 Whip 데코레이터를 만들고 그 객체로 Mocha를 감싼다.
   2번 과정과 같다. 이 과정이 머리속에서 그려지면 좋을듯. Whip[ Mocha[ DarkRoast ] ]
4. 이제 가격 계산을 시작한다. 가장 바깥쪽에 있는 데코레이터인 Whip의 Cost()를 호출한다.
   그러면 Whip에서는 그 객체가 장식하고 있는 객체한테 가격 계산을 위임한다.
   가격이 구해지고 나면, 구해진 가격에 휘핑 크림의 가격을 더한 다음 그 결과를 리턴한다.
   마치 재귀함수가 호출되듯이 작동된다.
******************************/

/*****************************
지금까지 배운 내용을 정리해보자.
 - 데코레이터의 수퍼클래스는 자신이 장식하고 있는 개체의 수퍼클래스와 같다.
 - 한 객체를 여러 개의 데코레이터로 감쌀 수 있다.
 - 데코레이터는 자신이 감싸고 있는 객체와 같은 수퍼클래스를 가지고 있기 때문에
   원래 객체가 들어갈 자리에 데코레이터 객체를 집어넣어도 상관없다.
 - 데코레이터는 자신이 장식하고 있는 객체에게 어떤 행동을 위임하는 것 외에
   원하는 추가적인 작업을 수행할 수 있다. (Key point)
 - 객체는 언제든지 감쌀 수 있기 때문에 실행중에 필요한 데코레이터를 마음대로 적용할 수 있다.

Pattern #03 Decorator
데코레이터 패턴에서는 객체에 추가적인 요건을 동적으로 첨가한다.
데코레이터는 서브클래스를 만드는 것을 통해서 기능을 유연하게 확장할 수 있는 방법을 제공한다.

어제 옵저버 패턴의 클래스 다이어그램을 직접 그렸는데, 클래스 다이어그램이 점점 복잡해진다...
따라서,
참고자료 https://en.wikipedia.org/wiki/Decorator_pattern#/media/File:Decorator_UML_class_diagram.svg
대충 클래스들의 관계만 설명하자면,
Component (Beverage) 
└ ConcreteComponent (DarkRoast)
└ Decorator (CondimentDecorator) 각 데코레이터 안에는 Component 객체가 들어있다.
                                  즉, 데코레이터에는 구성요소에 대한 레퍼런스가 들어있는 인스턴스 변수가 있다.
   └ ConcreteDecorator (Whip) Component 객체는 데코레이터가 장식하고 있는 것을 위한 인스턴스 변수이다. wrappedObj.

데코레이터에서 새로운 메소드를 추가할 수도 있다.
하지만 일반적으로 새로운 메소드를 추가하는 대신에
Component에 원래 있던 메소드를 호출하기 전/후에 별도의 작업을 처리하는 방식으로 새로운 기능을 추가한다.
******************************/

/*****************************
이제 제대로된 스타버즈의 시스템을 구현해보자.
******************************/

namespace Starbuzz
{
    public abstract class Beverage  /// 사실 인터페이스로 만들어도 되지만.
    {                               /// 불필요한 코드 변경은 없도록 하는 것이 좋다.
        public virtual string Description { get; protected set; } = "";

        public abstract double Cost();  /// 이번엔 책의 코드를 따라해보자. 달러니까 double.
    }

    /// <summary>
    /// 첨가물을 나타내는 추상 클래스.(데코레이터 클래스)
    /// </summary>
    public abstract class CondimentDecorator : Beverage
    {
        public abstract override string Description { get; } /// 이 코드의 이유는 나중에 알아보자.
    }

    #region ConcreteBeverage
    public class Espresso : Beverage
    {
        public Espresso()
        {
            Description = "Espresso";
        }

        public override double Cost()
        {
            return 1.99;    /// 에스프레소의 가격은 마지막으로 계산된다. 따라서 그냥 리턴 하면 끝.
        }
    }

    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            Description = "House Blend";
        }

        public override double Cost()
        {
            return .89;
        }
    }
    #endregion

    #region ConcreteCondiment
    public class Mocha : CondimentDecorator
    {
        private Beverage beverage;  /// 이건 CondimentDecorator에 있어도 되지 않을까?

        public Mocha(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override string Description
        { get { return beverage.Description + ", Mocha"; } }    /// 바로 이것을 위해 Description을 abstract로!

        public override double Cost()
        {
            return .20 + beverage.Cost();
        }
    }

    public class Whip : CondimentDecorator
    {
        private Beverage beverage;

        public Whip(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override string Description
        { get { return beverage.Description + ", Whipping Cream"; } }

        public override double Cost()
        {
            return .10 + beverage.Cost();
        }
    }
    #endregion
}

/*****************************
테스트 시작. 여기서 어떻게 데코레이션을 하는지 유심히 보도록 하자.
******************************/

namespace CSDesignPatternTrack
{
    using Starbuzz;
    class Class03
    {
        public Class03()
        {
            // 아무것도 넣지 않은 에스프레소
            Beverage beverage = new Espresso();
            Console.WriteLine(beverage.Description + " $" + beverage.Cost());

            // 하우스블렌드, 모카x2, 휘핑 추가.
            Beverage beverage2 = new HouseBlend();
            beverage2 = new Mocha(beverage2);
            beverage2 = new Mocha(beverage2);
            beverage2 = new Whip(beverage2);
            Console.WriteLine(beverage2.Description + " $" + beverage2.Cost());
        }
    }
}

/*****************************
데코레이터 패턴은 구상 구성요소의 형식을 알아내서 그 결과를 바탕으로 어떤 작업을 처리하는 코드에 적절하지 않다.
그리고 데코레이터 패턴을 쓰면 관리해야 할 객체가 늘어나고, 그만큼 비용이 증가한다.
(많은 부품은 많은 고장을 유발한다.)
따라서 데코레이터 패턴은 일반적으로 팩토리나 빌더 같은 다른 패턴을 써서 만들고 사용하게 된다.
******************************/

/*****************************
책에서는 데코레이터 패턴이 적용된 예를 보여준다. 바로 자바 I/O.
I/O 관련 클래스를 찾을 때 무슨무슨InputStream 종류가 여러가지인걸 볼 수 있다.
이것이 바로 데코레이터 패턴을 사용한 객체.
여기에 직접 입력 데코레이터를 만들 수도 있다. C#에도 있을진 모르겠네. 있겠지 뭐.
******************************/

/*****************************
끝으로, 오늘의 주제 과유불급에 맞춰 데코레이터 패턴 또한 오남용이 위험하다는 것을 짚고 가고 끝내겠다. p.142
1. 자잘한 클래스들이 지나치게 많이 추가될 수 있다. 복잡해짐.
2. 구성 요소의 클라이언트 입장에서는 데코레이터의 존재를 알 수 없다.
   따라서 특정 형식에 의존하는 코드에 데코레이터를 적용하기 곤란하다.
3. 구성 요소를 초기화하는데 코드가 복잡해진다.
******************************/

// Anteater