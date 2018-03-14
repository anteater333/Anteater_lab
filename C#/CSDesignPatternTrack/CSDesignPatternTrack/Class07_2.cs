/*****************************
* 2017.12.15 디자인 패턴
* 목표 : Chapter 07 - Adapter 패턴과 Facade 패턴 (2)
     ******* 코멘트 *******
요즘 완전 낮잠에 중독이야...
빨리 하고 갓겜 트오세 하러가자.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
이번 챕터는 두 가지 패턴을 소개한다.
어제 어댑터 패턴에서는 어떤 클래스의 인터페이스를 클라이언트에서 원하는 인터페이스로 변환하는 방법에 대해서 배웠다.
오늘은 인터페이스를 단순화시키기 위해서 인터페이스를 변경하는 패턴을 배운다. 바로 퍼사드(외관, 겉모양) 패턴.
퍼사드 패턴은 하나 이상의 클래스의 복잡한 인터페이스를 깔끔하면서도 말쑥한 외관으로 덮어준다.

기능이 비슷해 보이는 패턴들
 - 데코레이터
   : 인터페이스는 바꾸지 않고 책임(기능)만 추가
 - 어댑터
   : 한 인터페이스를 다른 인터페이스로 변환
 - 퍼사드
   : 인터페이스를 간단하게 바꿈
******************************/

/*****************************
오늘은 홈 씨어터를 만들어보자.
홈 씨어터에는 DVD 플레이어, 프로젝터, 자동 스크린, 서라운드 음향 및 팝콘 기계까지 갖추었다.
이 구성요소들의 관계는 서로 복잡하게 얽혀있는 상태이다. p.293의 그림을 한 번 보도록 하자.
앰프는 CD플레이어, DVD플레이어, 튜너가 구성되어 있고 다시 CD, DVD, 튜너 각각은 앰프를 가지고있고,
Projector는 그 옆에서 DVD를 가지고있고... 기타 등등 떨어져있는 클래스들도 있고...

그러니까 홈 씨어터로 영화를 보는 과정을 정리하자면 다음과 같다.
(1) 팝콘 기계를 켠다 -> (2) 팝콘 튀기기 시작 -> (3) 전등을 어둡게 조절
-> (4) 스크린을 내린다 -> (5) 프로젝터를 켠다 -> (6) 프로젝터로 DVD 신호가 입력되도록 한다
-> (7) 프로젝터를 와이드 스크린 모드로 전환한다 -> (8) 앰프를 켠다 -> (9) 앰프 입력을 DVD로 변환한다
-> (10) 앰프를 서라운드 음향 모드로 전환한다 -> (11) 앰프 볼륨을 중간으로 설정한다 -> (12) DVD 플레이어를 켠다
-> (13) DVD를 재생한다
영화 한편 보려다가 날을 새겠다.
코드로 적으면 다음과 같은데,
popper.on();
popper.off();
lights.dim(10);
screen.down();
projector.on();
projector.setInput(dvd);
projector.wideScreenMode();
amp.on();
amp.setDvd(dvd);
amp.setSurroundSound();
amp.setVolume(5);
dvd.on();
dvd.play(movie);
클래스는 여섯 개나 필요하고 각 클래스마다 호출할 메소드들도 너무 많다.
여기에 영화가 끝났을때 작업, CD나 라디오를 들을 때 작업, 시스템을 업그레이드 한 경우등을 생각하면
씨어터 사용법은 클라이언트에게 지나치게 복잡하다.

이런 경우에 퍼사드 패턴을 사용한다.
퍼사드 패턴을 쓰면 훨씬 쓰기 쉬운 인터페이스를 제공하는 퍼사드 클래스를 구현함으로써 복잡한 시스템을 쉽게 사용할 수 있다.

1. 먼저 홈 씨어터 시스템용 퍼사드를 만든다.
   watchMovie() 같이 몇 가지 간단한 메소드만 들어있는 HomeTheaterFacade라는 클래스를 만든다.
2. 퍼사드 클래스에서는 홈 씨어터 구성요소들을 하나의 서브시스템으로 간주하고,
   watchMovie() 메소드에서는 서브시스템의 메소드들을 호출하여 필요한 작업을 처리한다.
3. 이제 클라이언트 코드에서는 서브시스템이 아닌 홈 씨어터 퍼사드에 잇는 메소드를 호출한다.
   이제 watchMove() 메소드만 호출하면 알아서 전등, DVD, 프로젝터, 앰프, 스크린, 팝콘 기계가 준비된다.
4. 퍼사드를 쓰더라도 서브시스템에는 여전히 직접 접근할 수 있다.
   서브시스템 클래스의 고급 기능이 필요하다면 언제든지 마음대로 사용할 수 있다.
******************************/

/*****************************
퍼사드에 대해서 몇가지 짚고 넘어가자.
퍼사드 클래스에서는 서브시스템 클래스들을 캡슐화하지 않는다.
그냥 서브 시스템의 기능을 사용할 수 있는 간단한 인터페이스를 제공할 뿐.
클라이언트에서 특정 인터페이스가 필요하다면 서브시스템 클래스를 그냥 사용하면 된다.

한 서브시스템에 대해 만들 수 있는 퍼사드의 개수는 상관이 없으며,
단순히 서브시스템을 활용하는것 외에, 자유롭게 "똑똑한" 기능을 추가할 수 있다.

퍼사드 패턴으로 클라이언트 구현과 서브시스템을 분리시킬 수 있다.
변경에 있어서 유연해진다는 것.
******************************/

/*****************************
다시 홈 씨어터로 돌아와서, HomeTheaterFacade를 구현해보자.
어... 저 수많은 서브시스템의 클래스들은... 코드 찾으면 나올까...?
헤드 퍼스트 이 나쁜놈들이 코드를 다 분리해놨어! 복붙하기가 어렵잖아!
******************************/

namespace HomeTheater
{
    #region Home Theater Classes
    // 노가다다 노가다 흫흐흐흐흫
    public class Amplifier
    {
        public void On()
        {
            Console.WriteLine("Amplifier on");
        }
        public void SurroundSoundMode()
        {
            Console.WriteLine("Amplifier surround sound on (5 speakers, 1 subwoofer)");
        }
        public void Volume(int vol)
        {
            Console.WriteLine("Amplifier setting volume to " + vol);
        }
        public void Off()
        {
            Console.WriteLine("Amplifier off");
        }
    }
    public class Tuner
    {
    }
    public class DvdPlayer
    {
        string movie = "";

        public void On()
        {
            Console.WriteLine("DVD Player on");
        }
        public void Play(string movie)
        {
            this.movie = movie;
            Console.WriteLine("DVD Player playing \"" + movie + "\"");
        }
        public void Stop()
        {
            Console.WriteLine("DVD Player stopped \"" + movie + "\"");
        }
        public void Eject()
        {
            movie = "";
            Console.WriteLine("DVD Player eject");
        }
        public void Off()
        {
            Console.WriteLine("DVD Player off");
        }
    }
    public class CdPlayer
    { }
    public class Projector
    {
        public void On()
        {
            Console.WriteLine("Projector on");
        }
        public void WideScreenMode()
        {
            Console.WriteLine("Projector in widescreen mode (16x9 aspect ratio)");
        }
        public void Off()
        {
            Console.WriteLine("Projector off");
        }
    }
    public class TheaterLights
    {
        public void Dim(int level)
        {
            Console.WriteLine("Theater Ceiling Lights dimming to " + level + "%");
        }
        public void On()
        {
            Console.WriteLine("Theater Ceiling Lights on");
        }
    }
    public class Screen
    {
        public void Down()
        {
            Console.WriteLine("Theater Screen going down");
        }
        public void Up()
        {
            Console.WriteLine("Theater Screen going up");
        }
    }
    public class PopcornPopper
    {
        public void On()
        {
            Console.WriteLine("Popcorn Popper on");
        }
        public void Pop()
        {
            Console.WriteLine("Popcorn Popper popping popcorn!");
        }
        public void Off()
        {
            Console.WriteLine("Popcorn Popper off");
        }
    }
    #endregion

    public class HomeTheaterFacade
    {
        Amplifier amp;
        Tuner tuner;
        DvdPlayer dvd;
        CdPlayer cd;
        Projector projector;
        TheaterLights lights;
        Screen screen;
        PopcornPopper popper;

        public HomeTheaterFacade(
            Amplifier amp,
            Tuner tuner,
            DvdPlayer dvd,
            CdPlayer cd,
            Projector projector,
            Screen screen,
            TheaterLights lights,
            PopcornPopper popper)
        {
            this.amp = amp;
            this.tuner = tuner;
            this.dvd = dvd;
            this.cd = cd;
            this.projector = projector;
            this.lights = lights;
            this.screen = screen;
            this.popper = popper;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("Get ready to watch a movie...");

            popper.On();
            popper.Pop();
            lights.Dim(10);
            screen.Down();
            projector.On();
            projector.WideScreenMode();
            amp.On();
            amp.SurroundSoundMode();
            amp.Volume(5);
            dvd.On();
            dvd.Play(movie);
        }

        public void EndMovie()
        {
            Console.WriteLine("Shutting movie theater down...");

            popper.Off();
            lights.On();
            screen.Up();
            projector.Off();
            amp.Off();
            dvd.Stop();
            dvd.Eject();
            dvd.Off();
        }
    }
}

/*****************************
정말 힘들고 쓸데없이 긴 구현이었다. 테스트해보자.
******************************/

namespace CSDesignPatternTrack
{
    using HomeTheater;
    partial class Class07
    {
        void Section02()
        {
            Console.WriteLine(" = Section #02 = ");

            Amplifier amp = new Amplifier();
            Tuner tuner = new Tuner();
            DvdPlayer dvd = new DvdPlayer();
            CdPlayer cd = new CdPlayer();
            Projector projector = new Projector();
            Screen screen = new Screen();
            TheaterLights lights = new TheaterLights();
            PopcornPopper popper = new PopcornPopper();

            HomeTheaterFacade homeTheater =
                new HomeTheaterFacade(amp, tuner, dvd, cd, projector, screen, lights, popper);
            homeTheater.WatchMovie("The Shawshank Redemption");
            Console.WriteLine("\nBeing touched with the film...\n");
            homeTheater.EndMovie();
        }
    }
}

/*****************************
결과적으로 이 패턴이 주는 의의는, 클라이언트가 WatchMovie() 메소드를 호출하는것 만으로
영화를 볼 수 있는 환경을 만들고 영화를 재생시킬 수 있다는것.
여기서 클라이언트가 HomeTheaterFacade를 쓰지 않고 직접 서브시스템을 이루는 인스턴스들을 다룰 수도 있다.

Pattern #09 Facade
어떤 서브시스템의 일련의 인터페이스에 대한 통합된 인터페이스를 제공한다.
퍼사드에서 고수준 인터페이스를 정의하기 때문에 서브시스템을 더 쉽게 사용할 수 있다.

여기서 새겨둬야 할 점은, 패턴이 어떤 용도로 쓰이는지 잘 알아둬야 한다는 것이다.
정의에 따르면 퍼사드 패턴은 단순화된 인터페이스를 통해서 서브시스템을 더 쉽게 사용할 수 있도록 하기 위한 용도로 쓰인다.

이런 퍼사드 패턴이 우리가 지키게끔 만들어주는 디자인 원칙.
==============================
최소 지식 원칙 - 정말 친한 친구하고만 얘기하라.
==============================

객체 사이의 상호작용은 될 수 있으면 아주 가까운 "친구" 사이에서만 허용하는 것이 좋다.
시스템을 디자인 할 때, 어떤 객체든 그 객체와 상호작용을 하는 클래스의 개수에 주의해야 하며,
그런 객체들과 어떤 식으로 상호작용을 하는지도 주의를 기울여야 한다는 뜻이다.
이 원칙을 지킴으로, 여러 클래스들이 복잡하게 얽혀서 시스템의 한 부분을 변경했을 때
다른 부분까지 줄줄이 고쳐야 되는 상황을 미리 방지할 수 있다.

그렇다면 어떻게 여러 객체들과 인연을 맺는 것을 피할 수 있을까?
최소 지식 원칙에서는 몇 가지 가이드라인을 제시한다.
어떤 메소드에서든지 다음 네 종류의 객체의 메소드만 호출하면 된다.
 - 객체 자체
 - 메소드에 매개변수로 전달된 객체
 - 그 메소드에서 생성하거나 인스턴스를 만든 객체
 - 그 객체에 속하는 구성요소 ("A에는 B가 있다" 관계)

이 가이드라인에 따르면 다른 메소드를 호출해서 리턴 받은 객체의 메소드를 호출하는 것도 바람직하지 않다.
왜냐? 그렇게 하면 다른 객체의 일부분에 대해서 요청을 하게 되는 것이고,
그러다 보면 직접적으로 알고 지내는 객체의 개수가 늘어나게 된다.
이 경우 최소 지식 원칙을 따르려면 그 객체 쪽에서 대신 요청을 하도록 만들어야 한다.
다음 코드를 보자.

// 원칙을 따르지 않은 경우
public float getTemp()
{
    Thermometer thermometer = station.getThermometer();
    return thermometer.getTemperature();
} // Station으로부터 thermometer라는 개체를 받은 다음, 그 객체의 getTemperature() 메소드를 직접 호출한다.

// 원칙을 따르는 경우
public float getTemp()
{
    return station.getTemperature();
} // 최소 지식 원칙을 적용하여 Station 클래스에 thermometer에 요청을 해 주는 클래스를 추가했다.
  // 이렇게 해서 의존해야 하는 클래스의 개수를 줄일 수 있다.

최소 지식 원칙을 따르면서 메소드를 호출하는 예제를 하나 더 보자.
public class Car
{
    Engine engine;  // 이 클래스의 구성요소. 이 구성요소의 메소드는 호출 가능.

    public Car()
    { } // 엔진 초기화 등을 처리

    public void Start(Key key)      // 매개변수로 전달된 객체. 메소드 호출 가능.
    {
        Doors doors = new Doors();  // 새로 생성한 객체. 이 객체의 메소드도 호출 가능.

        Boolean authorized = key.Turns();   // 매개변수로 전달한 객체의 메소드
    
        if (authorized)
        {
            engine.Start();             // 구성요소의 메소드
            UpdateDashboardDisplay();   // 객체 내에 있는 메소드
            doors.Lock();               // 직접 생성한 객체의 메소드
        }
    }

    public void UpdateDashboardDisplay()
    { } // 디스플레이 갱신
}

물론 이 원칙도 만능은 아니다.
원칙을 적용하다 보면 다른 구성요소에 대한 메소드 호출을 처리하기 위해 래퍼 클래스를 더 만들어야 할 수도 있다.
즉, 시스템이 더 복잡해지고, 성능이 떨어질 수도 있다.
당장에 System.out.println() 메소드도 원칙에는 위배된다.

오늘 내용 끝.
기억해야 할 것, 퍼사드 패턴, 최소 지식 원칙.
갓겜 트오세 하러 갑시다.

음... 끝내기 전에...
하루 정도 복습하는 날을 잡는건 어떨까, 시험 까지는 아니더라도.
이제 한 절반 정도 온 것 같다.
******************************/

// Anteater