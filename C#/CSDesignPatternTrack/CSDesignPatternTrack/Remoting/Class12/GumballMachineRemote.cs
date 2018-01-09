using System;
using System.Runtime.Serialization;

namespace Gumball05
{
    public abstract class GumballMachineRemote : MarshalByRefObject
    {
        public abstract int Count { get; }
        public abstract string Location { get; }
        public abstract IState State { get; set; }
    }

    public interface IState
    {
        void InsertQuarter();
        void EjectQuarter();
        void TurnCrank();
        void Dispense();
    }

    public class GumballMachine : GumballMachineRemote
    {
        #region Fields
        IState soldOutState;
        IState noQuarterState;
        IState hasQuarterState;
        IState soldState;
        IState winnerState;

        IState state;
        int count = 0;
        string location;
        #endregion

        #region Properties
        public IState SoldOutState { get { return soldOutState; } }
        public IState NoQuarterState { get { return noQuarterState; } }
        public IState HasQuarterState { get { return hasQuarterState; } }
        public IState SoldState { get { return soldState; } }
        public IState WinnerState { get { return winnerState; } }
        public override IState State { get { return state; } set { state = value; } }
        public override int Count { get { return count; } }
        public override string Location { get { return location; } }
        #endregion

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
    [Serializable()]
    public class NoQuarterState : IState
    {
        [NonSerialized] private GumballMachine gumballMachine;

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

    [Serializable()]
    public class SoldOutState : IState
    {
        [NonSerialized] private GumballMachine gumballMachine;

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

    [Serializable()]
    public class HasQuarterState : IState
    {
        [NonSerialized] private GumballMachine gumballMachine;
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

    [Serializable()]
    public class SoldState : IState
    {
        [NonSerialized] private GumballMachine gumballMachine;

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

    [Serializable()]
    public class WinnerState : IState
    {
        [NonSerialized] private GumballMachine gumballMachine;

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
