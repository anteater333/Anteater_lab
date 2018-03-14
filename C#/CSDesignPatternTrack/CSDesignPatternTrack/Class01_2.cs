using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
이제 Duck의 행동들을 구현해보자.
******************************/

namespace CSDesignPatternTrack
{

/*****************************
날 수 있는 클래스에서는 무조건 FlyBehavior 인터페이스를 구현해야한다.
즉 날 수 있는 클래스를 새로 만들 때는 무조건 fly 메소드를 구현해야한다는 뚯.
******************************/
    /// <summary>
    /// Fly 행동 클래스 집합
    /// </summary>
    interface IFlyBehavior
    {
        void Fly();
    }
    namespace FlyBehavior
    {
        /// <summary>
        /// 날개가 달린 오리들의 동작
        /// </summary>
        class FlyWithWings : IFlyBehavior
        {
            private string name;

            public FlyWithWings(string name)
            {
                this.name = name;
            }

            public void Fly()
            {
                Console.WriteLine(name + ": Fly Fly!");
            }
        }

        /// <summary>
        /// 날 수 없는 오리들의 동작
        /// </summary>
        class FlyNoWay : IFlyBehavior
        {
            private string name;

            public FlyNoWay(string name)
            {
                this.name = name;
            }

            public void Fly()
            {
                Console.WriteLine(name + ": ...?");
            }
        }
    }

/*****************************
FlyBehavoir와 동일.
******************************/
    /// <summary>
    /// Quack 행동 클래스 집합
    /// </summary>
    interface IQuackBehavior
    {
        void Quack();
    }
    namespace QuackBehavior
    {
        /// <summary>
        /// 꽥꽥 소리를 냄
        /// </summary>
        class Quacker : IQuackBehavior
        {
            private string name;

            public Quacker(string name)
            {
                this.name = name;
            }

            public void Quack()
            {
                Console.WriteLine(name + ": Quack Quack!");
            }
        }

        /// <summary>
        /// 삑삑 소리를 냄
        /// </summary>
        class Squeaker : IQuackBehavior
        {
            private string name;

            public Squeaker(string name)
            {
                this.name = name;
            }

            public void Quack()
            {
                Console.WriteLine(name + ": Squeak Squeak!");
            }
        }

        /// <summary>
        /// 소리를 못 냄
        /// </summary>
        class MuteQuacker : IQuackBehavior
        {
            private string name;

            public MuteQuacker(string name)
            {
                this.name = name;
            }

            public void Quack()
            {
                Console.WriteLine(name + ": ...?");
            }
        }
    }
}

/*****************************
추가적으로, 
위와 같이 디자인 함으로써 다른 형식의 객체에서도 나는 행동과 꽥꽥거리는 행동을 재사용할 수 있다.
왜냐면 이제 더이상 Duck 행동에 그런 행동들이 종속되지 않기 때문.
또한 기존의 행동 클래스를 수정하거나 날아다니는 행동을 사용하는 Duck 클래스를 전혀 건드리지 않고도
새로운 행동을 추가할 수 있다.

책에서 보여준 예로는,
로켓 추진 기계 오리의 행동을 FlyBehavior에 추가한다던가.
오리 호출기 클래스에 IQuackBehavior 를 활용한다던가.

그리고 행동만을 나타내는 클래스라고 해서 꼭 메소드 하나만을 포함할 필요는 없다.
******************************/

/*****************************
이제 Duck 클래스를 다시 구현해보자.
앞서 구현했던 안좋은 Duck 클래스랑 이름이 충돌하니까 이름을 GoodDuck으로.
******************************/

namespace CSDesignPatternTrack
{
    abstract partial class GoodDuck
    {
        protected string name;

        protected IFlyBehavior flyBehavior;
        protected IQuackBehavior quackBehavior;
        
        // 새롭게 추가된 메소드들. Fly()와 Quack()을 대체함.
        public void PerformFly()
        {
            flyBehavior.Fly();  // 모든 서브 Duck들에게는 IFlyBehavior 인터페이스를 구현하는 것에대한 레퍼런스가 있다.
        }
        public void PerformQuack()
        {
            quackBehavior.Quack();
        }

        public void Swim()
        {
            Console.WriteLine(name + ": Swim Swim!");
        }

        public abstract void Display();
    }

    class RedheadDuck : GoodDuck
    {
        public RedheadDuck()
        {
            name = "RedheadDuck";
            flyBehavior = new FlyBehavior.FlyWithWings(name);
            quackBehavior = new QuackBehavior.Quacker(name);
        }

        public override void Display()
        {
            Console.WriteLine("This is a " + name);
        }
    }

    class GoodRubberDuck : GoodDuck
    {
        public GoodRubberDuck()
        {
            name = "RubberDuck";
            flyBehavior = new FlyBehavior.FlyNoWay(name);
            quackBehavior = new QuackBehavior.Squeaker(name);
        }

        public override void Display()
        {
            Console.WriteLine("This is a " + name);
        }
    }
}

/*****************************
상위 클래스에서 인터페이스 멤버 변수를 만듬
=> 하위 클래스에서 인터페이스에 맞는 클래스 집합 중 해당되는 클래스의 인스턴스를 할당.

이제 테스트 해보자.
******************************/

namespace CSDesignPatternTrack
{
    partial class Class01
    {
        /// <summary>
        /// strategy pattern
        /// </summary>
        public Class01()
        {
            GoodDuck redheadDuck = new RedheadDuck();
            redheadDuck.Display();
            redheadDuck.PerformFly();
            redheadDuck.PerformQuack();

            GoodDuck rubberDuck = new GoodRubberDuck();
            rubberDuck.Display();
            rubberDuck.PerformFly();
            rubberDuck.PerformQuack();

            Section02();
        }
    }
}

/*****************************
동적으로 행동을 지정할 수도 있다.
예를들어, 슬프지만 오리가 시뮬레이션을 하는 중 날개를 다쳤을 경우, 오리는 더이상 날 수 없어야 한다.
동적으로 행동을 지정하려면 행동 형식을 프로퍼티(java에선 setter)로 설정하는 방법을 사용하면 된다.
******************************/

namespace CSDesignPatternTrack
{
    abstract partial class GoodDuck
    {
        public string Name
        {
            get
            {
                return name;
            }
        }

        public IFlyBehavior FlyBehavior
        {
            get { return this.flyBehavior; }
            set { this.flyBehavior = value; }
        }
        public IQuackBehavior QuackBehavior
        {
            get { return this.quackBehavior; }
            set { this.quackBehavior = value; }
        }
    }

    partial class Class01
    {
        private void Section02()
        {
            Console.WriteLine(" = Section #02 = ");
            GoodDuck redheadDuck = new RedheadDuck();
            redheadDuck.Display();
            redheadDuck.PerformFly();
            Console.WriteLine("NO! Redhead hurt his wings!");
            redheadDuck.FlyBehavior = new FlyBehavior.FlyNoWay(redheadDuck.Name); // 매개변수 불편함. 책이랑은 다른 부분.
            redheadDuck.PerformFly();
        }
    }
}

/*****************************
위 코드에서는 GoodDuck 클래스에 프로퍼티를 추가해 동적으로 FlyBehavior.* 인스턴스를 할당하고 있다.
******************************/

/*****************************
SimUDuck의 예에서 나온 행동 클래스 집합들은 일반화해서 생각하면 알고리즘군으로 여겨질 수 있다.
p.60에 이에 캡슐화된 SimUDuck 프로그램을 나타내는 그림이 있다. 참고.

"A는 B이다." 관계는 상속을 나타낸다.
"A에는 B가 있다."에 대해서 생각해보자.
각 오리에는 FlyBehavior와 QuackBehavior가 있으며, 각각 나는 행동과 꽥꽥거리는 행동을 위임받는다.
두 클래스를 이런 식으로 합치는 것을 "구성"을 이용하는 것이라고 부른다.
즉 오리 클래스에서는 행동을 상속받는 대신, 올바른 행동 객체로 구성됨으로써 행동을 부여받게 된다.

세 번째 디자인 원칙
==============================
상속보다는 구성을 활용한다.
==============================

구성을 통해 시스템을 만들면 유연성을 크게 향상시킬 수 있다.
단순히 알고리즘군을 별도의 클래스 집합으로 캡슐화할 수 있도록 만들어주는 것 뿐 아니라,
구성요소로 사용하는 객체에게 올바른 행동 인터페이스를 구현하기만 하면 실행시에 행동을 바꿀 수도 있게 해 준다. (동적)
******************************/

/*****************************
어머나 세상에! 이렇게 첫 번째 패턴을 배웠다! 패턴 도감에 추가하자!

Pattern #01 Strategy
스트래티지 패턴에서는 알고리즘군을 정의하고 각각을 캡슐화하여 교환해서 사용할 수 있도록 만든다.
스트래티지 패턴을 활용하면 알고리즘을 사용하는 클라이언트와는 독립적으로 알고리즘을 변경할 수 있다.
******************************/

/*****************************
오늘은 첫날이니까 끝으로 디자인 패턴을 왜 배우는가에 대해 짚고 넘어가자.
뭐 큰 결심을 한건 아니고 그냥 책에 나온다.

미리 정의된 용어를 사용할 수 있게되고(마치 커피 주문을 하는 것 처럼. 커피에 뜨거운 우유를 넣어주세요 대신 라떼로 주세요),
패턴수준에서 이야기를 함으로써 디자인에 더 오랫동안 집중할 수 있다.
그리고 전문용어는 신참 개발자들에게 훌륭한 자극제가 된다고 한다. 공감되는 말이다.

참고로 디자인 패턴은 라이브러리보다 높은 단계에 있다고 생각하면 된다.
따라서 라이브러리나 프레임워크를 쓰듯이 미리 정의된 패턴을 불러오고 그런건 힘들다는 말.
대신에 패턴 카탈로그라는것이 있다고 한다. 천천히 알아보자.
******************************/

// Anteater