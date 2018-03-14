/*****************************
* 2017.12.13 디자인 패턴
* 목표 : Chapter 06 - Command 패턴
     ******* 코멘트 *******
 "한 차원 높은 단계의 캡슐화"

메소드 호출을 캡슐화하자!
메소드 호출을 캡슐화하면 계산 과정의 각 부분들을 결정화시킬 수 있기 때문에,
계산하는 코드를 호출한 객체에서는 어떤 식으로 일을 처리해야 하는지에 대해 전혀 신경 쓰지 않아도 된다.
그냥 결정화된 메소드를 호출해서 필요한 일만 잘 하면 된다.
이를 통해 캡슐화된 메소드 호출을 로그 기록용으로 저장하고, undo 기능을 만들수도 있다.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
오늘의 예제는 (주)홈 오토메이션의 홈 오토메이션 리모컨 API 디자인이다.
요즘...이라기엔 좀 됐지만 어쨌든 핫한 IoT 기술을 사용하나보다. 스마트 TV, 스마트 전등, 스마트 밥솥...

일단 제공받는 클래스들은 상당히 많고, 딱히 공통적인 인터페이스는 없다.
어떤건 on()/off() 메소드인데, 다른건 arm()/disarm(), waterOn()/waterOff(), 기타등등.
그리고 제품 특성상 앞으로도 이런 클래스들이 더 추가될 가능성도 있다.

확실한것은, 리모컨에서 이 수많은 클래스들에 대해 자세히 알 필요가 없도록 하는게 좋다.
만약 그렇게 하지 않는다면, 새로운 클래스가 추가될 때마다 리모컨의 코드를 고쳐야 한다.
이를 해결하기 위해 쓰는 패턴이 오늘 배울 커맨드 패턴.
******************************/

/*****************************
식당을 예로 들어 커맨드 패턴에 대해 간단히 알아보자.
식당에서 주문을 하는 과정은 다음과 같다.

1. 고객이 웨이트리스한테 주문을 한다.
2. 웨이트리스는 주문을 받아서 카운터에 갖다 준다.
3. 주방장이 주문대로 음식을 준비한다.

이 과정을 좀 더 자세히 알아보자.

1. 고객이 원하는 것을 주문한다. 치즈 버거, 몰트 셰이크. (createOrder())
2. 주문(Order)은 계산서와 그 위에 적혀있는 고객이 주문한 메뉴 항목으로 구성된다.
3. 웨이트리스는 그 주문을 받아서(takeOrder()) 주문을 처리하기 위한 준비를 시작하는 orderUp() 메소드를 호출한다.
4. Order 객체에는 음식을 준비하기 위한 모든 지시 사항이 들어있다.
   Order객체가 주방장한테 makeBurger() 같은 메소드 호출을 통해 지시를 내린다.
5. 주방장은 Order에서 전달받은 지시사항(makeBurger(), makeShake())에 따라서 음식을 준비한다.

주문서는 주문한 메뉴를 캡슐화한다.
이 객체의 인터페이스에는 orderUp()이라는 식사를 준비하기 위한 행동을 캡슐화한 메소드가 들어있다.
웨이트리스는 주문서를 받아서 거기에 있는 orderUp()메소드를 호출하는 일을 한다.
웨이트리스가 아는 것은 오직 주문서에 orderUp() 메소드가 있고, 그 메소드를 호출하면 식사가 준비된다는 것.
주방장은 식사를 준비하는데 필요한 정보를 가지고 있다.
웨이트리스가 orderUp() 메소드를 호출하면 주방장이 그 주문을 받아서 음식을 만들기 위한 메소드를 전부 처리한다.
이 과정에서 주방장과 웨이트리스 사이에 그 어떤 대화도 필요하지 않다. 즉, 둘은 서로 완전히 분리되어 있다.

이 과정을 일반화 해보자. p.239의 그림. 말로 풀어 설명하기가 몹시 곤란하다..
식당            -           커맨드 패턴
손님                        클라이언트 객체
주문서                      커맨드 객체
웨이트리스                  인보커 객체
takeOrder()                 setCommand()
orderUp()                   execute()
주방장                      리시버 객체

먼저 클라이언트는 커맨드 객체를 생성해야한다. 커맨드 객체는 리시버에 전달할 일련의 행동으로 구성된다.
이를 나타내는 메소드가 createCommandObject()
커맨드 객체에는 행동과 리시버에 대한 정보가 같이 들어있다.
커맨드 객체에서 제공되는 메소드는 execute() 하나 뿐이다.
이 메소드는 행동을 캡슐화하며, 리시버에 있는 특정 행동을 처리하기 위한 메소드를 호출하기 위한 메소드이다.
클라이언트에서는 인보커 객체의 setCommand() 메소드를 호출한다.
이때 커맨드 객체를 넘겨주는데, 그 커맨드 객체는 나중에 쓰이기 전까지 인보커 객체에 보관된다.
여기서 인보커에 커맨드 객체가 저장됨으로, 나중에 클라이언트에서 인보커에게 직접 그명령을 실행시켜 달라는 요청을 할 수 있다.
어쨌든 요청받은 인보커는 커맨드 객체의 execute() 메소드를 호출하고,
리시버에 있는 특정 행동을 하는 메소드가 호출된다.
******************************/

/*****************************
이제 커맨드 객체를 만들어보자.

커맨드 객체는 모두 같은 인터페이스를 구현해야 한다.
그 인터페이스에는 메소드가 하나밖에 없다. 식당의 예에서는 orderUp() 이라는 메소드였고, 일반적으로는 execute()
******************************/

namespace RemoteAPI
{
    public interface ICommand
    {
        void Execute();
    }
}

/*****************************
위의 인터페이스로 커맨드 클래스를 구현해보자.
******************************/

namespace RemoteAPI
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
    }
    public class GarageDoorOpenCommand : ICommand
    {
        GarageDoor door;

        public GarageDoorOpenCommand(GarageDoor door)
        {
            this.door = door;
        }

        public void Execute()
        {
            door.Up();
        }
    }
    public class GarageDoorCloseCommand : ICommand
    {
        GarageDoor door;

        public GarageDoorCloseCommand(GarageDoor door)
        {
            this.door = door;
        }

        public void Execute()
        {
            door.Down();
        }
    }
    public class StereoOnWithCDCommand : ICommand
    {
        Stereo stereo;

        public StereoOnWithCDCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }

        public void Execute()
        {
            stereo.On();
            stereo.SetCD();
            stereo.Volume = 11;     // execute()에서 stereo의 여러 메소드를 호출
        }
    }
    public class StereoOffCommand : ICommand
    {
        Stereo stereo;

        public StereoOffCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }

        public void Execute()
        {
            stereo.Off();
        }
    }
    #endregion
}

/*****************************
이제 버튼이 하나 밖에 없는 리모콘이 있다고 가정하고 리모콘의 코드를 짜서 테스트해보자.
******************************/

namespace RemoteAPI
{
    public class SimpleRemoteControl
    {
        ICommand slot;

        public SimpleRemoteControl() { }

        public void SetCommand(ICommand command)
        {
            slot = command;
        }

        public void ButtonWasPressed()
        {
            slot.Execute();
        }
    }
}

namespace CSDesignPatternTrack
{
    using RemoteAPI;
    partial class Class06   // 클라이언트 객체
    {
        public Class06()
        {
            SimpleRemoteControl remote = new SimpleRemoteControl();     // 인보커
            Light light = new Light();                                  // 리시버
            LightOnCommand lightOn = new LightOnCommand(light);         // 커맨드

            remote.SetCommand(lightOn);     // 커맨드 객체를 인보커에 전달

            remote.ButtonWasPressed();      // 클라이언트가 버튼을 누름!

            GarageDoor door = new GarageDoor();
            GarageDoorOpenCommand doorOpen = new GarageDoorOpenCommand(door);

            remote.SetCommand(doorOpen);
            remote.ButtonWasPressed();

            Section02();
        }
    }
}

/*****************************
Pattern #07 Command
커맨드 패턴을 이용하면 요구 사항을 객체로 캡슐화 할 수 있으며,
매개변수를 써서 여러 가지 다른 요구 사항을 집어넣을 수도 있다.
또한 요청 내역을 큐에 저장하거나 로그로 기록할 수도 있으며, 작업취소 기능도 지원 가능하다.

p.245의 클래스 다이어그램에서 각 객체들의 역할을 살펴보자.
클라이언트는 ConcreteCommand를 생성하고 Receiver를 설정한다.
리시버는 요구 사항을 수행하기 위해 어떤 일을 처리해야 하는지 알고있는 객체이다. 주방장.
커맨드 인터페이스는 execute() 메소드를 포함한다. 모든 명령은 execute() 메소드 호출을 통해 수행된다.
execute() 메소드는 리시버에 특정 작업을 처리하라는 지시를 전달한다. 참고로 undo() 메소드도 있는데 이건 좀있다가.
커맨드 인터페이스를 구현한 ConcreteCommand는 특정 행동과 리시버 사이를 연결해준다.
인보커에서 execuete() 호출을 통해 요청을 하면 ConcreteCommand 객체에서 리시버에 있는 메소드를 호출함으로써 그 작업을 처리한다.
******************************/

/*****************************
이제 리모컨 API를 조금 더 발전시켜보자.
리모컨의 각 슬롯에 명령을 할당한다. 여기서 리모컨이 바로 인보커 역할을 한다.
사용자가 버튼을 누르면 그 버튼에 상응하는 커맨드 객체의 execute() 메소드가 호출되고,
그러면 리시버(전구, 차고)에서 특정 행동을 하는 메소드가 실행된다.

흠... 코드 너무 귀찮은데... 반복작업...
******************************/

namespace RemoteAPI
{
    public class NoCommand : ICommand
    {
        public void Execute() { }
    }
    public class RemoteControl
    {
        ICommand[] onCommands;
        ICommand[] offCommands;

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot">슬롯 번호</param>
        /// <param name="onCommand">On Command</param>
        /// <param name="offCommand">Off Command</param>
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            onCommands[slot] = onCommand;
            offCommands[slot] = offCommand;
        }

        public void OnButton(int slot)
        {
            onCommands[slot].Execute();
        }

        public void OffButton(int slot)
        {
            offCommands[slot].Execute();
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
테스트를 해보자. 적당히 해보자 적당히.
******************************/

namespace CSDesignPatternTrack
{
    using RemoteAPI;
    partial class Class06
    {
        void Section02()
        {
            Console.WriteLine(" = Section #02 = ");

            RemoteControl remoteControl = new RemoteControl();

            Light livingRoomLight = new Light();
            Light kitchenLight = new Light();
            GarageDoor garageDoor = new GarageDoor();
            Stereo stereo = new Stereo();

            LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
            LightOnCommand kitchenLightOn = new LightOnCommand(kitchenLight);
            LightOffCommand kitchenLightOff = new LightOffCommand(kitchenLight);
            GarageDoorOpenCommand garageDoorOpen = new GarageDoorOpenCommand(garageDoor);
            GarageDoorCloseCommand garageDoorClose = new GarageDoorCloseCommand(garageDoor);
            StereoOnWithCDCommand stereoOnWithCD = new StereoOnWithCDCommand(stereo);
            StereoOffCommand stereoOff = new StereoOffCommand(stereo);

            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            remoteControl.SetCommand(1, kitchenLightOn, kitchenLightOff);
            remoteControl.SetCommand(2, garageDoorOpen, garageDoorClose);
            remoteControl.SetCommand(3, stereoOnWithCD, stereoOff);     // 커맨드 로드

            Console.WriteLine(remoteControl);       // ToString() 호출

            for (int i = 0; i < 4; i++)
            {
                remoteControl.OnButton(i);
                remoteControl.OffButton(i);
            }

            Section03();
        }
    }
}

/*****************************
여기서 잠깐, NoCommand 클래스에 대해서.
NoCommand는 슬롯에 커맨드가 로드되어 있는지 확인하는 것을 더욱 편리하게 만들기 위해 사용한 일종이 편법이다.
편법인데 나쁜건 아니고, 테크닉 같은느낌.
NoCommand는 ICommand 인터페이스를 구현하지만 아무것도 하지 않는 커맨드 클래스이다.
그리고 RemoteControl의 생성자에서 모든 슬롯에 기본 커맨드 객체로 NoCommand 객체를 집어넣는다.
이렇게 하면 굳이 버튼을 누르는 메소드에서 onCommands[slot]이 null인지 확인할 필요 없이
모든 슬롯에 커맨드 객체가 들어있음을 보장할수있다.

NoCommand 객체와 같은 객체를 널 객체(null object)라고 한다.
딱히 리턴할 객체는 없지만 클라이언트 쪽에서 null을 처리하지 않아도 되도록 하고 싶을 때 널 객체를 활용하면 좋다.
널 객체는 여러 디자인 패턴에서 유용하게 쓰이고, 일종의 디자인 패턴으로 분류하기도 한다.
******************************/

// Anteater