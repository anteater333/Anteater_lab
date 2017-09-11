/*****************************
* 2017.09.11 서보모터 제어 학습
* 목표 : 통신 테스트
     ******* 코멘트 *******
java로 간단한 서버를 만들어서 어떻게 통신하는지 배워보기.
참고자료 http://deneb21.tistory.com/274 (라이브러리 사용법, sendData 함수)
지난코드에 덧붙이는식으로 작성함.
비밀번호는 코드 안에 넣을 수 밖에 없는것인가.
오후 8시. Java socket이랑 어떻게 연결할지 고민중.
******************************/

#include <Servo.h>
#include <SoftwareSerial.h>

#define TG1 8
#define DEBUG true

Servo servo1;
SoftwareSerial esp(2,3); // RX, TX 핀 설정 (주의. Rx --- Tx로 연결)
int readToggle;

String sendData(String command, const int timeout, boolean debug); // 데이터 전송 함수

void setup()
{
  Serial.begin(9600); // 시리얼 통신
  esp.begin(9600); // ESP 모듈 사용설정. baud rate 9600 
  
  servo1.attach(A0);
  pinMode(TG1, INPUT_PULLUP);

  //servo1.write(90);
  readToggle = LOW;

  // ESP 모듈 setup
  sendData("AT+RST\r\n",2000,DEBUG); // reset
  sendData("AT+CWMODE=1\r\n",1000,DEBUG); // set STA mode
  sendData("AT+CWLAP\r\n",3000,DEBUG); // AP list
  sendData("AT+CWJAP=\"아멘\",\"10084150\"\r\n",5000,DEBUG); // join AP
  sendData("AT+CIFSR\r\n",1000,DEBUG); // get IP
}

void loop()
{
  readToggle = digitalRead(TG1);
  if (readToggle == HIGH)
    servo1.write(45);
  else
    servo1.write(135);

  if (esp.available()) // 모듈이 메세지를 전송중인지 확인
  {
    if (esp.find("+IPD,")) // +IPD : received data. find() : 커서이동.
    {
      delay(1000);
      int connectionId = esp.read()-48; // read()함수는 ASCII 코드값을 반환하기 때문에 48만큼 뺌.
    }
  }
}

/*
 * Ref : http://deneb21.tistory.com/274
 * Desc : send data to ESP8266
 * Params : command - the data/command to send
 *          timeout - the time to wait for response
 *          debug - print to Serial window
 * Return : The response from the ESP8266 (if there is)
 */
String sendData(String command, const int timeout, boolean debug)
{
  String response = "";
  esp.print(command); // ESP8266에 문자 전송
  long int curTime = millis();

  while ((curTime+timeout) > millis())
  {
    while (esp.available())
    {
      char c = esp.read();
      response += c;
    }
  }

  if(debug)
    Serial.print(response);
}

