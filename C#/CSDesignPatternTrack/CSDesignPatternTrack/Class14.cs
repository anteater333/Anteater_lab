﻿/*****************************
* 2018.01.04 디자인 패턴
* 목표 : Chapter 13 - 패턴과 함께 하는 행복한 삶
     ******* 코멘트 *******
 "실전에서의 디자인 패턴"

지난 두 챕터 정도를 아주 날로 맛있게 먹은 듯 하다.
사실 오늘도 그럴 느낌이 쌔하게 든다.
일단은 클래스를 만들긴 했는데, 코드를 구현할 건 하나도 없다.
******************************/

/*****************************
단도직입적으로,
실전에서 패턴을 활용하는 데 도움이 될 팁들을 바로 알아보자.

 - 널리 퍼져 있는 "디자인 패턴"의 정의에 대한 오해에 대해서 확실히 파악하고 넘어가자.
 - 디자인 패턴 카탈로그에 대해 알아보고, 그런 카탈로그의 필요성을 제대로 파악하도록 하자.
 - 적절한 시기에 디자인 패턴을 사용하도록 하자.
 - 패턴을 제 범주에 맞게 사용하도록 하자.
 - 패턴을 발견하는 것은 고수들만이 할 수 있는 일은 아니다.
   간단한 설명을 읽어보고 나면 우리도 패턴을 만들어낼 수 있을 수도 있다.
 - 기타등등 쓸데없는 것들...
******************************/

/*****************************
패턴을 사전적으로 정의하면 뭘까?

패턴이란 특정 컨텍스트 내에서 주어진 문제에 대한 해결책이다.

이 정의를 좀 더 파헤쳐 보자.
 - 컨텍스트 : 패턴이 적용되는 상황.
              반복적으로 일어날 수 있는 상황이어야 한다.
 - 문제 : 컨텍스트 내에서 이루고자 하는 목적.
          컨텍스트 내에서 생길 수 있는 제약조건도 문제에 포함된다.
 - 해결책 : 우리가 찾아내야 할 것.
            누구든지 적용해서 일련의 제약조건 내에서 목적을 달성할 수 있는 일반적인 디자인.

간단히 예를 들자면,
 - 문제 : 어떻게 회사에 제 시간에 도착할 것인가?
 - 컨텍스트 : 열쇠를 차에 두고 문을 잠그고 나와 버렸다.
 - 해결책 : 유리를 깬다. 차에 들어간다. 시동을 걸고 차를 몰고 출근하다.

쉽다...?

사실 이런걸 "디자인 패턴" 이라고 하긴 좀 그렇지.
왜냐면, 패턴이라는 것은 반복적으로 등장하는 문제에 적용될 수 있어야 하기 때문.
차 유리를 깨는게 반복적으로 적용할 수 있는 문제는 아니지.
또 다른 이유로, 이런 해결책을 다른 사람한테 알려주고, 그사람이 처한 문제에 대한 해결책으로 적용하게 하기가 힘들다.
또, 몹시 중요한 결점으로, 패턴의 특징을 위배했다. 패턴에 이름이 없잖아!
단순히 농담이 아니라, 패턴에 이름이 없다면 다른 개발자들과 그 패턴에 대해서 토론하는 것이 불가능하다.

참고로, 문제는 목적과 일련의 제약조건으로 구성된다고 했는데, 이를 포스(force)라고 부르는 사람도 있다고 한다.
may the force be with you...
******************************/

/*****************************
패턴 카탈로그
패턴 카탈로그에서는 일련의 패턴을 택해서 각각의 패턴을 다른 패턴과의 관계와 함께 자세하게 설명해준다.
그러니까, 패턴 카탈로그는 여러 가지가 있다.
기본 디자인 패턴에 대한 카탈로그도 있고, EJB 패턴 같이 특정 영역에서 쓰이는 패턴에 대한 카탈로그도 있다.

제일 유명한 패턴 카탈로그는 아마도 GoF의 패턴 책이 아닐까.
******************************/

/*****************************
디자인 패턴을 기억하기에 도움이 되는 방법.
디자인 패턴을 분류해보자.
만약에 새로운 디자인 패턴을 발견했을때에, 이런 분류에 맞춰서 패턴을 분류하면 좋을것이다.

 - 생성 관련 패턴 -
객체 인스턴스 생성을 위한 패턴으로, 클라이언트와 그 클라이언트에서 생성해야 할 객체 인스턴스 사이의 연결을 끊어주는 패턴이다.
=> 싱글턴, 추상 팩토리, 팩토리 메소드, 빌더, 프로토타입

 - 행동 관련 패턴 -
클래스와 객체들이 상호작용하는 방법 및 역할을 분담하는 방법과 관련된 패턴이다.
=> 템플릿 메소드, 커맨드, 이터레이터, 옵저버, 스테이트, 스트래티지,
   비지터, 미디에이터, 인터프리터, 메멘토, 역할 사슬

 - 구조 관련 패턴 -
클래스 및 객체들을 구성을 통해서 더 큰 구조로 만들 수 있게 해주는 것과 관련된 패턴이다.
=> 데코레이터, 프록시, 컴포지트, 어댑터, 퍼사드, 플라이웨이트, 브리지

이게 제일 유명한 분류법이지만, 다른 분류법도 있다. 클래스를 다루는 패턴인지, 객체를 다루는 패턴인지 분류하는 방법.

 - 클래스 패턴 -
클래스 사이의 관계가 상속을 통해서 어떤 식으로 정의되는지를 다룬다.
클래스 패턴에서는 컴파일시에 관계가 결정된다.
=> 템플릿 메소드, 팩토리 메소드, 어댑터, 인터프리터

 - 객체 패턴 -
객체 사이의 관계를 다루며, 객체 사이의 관계는 보통 구성을 통해서 정의된다.
객체 패턴에서는 일반적으로 실행 중에 관계가 생성되기 때문에 더 동적이고 유연하다.
=> 컴포지트, 데코레이터, 이터레이터, 커맨드, 퍼사드, 프록시, 옵저버,
   스트래티지, 스테이트, 추상 팩토리, 싱글턴,
   비지터, 메멘토, 역할 사슬, 미디에이터, 브리지, 플라이웨이트, 프로토타입, 빌더
******************************/

/*****************************
패턴으로 생각하기
신속하게 요점만 훑고 지나가보자.

최대한 단순하게. (KISS - Keep it Simple, S가 하나밖에 없는데요?)
어떻게 패턴을 적용할까? 가 아니라
어떻게 단순하게 만들까? 가 되어야 한다.
디자인 패턴은 만병통치약이 아니다.

즉 디자인 패턴은 필요한 순간에 사용해야한다.
우리가 그것을 먼저 요구해선 안된다.
******************************/

/*****************************
이제 뭐 전문 용어를 공유하는 방법, 유용한 자료들, 한 때 존재했던 패턴들, 안티 패턴 에 대해서 나오는데,
안티 패턴은 전에 다른곳에서 미리 알아봤었고, 나머지는 필요로 하면 그때 읽어보자.
끄으으읏

이 아니라 내일은 나머지 디자인 패턴에 대해서 알아보도록 하자.
******************************/