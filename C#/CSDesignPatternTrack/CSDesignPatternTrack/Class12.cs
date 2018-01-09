/*****************************
* 2017.12.27 디자인 패턴
* 목표 : Chapter 11 - Proxy 패턴 (1)
     ******* 코멘트 *******
 "Good cop, Bad cop."

프록시는 컴퓨터 네트워크를 배울 때 나온 용어이다. 간단하게 말해서, 대리자.
조금 분명하게 말하자면, 접근 제어.
Good cop, Bad cop이라는 관용어가 있다.
좋은 경찰이 다양한 서비스를 친절하고 성심 성의껏 제공하는데,
모든 사람들이 다 서비스를 요구하게되면 일이 너무 많아지기 때문에,
나쁜 경찰이 좋은 경찰에 대한 접근을 제어한다.
이게 바로 프록시 패턴. 끝.
프록시는 자신이 대변하는 객체와 그 객체에 접근하고자 하는 클라이언트 사이에서 여러 가지 방식으로 작업을 처리한다.
인터넷을 통해 들어오는 메소드 호출을 쫓아내거나, 게으른 객체들을 대신해서 끈기있게 기다리는 일을 한다던가.

이번 챕터는 왠지 분량이 좀 길어보인다. 때마침 목감기가 온 상태라서... 적당히 해보고 두 파트로 끊어질 수도 있을듯.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
(주)왕뽑기의 CEO가 새로운 요청을 부탁해왔다.
모든 뽑기 기계의 재고와 현재 상태를 알아낼 수 있도록 만들어 달라고 한다.
다행히 프로퍼티들이 잘 설정되어있다. 조금만 추가하면 될 것 같다.
******************************/

namespace Gumball04
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
        #region Fields
        IState soldOutState;
        IState noQuarterState;
        IState hasQuarterState;
        IState soldState;
        IState winnerState;

        IState state;
        int count = 0;
        #endregion
        string location;

        #region Properties
        public IState SoldOutState { get => soldOutState; }
        public IState NoQuarterState { get => noQuarterState; }
        public IState HasQuarterState { get => hasQuarterState; }
        public IState SoldState { get => soldState; }
        public IState WinnerState { get => winnerState; }
        public IState State { get => state; set => state = value; }
        public int Count { get => count; }
        #endregion
        public string Location { get => location; }

        public GumballMachine(string location, int numberGumballs)
        {
            soldOutState = new SoldOutState(this);
            noQuarterState = new NoQuarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);
            winnerState = new WinnerState(this);
            this.location = location;
            this.count = numberGumballs;
            if (numberGumballs > 0)
                state = noQuarterState;
            else
                state = soldOutState;
        }

        #region Methods
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
        #endregion
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

    public class GumballMonitor
    {
        GumballMachine machine;

        public GumballMonitor(GumballMachine machine)
        {
            this.machine = machine;
        }
        
        public void Report()
        {
            Console.WriteLine("뽑기 기계 위치: " + machine.Location);
            Console.WriteLine("현재 재고: " + machine.Count + " 개");
            Console.WriteLine("현재 상태: " + machine.State);       // ToString()은 있다고 생각하자.
        }
    }
}

/*****************************
벌써 다 만들었다. 어서 테스트 해보자.
******************************/

namespace CSDesignPatternTrack
{
    using Gumball04;
    partial class Class12
    {
        public Class12()
        {
            GumballMachine gumballMachine = new GumballMachine("Seattle", 112);
            GumballMonitor monitor = new GumballMonitor(gumballMachine);

            for (int i = 0; i < 10; i++)
            {
                gumballMachine.InsertQuarter();
                gumballMachine.TurnCrank();
            }

            monitor.Report();
        }
    }
}

/*****************************
문제가 생겼다!
CEO가 정말로 원한건 뽑기 기계를 원격으로 모니터링 하는 기능이었다.
앞으로 코딩을 시작하기 전엔 항상 요구사항을 확실히 파악하도록 하자.

지금 우리가 필요한 것은 원격 프록시.
모니터링용 코드는 이미 만들어놨다. GumballMonitor에 뽑기 기계에 대한 레퍼런스만 넘겨주면,
그 객체에서 보고서를 만들어준다. 여기서 문제는, 그 객체가 뽑기 기계와 같은 JVM에서 돌아가는데,
CEO는 회사에서 자기 자리에 있는 컴퓨터를 통해서 멀리 떨어져 있는 뽑기 기계를 모니터링 하고 싶어한다는 것.

코드는 그대로 두고, 모니터링용 클래스에 GumballMachine의 프록시 버전에 대한 레퍼런스를 건네준다.
그 프록시가 진짜 객체인 양 행동하지만, 실제로는 네트워크를 통해서 진짜 객체와 데이터를 주고 받게 된다.

원격 프록시는 원격 객체에 대한 로컬 대변자 역할을 한다.
원격 객체란, 다른 자바 가상 머신의 힙에서 살고 있는 객체를 말한다.
로컬 대변자란, 어떤 메소드를 호출했을 때
다른 원격 객체한테 메소드 호출을 전달해주는 역할을 맡고 있는 객체를 말한다.

 <CEO의 테스크탑>                              <뽑기 기계> 
(GumballMonitor -> Proxy) ==================>> GumballMachine

다시 한번, 정리해서,
클라이언트 객체에서는 원격 객체의 메소드 호출을 하는 것 처럼 행동한다.
하지만 실제로는 로컬 힙에 들어있는 '프록시' 객체의 메소드를 호출하고 있는 것이다.
******************************/

/*****************************
이번 챕터를 힘들게 하는 요소가 있으니...
완전히 Java 기준으로 코드가 작성되고, RMI라는 걸 쓴다. 이런건 처음보는데!
RMI는 Remote Method Invocation의 약자로, 서로 다른 가상 기계에 존재하는 함수를 호출하고 실행하는 기능을 담당한다.
어... .NET에도 저런게 있지 않을까...?
대충 찾아본 결과 .NET Remoting이란 것과, WCF라는게 있다.

이건 뭐 서버-클라이언트 나뉘어서 코드 짜고...
아니 2챕터 정도 남았는데 무슨 페이지가 150페이지를 넘어가는 것 같은디. 침착하고 책을 좀 읽어보자.

다행히 일단은 RMI의 기초를 알려주긴 알려준다.
******************************/

/*****************************
RMI의 기초를 배워보자.
 <클라이언트 힙>                             ||   <서버 힙>
(클라이언트객체 -> 클라이언트 보조 객체 ==)==||==(==> 서비스 보조 객체 -> 서비스 객체)
                                             ||

클라이언트 객체는 진짜 서비스의 메소드를 호출한다고 생각하게 된다.
클라이언트 보조 객체에서 실제로 자신이 원하는 작업을 처리한다고 생각하기 때문.
클라이언트 보조 객체가 바로 프록시이고, 클라이언트 객체에게는 서비스인 척 하고 있지만
실제로는 진짜 객체에 연락을 취하고 응답을 기다린다. 실제 메소드 로직이 들어있는건 아니다.
서비스 보조 객체에서는 클라이언트 보조 객체로부터 요청을 받아서
그 내용을 해석해 진짜 서비스에 있는 메소드를 호출한다.
서비스 객체가 진짜 서비스에 해당하고, 실제로 작업을 처리한다.

RMI를 통한 메소드 호출 과정은 다음과 같다.
1. 클라이언트 객체에서 클라이언트 보조 객체의 doBigThing()을 호출한다.
2. 클라이언트 보조 객체에서는 메소드 호출에 대한 정보(인자, 메소드 이름 등)를
   잘 포장해서 네트워크를 통해 서비스 보조 객체에게 전달한다.
   "클라이언트에서 메소드를 호출했습니다."
3. 서비스 보조 객체에서는 클라이언트 보조 객체로부터 받은 정보를 해석해서
   어떤 객체의 어떤 메소드를 호출할지 알아낸 다음 진짜 서비스 객체의 진짜 메소드를 호출한다.
4. 서비스 객체의 메소드가 호출되고, 메소드 실행이 끝나면 서비스 보조 객체에 결과가 리턴된다.
5. 서비스 보조 객체에서 호출 결과로 리턴된 정보를 포장해서 네트워크를 통해 클라이언트 보조 객체에 전달한다.
6. 클라이언트 보조 객체에서 리턴된 값을 해석하여 클라이언트 객체에게 리턴한다.

위의 과정에서 클라이언트 객체 입장에서는 메소드 호출이 어디로 전달되었었는지, 어디에서 왔는지 전혀 알 수 없다.

RMI에서 클라이언트 보조 객체는 스터브(stub),
서비스 보조 객체는 스켈레톤(skeleton)이라고 부른다.

C#에서는... 일단 ASP 자체가 좀 안쓰이는 기술이라... 한글자료도 적고...
일단은 이걸 따라한다
참고자료 https://msdn.microsoft.com/en-us/library/txct33xt(v=vs.100).aspx

그리고 배우기는 Remoting을 배우는데 나중에 사용하길 추천하는 기능은 WCF
Remoting 폴더에 몹시 기초적인 Remoting 예제가 있음.
CLI 환경에서 csc 명령어로 컴파일해서 실행파일 실행시켜보면 된다.

오늘은 여기까지.
******************************/