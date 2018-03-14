/*****************************
* 2017.12.29 디자인 패턴
* 목표 : Chapter 12 - Compound 패턴 (1)
     ******* 코멘트 *******
 "백지장도 맞들면 낫다."

대망의 마지막 패턴... 은 아니고 사실 지난시간 Proxy가 마지막 패턴이었다.
어쩐지 내용이 많더라.
컴파운드는 번역하자면 복합체. 즉, 컴파운드 패턴은 패턴들로 이루어진 패턴이다.
그말인 즉슨, 사전적으로 정의되는 패턴은 아니다.
******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
정리해서 말하자면,
일련의 패턴을 함께 사용하여 다양한 디자인 문제를 해결하는 것을 컴파운드 패턴(Compound Pattern)이라고 부른다.

오리들에게 돌아가보자.
처음부터 만들어보자.
******************************/

namespace CompoundDuck01
{
    public interface IQuackable
    {
        string Name { get; }
        void Quack();
    }
    public class MallardDuck : IQuackable
    {
        public string Name { get { return "Mallard"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Quack");
        }
    }
    public class RedheadDuck : IQuackable
    {
        public string Name { get { return "Redhead"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Quack");
        }
    }
    public class DuckCall : IQuackable  // 오리 호출기
    {
        public string Name { get { return "DuckCall"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Kwak");
        }
    }
    public class RubberDuck : IQuackable // 고무 오리
    {
        public string Name { get { return "Rubber"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Squeak");
        }
    }

    public class Goose
    {
        public void Honk()
        {
            Console.WriteLine(": Honk");
        }
    }

    // 거위to오리 어댑터
    public class GooseAdapter : IQuackable
    {
        public string Name { get { return "Goose"; } }

        Goose goose;

        public GooseAdapter(Goose goose)
        {
            this.goose = goose;
        }

        public void Quack()
        {
            Console.Write(Name);
            goose.Honk();
        }
    }

    // 꽥소리 카운터, 데코레이터
    public class QuackCounter : IQuackable
    {
        IQuackable duck;
        static int numberOfQuacks = 0;

        public string Name { get { return "Counter"; } }

        public QuackCounter(IQuackable duck)
        {
            this.duck = duck;
        }

        public void Quack()
        {
            duck.Quack();
            numberOfQuacks++;
        }

        public static int Quacks { get { return numberOfQuacks; } }

        public static void Reset()
        {
            numberOfQuacks = 0;
        }
    }

    public partial class DuckSimulator
    {
        public void Simulate01()
        {                            // 데코레이터 사용
            IQuackable mallardDuck = new QuackCounter(new MallardDuck());
            IQuackable redheadDuck = new QuackCounter(new RedheadDuck());
            IQuackable duckCall = new QuackCounter(new DuckCall());
            IQuackable rubberDuck = new QuackCounter(new RubberDuck());

            IQuackable gooseDuck = new QuackCounter(new GooseAdapter(new Goose()));   // 어댑터 사용

            Console.WriteLine("\nDuck Simulator");

            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);
            Simulate(gooseDuck);

            Console.WriteLine("The ducks quacked " + QuackCounter.Quacks + " times\n");
        }
        private void Simulate(IQuackable duck)
        {
            duck.Quack();
        }
    }
}

namespace CSDesignPatternTrack
{
    partial class Class13
    {
        public Class13()
        {
            CompoundDuck01.DuckSimulator simulator = new CompoundDuck01.DuckSimulator();
            simulator.Simulate01();

            Section02();
        }
    }
}

/*****************************
어댑터와 데코레이터를 써봤다.

이번엔 팩토리 패턴을 써보자.
팩토리 패턴으로 데코레이터를 사용하는 부분을 캡슐화 할 수 있다.
******************************/

namespace CompoundDuck01
{
    // 추상 팩토리
    public abstract class AbstractDuckFactory
    {
        public abstract IQuackable CreateMallardDuck();
        public abstract IQuackable CreateRedheadDuck();
        public abstract IQuackable CreateDuckCall();
        public abstract IQuackable CreateRubberDuck();
    }

    // 데코레이터가 없는 오리를 만드는 팩토리.
    public class DuckFactory : AbstractDuckFactory
    {
        public override IQuackable CreateDuckCall()
        {
            return new DuckCall();
        }

        public override IQuackable CreateMallardDuck()
        {
            return new MallardDuck();
        }

        public override IQuackable CreateRedheadDuck()
        {
            return new RedheadDuck();
        }

        public override IQuackable CreateRubberDuck()
        {
            return new RubberDuck();
        }
    }

    // 데코레이터를 사용하는 오리를 만드는 팩토리.
    public class CountingDuckFactory : AbstractDuckFactory
    {
        public override IQuackable CreateDuckCall()
        {
            return new QuackCounter(new DuckCall());
        }

        public override IQuackable CreateMallardDuck()
        {
            return new QuackCounter(new MallardDuck());
        }

        public override IQuackable CreateRedheadDuck()
        {
            return new QuackCounter(new RedheadDuck());
        }

        public override IQuackable CreateRubberDuck()
        {
            return new QuackCounter(new RubberDuck());
        }
    }

    public partial class DuckSimulator
    {
        public void Simulate02()
        {
            QuackCounter.Reset();

            AbstractDuckFactory duckFactory = new CountingDuckFactory();

            IQuackable mallardDuck = duckFactory.CreateMallardDuck();
            IQuackable redheadDuck = duckFactory.CreateRedheadDuck();
            IQuackable duckCall = duckFactory.CreateDuckCall();
            IQuackable rubberDuck = duckFactory.CreateRubberDuck();

            Console.WriteLine("\nDuck Simulator");

            Simulate(mallardDuck);
            Simulate(redheadDuck);
            Simulate(duckCall);
            Simulate(rubberDuck);

            Console.WriteLine("The ducks quacked " + QuackCounter.Quacks + " times\n");
        }
    }
}

namespace CSDesignPatternTrack
{
    partial class Class13
    {
        private void Section02()
        {
            Console.WriteLine(" = Section #02 = ");
            CompoundDuck01.DuckSimulator duckSimulator = new CompoundDuck01.DuckSimulator();
            duckSimulator.Simulate02();

            Section03();
        }
    }
}

/*****************************
오리들을 하나하나 관리하려니 힘들다.
오리 '떼'를 관리할 수 있게 만들자.

컴포지트 패턴이 뭔지 떠올려보자.
복합객체와 개별객체를 똑같이 처리할 수 있게 만든다.
이터레이터와 같이 사용된다.
******************************/

namespace CompoundDuck01
{
    public class Flock : IQuackable
    {
        List<IQuackable> quackers = new List<IQuackable>();

        public string Name { get { return "Flock"; } }

        public void Add(IQuackable quacker)
        {
            quackers.Add(quacker);
        }

        public void Quack()
        {
            IEnumerator<IQuackable> enumerator = quackers.GetEnumerator();  // 이터레이터 패턴
            while (enumerator.MoveNext())
            {
                IQuackable quacker = enumerator.Current;
                quacker.Quack();
            }

            /*
            foreach(IQuackable quacker in quackers)     // 이렇게 해도 된다
            {
                quacker.Quack();
            }
            */
        }
    }

    public partial class DuckSimulator
    {
        public void Simulate03()
        {
            QuackCounter.Reset();

            AbstractDuckFactory duckFactory = new CountingDuckFactory();

            IQuackable mallardDuck = duckFactory.CreateMallardDuck();
            IQuackable redheadDuck = duckFactory.CreateRedheadDuck();
            IQuackable duckCall = duckFactory.CreateDuckCall();
            IQuackable rubberDuck = duckFactory.CreateRubberDuck();

            Console.WriteLine("\nDuck Simulator");

            Flock flockOfDucks = new Flock();

            flockOfDucks.Add(redheadDuck);
            flockOfDucks.Add(mallardDuck);
            flockOfDucks.Add(duckCall);
            flockOfDucks.Add(rubberDuck);

            Flock flockOfMallards = new Flock();

            for(int i = 0;i < 5;i++)
                flockOfMallards.Add(duckFactory.CreateMallardDuck());

            flockOfDucks.Add(flockOfMallards);

            Console.WriteLine("\nDuck Simulator: Whole Flock Simulation");
            Simulate(flockOfDucks);

            Console.WriteLine("\nDuck Simulator: Mallard Flock Simulation");
            Simulate(flockOfMallards);

            Console.WriteLine("The ducks quacked " + QuackCounter.Quacks + " times\n");
        }
    }
}

namespace CSDesignPatternTrack
{
    partial class Class13
    {
        private void Section03()
        {
            Console.WriteLine(" = Section #03 = ");
            CompoundDuck01.DuckSimulator duckSimulator = new CompoundDuck01.DuckSimulator();
            duckSimulator.Simulate03();

            Section04();
        }
    }
}

/*****************************
이번엔 반대로 오리들을 각각 하나씩 챙기는 기능을 만들어보자.
옵저버 패턴.
******************************/

namespace CompoundDuck02
{
    public interface IQuackObservable
    {
        void RegisterObserver(IObserver observer);
        void NotifyObservers();
    }

    public interface IQuackable : IQuackObservable
    {
        string Name { get; }
        void Quack();
    }

    public class Observable : IQuackObservable
    {
        List<IObserver> observers = new List<IObserver>();
        IQuackObservable duck;

        public Observable(IQuackObservable duck)
        {
            this.duck = duck;
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in observers)
            {
                observer.Update(duck);
            }
        }
    }

    public class MallardDuck : IQuackable
    {
        public string Name { get { return "Mallard"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Quack");
            NotifyObservers();
        }

        Observable observable;

        public MallardDuck()
        {
            observable = new Observable(this);
        }

        public void RegisterObserver(IObserver observer)
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            observable.NotifyObservers();
        }
    }
    public class RedheadDuck : IQuackable
    {
        public string Name { get { return "Redhead"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Quack");
            NotifyObservers();
        }

        Observable observable;

        public RedheadDuck()
        {
            observable = new Observable(this);
        }

        public void RegisterObserver(IObserver observer)
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            observable.NotifyObservers();
        }
    }
    public class DuckCall : IQuackable  // 오리 호출기
    {
        public string Name { get { return "DuckCall"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Kwak");
            NotifyObservers();
        }

        Observable observable;

        public DuckCall()
        {
            observable = new Observable(this);
        }

        public void RegisterObserver(IObserver observer)
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            observable.NotifyObservers();
        }
    }
    public class RubberDuck : IQuackable // 고무 오리
    {
        public string Name { get { return "Rubber"; } }

        public void Quack()
        {
            Console.WriteLine(Name + ": Squeak");
            NotifyObservers();
        }

        Observable observable;

        public RubberDuck()
        {
            observable = new Observable(this);
        }

        public void RegisterObserver(IObserver observer)
        {
            observable.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            observable.NotifyObservers();
        }
    }

    public interface IObserver
    {
        void Update(IQuackObservable duck);
    }

    // 관측자. 꽥학자...
    public class Quackologist : IObserver
    {
        public void Update(IQuackObservable duck)
        {
            Console.WriteLine("Quackologist: " + duck + " just quacked.");
        }
    }

    public class QuackCounter : IQuackable
    {
        IQuackable duck;
        static int numberOfQuacks = 0;

        public string Name { get { return "Counter"; } }

        public QuackCounter(IQuackable duck)
        {
            this.duck = duck;
        }

        public void Quack()
        {
            duck.Quack();
            numberOfQuacks++;
        }

        public void RegisterObserver(IObserver observer)
        {
            duck.RegisterObserver(observer);
        }

        public void NotifyObservers()
        {
            duck.NotifyObservers();
        }

        public static int Quacks { get { return numberOfQuacks; } }

        public static void Reset()
        {
            numberOfQuacks = 0;
        }
    }

    public class Flock : IQuackable
    {
        List<IQuackable> quackers = new List<IQuackable>();

        public string Name { get { return "Flock"; } }

        public void Add(IQuackable quacker)
        {
            quackers.Add(quacker);
        }

        public void Quack()
        {
            IEnumerator<IQuackable> enumerator = quackers.GetEnumerator();  // 이터레이터 패턴
            while (enumerator.MoveNext())
            {
                IQuackable quacker = enumerator.Current;
                quacker.Quack();
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            foreach(IQuackable duck in quackers)
            {
                duck.RegisterObserver(observer);
            }
        }

        // Quack() 메소드가 호출될 때 알아서 Notify되기 때문에 특별이 할 일 없음.
        public void NotifyObservers()
        { /* DO NOTHING */ }
    }

    // 추상 팩토리
    public abstract class AbstractDuckFactory
    {
        public abstract IQuackable CreateMallardDuck();
        public abstract IQuackable CreateRedheadDuck();
        public abstract IQuackable CreateDuckCall();
        public abstract IQuackable CreateRubberDuck();
    }

    // 데코레이터가 없는 오리를 만드는 팩토리.
    public class DuckFactory : AbstractDuckFactory
    {
        public override IQuackable CreateDuckCall()
        {
            return new DuckCall();
        }

        public override IQuackable CreateMallardDuck()
        {
            return new MallardDuck();
        }

        public override IQuackable CreateRedheadDuck()
        {
            return new RedheadDuck();
        }

        public override IQuackable CreateRubberDuck()
        {
            return new RubberDuck();
        }
    }

    // 데코레이터를 사용하는 오리를 만드는 팩토리.
    public class CountingDuckFactory : AbstractDuckFactory
    {
        public override IQuackable CreateDuckCall()
        {
            return new QuackCounter(new DuckCall());
        }

        public override IQuackable CreateMallardDuck()
        {
            return new QuackCounter(new MallardDuck());
        }

        public override IQuackable CreateRedheadDuck()
        {
            return new QuackCounter(new RedheadDuck());
        }

        public override IQuackable CreateRubberDuck()
        {
            return new QuackCounter(new RubberDuck());
        }
    }

    public class DuckSimulator
    {
        public void Simulate()
        {
            QuackCounter.Reset();

            AbstractDuckFactory duckFactory = new CountingDuckFactory();

            IQuackable mallardDuck = duckFactory.CreateMallardDuck();
            IQuackable redheadDuck = duckFactory.CreateRedheadDuck();
            IQuackable duckCall = duckFactory.CreateDuckCall();
            IQuackable rubberDuck = duckFactory.CreateRubberDuck();

            Console.WriteLine("\nDuck Simulator");

            Flock flockOfDucks = new Flock();

            flockOfDucks.Add(redheadDuck);
            flockOfDucks.Add(mallardDuck);
            flockOfDucks.Add(duckCall);
            flockOfDucks.Add(rubberDuck);

            Flock flockOfMallards = new Flock();

            for (int i = 0; i < 5; i++)
                flockOfMallards.Add(duckFactory.CreateMallardDuck());

            flockOfDucks.Add(flockOfMallards);

            Quackologist quackologist = new Quackologist();
            flockOfDucks.RegisterObserver(quackologist);

            Simulate(flockOfDucks);
            
            Console.WriteLine("The ducks quacked " + QuackCounter.Quacks + " times\n");
        }

        private void Simulate(IQuackable duck)
        {
            duck.Quack();
        }
    }
}

namespace CSDesignPatternTrack
{
    public partial class Class13
    {
        private void Section04()
        {
            Console.WriteLine(" = Section #03 = ");
            CompoundDuck02.DuckSimulator duckSimulator = new CompoundDuck02.DuckSimulator();
            duckSimulator.Simulate();

            //Section05();
        }
    }
}

/*****************************
오늘은 새로운 뭔가를 배웠다기 보단, 이때까지 배운 패턴들을 복습하는 시간에 가까웠음.
내년엔 컴파운드 패턴의 왕이라는 MVC를 배워보자.
p.562의 클래스 다이어그램은 한번 참고해보고.
******************************/

// Anteater