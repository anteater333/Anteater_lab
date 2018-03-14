/*****************************
* 2017.12.22 디자인 패턴
* 목표 : Chapter 09 - Iterator 패턴과 Composite 패턴 (2)
     ******* 코멘트 *******
원래 21일에 했어야 할 내용이지만, 밤새 놀고 다음날 두통 때문에 하루 휴식.
******************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*****************************
지난 내용에서 이어져서...
집합체에서 내부 컬렉션과 관련된 기능과 반복자용 메소드 관련 기능을 전부 구현했다면 어땠을까?
집합체에 들어가는 메소드 개수가 늘어나는 것이 그렇게 나쁜 일일까?

==============================
단일 역할 원칙 - 클래스를 바꾸는 이유는 한 가지 뿐이어야 한다.
==============================

원래 클래스의 역할 외에 다른 역할을 처리하도록 하면, 두 가지 이유로 인해 그 클래스가 바뀔 수 있다.
클래스를 고치는 것은 최대한 피해야 할 일이다.
코드를 변경할 만한 이유가 두 가지가 되면 그만큼 그 클래스를 나중에 고쳐야 할 가능성이 커지게 될 뿐 아니라,
디자인에 있어서 두 가지 부분이 동시에 영향이 미치게 된다.

이를 나타내는 응집도라는 개념이 있다.
응집도란 한 클래스 또는 모듈이 특정 목적 또는 역하을 얼마나 일관되게 지원하는지를 나타내는 척도이다.
응집도가 높다면, 어떤 모듈 또는 클래스에 일련의 서로 연관된 기능들이 묶여있다는 것을 의미한다.
반대로 응집도가 낮다면, 서로 상관 없는 기능들이 묶여있다는 것을 의미한다.
******************************/

/*****************************
통합된 객체마을 식당이 이번엔 객체마을 카페를 합병한다고 한다.
객체마을 카페의 메뉴를 추가해보자.

그 전에 Iterator들을 IEnumerator로 바꿔보자.
몹시 할일이 많아보임.
참고자료 https://msdn.microsoft.com/en-us/library/system.collections.ienumerator(v=vs.110).aspx
******************************/

namespace Restaurant04
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

    public interface IMenu
    {
        IEnumerator CreateIterator();
    }

    public class DinerMenuIterator : IEnumerator
    {
        private MenuItem[] items;   // 반복 작업을 수행할 메뉴 항목
        private int position = -1;   // 반복 작업 중 현재 위치

        public DinerMenuIterator(MenuItem[] items)
        {
            this.items = items;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public MenuItem Current
        {
            get
            {
                try
                {
                    return items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Length) && (items[position] != null);
        }

        public void Reset()
        {
            position = -1;
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

        public IEnumerator CreateIterator()       // 클라이언트에게 반복자를 제공한다.
        {
            return new DinerMenuIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }

    public class PancakeHouseIterator : IEnumerator
    {
        private ArrayList items;
        private int position = -1;

        public PancakeHouseIterator(ArrayList items)
        {
            this.items = items;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public MenuItem Current
        {
            get
            {
                try
                {
                    return (MenuItem)items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Count);
        }

        public void Reset()
        {
            position = -1;
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

        public IEnumerator CreateIterator()
        {
            return new PancakeHouseIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }
    
    public class CafeMenu : IMenu
    {
        Hashtable menuItems = new Hashtable();

        public CafeMenu()
        {
            AddItem("베지 버거와 에어 프라이",
                "통밀빵, 상추, 토마토, 감자튀김이 첨가된 베지 버거",
                true, 3.99);
            AddItem("오늘의 스프",
                "샐러드가 곁들여진 오늘의 스프",
                false, 3.69);
            AddItem("베리또",
                "통 핀토콩과 살사, 구아카몰이 곁들여진 푸짐한 베리또",
                true, 4.29);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem.Name, menuItem);
        }

//      public Hashtable getItems()
//      {
//          return menuItems;
//      }                           // Iterator로 대체. 필요 없는 코드

        public IEnumerator CreateIterator()
        {
            return menuItems.Values.GetEnumerator();
        }       // 별다른 Iterator를 따로 만들지 않았다.
    }           // 컬렉션에서 지원하는 GetEnumerator() 메소드를 그냥 사용함.

    public class Waitress
    {
        IMenu pancakeHouseMenu;
        IMenu dinerMenu;
        IMenu cafeMenu;

        public Waitress(IMenu pancakeHouseMenu, IMenu dinerMenu, IMenu cafeMenu)
        {
            this.pancakeHouseMenu = pancakeHouseMenu;
            this.dinerMenu = dinerMenu;
            this.cafeMenu = cafeMenu;
        }

        public void PrintMenu()
        {
            IEnumerator pancakeIterator = pancakeHouseMenu.CreateIterator();
            IEnumerator dinerIterator = dinerMenu.CreateIterator();
            IEnumerator cafeIterator = cafeMenu.CreateIterator();

            Console.WriteLine("메뉴\n----\n아침 메뉴");
            PrintMenu(pancakeIterator);
            Console.WriteLine("\n점심 메뉴");
            PrintMenu(dinerIterator);
            Console.WriteLine("\n저녁 메뉴");
            PrintMenu(cafeIterator);
        }

        private void PrintMenu(IEnumerator iterator)  // 메소드 오버로딩
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                Console.Write(menuItem.Name + ", $");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }

        // And more...
    }
}

/*****************************
테스트.
******************************/

namespace CSDesignPatternTrack
{
    using Restaurant04;
    partial class Class10
    {
        private void Section02()
        {
            Console.WriteLine(" = Section #02 = ");

            PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();
            CafeMenu cafeMenu = new CafeMenu();

            Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);

            waitress.PrintMenu();

            Section03();
        }
    }
}

/*****************************
흠 한방에 되진 않았지만 큰 문제는 아니였고,
카페메뉴가 역순으로 나오긴 하는데...
어쨌든 작동은 된다.

참고로 좀 더 제대로 IEnumerator 인터페이스를 쓰고싶다면,
Menu에 IEnumerable 인터페이스를 추가하면 되는 것 같다.

사실 이때까지 컬렉션에다가 직접 루프문을 돌리는 코드를 짰지만,
비교적 최신기술인 제너릭과 foreach문을 쓰면 더 편하다.
******************************/

/*****************************
여전히 코드는 고칠 부분이 있다.

Waitress코드의 PrintMenu() 메소드에서는 CreateIterator()를 세 번 호출하고,
PirntMenu() 메소드도 세 번 호출해야 한다. 메뉴를 추가하거나 삭제할 때 마다 이 코드를 직접 수정해야 한다.
OCP 원칙을 기억해보자.
클래스는 확장에 대해서는 열려 있어야 하지만
코드 변경에 대해서는 닫혀 있어야 한다.

즉 여러 메뉴를 한꺼번에 관리할 수 있는 방법이 필요하다.
******************************/

namespace Restaurant05
{
    #region Concrete
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

    public interface IMenu
    {
        IEnumerator CreateIterator();
    }

    public class DinerMenuIterator : IEnumerator
    {
        private MenuItem[] items;   // 반복 작업을 수행할 메뉴 항목
        private int position = -1;   // 반복 작업 중 현재 위치

        public DinerMenuIterator(MenuItem[] items)
        {
            this.items = items;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public MenuItem Current
        {
            get
            {
                try
                {
                    return items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Length) && (items[position] != null);
        }

        public void Reset()
        {
            position = -1;
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

        public IEnumerator CreateIterator()       // 클라이언트에게 반복자를 제공한다.
        {
            return new DinerMenuIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }

    public class PancakeHouseIterator : IEnumerator
    {
        private ArrayList items;
        private int position = -1;

        public PancakeHouseIterator(ArrayList items)
        {
            this.items = items;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public MenuItem Current
        {
            get
            {
                try
                {
                    return (MenuItem)items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < items.Count);
        }

        public void Reset()
        {
            position = -1;
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

        public IEnumerator CreateIterator()
        {
            return new PancakeHouseIterator(menuItems);
        }

        // 기타 메뉴 관련 메소드
    }

    public class CafeMenu : IMenu
    {
        Hashtable menuItems = new Hashtable();

        public CafeMenu()
        {
            AddItem("베지 버거와 에어 프라이",
                "통밀빵, 상추, 토마토, 감자튀김이 첨가된 베지 버거",
                true, 3.99);
            AddItem("오늘의 스프",
                "샐러드가 곁들여진 오늘의 스프",
                false, 3.69);
            AddItem("베리또",
                "통 핀토콩과 살사, 구아카몰이 곁들여진 푸짐한 베리또",
                true, 4.29);
        }

        public void AddItem(string name, string description, bool vegetarian, double price)
        {
            MenuItem menuItem = new MenuItem(name, description, vegetarian, price);
            menuItems.Add(menuItem.Name, menuItem);
        }

        public IEnumerator CreateIterator()
        {
            return menuItems.Values.GetEnumerator();
        }
    }
    #endregion

    public class Waitress
    {
        ArrayList menus;        // 메뉴들을 ArrayList로 묶는다.

        public Waitress(ArrayList menus)
        {
            this.menus = menus;
        }

        public void PrintMenu()
        {
            IEnumerator menuIterator = menus.GetEnumerator();
            while(menuIterator.MoveNext())
            {
                IMenu menu = (IMenu)menuIterator.Current;
                PrintMenu(menu.CreateIterator());
            }
        }

        private void PrintMenu(IEnumerator iterator)
        {
            while (iterator.MoveNext())
            {
                MenuItem menuItem = (MenuItem)iterator.Current;
                Console.Write(menuItem.Name + ", $");
                Console.Write(menuItem.Price + " -- ");
                Console.WriteLine(menuItem.Description);
            }
        }
    }
}

/*****************************
괜찮아 보이지만...
디저트 서브메뉴를 추가해달라는 주문이 들어왔다.
DinerMenu 안에 서브메뉴가 들어갈 수 있으면 좋겠지만,
형식이 다르기 때문에 MenuItem으로 구성된 서브메뉴를 집어넣을 수가 없다.

디자인에 개선이 필요하다.
 - 메뉴, 서브메뉴, 메뉴 항목 등을 모두 집어넣을 수 있는 트리 형태의 구조가 필요하다.
 - 각 메뉴에 있는 모든 항목에 대해서 돌아가면서 어떤 작업을 할 수 있는 방법을 제공해야 하며,
   그 방법은 적어도 지금 사용중인 반복자 정도로 편리해야 한다.
 - 더 유연한 방법으로 아이템에 대해서 반복작업을 수행할 수 있어야 한다.
   예를 들어, 객체마을 식당 메뉴에 껴있는 디저트 메뉴에 대해서만 반복작업을 한다거나,
   디저트 서브메뉴를 포함한, 객체마을 식당 메뉴 전체에 대해서 반복작업을 하는 것도 가능해야 한다.
즉, p.393의 트리 같은 구조의 디자인이 필요하다.
******************************/

/*****************************
Pattern #12 Composite
컴포지트 패턴을 이용하면 객체들을 트리 구조로 구성하여 부분과 전체를 나타내는 계층구조로 만들 수 있다.
이 패턴을 이용하면 클라이언트에서 개별 객체와 다른 객체들로 구성된 복합 객체를 똑같은 방법으로 다를 수 있다.

왠지 DeZipper가 생각나는 패턴이다.
어쨌든 지금 만들고 있는 메뉴를 기준으로 생각해보자.
이 패턴을 이용하면 중첩되어 있는 메뉴 그룹과 메뉴 항목을 똑같은 구조 내에서 처리할 수 있다.
메뉴와 메뉴 항목을 같은 구조에 집어넣어서 부분-전체 계층구조를 생성할 수 있다.

부분-전체 계층구조(part-whole hierachy)
부분들이 모여있지만, 모든 것을 하나로 묶어서 전체로 다룰 수 있는 구조.

같은 이야기를 계속 하는 것 같은데,
p.395의 세 번째 트리 그림을 보면 확실히 알 수 있다.
트리 전체에 대해서 print() 작업을 할 수도 있고,
트리의 부분에 대해서도 print() 작업을 할 수 있다.

이런 구조의 비결은 짐작했겠지만,
Leaf(MenuItem)와 Node(Menu)가 같은 인터페이스를 가지는 것.
클래스 다이어그램에 맞춰 설명하자면,
Component라는 인터페이스를 Leaf와 Composite이 구현한다.
여기서 Component는 복합 객체 내에 들어있는 모든 객체들에 대한 인터페이스를 정의한다.
복합 노드 뿐 아니라 잎 노드에 대한 메소드도 정의한다.
그리고 Component는 Composite을 구성한다.
... 설명이 길어지는데, 그냥 클래스 다이어그램을 한 번 보는게 제일 좋다.
******************************/

/*****************************
이 컴포지트 패턴에 맞춰 메뉴를 디자인해보자.
******************************/

namespace RestaurantComposite01
{
    // Component 인터페이스(추상 클래스)
    public abstract class MenuComponent
    {
        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }
        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }
        public virtual MenuComponent Child(int i)
        {
            throw new NotSupportedException();
        }

        public virtual string Name
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual string Description
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual double Price
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual bool Vegetarian
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public virtual void Print()
        {
            throw new NotSupportedException();
        }
    }

    // Leaf. No Child.
    public class MenuItem : MenuComponent
    {
        private string name;
        private string description;
        private bool vegetarian;
        private double price;

        public MenuItem(string name, string description,
            bool vegetarian, double price)
        {
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }

        public override string Name
        { get { return this.name; } }

        public override string Description
        { get { return this.description; } }

        public override double Price
        { get { return this.price; } }

        public override bool Vegetarian
        { get { return this.vegetarian; } }

        public override void Print()
        {
            Console.Write(" " + Name);
            if (Vegetarian)
                Console.Write("(v)");
            Console.WriteLine(", $" + Price);
            Console.WriteLine("    -- " + Description);
        }
    }

    // Composite. Yes Child.
    public class Menu : MenuComponent
    {
        private ArrayList menuComponents = new ArrayList();
        private string name;
        private string description;

        public Menu(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override string Name { get => this.name; }
        public override string Description { get => this.description; }

        public override void Add(MenuComponent menuComponent)
        {
            menuComponents.Add(menuComponent);
        }

        public override void Remove(MenuComponent menuComponent)
        {
            menuComponents.Remove(menuComponent);
        }

        public override MenuComponent Child(int i)
        {
            return (MenuComponent)menuComponents[i];
        }

        public override void Print()
        {
            Console.Write("\n" + Name);
            Console.WriteLine(", " + Description);
            Console.WriteLine("------------------");

            // Menu에 들어있는 구성요소들 까지 출력
            IEnumerator enumerator = menuComponents.GetEnumerator();
            while (enumerator.MoveNext())
            {
                MenuComponent menuComponent = (MenuComponent)enumerator.Current;
                menuComponent.Print();
            }
        }
    }

    // Waitress코드는 엄청나게 짧아졌다.
    public class Waitress
    {
        MenuComponent allMenus;

        public Waitress(MenuComponent allMenus) => this.allMenus = allMenus;

        public void PrintMenu()
        {
            allMenus.Print();
        }
    }
}

/*****************************
테스트를 하긴 하는데... 급한대로 메뉴를 테스트 코드에서 만든다.
******************************/

namespace CSDesignPatternTrack
{
    using RestaurantComposite01;
    partial class Class10
    {
        private void Section03()
        {
            Console.WriteLine(" = Section #03 = ");

            MenuComponent pancakeHouseMenu =
                new Menu("팬케이크 하우스 메뉴", "아침 메뉴");
            MenuComponent dinerMenu =
                new Menu("객체마을 식당 메뉴", "점심 메뉴");
            MenuComponent cafeMenu =
                new Menu("카페 메뉴", "저녁 메뉴");
            MenuComponent dessertMenu =
                new Menu("디저트 메뉴", "디저트를 즐겨 보세요!");

            MenuComponent allMenus = new Menu("전체 메뉴", "전체 메뉴");

            allMenus.Add(pancakeHouseMenu);
            allMenus.Add(dinerMenu);
            allMenus.Add(cafeMenu);

            dinerMenu.Add(new MenuItem(
                "파스타",
                "마리나라 소스 스파게티, 효모빵도 드립니다.",
                true, 3.89));

            dinerMenu.Add(dessertMenu);

            dessertMenu.Add(new MenuItem(
                "애플 파이",
                "바삭바삭한 크러스트에 바닐라 아이스크림이 얹혀 있는 애플 파이",
                true, 1.59));

            Waitress waitress = new Waitress(allMenus);
            waitress.PrintMenu();

            Section04();
        }
    }
}

/*****************************
전에 배운 디자인 원칙에서는 한 클래스에서 한 역할만 맡아야 한다고 했다.
그런데 이 패턴에서는 계층구조를 관리하는 일과 메뉴와 관련된 작업을 처리하는 일을 한 클래스가 처리한다.

컴포지트 패턴은 단일 역할 원칙을 깨면서 대신에 투명성을 확보한다.
투명성?
Component 인터페이스에 자식들을 관리하기 위한 기능과 잎으로써의 기능을 모두 넣음으로써
클라이언트가 복합 객체와 잎 노드를 똑같은 방식으로 처리할 수 있다.
즉, 어떤 원소가 복합 객체인지 잎 노드인지가 클라이언트에게는 투명하게 느껴지는 것.

이 패턴은 디자인 원칙을 상황에 따라 적절하게 사용해야 한다는 것을 보여주는 대표적인 사례.
******************************/

/*****************************
컴포지트 패턴 내에서 이터레이트 패턴을 활용해보자.
예를들어 Waitress에서 채식주의자용 메뉴 항목만 뽑아내야 한다거나 하는 경우에
복합 객체 전체에 대해서 반복작업을 수행할 수 있도록.
******************************/

namespace RestaurantComposite02
{
    public abstract class MenuComponent
    {
        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }
        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }
        public virtual MenuComponent Child(int i)
        {
            throw new NotSupportedException();
        }

        public virtual string Name
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual string Description
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual double Price
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public virtual bool Vegetarian
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public virtual void Print()
        {
            throw new NotSupportedException();
        }

        public virtual IEnumerator CreateEnumerator()
        {
            throw new NotSupportedException();
        }
    }
    
    public class MenuItem : MenuComponent
    {
        private string name;
        private string description;
        private bool vegetarian;
        private double price;

        public MenuItem(string name, string description,
            bool vegetarian, double price)
        {
            this.name = name;
            this.description = description;
            this.vegetarian = vegetarian;
            this.price = price;
        }

        public override string Name
        { get { return this.name; } }

        public override string Description
        { get { return this.description; } }

        public override double Price
        { get { return this.price; } }

        public override bool Vegetarian
        { get { return this.vegetarian; } }

        public override void Print()
        {
            Console.Write(" " + Name);
            if (Vegetarian)
                Console.Write("(v)");
            Console.WriteLine(", $" + Price);
            Console.WriteLine("    -- " + Description);
        }

        public override IEnumerator CreateEnumerator()
        {
            return new NullEnumerator();            // 널 반복자. 천천히 알아보자.
        }
    }
    
    public class Menu : MenuComponent
    {
        private ArrayList menuComponents = new ArrayList();
        private string name;
        private string description;

        public Menu(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override string Name { get => this.name; }
        public override string Description { get => this.description; }

        public override void Add(MenuComponent menuComponent)
        {
            menuComponents.Add(menuComponent);
        }

        public override void Remove(MenuComponent menuComponent)
        {
            menuComponents.Remove(menuComponent);
        }

        public override MenuComponent Child(int i)
        {
            return (MenuComponent)menuComponents[i];
        }

        public override void Print()
        {
            Console.Write("\n" + Name);
            Console.WriteLine(", " + Description);
            Console.WriteLine("------------------");

            // Menu에 들어있는 구성요소들 까지 출력
            IEnumerator enumerator = menuComponents.GetEnumerator();
            while (enumerator.MoveNext())
            {
                MenuComponent menuComponent = (MenuComponent)enumerator.Current;
                menuComponent.Print();
            }
        }

        public override IEnumerator CreateEnumerator()
        {
            return new CompositeEnumerator(menuComponents.GetEnumerator());
        }
    }

    // 복합 반복자. 코드가 조금 복잡하고, 심지어 책이랑 상당부분 다르다.
    public class CompositeEnumerator : IEnumerator
    {
        private Stack stack = new Stack();
        private object current;

        public CompositeEnumerator(IEnumerator enumerator)  // 반복작업을 처리할 대상 중 최상위 복합 객체의 반복자
        {
            stack.Push(enumerator);
        }

        public object Current
        {
            get
            {
                if (stack.Count > 0)
                {
                    return current;
                }
                else
                    return null;
            }
        }

        public bool MoveNext()
        {
            if (stack.Count == 0)
                return false;
            else
            {
                IEnumerator enumerator = (IEnumerator)stack.Peek();
                bool next = enumerator.MoveNext();

                if (!next)
                {
                    stack.Pop();
                    return MoveNext();
                }
                else
                {
                    MenuComponent component = (MenuComponent)enumerator.Current;
                    if (component is Menu)
                        stack.Push(component.CreateEnumerator());
                    current = component;
                    return true;
                }
            }
        }

        public void Reset() => stack.Clear();
    }

    /// <summary>
    /// 널 반복자.
    /// MenuItem에 대해서 생각해 보면, 반복작업을 할 대상이 없다.
    /// 따라서 CreateIterator()를 구현하기가 애매해지는데,
    /// 그냥 null을 리턴하기엔 클라이언트에서 null을 판단하는 조건문을 사용하게됨.
    /// 따라서 MoveNext()에서 무조건 false를 반환하는 반복자를 만듬.
    /// </summary>
    public class NullEnumerator : IEnumerator
    {
        public object Current
        { get { return null; } }

        public bool MoveNext()
        {
            return false;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }

    public class Waitress
    {
        MenuComponent allMenus;

        public Waitress(MenuComponent allMenus) => this.allMenus = allMenus;

        public void PrintMenu()
        {
            allMenus.Print();
        }

        // 채식주의자용 메뉴 출력
        public void PrintVegetarianMenu()
        {
            IEnumerator enumerator = allMenus.CreateEnumerator();
            Console.WriteLine("\nVEGETARIAN MENU\n----");
            while (enumerator.MoveNext())
            {
                MenuComponent menuComponent = (MenuComponent)enumerator.Current;
                try
                {
                    if (menuComponent.Vegetarian)
                        menuComponent.Print();
                }
                catch (NotSupportedException) { }
            }
        }
    }
}

namespace CSDesignPatternTrack
{
    using RestaurantComposite02;
    partial class Class10
    {
        private void Section04()
        {
            Console.WriteLine(" = Section #04 = ");

            MenuComponent pancakeHouseMenu = 
                new Menu("PANCAKE HOUSE MENU", "Breakfast");
            MenuComponent dinerMenu =
                new Menu("DINER MENU", "Lunch");
            MenuComponent cafeMenu =
                new Menu("CAFE MENU", "Dinner");
            MenuComponent dessertMenu =
                new Menu("DESSERT MENU", "Dessert of course!");
            MenuComponent coffeeMenu = new Menu("COFFEE MENU", "Stuff to go with your afternoon coffee");

            MenuComponent allMenus = new Menu("ALL MENUS", "All menus combined");

            allMenus.Add(pancakeHouseMenu);
            allMenus.Add(dinerMenu);
            allMenus.Add(cafeMenu);

            pancakeHouseMenu.Add(new MenuItem(
                "K&B's Pancake Breakfast",
                "Pancakes with scrambled eggs, and toast",
                true,
                2.99));
            pancakeHouseMenu.Add(new MenuItem(
                "Regular Pancake Breakfast",
                "Pancakes with fried eggs, sausage",
                false,
                2.99));
            pancakeHouseMenu.Add(new MenuItem(
                "Blueberry Pancakes",
                "Pancakes made with fresh blueberries, and blueberry syrup",
                true,
                3.49));
            pancakeHouseMenu.Add(new MenuItem(
                "Waffles",
                "Waffles, with your choice of blueberries or strawberries",
                true,
                3.59));

            dinerMenu.Add(new MenuItem(
                "Vegetarian BLT",
                "(Fakin') Bacon with lettuce & tomato on whole wheat",
                true,
                2.99));
            dinerMenu.Add(new MenuItem(
                "BLT",
                "Bacon with lettuce & tomato on whole wheat",
                false,
                2.99));
            dinerMenu.Add(new MenuItem(
                "Soup of the day",
                "A bowl of the soup of the day, with a side of potato salad",
                false,
                3.29));
            dinerMenu.Add(new MenuItem(
                "Hotdog",
                "A hot dog, with saurkraut, relish, onions, topped with cheese",
                false,
                3.05));
            dinerMenu.Add(new MenuItem(
                "Steamed Veggies and Brown Rice",
                "Steamed vegetables over brown rice",
                true,
                3.99));

            dinerMenu.Add(new MenuItem(
                "Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true,
                3.89));

            dinerMenu.Add(dessertMenu);

            dessertMenu.Add(new MenuItem(
                "Apple Pie",
                "Apple pie with a flakey crust, topped with vanilla icecream",
                true,
                1.59));

            dessertMenu.Add(new MenuItem(
                "Cheesecake",
                "Creamy New York cheesecake, with a chocolate graham crust",
                true,
                1.99));
            dessertMenu.Add(new MenuItem(
                "Sorbet",
                "A scoop of raspberry and a scoop of lime",
                true,
                1.89));

            cafeMenu.Add(new MenuItem(
                "Veggie Burger and Air Fries",
                "Veggie burger on a whole wheat bun, lettuce, tomato, and fries",
                true,
                3.99));
            cafeMenu.Add(new MenuItem(
                "Soup of the day",
                "A cup of the soup of the day, with a side salad",
                false,
                3.69));
            cafeMenu.Add(new MenuItem(
                "Burrito",
                "A large burrito, with whole pinto beans, salsa, guacamole",
                true,
                4.29));

            Waitress waitress = new Waitress(allMenus);

            waitress.PrintVegetarianMenu();
        }
    }
}

/*****************************
휴... 오늘 내용이 이때까지 배운 것 중에 제일 힘들었음.
개념이 이해안가는게 아니라,
java 코드를 C#으로 옮기는게 엄청 힘들었다.
******************************/

// Anteater