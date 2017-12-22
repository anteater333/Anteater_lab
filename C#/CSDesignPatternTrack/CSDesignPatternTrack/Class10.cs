/*****************************
* 2017.12.21 디자인 패턴
* 목표 : Chapter 09 - Iterator 패턴과 Composite 패턴 (1)
     ******* 코멘트 *******
 "잘 관리된 컬렉션"

컬렉션이란 간단히 말해서 자료구조.
컬렉션에서는 데이터를 object 타입으로 저장한다. 따라서 형변환으로 인한 오버헤드 발생 가능.
제네릭은 일반화된 컬렉션으로 형식을 지정한다. <T> 이게 바로 그 증거.

어쨌든 객체를 컬렉션에 집어넣는 방법은 다양하다. 배열, 스택, 헤시테이블 등등...
그런데 언젠가는 클라이언트가 컬렉션에 있는 모든 객체들에게 일일이 접근하는 작업을 하고 싶어질 수 있다.
이 경우에 클라이언트에게 객체들을 어떤 식으로 저장했는지 모두 보여줘야 할까???
객체를 저장하는 방식은 보여주지 않으면서, 클라이언트가 객체들에게 일일이 접근할 수 있게 해 주는 방법이 필요하다.
******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
객체마을 식당과 객체마을 팬케이크 하우스가 합병하기로 했다!
손님 입장에선 나쁜 일은 아니지만, 문제가 생겼다.

아침에는 팬케이크 하우스 메뉴를 쓰고, 점심에는 객체마을 식당 메뉴를 쓴다.
메뉴 항목을 구현하는 방법을 찾아야 한다.
펜케이크 하오스에선 메뉴에 들어갈 내용을 ArrayList에 저장했었고,
객체마을 식당에선 배열에 저장했었다.
둘 다 지금까지 기존 자료에 기반해 만들어놓은 코드가 많기 때문에 수정에 대해서 회의적인 입장이다.

일단은 메뉴 항목을 나타내는 MenuItem에 대해서는 합의가 되었다.
******************************/

namespace Restaurant01
{
    public class MenuItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Vegetarian { get; private set; }
        public double Price { get; private set; }

        public MenuItem(string name,
            string description,
            bool vegetarian,
            double price)
        {
            this.Name = name;
            this.Description = description;
            this.Vegetarian = vegetarian;
            this.Price = price;
        }
    }
}

/*****************************
두 식당의 메뉴 구현을 보자.
******************************/

namespace Restaurant01
{
    // 팬케이크 하우스. ArrayList.
    public class PancakeHouseMenu
    {
        public ArrayList MenuItems { get; private set; }

        public PancakeHouseMenu()
        {
            MenuItems = new ArrayList();

            AddItem("K&G 팬케이크 세트",
                "스크램블드 에그와 토스트가 곁들여진 팬케이크",
                true, 2.99);
            AddItem("레귤러 팬케이크 세트",
                "달걀 후라이와 소시지가 곁들여진 팬케이크",
                false, 2.99);
            AddItem("블루베리 팬케이크",
                "신선한 블루베리와 블루베리 시럽으로 만든 팬케이크",
                true, 3.49);
            AddItem("와플",
                "와플, 취향에 따라 블루베리나 딸기를 얹을 수 있습니다.",
                true, 3.59);
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            MenuItems.Add(menuItem);
        }

        // 기타 메뉴 관련 메소드
    }

    // 객체마을 식당. 배열.
    public class DinerMenu
    {
        static readonly int MAX_ITEMS = 6;
        private int numberOfItems = 0;

        public MenuItem[] MenuItems { get; private set; }

        public DinerMenu()
        {
            MenuItems = new MenuItem[MAX_ITEMS];

            AddItem("채식주의 BLT",
                "통밀 위에 (식물성) 베이컨, 상추, 토마토를 얹은 메뉴",
                true, 2.99);
            AddItem("BLT",
                "통밀 위에 베이컨, 상추, 토마토를 얹은 메뉴",
                false, 2.99);
            AddItem("오늘의 스프",
                "감자 샐러드를 곁들인 오늘의 스프",
                false, 3.29);
            AddItem("핫도그",
                "사워크라우트, 갖은 양념, 양파, 치즈가 곁들여진 핫도그",
                false, 3.05);
            // 기타 메뉴 항목이 추가되는 부분
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            if (numberOfItems >= MAX_ITEMS)
                Console.Error.WriteLine("죄송합니다, 메뉴가 꽉 찼습니다. 더 이상 추가할 수 없습니다.");
            else
            {
                MenuItems[numberOfItems] = menuItem;
                numberOfItems++;
            }
        }

        // 기타 메뉴 관련 메소드
    }
}

/*****************************
두 식당의 방식이 다르면 어떤 문제가 발생할까?
두 메뉴를 사용하는 클라이언트가 있다고 생각해보자.
이름하야 Java waitress, written in C#.
이 웨이트리스는 손님이 주문한 내용에 맞춰서 고객 주문 메뉴를 출력하고,
주방장에게 물어보지 않고도 알아서 어떤 메뉴 항목이 채식주의자용 메뉴인지 알아내는 능력도 있어야 한다.
printMenu()
printBreakfastMenu()
printLunchMenu()
printVegetarianMenu()
isItemVegetarian(name)
즉, 위의 메소드를 가지고 있어야 한다.

printMenu()의 구현을 차근차근 짚어보자.
1. 각 메뉴에 들어있는 모든 항목을 출력하려면,
   PancakeHouseMenu와 DinerMenu의 MenuItem 프로퍼티를 통해 메뉴 항목을 호출해야 한다.
   여기서 주의할 점은, 이 두메소드의 리턴 형식이 서로 다르다는 것.
2. PancakeHouseMenu에 있는 항목을 출력하기 위해서 ArrayList에 들어있는 모든 항목들에 대해 순환문을 돌린다.
   그리고 DinerMenu에 들어있는 항목을 출력하기 위해서는 배열에 대해서 순환문을 돌린다.
코드를 계속 이렇게 짜야한다. 항상 두 메뉴를 이용하고, 순환문 두개 를 돌리고,
만약에 다른 레스토랑을 또 합병하면 순환문이 3개가 된다.

흠... 고집불통인 두 식당의 사장은 둘 다 자기가 사용하는 메뉴 클래스에 있는 코드를 건드리지 않으려 한다.
그렇다면 만약 각 메뉴에 대한 똑같은 인터페이스를 구현할 수 있게 해 준다면 일이 쉬워지지 않을까.
******************************/

/*****************************
지금까지 패턴들을 배워오면서 배운 내용 중 가장 중요한 것을 꼽자면,
"바뀌는 부분을 캡슐화하라"는 내용일 것이다.

이번 문제에서 바뀌는 부분은 바로
메뉴에서 리턴되는 객체 컬렉션의 형식이 다르기 때문에 달라지는 반복 작업.
즉 반복을 캡슐화해야한다.
******************************/

/*****************************
어떻게 반복을 캡슐화 할까.
(Java 기준)
1. breakfastItems의 각 항목에 대해서 순환문을 돌릴 때는 ArrayList의 size()와 get()메소드를 사용한다.
for (int i = 0; i < breakfastItems.size(); i++)
    MenuItem menuItem = (MenuItem)breakfastItems.get(i);
2. lunchItems에 대해서 순환문을 돌릴 때는 배열의 length 필드와 배열 첨자를 이용한다.
for (int i = 0; i < lunchItems.lenght; i++)
    MenuItem menuItem = lunchItems[i];
3. 여기에 객체의 컬렉션에 대한 반복작업을 처리하는 방법을 캡슐화한 Iterator라는 객체를 만든다.
   그리고 Iterator를 ArrayList에 적용해보자.
Iterator iterator = breakfastMenu.createIterator();
while (iterator.hasNext())
    MenuItem menuItem = (MenuItem)iterator.next();
4. 배열에도 적용해보자.
Iterator iterator = lunchMenu.createIterator();
while (iterator.hasNext())
    MenuItem menuItem = (MenuItem)iterator.next();

Iterator를 적용하니 ArrayList와 배열 모두 순환 방법이 같아졌다.

이것이 바로 Iterator 패턴.
이터레이터 패턴은 Iterator라는 다음과 같은 인터페이스에 의존한다.
<<Interface>> Iterator
hasNext()
next()

이 인터페이스로 배열, 리스트, 해시테이블 등등 모든 컬렉션에 대해 반복자를 구현할 수 있다.
DinerMenu에서 사용하는 배열에서 Iterator 인터페이스를 구현하기로 했다고 가정하면,
<<Interface>> Iterator
hasNext()
next()
        ∧
        │
        │
        │
        │
        │
DinerMenuIterator
hasNext()
next()

실제로 Iterator를 구현해보자.
******************************/

namespace Restaurant02
{
    public class MenuItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Vegetarian { get; private set; }
        public double Price { get; private set; }

        public MenuItem(string name,
            string description,
            bool vegetarian,
            double price)
        {
            this.Name = name;
            this.Description = description;
            this.Vegetarian = vegetarian;
            this.Price = price;
        }
    }

    // Iterator 인터페이스
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    // DinerMenu의 Iterator
    public class DinerMenuIterator : IIterator
    {
        private MenuItem[] items;   // 반복 작업을 수행할 메뉴 항목
        private int position = 0;   // 반복 작업 중 현재 위치

        public DinerMenuIterator(MenuItem[] items)
        {
            this.items = items;
        }

        public object Next()        // 배열의 다음 원소를 리턴하고 position 변수의 값을 1 증가시킨다.
        {
            MenuItem menuItem = items[position];
            position++;
            return menuItem;
        }

        public bool HasNext()       // 배열을 다 돌았는지 확인
        {
            if (position >= items.Length || items[position] == null)
                return false;
            else
                return true;
        }
    }

    public class DinerMenu
    {
        static readonly int MAX_ITEMS = 6;
        private int numberOfItems = 0;
        private MenuItem[] menuItems;

        // MenuItems 프로퍼티는 더이상 필요 없다.

        public DinerMenu()
        {
            menuItems = new MenuItem[MAX_ITEMS];

            AddItem("채식주의 BLT",
                "통밀 위에 (식물성) 베이컨, 상추, 토마토를 얹은 메뉴",
                true, 2.99);
            AddItem("BLT",
                "통밀 위에 베이컨, 상추, 토마토를 얹은 메뉴",
                false, 2.99);
            AddItem("오늘의 스프",
                "감자 샐러드를 곁들인 오늘의 스프",
                false, 3.29);
            AddItem("핫도그",
                "사워크라우트, 갖은 양념, 양파, 치즈가 곁들여진 핫도그",
                false, 3.05);
            // 기타 메뉴 항목이 추가되는 부분
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            if (numberOfItems >= MAX_ITEMS)
                Console.Error.WriteLine("죄송합니다, 메뉴가 꽉 찼습니다. 더 이상 추가할 수 없습니다.");
            else
            {
                menuItems[numberOfItems] = menuItem;
                numberOfItems++;
            }
        }
        
        public IIterator CreateIterator()       // 클라이언트에게 반복자를 제공한다.
        {
            return new DinerMenuIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }
}

/*****************************
이제 슬슬 감이 잡히고 있다. 팬케이크 가게에 대해서도 만들어보자.
사실 C#이 배열과 ArrayList 사용법이 몹시 유사해서...
******************************/

namespace Restaurant02
{
    public class PancakeHouseIterator : IIterator
    {
        private ArrayList items;
        private int position = 0;

        public PancakeHouseIterator(ArrayList items)
        {
            this.items = items;
        }
        
        public object Next()
        {
            MenuItem menuItem = (MenuItem)items[position];
            position++;
            return menuItem;
        }
        
        public bool HasNext()
        {
            if (position >= items.Count || items[position] == null)
                return false;
            else
                return true;
        }
    }

    public class PancakeHouseMenu
    {
        private ArrayList menuItems;

        public PancakeHouseMenu()
        {
            menuItems = new ArrayList();

            AddItem("K&G 팬케이크 세트",
                "스크램블드 에그와 토스트가 곁들여진 팬케이크",
                true, 2.99);
            AddItem("레귤러 팬케이크 세트",
                "달걀 후라이와 소시지가 곁들여진 팬케이크",
                false, 2.99);
            AddItem("블루베리 팬케이크",
                "신선한 블루베리와 블루베리 시럽으로 만든 팬케이크",
                true, 3.49);
            AddItem("와플",
                "와플, 취향에 따라 블루베리나 딸기를 얹을 수 있습니다.",
                true, 3.59);
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }

        public IIterator CreateIterator()
        {
            return new PancakeHouseIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }
}

/*****************************
웨이트리스를 만들고 테스트해보자.
******************************/

namespace Restaurant02
{
    public class Waitress
    {
        PancakeHouseMenu pancakeHouseMenu;
        DinerMenu dinerMenu;

        public Waitress(PancakeHouseMenu pancakeHouseMenu, DinerMenu dinerMenu)
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
        }

        public void PrintMenu()
        {
            IIterator pancakeIterator = pancakeHouseMenu.CreateIterator();
            IIterator dinerIterator = dinerMenu.CreateIterator();
            Console.WriteLine("메뉴\n----\n아침 메뉴");
            PrintMenu(pancakeIterator);
            Console.WriteLine("\n점심 메뉴");
            PrintMenu(dinerIterator);
        }

        private void PrintMenu(IIterator iterator)  // 메소드 오버로딩
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                Console.Write(menuItem.Name + ", $");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }

        // And more...
    }
}

namespace CSDesignPatternTrack
{
    using Restaurant02;
    partial class Class10
    {
        public Class10()
        {
            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu);

            waitress.PrintMenu();

            Section02();
        }
    }
}

/*****************************
Iterator를 통해 Waitress는 어떻점이 좋아졌나.
1. 메뉴 구현법이 캡슐화되었다. Waitress 입장에서 메뉴 항목의 컬렉션이 어떻게 저장되는지 전혀 알 수 없다.
2. Iterator만 구현된다면 어떤 컬렉션이든 다형성을 활용하여 한 개의 순환문으로 처리할 수 있다.
3. Waitress에서는 Iterator 인터페이스만 알고 있으면 된다.
4? Menu 인터페이스가 완전히 똑같...지 않다.
Waitress는 여전히 두 개의 구상 메뉴 클래스에 묶여있다.

사실 위에서 PancakeHouseMenu와 DinerMenu를 수정하면서 생긴 의문,
CreateIterator() 메소드가 서로 똑같은데 인터페이스를 만들지 않았다는것.
개선해보자.
******************************/

namespace Restaurant03
{
    public class MenuItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Vegetarian { get; private set; }
        public double Price { get; private set; }

        public MenuItem(string name,
            string description,
            bool vegetarian,
            double price)
        {
            this.Name = name;
            this.Description = description;
            this.Vegetarian = vegetarian;
            this.Price = price;
        }
    }

    // Iterator 인터페이스
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    // DinerMenu의 Iterator
    public class DinerMenuIterator : IIterator
    {
        private MenuItem[] items;   // 반복 작업을 수행할 메뉴 항목
        private int position = 0;   // 반복 작업 중 현재 위치

        public DinerMenuIterator(MenuItem[] items)
        {
            this.items = items;
        }

        public object Next()        // 배열의 다음 원소를 리턴하고 position 변수의 값을 1 증가시킨다.
        {
            MenuItem menuItem = items[position];
            position++;
            return menuItem;
        }

        public bool HasNext()       // 배열을 다 돌았는지 확인
        {
            if (position >= items.Length || items[position] == null)
                return false;
            else
                return true;
        }
    }

    public class DinerMenu : IMenu
    {
        static readonly int MAX_ITEMS = 6;
        private int numberOfItems = 0;
        private MenuItem[] menuItems;

        // MenuItems 프로퍼티는 더이상 필요 없다.

        public DinerMenu()
        {
            menuItems = new MenuItem[MAX_ITEMS];

            AddItem("채식주의 BLT",
                "통밀 위에 (식물성) 베이컨, 상추, 토마토를 얹은 메뉴",
                true, 2.99);
            AddItem("BLT",
                "통밀 위에 베이컨, 상추, 토마토를 얹은 메뉴",
                false, 2.99);
            AddItem("오늘의 스프",
                "감자 샐러드를 곁들인 오늘의 스프",
                false, 3.29);
            AddItem("핫도그",
                "사워크라우트, 갖은 양념, 양파, 치즈가 곁들여진 핫도그",
                false, 3.05);
            // 기타 메뉴 항목이 추가되는 부분
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            if (numberOfItems >= MAX_ITEMS)
                Console.Error.WriteLine("죄송합니다, 메뉴가 꽉 찼습니다. 더 이상 추가할 수 없습니다.");
            else
            {
                menuItems[numberOfItems] = menuItem;
                numberOfItems++;
            }
        }

        public IIterator CreateIterator()       // 클라이언트에게 반복자를 제공한다.
        {
            return new DinerMenuIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }

    public class PancakeHouseIterator : IIterator
    {
        private ArrayList items;
        private int position = 0;

        public PancakeHouseIterator(ArrayList items)
        {
            this.items = items;
        }

        public object Next()
        {
            MenuItem menuItem = (MenuItem)items[position];
            position++;
            return menuItem;
        }

        public bool HasNext()
        {
            if (position >= items.Count || items[position] == null)
                return false;
            else
                return true;
        }
    }

    public class PancakeHouseMenu : IMenu
    {
        private ArrayList menuItems;

        public PancakeHouseMenu()
        {
            menuItems = new ArrayList();

            AddItem("K&G 팬케이크 세트",
                "스크램블드 에그와 토스트가 곁들여진 팬케이크",
                true, 2.99);
            AddItem("레귤러 팬케이크 세트",
                "달걀 후라이와 소시지가 곁들여진 팬케이크",
                false, 2.99);
            AddItem("블루베리 팬케이크",
                "신선한 블루베리와 블루베리 시럽으로 만든 팬케이크",
                true, 3.49);
            AddItem("와플",
                "와플, 취향에 따라 블루베리나 딸기를 얹을 수 있습니다.",
                true, 3.59);
        }

        public void AddItem(string name, string description,
            bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem);
        }

        public IIterator CreateIterator()
        {
            return new PancakeHouseIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }

    public interface IMenu
    {
        IIterator CreateIterator();
    }

    public class Waitress
    {
        IMenu pancakeHouseMenu;
        IMenu dinerMenu;

        public Waitress(IMenu pancakeHouseMenu, IMenu dinerMenu)
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
        }

        public void PrintMenu()
        {
            IIterator pancakeIterator = pancakeHouseMenu.CreateIterator();
            IIterator dinerIterator = dinerMenu.CreateIterator();
            Console.WriteLine("메뉴\n----\n아침 메뉴");
            PrintMenu(pancakeIterator);
            Console.WriteLine("\n점심 메뉴");
            PrintMenu(dinerIterator);
        }

        private void PrintMenu(IIterator iterator)  // 메소드 오버로딩
        {
            while (iterator.HasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Next();
                Console.Write(menuItem.Name + ", $");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }

        // And more...
    }
}

/*****************************
정리. p.373
PancakeHouseMenu와 DinerMenu 클래스에서는 Menu 인터페이스를 구현한다.
Waitress 클래스에서 각 메뉴 객체를 참조할 때는 구상 클래스 대신 인터페이스를 이용하면 된다.
전에 배운 디자인 원칙 "특정 구현이 아닌 인터페이스에 맞춰서 프로그래밍한다"를 따르게 되기 때문에,
Waitress와 구상 클래스 사이의 의존성을 줄일 수 있다.
이는 Waitress가 구상 메뉴 클래스에 의존하는 문제를 해결해준다.

새로 정의한 Menu 인터페이스에서는 CreateIterator()라는 메소드 하나만 있을 뿐이다.
PancakeHouseMenu와 DinerMenu에서 모두 이 메소드를 구현한다.
각 메뉴 클래스에서 메뉴 항목을 내부적으로 구현한 방법에 따라
적절한 방식으로 구상 반복자 클래스를 만들어 리턴할 책임을 가지게 되는것.
이는 Waitress에서 MenuItem 구현에 의존하는 문제를 해결해준다.

즉 이제 Waitress가 신경써야 할 것은,
Menu 인터페이스와 Iterator 인터페이스 뿐이라는 이야기.

Pattern #11 Iterator
이터레이터 패턴은 컬렉션 구현 방법을 노출시키지 않으면서도
그 집합체 안에 들어있는 모든 항목에 접근할 수 있게 해 주는 방법을 제공해 준다.

이 패턴을 이용하면 집합체 내에서 어떤 식으로 일이 처리되는지에 대해서 전혀 모르는 상태로
그 안에 들어있는 모든 항목들에 대해서 반복작업을 수행할 수 있게 된다.
메뉴 항목이 배열로 저장되었든, ArrayList로 저장되었든 신경쓰지 않고 작업을 처리하는 PrintMenu() 메소드가 그 증거.

또 다른 중요한 이점은,
이터레이터 패턴을 사용하면 모든 항목에 일일이 접근하는 작업을 컬렉션 객체가 아닌 반복자 객체에서 맡게 된다는 점.
이렇게 하면 집합체의 인터페이스 및 구현이 간단해질 뿐 아니라,
집합체에서는 반복작업에서 손을 떼고 원래 자신이 할 일에만 전념할 수 있게 된다.

사실 Iterator 패턴은 전 부터 써 왔던 패턴으로,
foreach문을 생각하면 될 것 같다. 어떤 자료형을 넣든 반복을 알아서 해준다.

책에서는 사실 IMenu 인터페이스를 구현할때 코드를 java.util.Iterator에 맞춰서 변경했다.
C#에도 기본적으로 제공하는 Iterator 인터페이스가 있는데 그게 바로 IEnumarator 인터페이스.
사용법은 미묘하게 다른 것 같다. 그냥 거의 비슷한것 같긴 한데... 그 미묘함이 있음...

끝으로, 그냥 참고.
p.375의 이터레이터 패턴의 클래스 다이어그램을 보면 팩토리 메소드 패턴과 닮아있다는걸 알 수 있다.
******************************/
