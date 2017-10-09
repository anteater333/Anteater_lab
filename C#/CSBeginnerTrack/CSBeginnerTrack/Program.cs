/**************************************************
                * C# 기초학습 *
***************************************************
* 날짜 : 2017.10.09
* 목표 : 프로젝트 시작 및 콘솔 입출력
             ******* 코멘트 *******
참고자료 http://www.csharpstudy.com/
물론 다른 사이트도 여러군데 참고하면서 할 예정. 교재는 따로 없음.
기존에 사용하던 언어들과 문법이 크게 차이나지 않기 때문에 적당히 거를건 거르면서 해야함.

Console (System.Console) 클래스의 Read / Write 메소드를 통해 콘솔 입출력.
자바의 System.in / System.out 이 합쳐진거라고 생각.

메소드 Read는 반환형이 int
메소드 ReadLine은 반환형이 string
ReadKey라는 메소드도 있다. 말 그대로 키를 입력받는다. 즉 방향키도 입력가능하고, 엔터키를 누를 필요도 없다.
반환형은 ConsoleKeyInfo라는 클래스.
**************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBeginnerTrack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("안녕하세요, 여러분!");
            Console.WriteLine("종료하려면 아무 키나 입력하세요.");
            Console.ReadKey();
        }
    }
}
