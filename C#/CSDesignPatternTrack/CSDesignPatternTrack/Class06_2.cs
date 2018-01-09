using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
자 그럼 이제 앞서서 계속해서 언급했던 Undo 기능도 추가해야지.

undo 버튼은 마지막으로 했던 작업을 취소한다.
이 기능을 위해 이제 ICommand 인터페이스에 새로운 메소드 Undo()를 추가한다.
사실 뭐 대단한건 없고, Undo() 구현에서 Execute()에서 실행한 명령의 반대 명령을 실행한다.
******************************/

namespace RemoteAPIWithUnDo
{
    #region households(Receviers)
    public class Light
    {
        public void On()
        { Console.WriteLine("Light's on"); }
        public void Off()
        { Console.WriteLine("Light's off"); }
    }
    public class GarageDoor
    {
        public void Up()
        { Console.WriteLine("Garage Door is Open"); }
        public void Down()
        { Console.WriteLine("Garage Door is Closed"); }
        public void Stop()
        { Console.WriteLine("Garage Door is Stopped"); }
    }
    public class Stereo
    {
        public void On()
        { Console.WriteLine("Stereo is on"); }
        public void Off()
        { Console.WriteLine("Stereo is off"); }
        public void SetCD()
        { Console.WriteLine("Stereo is set for CD input"); }
        public void SetDVD() { }
        public void SetRadio() { }
        private int volume = 0;
        public int Volume
        {
            get { return this.volume; }
            set
            {
                volume = value;
                Console.WriteLine("Stereo volume set to " + volume);
            }
        }
    }
    #endregion
    #region Commands
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class LightOnCommand : ICommand
    {
        Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.On();
        }

        public void Undo()
        {
            light.Off();
        }
    }
    public class LightOffCommand : ICommand
    {
        Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void Execute()
        {
            light.Off();
        }

        public void Undo()
        {
            light.On();
        }
    }
    #endregion
}

/*****************************
그 다음 RemoteControl 클래스에 사용자가 마지막으로 누른 버튼을 기록하고,
Undo 버튼이 눌렸을 때 필요한 작업을 처리하기 위한 코드를 추가하면 된다.
******************************/

namespace RemoteAPIWithUnDo
{
    public class NoCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }
    public class RemoteControl
    {
        ICommand[] onCommands;
        ICommand[] offCommands;
        ICommand undoCommand;           // 마지막으로 사용한 커맨드

        public RemoteControl()
        {
            onCommands = new ICommand[7];
            offCommands = new ICommand[7];  // 7개 슬롯, 각각 on/off 버튼

            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7; i++)
            {
                onCommands[i] = noCommand;
                offCommands[i] = noCommand;
            }
            undoCommand = noCommand;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            onCommands[slot] = onCommand;
            offCommands[slot] = offCommand;
        }

        public void OnButton(int slot)
        {
            onCommands[slot].Execute();
            undoCommand = onCommands[slot];
        }

        public void OffButton(int slot)
        {
            offCommands[slot].Execute();
            undoCommand = offCommands[slot];
        }

        public void UndoButton()
        {
            undoCommand.Undo();
        }

        public override string ToString()
        {
            StringBuilder stringBuild = new StringBuilder();
            stringBuild.Append("\n----- Remote Control -----\n");
            for (int i = 0; i < onCommands.Length; i++)
                stringBuild.Append("[slot " + i + "] " + onCommands[i].GetType().Name
                    + "\t" + offCommands[i].GetType().Name + "\n");
            return stringBuild.ToString();
        }
    }
}

/*****************************
생각보다 진짜 별거 없어서... 테스트 해야하나 싶지만, 그래도 테스트 해 보자.
사실 커맨드들을 스택같은데다가 넣을줄 알았는데 책에선 그냥 최근 커맨드 하나만 실행취소함...
******************************/

namespace CSDesignPatternTrack
{
    using RemoteAPIWithUnDo;
    partial class Class06
    {
        void Section03()
        {
            Console.WriteLine(" = Section #03 = ");
            RemoteControl remoteControl = new RemoteControl();

            Light light = new Light();
            LightOnCommand lightOn = new LightOnCommand(light);
            LightOffCommand lightOff = new LightOffCommand(light);

            remoteControl.SetCommand(0, lightOn, lightOff);

            remoteControl.OnButton(0);
            remoteControl.OffButton(0);
            remoteControl.UndoButton();
        }
    }
}

/*****************************
물론 당연히 이런 작업취소 메소드나, 실행 메소드는 응용하기 나름이다.
어... 책에서 선풍기를 예로 작업취소 기능에 상태를 이용하는 방법을 설명하는데,
이런건 뭐 엄청 어려운것도 아니고, 패스.
파티모드도 이와 같은 느낌이다.
public class MacroCommand : ICommand
이 클래스를 만들어서 필드 멤버로 ICommand 배열을 두고,
execute() 메소드에서 배열의 모든 명령어를 수행한다. 이게 끝.
그리고 클라이언트에서 각 장치를 제어할 On 명령을 만들고,
ICommand 배열을 선언하고,
MacroCommand partyOnMacro = new MacroCommand(partyOn);
MacroCommand partyOffMacro = new MacroCommand(partyOff);
remoteControl.SetCommand(0, partyOnMacro, partyOffMacro);
끝.
******************************/

/*****************************
마지막으로, 커맨드 패턴의 활용사례들.

그 첫 번째는 요청을 큐에 저장하기.
커맨드를 이용해서 컴퓨터이션의 한 부분, 즉 리시버와 일련의 행동을 패키지로 묶어서 일급 객체 형태로 전달하는것이 가능하다.
어떤 클라이언트 애플리케이션에서 커맨드 객체를 생성하고 한참 후에 그 컴퓨테이션을 호출하는것.
p.266의 깔대기 그림을 보면 이해가 쉬울것이다.

두 번째는 요청을 로그에 기록하기.
어떤 애플리케이션에서는 모든 행동을 기록해놨다가 그 애플리케이션이 다운되었을 때,
나중에 그 행동들을 다시 호출해서 복구를 할 수 있어야 한다.
커맨드 인터페이스에 store(), load() 메소드를 추가한다.
명령을 실행하면서 디스크에 실행 히스토리를 기록한다.
애플리케이션이 다운되면 커맨드 객체를 다시 로딩하고, execute() 메소드들을 자동으로 순서대로 실행하면 된다.
역시 p.267의 그림을 참고하자.
******************************/
