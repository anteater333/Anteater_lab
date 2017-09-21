/*****************************
* 2017.09.21 아두이노 볼티미터
* 목표 : 테스트용 볼티미터 만들기
     ******* 코멘트 *******
DC 전원잭 테스트도 하고, 납땜 잘됐는지 확인도 할겸.
참고자료 https://blog.udemy.com/arduino-voltmeter/

 * Udemy.com 

100옴짜리 저항이 없는데 어떻게 할까.

********* DC 005 연결 *********
    ---------------
구멍  ■■■■■■■l
    ---------------
         ll       ll
         ll       ll
         -         +

******************************/

float vPow = 4.7;   // When powered over a USB cable, it is common for the Arduino's 5V power supply to be a little less than that ideal
float r1 = 10000;  // the value (in ohms) of the first resistor in the circuit.
float r2 = 10000;   // the value (in ohms) of the second resistor in the circuit.

void setup() {
  Serial.begin(9600);

  // Send ANSI terminal codes
  Serial.print("\x1B");
  Serial.print("[2J");
  Serial.print("\x1B");
  Serial.println("[H");
  // End ANSI terminal codes

  Serial.println("-------------------------");
  Serial.println("DC VOLTMETER");
  Serial.print("Maximum Voltage: ");
  Serial.print((int)(vPow / (r2 / (r1 + r2))));
  Serial.println("V");
  Serial.println("-------------------------");
  Serial.println("");

  delay(2000);
}

void loop() {
  float v = (analogRead(0) * vPow) / 1024.0;
  float v2 = v / (r2 / (r1 + r2));

   // Send ANSI terminal codes
   Serial.print("\x1B");
   Serial.print("[1A");
   // End ANSI terminal codes
   
   Serial.println(v2);
}
