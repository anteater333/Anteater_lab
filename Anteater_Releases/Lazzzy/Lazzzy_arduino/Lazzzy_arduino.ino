#include <Servo.h>
#include <SoftwareSerial.h>

///////////////////////////////////
/*       CHANGE THIS VALUE       */
#define APNAME "APname"          //
#define PASSWD "password"        //
#define PORT "333"               //
///////////////////////////////////

#define TG1 7
#define LED1 8  // Booting 표시 LED
#define DEBUG false

Servo servo1;
SoftwareSerial esp(2, 3); // RX, TX 핀 설정 (주의. Rx --- Tx로 연결)

int switchState; // 아두이노에 연결된 toggle switch의 상태
int preswitchState;
int msgState = LOW; // client에서 온 msg에 따른 toggle 상태
int recentToggle = 0; // 0 : Not Toggled, 1 : toggle switch, 2 : msg
int connectionId; // 연결된 커넥션 id

String msgBuf = "";

void setup()
{
  Serial.begin(9600); // 시리얼 통신. 디버그용
  esp.begin(9600); // ESP 모듈 사용설정. baud rate 9600 
  
  servo1.attach(A0);
  pinMode(TG1, INPUT_PULLUP);
  pinMode(LED1, OUTPUT);
  
  switchState = digitalRead(TG1);
  preswitchState = switchState;

  servo1.write(135);
  delay(500);
  servo1.write(45);

  // ESP 모듈 setup
  String cwjapStr = "AT+CWJAP=\"";
  cwjapStr += APNAME;
  cwjapStr += "\",\"";
  cwjapStr += PASSWD;
  cwjapStr += "\"\r\n";
  String serverStr = "AT+CIPSERVER=1,";
  serverStr += PORT;
  serverStr += "\r\n";
  digitalWrite(LED1, HIGH);
  sendData("AT+RST\r\n",2000,DEBUG); // reset
  sendData("AT+CWMODE=1\r\n",2000,DEBUG); // set STA mode
  sendData("AT+CWLAP\r\n",3000,DEBUG); // AP list. UNNECESARY
  sendData(cwjapStr,10000,DEBUG); // join AP
  sendData("AT+CIFSR\r\n",2000,DEBUG); // get IP
  sendData("AT+CIPMUX=1\r\n",2000,DEBUG); // Multiple connections mode
  sendData(serverStr,2000,DEBUG); // Set the module as server.
  digitalWrite(LED1, LOW);

  servo1.detach();
}

void loop()
{
  switchState = digitalRead(TG1);
  if (switchState != preswitchState)
  {
    preswitchState = switchState;
    recentToggle = 1;
    msgState = switchState;
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
        recentToggle = 2;
        msgBuf = "OK";
        sendMsg(connectionId, msgBuf);
      }
      else if (command == 'S')
      {
        msgBuf += msgState;
        sendMsg(connectionId, msgBuf);
      }
    }
    msgBuf = "";
  }

  if (recentToggle == 1)      // 최근에 쓰인 toggle 수단이 스위치일 때
  {
    servoToggle(switchState);
    recentToggle = 0;
  }
  else if (recentToggle == 2) // 최근에 쓰인 toggle 수단이 무선 통신일 때
  {
    servoToggle(msgState);
    recentToggle = 0;
  }
  else
  {
    // Nothing happens
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
  servo1.attach(A0);
  if (toggle == LOW)
  {
    servo1.write(45);
    delay(500);
    servo1.detach();
    return LOW;
  }
  else
  {
    servo1.write(135);
    delay(500);
    servo1.detach();
    return HIGH;
  }
}

