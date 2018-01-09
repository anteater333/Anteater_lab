/*****************************
* 2017.12.06 디자인 패턴
* 목표 : Chapter 02 - Observer 패턴
     ******* 코멘트 *******
 "객체들에게 연락망을 돌립시다."

옵저버 패턴은 간단히 말해 뭔가 중요한 일이 일어났을 때 객체들한테 새소식을 알려줄 수 있는 패턴이다.
객체 쪽에서는 계속해서 정보를 받을지 여부를 실행중에 결정할 수 있다.

오늘은 거기에 더해 일대다 관계와 느슨한 결합에 대해서도 배울 예정이다. 소프트웨어 공학이 생각난다.
옵저버 패턴이 있으면 패턴당(Pattern party?)에서 한 자리를 차지할 거랜다. 양키개그진짜...
******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
SimUDuck에 이어서 오늘 다룰 사례는 바로,
Weather-O-Rama 사의 차세대 인터넷 기반 기상 정보 스테이션 구축 프로젝트.
참고로, CEO 이름이 조니 허리케인이다.

오늘 다룰 애플리케이션은 간단히 말해 기상 모니터링 애플리케이션이다.
이 시스템은 센서들을 통해 실제 기상 정보를 수집하는 장비인 기상 스테이션과,
기상 스테이션으로부터 오는 데이터를 추적하는 객체인 WeatherData 객체,
그리고 유저에게 현재 기상 조건을 보여줄 디스플레이,
총 세 요소로 이루어진다.

WeatherData 객체가 기상 스테이션 장비로부터 데이터를 가져와 디스플레이 장치에 표시할 항목은 다음과 같다.
1. 현재 조건(온도, 습도, 압력)
2. 기상 통계
3. 간단한 기상 예보

정리하자면 우리는 Weather-O-Rama 사에서 제공할 WeatherData 객체를 통해
현재 조건, 기상 통계, 기상 예측 세 항목을 디스플레이 장비에서 갱신해 가면서 보여주는 어플리케이션을 만들어야한다.

WeatherData 객체는 다음과 같다.
******************************/

namespace WeatherORamaProto
{
    public partial class WeatherData
    {
        private float temperature;
        private float humidity;
        private float pressure;

        public float Temperature { get { return temperature; } }
        public float Humidity { get { return humidity; } }
        public float Pressure { get { return pressure; } }

        /// <summary>
        /// 기상 관측값이 갱신될 때마다 알려주기 위한 메소드
        /// </summary>
        public void MeasurementsChanged()
        {

        }
    }
}

/*****************************
지금 알고 있는 것들
1. WeatherData 클래스에는 세 가지 측정값을 알아내기 위한 getter 메소드가 있다. 나는 C#이니까 읽기전용 프로퍼티로 만들었다.
2. 새로운 기상 측정 데이터가 나올 때마다 MeasurementsChanged() 메소드가 호출된다.
   (어떤 원리로 호출되는지는 중요치 않다.)
3. 기상 데이터를 사용하는 세 개의 디스플레이 항목을 구현해야 한다.
   현재 조건, 기상 통계, 기상 예보. WeatherData에서 새로운 측정값이 들어올 때마다 디스플레이를 갱신해야한다.
4. 시스템이 확장 가능해야한다. 다른 개발자들이 별도의 디스플레이 항목을 만들 수 있도록 해야하고,
   사용자들이 애플리케이션에 마음대로 디스플레이 항목을 추가/제거할 수 있도록 해야한다.
   지금은 세 가지 기본 디스플레이 형식에 대해서만 알고 있다.

한번 MeasurementsChanged()를 구현해보자.
******************************/

/*
public void MeasurementsChanged()
{
    float temp = Temperature;
    float humidity = Humidity;
    float pressure = Pressure;

    currentConditionsDisplay.update(temp, humidity, pressure); // 현재상태 디스플레이
    statisticsDisplay.update(temp, humidity, pressure); // 기상통계 디스플레이
    forecastDisplay.update(temp, humidity, pressure); // 기상예보 디스플레이
}
*/

/*****************************
한 눈에 봐도 문제가 있어보인다. 주석처리 돼있잖아.

어제 배웠던 내용을 생각해보자.
구현에 맞춰서 프로그래밍 하지 않아야한다.
currentConditionsDisplay 같은 ~Display 시리즈들은 보면 구체적인 구현에 맞춰서 코딩되었다는걸 알 수 있다.
추가/제거가 아주 불편해진다. 다행인것은 Display 들은 update라는 공통된 인터페이스를 쓰도록 할 수 있다.
따라서 바꿀 수 있는 부분, Display 들을 캡슐화 해야한다.
******************************/

/*****************************
근데 옵저버 패턴은 언제나와요?
그러니까 이제 옵저버 패턴에 대해 알아보자. 먼저 알아보고 적용해보자.

신문이나 잡지는 어떤 과정으로 구독하는가?
1. 신문사가 사업을 시작하고 신문을 찍어내기 시작한다.
2. 독자가 특정 신문사에 구독 신청을 하면 매번 새로운 신문이 나올 때마다 배달을 받을 수 있다.
   계속 구독자로 남아있는 이상 계속해서 신문을 받을 수 있다.
3. 신문을 더 이상 보고 싶지 않으면 구독 해지 신청을 한다.
4. 신문사가 계속 영업을 하는 이상 여러 개인 독자, 호텔, 항공사 및 기타 회사등에서부터 꾸준히 구독 및 해지가 계속된다.
어... 왠지 이벤트가 생각나는데...

어쨌든 여기서 신문사와 구독자를 합치면 바로 옵저버 패턴이다.
여기서 신문사를 주제(subject), 구독자를 옵저버(observer)라고 부른다.
이제부턴 책에 있는 그림들을 잘 살펴볼것.

 < 주제-옵저버 관계 > 
주제 객체에서는 일부 데이터를 관리한다.
주제의 데이터가 달라지면 옵저버한테 그 소식과 새로운 데이터 값이 어떤 방식으로든 전해진다.
옵저버 객체들은 주제 객체를 구독하고 있으며(즉, 주제 객체에 등록됨) 주제의 데이터가 바뀌면 갱신 내용을 전달받는다.

 < 옵저버 등록 > 
이때 옵저버도, 주제도 아닌 객체가 하나 있다고 하자. 외로운 오리 객체 Duck.
Duck이 주제 객체에 옵저버가 되고 싶다고 말을 하면(즉, 주제 객체에 등록하면),
Duck은 기존의 옵저버들처럼 주제 객체에게서 데이터를 받을 수 있게된다.

 < 옵저버 해제 > 
옵저버들 중 Mouse 객체는 이제 주제 객체로부터 오는 데이터가 더이상 필요없어졌다.
Mouse 객체는 주제 객체에 탈퇴 요청을 하고, 주제 객체가 옵저버들 중 Mouse를 제외시킨다.
이제 Mouse는 주제 객체로 부터 새로운 값을 받을 수 없다. 다시 등록을 요청하지 않는 이상.

그 다음 페이지에는 5분 드라마가 나오는데 심심풀이로 읽어보자. 왠지 같은 설명을 한 세 네번 반복하는거같다.
다만 5분 드라마의 마지막에 재미있는 이야기가 나온다.
옵저버인줄 알았던 객체가 사실 또 다른 옵저버들을 가지고있는 주제였던것!
어떤 객체가 옵저버이면서 주제일 수 있다는 것을 보여주고 있다.
******************************/

/*****************************
이쯤 설명 했으니 이제 옵저버 패턴에 대해 어느 정도 이해할 수 있을 것 같다.

Pattern #02 Observer
옵저버 패턴에서는 한 객체의 상태가 바뀌면 그 객체에 의존하는 다른 객체들한테 연락이 가고
자동으로 내용이 갱신되는 방식으로 일대다(one-to-many) 의존성을 정의한다.

다시 말하자면,
옵저버 패턴에서는 일련의 객체들 사이에서 일대다 관계를 정의한다.
한 객체의 상태가 변경되면 그 객체에 의존하는 모든 객체에 연락이 간다.

일대다 관계는 주제와 옵저버에 의해 정의된다.
옵저버는 주제에 의존한다. 주제의 상태가 바뀌면 옵저버에게 연락이 간다.
연락 방법에 따라 옵저버에 있는 값이 새로운 값으로 갱신될 수도 있다.

p.90의 클래스 다이어그램을 살펴보자.
                                옵저버
<<interface>> Subject           ========>       <<interface>> Observer
registerObserver()                              update()
removeObserver()                                
notifyObservers()
    ∧                                              ∧
    │                                              │
    │                                              │
    │                                              │
    │                                              │
    │                                              │
    │                          주제                │
ConcreteSubject                 <========       ConcreteObserver
registerObserver() {...}                        update()
removeObserver() {...}                          // 기타 옵저버용 메소드
notifyObservers() {...}
State { get; set; }

하나 하나 정리해보자.
1. <<interface>> Subject. 주제를 나타내는 Subject 인터페이스.
   객체에서 옵저버로 등록하거나 옵저버 목록에서 탈퇴하고 싶을 때는 이 인터페이스에 있는 메소드를 사용한다.
2. <<interface>> Observer. 옵저버가 될 가능성이 있는 객체에서는 반드시 Observer 인터페이스를 구현해야 한다.
   이 인터페이스에는 주제의 상태가 바뀌었을 때 호출되는 update() 메소드 밖에 없다.
3. ConcreteSubject. 주제 역할을 하는 구상 클래스.
   항상 Subject 인터페이스를 구현해야 한다.
   주제 클래스에서는 등록 및 해지를 위한 메소드 외에,
   상태가 바뀔 때 마다 모든 옵저버들에게 연락을 하기 위한 notifyObservers() 메소드도 구현해야 한다.
   이는 Subject 인터페이스에 이미 정의되어 있는 것들이고,
   상태를 설정하고 알아내기 위한 프로퍼티 State가 있을 수도 있다.
4. ConcreteObserver.
   옵저버 인터페이스만 구현한다면 무엇이든 옵저버 클래스가 될 수 있다.
   각 옵저버는 특정 주제 객체에 등록을 해서 연락을 받을 수 있다.

여기서 데이터의 주인은 주제이다.
옵저버는 데이터가 변경되었을 때 주제에서 갱신해 주기를 기다리는 입장이기 때문에 의존성을 가진다고 할 수 있다.

느슨한 결합
두 객체가 느슨하게 결합되어 있다는 것은, 그 둘이 상호작용을 하긴 하지만 서로에 대해 서로 잘 모른다는 것을 의미한다.
옵저버 패턴에서는 주제와 옵저버가 느슨하게 결합되어 있는 객체 디자인을 제공한다.
왜 그런지 천천히 살펴보자.
주제가 옵저버에 대해서 아는 것은 옵저버가 특정 인터페이스를 구현한다는 것 뿐이다.
옵저버는 언제든지 새로 추가할 수 있다. 심지어 실행 중에도.
새로운 형식의 옵저버를 추가하려고 할 때도 주제를 전혀 변경할 필요가 없다. 주제는 그냥 간단하게 I don't giva a F. 라고 말할 뿐.
주제와 옵저버는 서로 독립적으로 재사용할 수 있다. 그 둘은 서로 단단하게 결합된 상태가 아니니까 문제없다.
따라서 주제나 옵저버가 바뀌더라도 서로한테 영향을 미치지는 않는다.

디자인 원칙
==============================
서로 상호작용을 하는 객체 사이에서는
가능하면 느슨하게 결합하는 디자인을 사용해야 한다.
==============================

느슨하게 결합하는 디자인을 사용하면 변경 사항이 생겨도 무난히 처리할 수 있는 유연한 객체지향 시스템을 구축할 수 있다.
객체 사이의 상호의존성을 최소화할 수 있기 때문.
******************************/

/*****************************
이제 Weather-O-Rama 사의 프로젝트로 돌아와 보자.
WeatherData 클래스를 포함해서 기상 스테이션을 구현하는 데 필요한 클래스들에 대해 정리해야한다.
우선 일-대-다 에서 '일'은 WeatherData 클래스, '다'는 디스플레이 항목에 해당된다.
즉 WeatherData 객체를 주제 객체, 디스플레이 항목을 옵저버.
다이어그램은 p.94를 참고.
이를 통해 구현해보자.
******************************/

namespace WeatherORama
{
    #region Interfaces
    public interface IWeatherSubject
    {
        void RegisterObserver(IWeatherObserver o);
        void RemoveObserver(IWeatherObserver o);    // 두 메소드 모두 Observer를 인자로 받는다.
        void NotifyObservers();
    }

    public interface IWeatherObserver
    {
        void Update(float temp, float humidity, float pressure);    // 이 방법이 최선일까?
    }
    
    public interface IDisplayElement
    {
        void Display(); // 디스플레이 항목을 화면에 표시해야 하는 경우에 호출하는 메소드.
    }
    #endregion

    #region Subject
    public class WeatherData : IWeatherSubject
    {
        private ArrayList observers;    // observer 객체를 저장하기 위한 리스트
        private float temperature;
        private float humidity;
        private float pressure;

        public WeatherData()
        {
            observers = new ArrayList();
        }

        #region Methods
        public void RegisterObserver(IWeatherObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IWeatherObserver o)
        {
            int i = observers.IndexOf(o);
            if (i >= 0)
                observers.Remove(i);
        }

        public void NotifyObservers()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                IWeatherObserver observer = (IWeatherObserver)observers[i];
                observer.Update(temperature, humidity, pressure);
            }
        }

        public void MeasuremetsChanged()
        {
            NotifyObservers();
        }

        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.pressure = pressure;
            MeasuremetsChanged();
        }

        // Etc.
        #endregion

        #region Properties
        public float Temperature
        {
            get { return temperature; }
            set
            {
                this.temperature = value;
                MeasuremetsChanged();
            }
        }
        public float Humidity
        {
            get { return humidity; }
            set
            {
                this.humidity = value;
                MeasuremetsChanged();
            }
        }
        public float Pressure
        {
            get { return pressure; }
            set
            {
                this.pressure = value;
                MeasuremetsChanged();
            }
        }
        #endregion
    }
    #endregion

    #region Observer
    public class CurrentConditionsDisplay : IWeatherObserver, IDisplayElement
    {
        private float temperature;
        private float humidity;
        private IWeatherSubject weatherData; // 여기선 생성자에서 등록하는데에만 사용하지만
                                             // 나중에 옵저버 목록에서 탈퇴할 때 사용할 수 있다.

        public CurrentConditionsDisplay(IWeatherSubject weatherData)    // 생성자에서 주제에 등록
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            Display();  // 업데이트 후 출력
        }

        public void Display()   // private는 아니네? 사실 나중에 모델-뷰-컨트롤러 패턴을 배울때 자세히 알아볼 예정이란다.
        {
            Console.WriteLine("Current conditions: " + temperature
                + "F degrees and " + humidity + "% humidity");
        }
    }
    #endregion
}

/*****************************
이렇게 구현한 애플리케이션을 테스트해보자.
******************************/

namespace CSDesignPatternTrack
{
    using WeatherORama;

    partial class Class02
    {
        public Class02()
        {
            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
            /// 2 줄 더 있지만 귀찮으니 패스. 그냥 세 디스플레이 모두를 구현한 코드를 썼을때임.
            /// 보기엔 다채롭겠지만... 중요한건 아니잖어?

            weatherData.SetMeasurements(80, 65, 30.4f);
            weatherData.Temperature = 78;
            weatherData.Humidity = 90;

            Section02();
        }
    }
}

/*****************************
weatherData가 가지고 있는 값이 바뀌기만 했는데 막 출력이 된다.
이참에 연습문제나 한 번 풀어볼까.
클라이언트께서 체감 온도(heat index) 디스플레이 항목이 필요하다고 한다.
******************************/

namespace WeatherORama
{
    public class HeatIndexDisplay : IWeatherObserver, IDisplayElement
    {
        private float heatIndex = 0.0f;
        private WeatherData weatherData;

        public HeatIndexDisplay(WeatherData weatherData)
        {
            this.weatherData = weatherData;
            weatherData.RegisterObserver(this);
        }

        /// <summary>
        /// source :
        /// https://github.com/bethrobson/Head-First-Design-Patterns
        /// </summary>
        private float ComputeHeatIndex(float t, float rh)
        {
            float index = (float)((16.923 + (0.185212 * t) + (5.37941 * rh) - (0.100254 * t * rh)
                + (0.00941695 * (t * t)) + (0.00728898 * (rh * rh))
                + (0.000345372 * (t * t * rh)) - (0.000814971 * (t * rh * rh)) +
                (0.0000102102 * (t * t * rh * rh)) - (0.000038646 * (t * t * t)) + (0.0000291583 *
                (rh * rh * rh)) + (0.00000142721 * (t * t * t * rh)) +
                (0.000000197483 * (t * rh * rh * rh)) - (0.0000000218429 * (t * t * t * rh * rh)) +
                0.000000000843296 * (t * t * rh * rh * rh)) -
                (0.0000000000481975 * (t * t * t * rh * rh * rh)));
            return index;
        }

        public void Update(float temperature, float humidity, float pressure)
        {
            heatIndex = ComputeHeatIndex(temperature, humidity);
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Heat index is " + heatIndex);
        }
    }
}

namespace CSDesignPatternTrack
{
    using WeatherORama;
    partial class Class02
    {
        void Section02()
        {
            Console.WriteLine(" = Section #02 = ");

            WeatherData weatherData = new WeatherData();

            CurrentConditionsDisplay current = new CurrentConditionsDisplay(weatherData);
            HeatIndexDisplay heatIndex = new HeatIndexDisplay(weatherData);

            weatherData.SetMeasurements(80, 75, 29.2f);

            Section03();
        }
    }
}

/*****************************
헤헤헤 재미따...헤헤헤...
******************************/