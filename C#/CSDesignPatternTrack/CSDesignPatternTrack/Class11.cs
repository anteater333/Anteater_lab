/*****************************
* 2017.12.26 디자인 패턴
* 목표 : Chapter 10 - State 패턴
     ******* 코멘트 *******
 "자, 소리내서 말해 보세요. 난 할 수 있다! 난 똑똑하다! 해야 한다!"

롱 타임 노 씨. 연휴 끝. 연휴라 해봐야 주말+크리스마스 하루 였지만.

스트래티지 패턴을 다시 떠올려 보자.
스트래티지 패턴은 알고리즘을 바꿀 수 있게 해주는 패턴이였다.
스테이트 패턴은 내부 상태를 바꿈으로써 객체에서 행동을 바꾸는 것을 도와주는 패턴이다.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
주식회사 왕뽑기 - 알맹이가 넘치는 곳
자바 가상 머신이 들어간 뽑기 기계를 만드는 회사에서 도움을 청해 왔다.
p.424의 다이어그램과 같은 식으로 작동하는 뽑기 기계 컨트롤러용 자바 코드를 구현해달라고 한다.
다이어그램은 마치 상태 다이어그램 처럼 생겼다.
시작 상태는 아마 [동전 없음].
[동전 없음] 상태에서 동전을 넣으면 [동전 있음] 상태로 상태가 전환이 된다.
이 시스템에서 상태는 총 4개, 행동도 총 4개이다.
상태 : [동전 없음], [동전 있음], [알맹이 판매], [알맹이 매진]
행동 : (동전 투입), (동전 반환), (손잡이 돌림), (알맹이 내보냄)
그리고 한 상태에서 다른 상태로 넘어가는 전환 종류가 다섯 가지다.

사용자가 어떤 행동을 하든지 지금 어떤 상태에 있는지 확인하고,
그 상태에 맞게 적절한 행동을 취해야 한다.

State machine을 구현해보자.
상태 기계를 만들 때는 보통
상태 값을 저장하기 위한 인스턴스 변수를 만들고,
메소드 내에서 조건문을 써서 다양한 상태들을 처리하는 방법을 사용한다.
******************************/

namespace Gumball01
{
    public class GumballMachine
    {
        const int SOLD_OUT = 0;         // [알맹이 매진]
        const int NO_QUARTER = 1;       // [동전 없음]
        const int HAS_QUARTER = 2;      // [동전 있음]
        const int SOLD = 3;             // [알맹이 판매]

        int state = SOLD_OUT;
        int count = 0;

        public GumballMachine(int count)
        {
            this.count = count;
            if (count > 0)
                state = NO_QUARTER;
        }

        public void InsertQuarter()     // (동전 투입)
        {
            if (state == HAS_QUARTER)
                Console.WriteLine("동전은 한 개만 넣어주세요.");
            else if (state == NO_QUARTER)
            {
                state = HAS_QUARTER;
                Console.WriteLine("동전을 넣으셨습니다.");
            }
            else if (state == SOLD_OUT)
                Console.WriteLine("매진되었습니다. 다음에 이용해주세요.");
            else if (state == SOLD)
                Console.WriteLine("알맹이가 나가고 있습니다. 잠시만 기다려주세요.");
        }

        public void EjectQuarter()      // (동전 반환)
        {
            if (state == HAS_QUARTER)
            {
                Console.WriteLine("동전이 반환됩니다.");
                state = NO_QUARTER;
            }
            else if (state == NO_QUARTER)
                Console.WriteLine("동전을 넣어주세요.");
            else if (state == SOLD)
                Console.WriteLine("이미 알맹이를 뽑으셨습니다.");
            else if (state == SOLD_OUT)
                Console.WriteLine("동전을 넣지 않으셨습니다. 동전이 반환되지 않습니다.");
        }

        public void TurnCrank()         // (손잡이 돌림)
        {
            if (state == SOLD)
                Console.WriteLine("손잡이는 한 번만 돌려주세요.");
            else if (state == NO_QUARTER)
                Console.WriteLine("동전을 넣어주세요.");
            else if (state == SOLD_OUT)
                Console.WriteLine("매진되었습니다.");
            else if (state == HAS_QUARTER)
            {
                Console.WriteLine("손잡이를 돌리셨습니다.");
                state = SOLD;
                Dispense();
            }
        }

        public void Dispense()          // (알맹이 내보냄)
        {
            if (state == SOLD)
            {
                Console.WriteLine("알맹이가 나가고 있습니다.");
                count = count - 1;
                if (count == 0)
                {
                    Console.WriteLine("더 이상 알맹이가 없습니다.");
                    state = SOLD_OUT;
                }
                else
                    state = NO_QUARTER;
            }
            else if (state == NO_QUARTER)
                Console.WriteLine("동전을 넣어주세요.");
            else if (state == SOLD_OUT)
                Console.WriteLine("매진입니다.");
            else if (state == HAS_QUARTER)
                Console.WriteLine("알맹이가 나갈 수 없습니다.");
        }
    }
}

/*****************************
테스트는 굳이 해보진 말자. 노가다가 좀 씨다.

사실 테스트는 (주)왕뽑기에서 잘 수행했다.
테스트가 끝나고 나기 새로운 요청이 들어왔다.
열 번에 한 번 꼴로 손잡이를 돌릴 때 알맹이 두 개가 나오게 하는 기능을 부탁받았다.

위에서 구현한 코드를 보면 알겠지만,
확장하기에 상당히 난감하다.
우선 WINNER 상태를 추가하고,
각 메소드마다 WINNER 상태인 경우를 추가해야하고...
지금까지 배워온 원칙들을 싸그리 무시한 느낌이 든다.

새로운 디자인을 만들어보자.
계획은 다음과 같다.
1. 우선 뽑기 기계와 관련된 모든 행동에 대한 메소드가 들어있는 State 인터페이스를 정의해야 한다.
2. 그 다음에는 기계의 모든 상태에 대해서 상태 클래스를 구현해야 한다.
   기계가 어떤 상태에 있다면, 그 상태에 해당하는 상태 클래스가 모든 작업을 책임진다.
3. 마지막으로 조건문 코드를 전부 없애고 상태 클래스에 모든 작업을 위임한다.
이게 바로 State 패턴이다.
<<interface>> State
InsertQuarter()
EjectQuarter()
TurnCrank()
Dispense()
******************************/

namespace Gumball02
{
    public interface IState
    {
        void InsertQuarter();
        void EjectQuarter();
        void TurnCrank();
        void Dispense();
    }

    public class NoQuarterState : IState
    {
        private GumballMachine gumballMachine;

        public NoQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("동전을 넣으셨습니다.");
            gumballMachine.State = gumballMachine.HasQuarterState;
        }

        public void EjectQuarter()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }

        public void Dispense()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }
    }

/*****************************
물론 뽑기 기계의 코드도 변경한다.
상태와 관련된 인스턴스 변수를 정수에서 상태 객체를 사용하는 방식으로 고친다.
******************************/

    public class GumballMachine
    {
        IState soldOutState;
        IState noQuarterState;
        IState hasQuarterState;
        IState soldState;

        IState state;
        int count = 0;
        
        public IState SoldOutState { get => soldOutState; }
        public IState NoQuarterState { get => noQuarterState; }
        public IState HasQuarterState { get => hasQuarterState; }
        public IState SoldState { get => soldState; }
        public IState State { get => state; set => state = value; }
        public int Count { get => count; }

        public GumballMachine(int numberGumballs)
        {
            soldOutState = new SoldOutState(this);
            noQuarterState = new NoQuarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);
            this.count = numberGumballs;
            if (numberGumballs > 0)
                state = noQuarterState;
            else
                state = soldOutState;
        }

        public void InsertQuarter()
        {
            state.InsertQuarter();
        }

        public void EjectQuarter()
        {
            state.EjectQuarter();
        }

        public void TurnCrank()
        {
            state.TurnCrank();
            state.Dispense();
        }

        public void ReleaseBall()
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            if (count != 0)
                count = count - 1;
        }
    }

    #region States
    public class SoldOutState : IState
    {
        private GumballMachine gumballMachine;

        public SoldOutState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void Dispense()
        {
            Console.WriteLine("매진되었습니다.");
        }
    }

    public class HasQuarterState : IState
    {
        private GumballMachine gumballMachine;

        public HasQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("동전은 한 개만 넣어주세요.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("동전이 반환됩니다.");
            gumballMachine.State = gumballMachine.NoQuarterState;
        }

        public void TurnCrank()
        {
            Console.WriteLine("손잡이를 돌리셨습니다.");
            gumballMachine.State = gumballMachine.SoldState;
        }

        public void Dispense()
        {
            Console.WriteLine("알맹이가 나갈 수 없습니다.");
        }
    }

    public class SoldState : IState
    {
        private GumballMachine gumballMachine;

        public SoldState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("알맹이가 나가고 있습니다. 잠깐만 기다려 주세요.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("이미 알맹이를 뽑으셨습니다.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("손잡이는 한 번만 돌려주세요.");
        }

        public void Dispense()
        {
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count > 0)
                gumballMachine.State = gumballMachine.NoQuarterState;
            else
            {
                Console.WriteLine("Oops, out of gumballs!");
                gumballMachine.State = gumballMachine.SoldOutState;
            }
        }
    }
    #endregion
}

/*****************************
지금까지 한 일.
 - 각 상태의 행동을 별개의 클래스로 국지화시켰다.
 - 관리하기 힘든 골칫덩어리 if 선언문들을 없앴다.
 - 각 상태를 변경에 대해서는 닫혀 있도록 하면서,
   GumballMachine 자체는 새로운 상태 클래스를 추가하는 확장에 대해서 열려있도록 고쳤다. (OCP)
 - 처음에 주식회사 왕뽑기에서 제시했던 다이어그램에 훨씬 가까우면서 더 이해햐기 좋은 코드 베이스와 클래스 구조를 만들었다.

기능적인 면에서 조금 더 보자면,
뽑기 기계에는 각 상태 클래스의 인스턴스가 들어있다.
기계의 현재 상태는 항상 이 클래스들 가운데 하나이다.
어떤 행동이 호출되면(ex. TurnCrank()) 그 행동은 현재 상태에 맡겨진다.(GumballMachine.TurnCrank() 코드 참고)
******************************/

/*****************************
Pattern #13 State
스테이트 패턴을 이용하면 객체의 내부 상태가 바뀜에 따라서 객체의 행동을 바꿀 수 있다.
마치 객체의 클래스가 바뀌는 것과 같은 결과를 얻을 수 있다.

이 패턴에서는 상태를 별도의 클래스로 캡슐화한 다음 현재 상태를 나타내는 객체에게 행동을 위임한다.
그렇기 때문에 내부 상태가 바뀜에 따라서 행동이 달라지게 된다.

"클래스가 바뀌는 것 같은 결과"란 뭘까?
클라이언트 입장에서,
만약 객체의 행동이 완전히 달라진다면 마치 그 객체가 다른 클래스로부터 만들어진 객체처럼 느껴진다.
하지만 실제로 다른 클래스로 변신하는 게 아니고
구성을 통해서 여러 상태 객체를 바꿔가면서 사용하기 때문에 "바뀌는 것 같은" 결과를 얻을 수 있다는 것.

스테이트 패턴은 스트래티지 패턴과 몹시 비슷해보인다. 당장에 다이어그램부터가 똑같이 생겼다.
하지만 두 패턴은 용도에 있어서 차이가 있다.

스테이트 패턴을 사용할 때는 상태 객체에 일련의 행동이 캡슐화된다.
상황에 따라 Context 객체(위의 예에선 GumballMachine)에서 여러 상태 객체 중 한 객체에 모든 행동을 맡기게 된다.
그 객체의 내부 상태에 따라 현재 상태를 나타내는 객체가 바뀌게 되고,
그 결과로 컨텍스트 객체의 행동도 자연스럽게 바뀌게 된다.

스트래티지 패턴을 사용할 때는 일반적으로 클라이언트에서 컨텍스트 객체한테 어떤 전략 객체를 사용할지를 지정해준다.
스트래티지 패턴은 주로 실행시에 전략 객체를 변경할 수 있는 유연성을 제공하기 위한 용도로 쓰인다.

스테이트 패턴은 컨텍스트 객체에 수많은 조건문 대신 사용할 수 있는 패턴,
스트래티지 패턴은 서브클래스를 만드는 방법을 대신해 유연성을 극대화하기 위한 용도로 쓰이는 패턴이다.
******************************/

/*****************************
끝으로 요청받은 추가 기능을 구현해보자.
******************************/

namespace Gumball03
{
    public interface IState
    {
        void InsertQuarter();
        void EjectQuarter();
        void TurnCrank();
        void Dispense();
    }

    public class GumballMachine
    {
        IState soldOutState;
        IState noQuarterState;
        IState hasQuarterState;
        IState soldState;
        IState winnerState;

        IState state;
        int count = 0;

        public IState SoldOutState { get => soldOutState; }
        public IState NoQuarterState { get => noQuarterState; }
        public IState HasQuarterState { get => hasQuarterState; }
        public IState SoldState { get => soldState; }
        public IState WinnerState { get => winnerState; }
        public IState State { get => state; set => state = value; }
        public int Count { get => count; }

        public GumballMachine(int numberGumballs)
        {
            soldOutState = new SoldOutState(this);
            noQuarterState = new NoQuarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);
            winnerState = new WinnerState(this);
            this.count = numberGumballs;
            if (numberGumballs > 0)
                state = noQuarterState;
            else
                state = soldOutState;
        }

        public void InsertQuarter()
        {
            state.InsertQuarter();
        }

        public void EjectQuarter()
        {
            state.EjectQuarter();
        }

        public void TurnCrank()
        {
            state.TurnCrank();
            state.Dispense();
        }

        public void ReleaseBall()
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            if (count != 0)
                count = count - 1;
        }
    }

    #region States
    public class NoQuarterState : IState
    {
        private GumballMachine gumballMachine;

        public NoQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("동전을 넣으셨습니다.");
            gumballMachine.State = gumballMachine.HasQuarterState;
        }

        public void EjectQuarter()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }

        public void Dispense()
        {
            Console.WriteLine("동전을 넣어주세요.");
        }
    }

    public class SoldOutState : IState
    {
        private GumballMachine gumballMachine;

        public SoldOutState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("매진되었습니다.");
        }

        public void Dispense()
        {
            Console.WriteLine("매진되었습니다.");
        }
    }

    public class HasQuarterState : IState
    {
        private GumballMachine gumballMachine;
        private Random randomWinner = new Random();

        public HasQuarterState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("동전은 한 개만 넣어주세요.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("동전이 반환됩니다.");
            gumballMachine.State = gumballMachine.NoQuarterState;
        }

        public void TurnCrank()
        {
            Console.WriteLine("손잡이를 돌리셨습니다.");
            int winner = randomWinner.Next(10);
            if ((winner == 0) && (gumballMachine.Count > 1))
                gumballMachine.State = gumballMachine.WinnerState;
            else
                gumballMachine.State = gumballMachine.SoldState;
        }

        public void Dispense()
        {
            Console.WriteLine("알맹이가 나갈 수 없습니다.");
        }
    }

    public class SoldState : IState
    {
        private GumballMachine gumballMachine;

        public SoldState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("알맹이가 나가고 있습니다. 잠깐만 기다려 주세요.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("이미 알맹이를 뽑으셨습니다.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("손잡이는 한 번만 돌려주세요.");
        }

        public void Dispense()
        {
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count > 0)
                gumballMachine.State = gumballMachine.NoQuarterState;
            else
            {
                Console.WriteLine("Oops, out of gumballs!");
                gumballMachine.State = gumballMachine.SoldOutState;
            }
        }
    }

    public class WinnerState : IState
    {
        private GumballMachine gumballMachine;

        public WinnerState(GumballMachine gumballMachine)
        {
            this.gumballMachine = gumballMachine;
        }

        public void InsertQuarter()
        {
            Console.WriteLine("알맹이가 나가고 있습니다. 잠깐만 기다려 주세요.");
        }

        public void EjectQuarter()
        {
            Console.WriteLine("이미 알맹이를 뽑으셨습니다.");
        }

        public void TurnCrank()
        {
            Console.WriteLine("손잡이는 한 번만 돌려주세요.");
        }

        public void Dispense()
        {
            Console.WriteLine("축하합니다! 알맹이를 하나 더 받으실 수 있습니다.");
            gumballMachine.ReleaseBall();
            if (gumballMachine.Count == 0)
                gumballMachine.State = gumballMachine.SoldOutState;
            else
            {
                gumballMachine.ReleaseBall();
                if (gumballMachine.Count > 0)
                    gumballMachine.State = gumballMachine.NoQuarterState;
                else
                {
                    Console.WriteLine("더 이상 알맹이가 없습니다.");
                    gumballMachine.State = gumballMachine.SoldOutState;
                }
            }
        }
    }
    #endregion
}

/*****************************
테스트
******************************/

namespace CSDesignPatternTrack
{
    using Gumball03;
    class Class11
    {
        public Class11()
        {
            GumballMachine gumballMachine = new GumballMachine(11);

            for (int i = 0; i < 10; i++)
            {
                gumballMachine.InsertQuarter();
                gumballMachine.TurnCrank();
            }
        }
    }
}

/*****************************
오늘 내용 끝. 스킵한 부분이 조금 있긴 함. 중요할것 같진 않아서.
확실히 전날 밤을 새니까 집중이 안된다. 지금 시간도 밤 11시 41분, 엄청나게 늦음. 12시 안넘어간게 다행이다.
******************************/

// Anteater