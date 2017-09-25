/*****************************
* 2017.09.01 서보모터 제어 학습
* 목표 : 스위치 입력을 통한 서보모터 제어
     ******* 코멘트 *******
스위치에 대해 복습.
INPUT_PULLUP을 쓰면 아두이노 자체 풀업 저항 사용 가능.
저항이 10K밖에 없다.
참고자료 http://deneb21.tistory.com/378 (토글, 디바운싱)
토글 스위치 하나 있으면 편할텐데. 저항도.
디바운싱으로 고생하지말고 토글스위치를 사자.
******************************/

#include <Servo.h>

#define SW1 8

Servo servo1;

int servoState = LOW; // 모터 상태
int readState; // 입력상태
int preState = LOW; // 이전상태

long lastToggle = 0; // 마지막으로 상태가 바뀐 시간
long debounce = 100; // 디바운스 시간

void setup() {
  servo1.attach(A0);
  pinMode(SW1, INPUT_PULLUP);
}

void loop() {
  readState = digitalRead(SW1);

  if ( readState == HIGH && preState == LOW && millis() - lastToggle > debounce )
  {
  // 스위치가 눌린상태(High) & 이전상태는 안눌린상태(Low) & 현재시간 - 마지막시간 > 디바운싱값
    if ( servoState == HIGH)
    {
      servoState = LOW;
      servo1.write(45);
    }
    else
    {
      servoState = HIGH;
      servo1.write(135);
    }
  }

  preState = readState;                                                                                    
}
