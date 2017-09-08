/*****************************
* 2017.09.07 서보모터 제어 학습
* 목표 : 와이파이 연결, 통신. 재도전 & 토글스위치
     ******* 코멘트 *******
참고자료 http://deneb21.tistory.com/343 (USB to TTL로 ESP8266 펌웨어 업데이트)
문제를 드디어 알아냈다.
공유기 설정중 RTS한계와 Fragmentation값을 최하로 설정해놔서 연결이 안됐음.
몇일동안 별로 한게 없으니까 오늘은 노트북에서 아두이노로 핑쏘기, 토글스위치 두개.
우선 핑은 몹시 잘 날라간다.
토글스위치 MTS-102는 실습용이 아니라서... 브레드보드에 안들어가고 따로 납땜이 필요해보임.
우선은 임시방편 야매로 연결해 테스트해봄.
코드는 택트스위치 테스트했던 코드를 수정.
확실히 택트스위치에비해 코드도 간결한데 디바운싱 해줄 필요도 없음.
별개로 전력 소모를 줄일 수 있는 방법이 있는지 찾아봐야함.
그리고 영상을 보면 알겠지만 스위치가 생각보다 빡세다.
******************************/

#include <Servo.h>

#define TG1 8

Servo servo1;

int readToggle;

void setup()
{
  servo1.attach(A0);
  pinMode(TG1, INPUT_PULLUP);

  //servo1.write(90);
  readToggle = LOW;
}

void loop()
{
  readToggle = digitalRead(TG1);

  if (readToggle == HIGH)
    servo1.write(45);
  else
    servo1.write(135);
                                                                           
}
