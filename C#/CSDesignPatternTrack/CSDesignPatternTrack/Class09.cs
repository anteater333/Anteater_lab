/*****************************
* 2017.12.20 디자인 패턴
* 목표 : Chapter 08 - Template Method 패턴
     ******* 코멘트 *******
 "알고리즘 캡슐화"

지금까지 계속 캡슐화를 배워왔다. 객체 생성도 캡슐화하고,
메소드 호출도 캡슐화하고, 복잡한 인터페이스도 캡슐화했다.
이번 챕터에서는 알고리즘을 캡슐화해서,
서브클래스에서 언제든 필요할 때마다 가져다가 쓸 수 있도록 만드는 방법을 배운다.
******************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
코딩 바리스타가 되어 보자. 문답무용.
******************************/

namespace Barista01
{
    public class Coffee
    {
        void PrepareRecipe()
        {
            BoilWater();
            BrewCoffeeGrinds();
            PourInCup();
            AddSugarAndMilk();
        }

        public void BoilWater()
        {
            Console.WriteLine("물 끓이는 중");
        }

        public void BrewCoffeeGrinds()
        {
            Console.WriteLine("필터를 통해서 커피를 우려내는 중");
        }

        public void PourInCup()
        {
            Console.WriteLine("컵에 따르는 중");
        }

        public void AddSugarAndMilk()
        {
            Console.WriteLine("설탕과 우유를 추가하는 중");
        }
    }

    public class Tea
    {
        void PrepareRecipe()
        {
            BoilWater();
            SteepTeaBag();
            PourInCup();
            AddLemon();
        }

        public void BoilWater()
        {
            Console.WriteLine("물 끓이는 중");
        }

        public void SteepTeaBag()
        {
            Console.WriteLine("차를 우려내는 중");
        }

        public void AddLemon()
        {
            Console.WriteLine("레몬을 추가하는 중");
        }

        public void PourInCup()
        {
            Console.WriteLine("컵에 따르는 중");
        }
    }
}

/*****************************
두 클래스는 겹치는게 많아 보인다.
추상화를 적용하자.
******************************/

namespace Barista02
{
    public abstract class CaffeineBeverage
    {
        public void BoilWater()
        {
            Console.WriteLine("물 끓이는 중");
        }

        public void PourInCup()
        {
            Console.WriteLine("컵에 따르는 중");
        }

        public abstract void PrepareRecipe();
    }

    public class Coffee : CaffeineBeverage
    {
        public override void PrepareRecipe()
        {
            BoilWater();
            BrewCoffeeGrinds();
            PourInCup();
            AddSugarAndMilk();
        }

        public void BrewCoffeeGrinds()
        {
            Console.WriteLine("필터를 통해서 커피를 우려내는 중");
        }

        public void AddSugarAndMilk()
        {
            Console.WriteLine("설탕과 우유를 추가하는 중");
        }
    }

    public class Tea : CaffeineBeverage
    {
        public override void PrepareRecipe()
        {
            BoilWater();
            SteepTeaBag();
            PourInCup();
            AddLemon();
        }

        public void SteepTeaBag()
        {
            Console.WriteLine("차를 우려내는 중");
        }

        public void AddLemon()
        {
            Console.WriteLine("레몬을 추가하는 중");
        }
    }
}

/*****************************
Coffee와 Tea 사이에 또 다른 공통점은 없을까?
두 음료를 만드는 법을 보자. PrepareRecipe() 메소드에 해당되는 부분.
    커피                             차
(1) 물을 끓인다.                 (1) 물을 끓인다.
(2) 끓는 물에 커피를 우려낸다.   (2) 끓는 물에 차를 우려낸다.
(3) 커피를 컵에 따른다.          (3) 차를 컵에 따른다.
(4) 설탕과 우유를 추가한다.      (4) 레몬을 추가한다.

사실 두 음료를 만드는 법의 알고리즘은 서로 똑같다.
(1) 물을 끓인다.                                                                // 추상화된 부분
(2) 뜨거운 물을 이용하여 커피 또는 홍차를 우려낸다.     // 추상화되지 않은 부분     
(3) 만들어진 음료를 컵에 따른다.                                                // 추상화된 부분
(4) 각 음료에 맞는 첨가물을 추가한다.                   // 추상화되지 않은 부분

PrepareRecipe()를 추상화하자.
******************************/

namespace Barista03
{
/*****************************
첫 번째 문제점은 Coffe에서는 BrewCoffeGrinds()와 AddSugarAndMilk() 메소드를 쓰는데,
Tea 에서는 SteepTeaBag()과 AddLemon()이라는 메소드를 쓴다는 점이다.
먼저 그 둘을 Brew()와 AddCondiments()로 통일한다.
******************************/
    public abstract class CaffeineBeverage
    {
        public void PrepareRecipe()     // Java에선 final 키워드로 오버라이드를 막았지만,
        {                               // C#에선 abstract나 virtual 키워드가 없으면 오버라이드가 안된다.
            BoilWater();
            Brew();
            PourInCup();
            AddCondiments();
        }
        
        public void BoilWater()
        {
            Console.WriteLine("물 끓이는 중");
        }

        public void PourInCup()
        {
            Console.WriteLine("컵에 따르는 중");
        }

        public abstract void Brew();            //
        public abstract void AddCondiments();   // 일반화된 메소드
    }

/*****************************
이제 Coffee와 Tea 클래스를 처리한다.
이제 이 두 클래스에서 음료를 만드는 방법은 CaffeineBeverage에 의해 결정되므로
우려내는 부분과 첨가물을 추가하는 부분만 처리하면 된다.
******************************/

    public class Coffee : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("필터를 통해서 커피를 우려내는 중");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("설탕과 우유를 추가하는 중");
        }
    }
    public class Tea : CaffeineBeverage
    {
        public override void Brew()
        {
            Console.WriteLine("차를 우려내는 중");
        }

        public override void AddCondiments()
        {
            Console.WriteLine("레몬을 추가하는 중");
        }
    }
}

/*****************************
정리해 보자. p.323 그림.
조금 다른 방식으로 구현해야 하는 부분이 있긴 하지만,
커피와 차를 만드는 방법이 사실상 똑같기 때문에, 만드는 방법을 일반화시켜 베이스 클래스에 집어넣었따.

CaffeineBeverage에서 전체적인 처리 과정을 관리한다.
그리고 첫 번째와 새 번째 단계는 직접 처리한다. (BoilWater, PourInCup)
하지만 두 번째, 네 번째 단계는 Tea와 Coffee 서브클래스에 의존해야 한다. (Brew, AddCondiments)

이 예제가 기본적인 템플릿 메소드 패턴이다.
        public void PrepareRecipe()     // 이 메소드가 바로 템플릿 메소드
        {                               // 그 이유는
            BoilWater();                // (1) 메소드다.
            Brew();                     // (2) 어떤 알고리즘에 대한 틀(template) 역할을 한다.
            PourInCup();                //     이 경우에는 카페인이 들어있는 음료를 만들기 위한 알고리즘에 대한 템플릿.
            AddCondiments();
        }                               // 템플릿 내에서 알고리즘의 각 단계는 메소드로 표현된다.
        
        // 이 클래스에서 처리되는 메소드들
        public void BoilWater()
        {
            // 메소드를 구현하는 코드
        }
        public void PourInCup()
        {
            // 메소드를 구현하는 코드
        }
        
        // 서브클래스에서 구현해야 할 메소드들
        public abstract void Brew();            
        public abstract void AddCondiments();

간단히 정리해서 말하자면,
템플릿 메소드에서는 알고리즘의 각 단계들을 정의하며, 그 중 한 개 이상의 단계가 서브클래스에 의해 제공될 수 있다.
******************************/

namespace CSDesignPatternTrack
{
    using Barista03;
    partial class Class09
    {
        public Class09()
        {
            CaffeineBeverage myTea = new Tea();

            myTea.PrepareRecipe();

            /// at CaffeineBeverage.PrepareRecipe();
            /// CaffeineBeverage.BoilWater();
            /// Tea.Brew();
            /// CaffeineBeverage.PourInCup();
            /// Tea.AddCondiments();

            Section02();
        }
    }
}

/*****************************
처음의 Tea, Coffee 클래스와 템플릿 메소드가 적용된 Tea, Coffee의 차이점.
사실상 템플릿 메소드의 이점.
    처음                                               수정
Coffe와 Tea가 각각 작업을 처리한다.                CaffeinBeverage 클래스에서 작업을 처리한다.
두 클래스에서 각자 알고리즘을 수행한다.            알고리즘을 혼자 독점한다.

서로 중복된 코드가 있다.                           서브클래스에서 코드를 재사용 할 수 있다.

알고리즘이 바뀌면 서브클래스를 일일이 고쳐야한다.  알고리즘이 한 군데에 모여 있어 그 부분만 수정하면 된다.

새로운 음료를 추가하기 힘들다.                     새로운 카페인 음료를 추가할 수 있는 프레임워크를 제공한다.

알고리즘에 대한 지식과 구현 방법이 분산된다.       알고리즘에 대한 지식이 한 클래스에 집중된다.

Pattern #10 Template Method
템플릿 메소드 패턴에서는 메소드에서 알고리즘의 골격을 정의한다.
알고리즘의 여러 단계 중 일부는 서브클래스에서 구현할 수 있다.
템플릿 메소드를 이용하면 알고리즘의 구조는 그대로 유지하면서 서브클래스에서 특정 단계를 재정의할 수 있다.

알고리즘의 틀을 만드는 패턴.
이 틀은 일련의 단계들로 알고리즘을 정의한 메소드이다.
여러 단계 가운데 하나 이상이 추상 메소드로 정의되며, 그 추상 메소드는 서브클래스에서 구현된다.

템플릿 메소드 패턴에서의 AbstractClass의 일반적인 형태를 살펴보자.
******************************/

namespace TemplateMethod
{
    abstract class AbstractClass
    {
        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
            ConcreteOperation();
            Hook();
        }

        protected abstract void PrimitiveOperation1();
        protected abstract void PrimitiveOperation2();
        // 서브클래스에서 오버라이드 해야 한다.

        protected void ConcreteOperation()
        {
            // Something
        }   // 서브클래스에서 오버라이드 할 수 없다.

        protected virtual void Hook()
        {
            // Nothing
        }   // 서브클래스에서 오버라이드 할 수 있다. 안 할 수도 있다.
    }
}

/*****************************
갑자기 후크 라는 개념이 나왔다.
사실 저 코드를 보기 전에 잠깐 생각했는데,
virtual 키워드로 기본적인 행동을 정해놓고, 서브클래스에서 필요로 하는 경우에만 그걸 수정하는 방법을 쓰면 어떨까
했는데 띠용 진짜 있네. 바로 나오네.

후크는 추상 클래스에서 선언되는 메소드긴 하지만 기본적인 내용만 구현되어 있거나 아무 코드도 들어있지 않은 메소드이다.
이렇게 하면 서브클래스 입장에서는 다양한 위치에서 알고리즘에 끼어들 수 있다. 무시할 수도 있고.
후크를 어떻게 활용하는지 알아보자. 
******************************/

namespace Barista04
{
    public abstract class CaffeineBeverage
    {
        public void PrepareRecipe()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
                AddCondiments();
        }

        #region
        protected void BoilWater()
        {
            Console.WriteLine("물 끓이는 중");
        }

        protected void PourInCup()
        {
            Console.WriteLine("컵에 따르는 중");
        }

        protected abstract void Brew();
        protected abstract void AddCondiments();
        #endregion

        // Hook
        protected virtual Boolean CustomerWantsCondiments()     // 참고로 public보단 protected가 더 알맞다고 생각된다.
        {
            return true;
        }
    }

    public class Coffee : CaffeineBeverage
    {
        protected override void Brew()
        {
            Console.WriteLine("필터를 통해서 커피를 우려내는 중");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("설탕과 우유를 추가하는 중");
        }

        // Overriding Hook
        protected override bool CustomerWantsCondiments()
        {
            string answer = UserInput();

            if (answer.ToLower().StartsWith("y"))
                return true;
            else
                return false;
        }

        private string UserInput()
        {
            string answer = null;

            Console.WriteLine("커피에 우유와 설탕을 넣어 드릴까요? (y/n) ");

            answer = Console.ReadLine();

            if (answer == null)
                return "no";
            return answer;
        }
    }
}

namespace CSDesignPatternTrack
{
    using Barista04;
    partial class Class09
    {
        void Section02()
        {
            Console.WriteLine(" = Section #02 = ");

            CaffeineBeverage coffee1 = new Coffee();
            CaffeineBeverage coffee2 = new Coffee();

            Console.WriteLine("커피1");
            coffee1.PrepareRecipe();

            Console.WriteLine("커피2");
            coffee2.PrepareRecipe();
        }
    }
}

/*****************************
후크는 어떤 용도로 쓰이게 될까?
알고리즘에서 필수적이지 않은 부분을 필요에 따라 서브클래스에서 구현하도록 만들고 싶을 때,
템플릿 메소드에서 앞으로 일어날 일 또는 막 일어난 일에 대해 서브클래스에서 반응할 기회를 제공하려 할 때.

템플릿 메소드에서 추상 메소드가 너무 많아지면 좋지 않다. 일일이 서브클래스에서 구현해줘야한다.
따라서 템플릿 메소드를 만들 때 알고리즘을 적당한 크기로 쪼갤 수 있어야 한다.
또한 필수적이지 않은 부분은 후크로 만들수 있어야 하고.
******************************/

/*****************************
==============================
헐리우드 원칙 - 먼저 연락하지 마세요. 저희가 연락 드리겠습니다.
==============================

헐리우드 원칙은 의존성 부패를 방지할 수 있다.
의존성 부패?
어떤 고수준 구성요소가 저수준 구성요소에 의존하고, 그 저수준 구성요소는 다시 고수준 구성요소에 의존하고, 그 고수준 구성요소는 다시 또 다른 구성요소에 의존하고, 그 다른 구성요소는 또 저수준 구성요소에 의존하는 것
과 같은 식으로 의존성이 복잡하게 꼬여있는 것을 의존성 부패라고 부른다.
의존성 부패는 시스템이 어떤 식으로 디자인된 것인지 알아보기 어렵게 만든다.

헐리우드 원칙을 사용하면, 저수준 구성요소에서 시스템에 접속을 할 수는 있지만,
언제 어떤 식으로 그 구성요소들을 사용할지는 고수준 구성요소에서 결정하게 된다.
즉, 고수준 구성요소가 저수준 구성요소에게 "먼저 연락하지 마세요. 저희가 연락 드리겠습니다." 말하는 것과 같게 된다.

헐리우드 원칙은 템플릿 메소드 패턴과 맞닿아 있는걸로 보인다.
템플릿 메소드 패턴을 써서 디자인하면 서브클래스들에게 "우리가 연락할 테니까 먼저 연락하지 마"라고 이야기 하는 셈.
Coffee나 Tea 클래스에서는 메소드 구현을 제공함으로써 컴퓨테이션에 참여할 수 있지만,
호출 "당하기" 전까지는 추상 클래스를 직접 호출하지 않는다.

사실 헐리우드 원칙은 템플릿 메소드에서 처음으로 사용되는건 아니고,
팩토리 메소드, 옵저버 등등에서 이미 이런 형태를 가지고 있었다.
******************************/

/*****************************
실전에서의 템플릿 메소드 패턴.

C#, java에서 sort를 어떻게 쓰는지 기억해보자.

아래 코드는 java의 Arrays 클래스에서 제공하는 정렬 템플릿 메소드다.

public static void sort(Object[] a)
{
    Object aux[] = (Object[])a.clone();
    mergeSort(aux, a, 0, a.length, 0);
}

private static void mergeSort(Object src[], Object dest[], int low, int high, int off)
{
    for (int i = low; i < high; i++)
    {
        for (int j = i; j > low &&
            ((Comparable)dest[j-1]).compareTo((Comparable)dest[j]) > 0; j--)        // Abstract Method
        {
            swap(dest, j, j-1);             // Concrete Method
        }
    }
    return;
}

템플릿 메소드를 완성하려면 compareTo() 메소드를 구현해야한다.
만약 오리들을 정렬하려 한다면, compareTo() 메소드에 오리들을 비교하는 방법을 구현하면 되는 것.
이렇게
public class Duck implements Comparable
{
    ...
    ...

    public int compareTo(Object object)
    {
        Duck otherDuck = (Duck)object;

        if (this.weight < otherDuck.weight)
            return -1;
        else if (this.weight == otherDuck.weight)
            return 0;
        else if (this.weight > otherDuck.weight)
            return 1;
    }
}

이외에도 스윙 프레임이나 애플릿등이 템플릿 메소드 패턴으로 이루어져있다.
오늘 내용 끝.
******************************/
