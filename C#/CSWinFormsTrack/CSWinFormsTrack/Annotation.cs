/**************************************************
                * C# Windows Forms *
***************************************************/

/***************************************************
* 날짜 : 2017.10.23
* 목표 : 윈폼 학습 시작.
             ******* 코멘트 *******
참고자료 http://www.csharpstudy.com/
아마 실습 위주. 그러니까 컴포넌트들을 만들어 보고, 그 소스를 들여다보고... 사용법을 알아보고.
따로 필기할 내용이 많을진 모르겠음.
일별로 각 컴포넌트들을 뭉텅이로 배워보고,
마지막엔 생각해놓은 응용 프로그램(이름은 아마 DZipper/되짚어 가 되지 않을까)의 UI를 디자인하는걸로 하자.

기본적으로 몹시 인간-친화적인 작업 환경이라고 생각함. Visual Studio에서 윈폼을 다루는게.
Eclipse에서 swing하는건, 물론 그런 기능을 제공하는 플러그인같은게 있었을수도 있지만, 영 어려웠는데...

Visual Studio에서 윈폼 프로젝트를 생성했을때 만들어지는 주요 파일은 다음과 같다.
**Program.cs
Main() 메서드가 존재하는 프로그램의 몸체. 간단히 말해 메인 클래스.
**Form1.cs
프로그램 시작시 호출되는 기본 UI 화면 클래스. VS에서 코드 형태로 볼 수도 있고,
Form1.Designer.cs의 내용을 기반으로 랜더링 해 디자이너 형태로 볼 수도 있다.
주로 UI의 이벤트를 핸들링하는 코드들을 적게된다고한다.
**Form1.Designer.cs
Form1의 컴포넌트들에 대한 정보를 가진 클래스. Form1 클래스는 사실 partial로 선언되어서
이 Form1.Designer.cs와 Form1.cs에 나눠서 정의된다.

호출 순서를 한번 살펴보자.
먼저 메인 클래스에서 Application 클래스의 static 메소드로 어플리케이션에 대한 설정을 마친 후
Application.Run(new Form1()); 으로 Form1 클래스 인서턴스를 생성한다.
Form1의 생성자는 Form1.cs에서 정의되고, InitializeComponent();라는 private 메서드를 하나 호출한다.
InitializeComponent()는 Form1.Designer.cs에 정의되고 메서드명 그대로 컴포넌트를 초기화한다.

Form1.Designer.cs 코드를 살펴보면 InitializeComponent()는 사실 #region으로 숨겨져있다.
#region Windows Form 디자이너에서 생성한 코드
그리고 친절히 편집기로 이 코드를 수정하지 말라고 설명까지 해놨다. 우린 그냥 Form1.cs의 디자이너 화면에서
마우스 드래그&드롭으로 컴포넌트를 배치하고, 속성값을 변경해주면 된다는것.
설명만 들어도 몹시 쉬운 과정이 될 것 같지 않나?
물론 이벤트 처리하고 그런건 코드에서 해결해야 할테지만.
오늘은 가벼운 마음으로 여기까지.
***************************************************/

/***************************************************
* 날짜 : 2017.10.24
* 목표 : 컴포넌트 다루기
         ******* 코멘트 *******
인터넷이 끊켰다. 정확히는 정전. 10시부터 1시까지 무슨 점검하는지 아파트에서 전기 끊음.
오늘은 가볍게 컴포넌트를 어떻게 배치하고. 코드에서 어떻게 만져줘야 할지 알아보자.

컴포넌트 배치
비주얼 스튜디오 화면 왼쪽을 보면 도구 상자가 있음.
거기서 원하는 컴포넌트를 찾아서 Form1.cs의 디자이너 화면에 드래그 앤 드롭. 끝.
Button 컨트롤을 배치하니 Form1.Designer.cs의 InitiallizeComponent() 메서드에 다음과 같은 코드가 생성되었다.
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 89);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
생성에서 프로퍼티 설정까지 다 알아서 해준다.

컴포넌트 프로퍼티 설정
디자이너 화면에서 배치한 컴포넌트를 선택하면 속성 창에 값이 나온다. 거기서 수정해주면됨.

이벤트 핸들링
속성 창을 잘 보면 번개모양 아이콘이 있는데,
누르면 어떤 이벤트들이 있는지 확인할 수 있음.
디폴트 이벤트의 경우 컨트롤을 더블 클릭하면 바로 이벤트 핸들러 코드가 생성됨.
디폴트 이벤트가 아닌 경우는 속성창에서 해당 이벤트를 찾아 더블 클릭.
Button 컴포넌트를 더블클릭하니까 자동으로 Form1.cs에
private void button1_Click(object sender, EventArgs e)
메서드가 생성됐다.
그리고 Form1.Designer.cs의 Initialize 메서드에 이벤트를 추가하는 코드가 자동으로 생성됐다.
그러니까 내가 직접 Form1.cs에다가 코드 따라 적어넣으니까 안됨. 당연한 이야기지 이벤트 등록을 안했는데.

그리곤 계속 컴포넌트 하나씩 쭉 설명하는데... 하나하나 다 보긴 좀 걸릴거같고.
사용법은 직관적이라 굳이...

오늘 코딩하면서 알게된 VS의 기능 하나.
메서드 위에다가 /// 를 타이핑하니까 자동으로
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
이렇게 만들어줌. 두고두고 써먹자.
***************************************************/

/***************************************************
* 날짜 : 2017.10.25
* 목표 : ProgressBar, 멀티 쓰레딩.
         ******* 코멘트 *******
ProgressBar
진행상태를 알려주는 컨트롤.
멀티 쓰레딩을 위해 한번 배워보자. 코드 참고.

BackgroundWorker
별도의 쓰레드에게 어떤 일을 시키기 위해 사용하는 클래스.
보통 백그라운드 쓰레드/워커 쓰레드라고 부르는 쓰레드는 UI 쓰레드와 별도로 어떤 작업을 수행하는데 사용됨.
BackgroundWorker로부터 생성된 객체는
DoWork 이벤트 핸들러를 통해 실제 작업할 내용을 지정하고,
ProgressChanged 이벤트를 통해 진척 사항을 전달하며,
RunWorkerCompleted 이벤트를 통해 완료 후 실행될 작업을 지정한다.
정리해서,
DoWork 이벤트 핸들러는 워커 쓰레드에서.
ProgressChanged와 RunWorkerCompleted 이벤트 핸들러는 UI 쓰레드에서.
자세한것은 Form1.cs 코드 참고.

윈도우 멀티 쓰레딩
멀티쓰레드를 사용하는 방법
1) Thread 클래스로 새로운 쓰레드를 만듬
2) 쓰레드풀/Task 등을 이용
3) BackgroundWorker Wrapper 클래스를 사용
!! UI 컨트롤들을 갱신하기 위해서는 항상 해당 UI 컨트롤을 생성한 UI 쓰레드에서 갱신해야한다.
--> 예전에 이거때문에 고생한 경험이 있는거같은데...
어쨌든 이를 어기고 워커 쓰레드에서 UI 컨트롤에 접근하면 에러가 뜸.
"Cross-thread operation not valid: Control [progressBar1] accessed from a thread other than the thread it was created on."

InvokeRequired 속성
쓰레드 함수에서 UI 컨트롤에 접근할 때, 항상 Control 클래스의 InvokeRequired 속성을 체크해서
현재 쓰레드로 컨트롤을 엑세스할 수 있는지를 검사해야 한다.
InvokeRequired값이 true이면 현재 쓰레드는 Worker Thread니까 Invoke(동기 호출)나 BeginInvoke(비동기 호출)를 사용해서 UI 쓰레드로 메서드 호출을 보내야함.
delegate void ShowDelegate(int percent);
private void ShowProgress(int pct)
{
   if (InvokeRequired)
   {
      ShowDelegate del = new ShowDelegate(ShowProgress);
      //또는 ShowDelegate del = p => ShowProgress(p);
      Invoke(del, pct);
   }
   else
   {
      progressBar1.Value = pct;
   }
}
이런식으로.

근데 아까 BackgroundWorker 예제를 보면 저런 코드는 없음.
            worker.RunWorkerAsync();
전에 봤던 async 기능의 추가로 Async 메서드들을 통해 비동기 처리가 간단해짐.
***************************************************/

/***************************************************
* 날짜 : 2017.10.26
* 목표 : 드래그 앤 드롭, App.config
         ******* 코멘트 *******
Drag & Drop
마우스로 어떠한 아이템을 끌어서 원하는 장소에 이동시키는 행동.
디자이너 화면에서 컴포넌트를 배치할때 쓴 그것.
드래그 앤 드롭은 두가지 작업으로 구성된다.

Drop Source
윈폼의 기본 Base클래스인 Control 클래스는 DoDragDrop() 이라는 메서드를 가지고 있다.
Drop Source가 되는 컨트롤에서 DoDragDrop 메서드를 호출하면 드래그 앤 드롭을 시작하게 된다.
DoDragDrop 메서드는 Drop Source 컨트롤의 MouseDown 이벤트 핸들러에서 실행하게된다.
다만 ListView나 TreeView 등 일부 컨트롤에서는 특별한 ItemDrag라는 이벤트를 사용하기 때문에 ItemDrag 이벤트핸들러 안에서 실행된다.
    DoDragDrop(txtDropSource.Text, DragDropEffects.Copy);
DoDragDrop의 첫번째 파라미터는 타겟에 전달할 데이터. 두번째 파라미터는 Drop 방식.

Drop Target
Source에서 가져온 아이템을 옮길 Target이 될 컨트롤에선 다음과 같은 일을 해야한다.
1) Drop Target 컨트롤에서 AllowDrop 속성을 true로 설정한다.
2) Drop Target 컨트롤에서 DragEnter 이벤트 핸들러를 구현하여 e.Effect 속성을 지정한다.
3) Drop Target 컨트롤에서 DragDrop 이벤트 핸들러를 구현한다.
DragEnter 이벤트는 마우스가 컨트롤 내로 들어왔을 때 발생하는 이벤트로, 데이터를 받아들일지 아닐지를 결정한다.
DragDrop 이벤트는 마우스를 Release, 그러니까 버튼을 땜으로 Drop이 이루어졌을 때 발생하는 이벤트로, Drop된 데이터를 타겟에 복사하거나 이동하는등의 일을 하게 된다.

ListView에서의 드래그 앤 드롭
앞서 말했듯이 ListView같은 일부 컨트롤은 ItemDrag라는 특별한 커스텀 이벤트를 통해 드래그 앤 드롭을 수행한다.
소스만 간단하게 보도록 하자.
// Drop Source: ItemDrag 이벤트 핸들러에서 DoDragDrop 호출
private void lvwSrc_ItemDrag(object sender, ItemDragEventArgs e)
{
    DoDragDrop(e.Item, DragDropEffects.Copy);
}

// Drop Target: DragEnter 핸들러에서 해당 소스가 ListViewItem 인지 체크
private void lvwTgt_DragEnter(object sender, DragEventArgs e)
{
    if (e.Data.GetDataPresent(typeof(ListViewItem)))
    {
        e.Effect = DragDropEffects.Copy;
    }
    else
    {
        e.Effect = DragDropEffects.None;
    }
}

// Drop Target: DragDrop 핸들러에서 복사 실행
private void lvwTgt_DragDrop(object sender, DragEventArgs e)
{
    if (e.Data.GetDataPresent(typeof(ListViewItem)))
    {
        // 드래그된 소스 ListViewItem
        var item = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;        

        // ListViewItem를 Clone하여 추가
        lvwTgt.Items.Add(item.Clone() as ListViewItem);
    }        
}

App.config
윈폼 프로그램은 그 프로그램에서 사용할 옵션들을 실행파일 외부에 텍스트 파일 형태로 저장할 수 있다.
그 파일이 바로 App.Config. 프로젝트 생성하면 자동으로 생성된다.
참고로 이름은 실행파일명.config로 생성된다.
App.config 파일은 XML 형태로 저장된다. 사용자는 이 파일을 텍스트 에디터로 열어서 실행 옵션들을 임의로 지정할 수 있다.
예를 들면, 디폴트 Data  Directory를 C:\Data로 해서 App.config에 적어놨는데 사용자가 D:\Temp로 수정한다던가.
<appSettings>
    <add key="DataDirectory" value="C:\Data" />
</appSettings>

App.config 사용
윈폼 프로그램 내에서 App.config 파일에 지정된 옵션을 사용하기 위해서는
System.Configuration.ConfigurationManager 클래스를 사용해야 한다.
그런데 이게 기본으로 윈폼 프로젝트에 포함된게 아니라서 직접 추가해야함.
VS-솔루션탐색기-참조 마우스 오른쪽 클릭-참조 추가
참조를 추가했으면 ConfigurationManager 클래스를 통해 App.config의 옵션들을 사용할 수 있다.
using System.Configuration; 한 다음
        private void Form1_Load(object sender, EventArgs e)
        {
            // 2. AppSettings 에서 DataDirectory 값 읽기
            this.dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
        }
이렇게 접근.
***************************************************/