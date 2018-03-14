using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
아무튼 우리가 구현한 옵저버 패턴을 보면 데이터 전달에 있어 패턴을 배우기 전에 우리가 해왔던 방식과 다른점이 있다.
이때까진 옵저버에 해당되는 객체에서 주제.getData() 혹은 Data 프로퍼티 이런식으로 데이터를 가져왔지만, (풀 방식)
옵저버 패턴에선 주제에서 직접 데이터를 전달해준다. (푸시 방식)

왜 이 이야기를 했는가 하면, 이제부터 내장 옵저버 패턴을 알아볼 것이기 때문.
자바나, 자바를 밴치마킹한 C#이나 옵저버 패턴을 기본으로 제공해준다. 그래서 내가 앞에서 인터페이스 이름을 저따구로 지었지.
내장 옵저버 패턴을 사용하면 더 많은 기능을 사용할 수 있다. 푸시, 풀 두 방식 모두 사용 가능하다는 뜻.

내장 옵저버 패턴에서 주제는 Observable 인터페이스를 구현하고,
옵저버는 Observer 인터페이스를 구현하면 된다.
java의 Observer 인터페이스의 메소드는 update() 하나만 있는건 똑같고, 인자가 조금 다르다.
참고로 책에선 java 기반이니까 C# 메소드는 MSDN 가서 알아서 잘 찾아봐야함.
IObservable
https://msdn.microsoft.com/ko-kr/library/dd990377(v=vs.110).aspx
IObserver
https://msdn.microsoft.com/ko-kr/library/dd783449(v=vs.110).aspx
찾아보니까 많이 다른데...?

C#의 IObserver 구문을 보자
public interface IObserver<in T>
옵저버 등록을 템플릿 쓰듯이 하는 것 같다. --> 잘못된 추측!
메소드는 3개가 있다.
 OnNext() 메서드를 일반적으로 관찰자에 게 새 데이터 또는 상태 정보를 제공 하도록 공급자에 의해 호출 됩니다.
 OnError() 메서드는 일반적으로 사용할 수 없거나 손상 된 데이터 임을 나타내기 위해 공급자에 의해 호출, 또는 공급자에서 일부 오류 조건이 발생 했습니다.
 OnCompleted() 메서드는 일반적으로 관찰자에 게 알림 전송을 완료 했음을 나타내기 위해 공급자에 의해 호출 됩니다.
  - MSDN 문서에서 가져옴.

IObservable 구문은 다음과 같다.
public interface IObservable<out T>
여기도 T가 있네, 이번엔 out T.
문서를 천천히 읽어본 결과 T의 역할을 알아내었다.
T는 전송할 데이터의 형태라고 생각하면 된다.
앞서 구현했던 예제에서 update() 메소드에 인자로 넣은 값들을 생각하면 될듯.
IObservable에는 addObserver() 대신 Subscribe() 메소드가 있다. 다른 메소드는 없음.
근데 리턴 타입이 IDisposal이다. 뭔가 좀 어려워 보인다.
MSDN 문서에서는 이렇게 구현했다.
   public IDisposable Subscribe(IObserver<Location> observer) 
   {
      if (! observers.Contains(observer)) 
         observers.Add(observer);
      return new Unsubscriber(observers, observer);
   }
그리고 Unsubscriber라는 클래스를 IObservable을 구현한 클래스의 맴버로써 새로 정의했다.

감이 잘 안잡히지만 일단 직접 해보자.
******************************/

namespace WeatherORama
{
    /// <summary>
    /// Observer 패턴에 사용할 데이터 타입
    /// </summary>
    public struct CSWeatherData
    {
        private float temperature;
        private float humidity;
        private float pressure;

        public CSWeatherData(float temp, float humi, float press)
        {
            this.temperature = temp;
            this.humidity = humi;
            this.pressure = press;
        }

        public float Temperature
        { get { return this.temperature; } }
        public float Humidity
        { get { return this.humidity; } }
        public float Pressure
        { get { return this.pressure; } }
    }

    public class CSWeatherStation : IObservable<CSWeatherData>
    {
        private List<IObserver<CSWeatherData>> observers;

        public CSWeatherStation()
        {
            observers = new List<IObserver<CSWeatherData>>();
        }

        /// <summary>
        /// 옵저버 등록 메소드
        /// </summary>
        public IDisposable Subscribe(IObserver<CSWeatherData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<CSWeatherData>> _observers;
            private IObserver<CSWeatherData> _observer;

            public Unsubscriber(List<IObserver<CSWeatherData>> observers, IObserver<CSWeatherData> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        /// <summary>
        /// notifyObservers()의 역할을 하는 메서드
        /// </summary>
        public void CheckWeather(Nullable<CSWeatherData> data)
        {
            foreach (var observer in observers)
            {
                if (!data.HasValue)
                    observer.OnError(new NullReferenceException()); // 에러 발생을 알림. 배송 오류!
                else
                    observer.OnNext(data.Value);    // Update(). 신문 나왔어요.
            }
        }

        /// <summary>
        /// 전송 종료를 알리는 메서드
        /// </summary>
        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted(); // 전송 종료를 알림. 폐간합니다?

            observers.Clear();
        }
    }

    public class StatisticsDisplay : IObserver<CSWeatherData>, IDisplayElement
    {
        private IDisposable unsubscriber;
        private float maxTemp;
        private float minTemp;
        private float tempSum;
        private int numReadings;

        public StatisticsDisplay()
        {
            maxTemp = 0.0f;
            minTemp = 200.0f;
            tempSum = 0.0f;
        }

        public virtual void Subscribe(IObservable<CSWeatherData> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);    // 등록
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("이제 더는 받을 정보가 없을 때 발생합니다.");
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        public virtual void OnNext(CSWeatherData data)
        {
            tempSum += data.Temperature;
            numReadings++;

            if (data.Temperature > maxTemp)
                maxTemp = data.Temperature;
            if (data.Temperature < minTemp)
                minTemp = data.Temperature;

            Display();
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public void Display()
        {
            Console.WriteLine("Avg/Max/Min temperature = " + (tempSum / numReadings)
                + "/" + maxTemp + "/" + minTemp);
        }
    }
}


namespace CSDesignPatternTrack
{
    using WeatherORama;
    partial class Class02
    {
        void Section03()
        {
            Console.WriteLine(" = Section #03 = ");

            CSWeatherStation provider = new CSWeatherStation();
            StatisticsDisplay observer1 = new StatisticsDisplay();
            observer1.Subscribe(provider);

            provider.CheckWeather(new CSWeatherData(70, 60, 30));
            provider.CheckWeather(new CSWeatherData(50, 60, 30));
            provider.CheckWeather(new CSWeatherData(60, 60, 30));
            provider.CheckWeather(new CSWeatherData(59, 60, 30));
            provider.CheckWeather(new CSWeatherData(100, 60, 30));
        }
    }
}

/*****************************
책에는 java에서 observable의 단점을 말하고있다.
자바에서는 Observable이 인터페이스가 아니고 클래스라고 한다.
재사용성에 큰 타격이 올 수 밖에. 그러니까 갓갓 C# 씁시다.
참고로 자바 스윙에서 썼던 addActionListener() 메소드가 바로 이 옵저버.

끝으로, 주의점.
옵저버 패턴을 사용할 때 옵저버에게 연락이 가는 순서에 의존하면 절대 안된다.
순서가 정해져 있지 않다고 생각하고 설계해야한다.
******************************/

// Anteater