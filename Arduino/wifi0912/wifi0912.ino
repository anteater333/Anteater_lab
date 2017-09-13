/*****************************
* 2017.09.12 서보모터 제어 학습
* 목표 : 무선통신으로 모터 제어
     ******* 코멘트 *******
참고자료 http://deneb21.tistory.com/274 (라이브러리 사용법, sendData 함수)
지난코드에 덧붙이는식으로 작성함.
사용한 ESP8266모듈의 MAC주소 18-FE-34-54-A5-23
!!!! 아두이노가 굳이 메세지를 보낼 필요가 없다 !!!!
그래도 현재 상태가 on인지 off인지는 전송할수 있으면 좋겠는데.

client(java, android)에서 보내는 메세지 형태
msg:[MSG]
ex)msg:T     ---> 모터 Toggle

server(arduino)에서 보내는 메세지 형태(모터 상태를 전송)
STATE:[0|1]

오후 6시 38분. 토글스위치와 무선통신 토글명령어가 서로 조화를 이루게하려면 어떻게 해야할까?
값이 최근에 바뀐걸 기준으로!!

오후 7시 35분. 거의 완성된듯.

0913. 오전 8시 13분.
client에서 현재 상태를 받아올 수 있도록 명령어를 추가해야할듯.
******************************/

#include <Servo.h>
#include <SoftwareSerial.h>

#define TG1 7
#define LED1 8  // Booting 표시 LED
#define DEBUG true

Servo servo1;
SoftwareSerial esp(2,3); // RX, TX 핀 설정 (주의. Rx --- Tx로 연결)

int servoState = LOW; // 현재 servo motor의 상태
int switchState; // 아두이노에 연결된 toggle switch의 상태
int preswitchState = LOW;
int msgState = LOW; // client에서 온 msg에 따른 toggle 상태
int recentToggle = LOW; // LOW : toggle switch, HIGH : msg
int connectionId; // 연결된 커넥션 id

void setup()
{
  Serial.begin(9600); // 시리얼 통신. 디버그용
  esp.begin(9600); // ESP 모듈 사용설정. baud rate 9600 
  
  servo1.attach(A0);
  pinMode(TG1, INPUT_PULLUP);
  pinMode(LED1, INPUT);

  servo1.write(135);
  delay(500);
  servo1.write(45);

  // ESP 모듈 setup
  digitalWrite(LED1, HIGH);
  sendData("AT+RST\r\n",2000,DEBUG); // reset
  sendData("AT+CWMODE=1\r\n",2000,DEBUG); // set STA mode
  sendData("AT+CWLAP\r\n",3000,DEBUG); // AP list. UNNECESARY
  sendData("AT+CWJAP=\"아멘\",\"10084150\"\r\n",10000,DEBUG); // join AP
  sendData("AT+CIFSR\r\n",2000,DEBUG); // get IP
  sendData("AT+CIPMUX=1\r\n",2000,DEBUG); // Multiple connections mode
  sendData("AT+CIPSERVER=1,333\r\n",2000,DEBUG); // Set the module as server. port#333
  digitalWrite(LED1, LOW);
}

void loop()
{
  String msgBuf = "";
  switchState = digitalRead(TG1);
  if (switchState != preswitchState)
  {
    preswitchState = switchState;
    recentToggle = LOW;
  }
  
  if (esp.available()) // 모듈이 메세지를 전송중인지 확인
  {
    if (esp.find("+IPD,")) // +IPD : received data. find() : str을 찾아서 커서이동. bool값 반환.
                           // +IPD Response format : id, len : data
    {
      delay(1000);
      connectionId = esp.read()-48; // read()함수는 ASCII 코드값을 반환(char형이니까!)하기 때문에 48만큼 뺌.
                                    // 현재 연결된 커넥션.(최대 4개, 0~3)
      esp.find("msg:"); // 메세지를 읽기 위해 msg:로 커서 이동.
      char command = esp.read();
      if (command == 'T')
      {
        if (msgState == LOW)
          msgState = HIGH;
        else
          msgState = LOW;
        recentToggle = HIGH;
        msgBuf = "OK";
        sendMsg(connectionId, msgBuf);
      }
      else if (command == 'S')
      {
        if (recentToggle == LOW)
        {
          msgBuf += switchState;
          sendMsg(connectionId, msgBuf);
        }
        else
        {
          msgBuf += msgState;
          sendMsg(connectionId, msgBuf);
        }
      }
    }
  }

  if (recentToggle == LOW)  // 최근에 쓰인 toggle 수단이 스위치일 때
  {
    servoState = servoToggle(switchState);
  }
  else                      // 최근에 쓰인 toggle 수단이 무선 통신일 때
  {
    servoState = servoToggle(msgState);
  }
}

/*
 * Function : sendData
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

  return response;
}

/*
 * Function : sendMsg
 * Desc : simplicate sending "AT+CIPSEND=[id], [length];[msg]" command to ESP8266
 * Params : connectionId - ID of transmit connection
 *          strMsg - msg to send
 * Return : The response from the ESP8266 (if it works, "OK")
 */
String sendMsg(int conId, String strMsg)
{
  int iLength;
  String strMsgBuf;
  String strCmd;
  
  strMsgBuf = strMsg + "\r\n";
  iLength = strMsgBuf.length();
  
  strCmd = "AT+CIPSEND=";
  strCmd += conId;
  strCmd += ",";
  strCmd += iLength;
  strCmd += "\r\n";

  sendData(strCmd,500,DEBUG);
  
  return sendData(strMsgBuf,1000,DEBUG);
}

/*
 * Function : servoToggle
 * Desc : change servo motor's state
 * Params : toggle - switch? msg?
 * Return : motor's state(HIGH or LOW)
 */
int servoToggle(int toggle)
{
  if (toggle == LOW)
  {
    servo1.write(45);
    return LOW;
  }
  else
  {
    servo1.write(135);
    return HIGH;
  }
}

