/*****************************
 * 안드로이드 Client 만들기 *
 **********************
 * 날짜 : 2017.09.14
 * 목표 : socket 연결 구현.
 ******* 코멘트 *******
 실제로 아두이노 제어에 사용할 안드로이드 어플.
 아직은 안드로이드 개발을 제대로 해볼 생각은 없으니까, 대충 배워서 하자.
 그러니까 객체지향이 뭐죠?
 오늘은 대충 틀만 잡아보고, 내일은 더 깔끔하게 다듬기.
 만들고 나니까 그냥 이정도로 끝내도 깔끔하게 쓸 수 있을 것 같다.
 **********************
 * 날짜 : 2017.09.15
 * 목표 : tcp 연결 실패시 오류 해결
 ******* 코멘트 *******
 간단하게 isConnected 변수를 하나 만들면 될듯.
 ******************************/

package com.anteater.jihoon.appetizer;

import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.io.*;
import java.net.Socket;

public class MainActivity extends AppCompatActivity {

    BufferedReader readBuf;
    PrintWriter writeBuf;
    Socket arduinoSocket;   // arduino와 연결할 소켓

    TextView textState;
    Button btnToggle, btnState;
    Handler handler;
    String sendMsg;
    Boolean isConnected = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        textState = (TextView) findViewById(R.id.textState);

        handler = new Handler();
        sendMsg = "";

        new Thread() {
            public void run() {
                try {
                    arduinoSocket = new Socket("192.168.0.6", 333);

                    readBuf = new BufferedReader(new InputStreamReader(arduinoSocket.getInputStream()));
                    writeBuf = new PrintWriter(arduinoSocket.getOutputStream());

                    writeBuf.println("msg:S");
                    writeBuf.flush();
                    sendMsg = readBuf.readLine();

                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            textState.setText(sendMsg);
                        }
                    });

                    isConnected = true;
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }.start();

        btnToggle = (Button) findViewById(R.id.buttonToggle);
        btnState = (Button) findViewById(R.id.buttonState);

        btnToggle.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isConnected) {
                    new Thread() {
                        public void run() {
                            writeBuf.println("msg:T");
                            writeBuf.flush();
                        }
                    }.start();
                    if (textState.getText() == "0")
                        textState.setText("1");
                    else
                        textState.setText("0");

                    Toast.makeText(getApplicationContext(), "Toggled!", Toast.LENGTH_LONG).show();
                } else
                    Toast.makeText(getApplicationContext(), "연결이 안됨!", Toast.LENGTH_LONG).show();
            }
        });

        btnState.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isConnected) {
                    new Thread() {
                        public void run() {
                            try {
                                writeBuf.println("msg:S");
                                writeBuf.flush();
                                sendMsg = readBuf.readLine();

                                handler.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        textState.setText(sendMsg);
                                        Toast.makeText(getApplicationContext(), "State?", Toast.LENGTH_LONG).show();
                                    }
                                });
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }.start();
                } else
                    Toast.makeText(getApplicationContext(), "연결이 안됨!", Toast.LENGTH_LONG).show();
            }
        });
    }
}
