package com.anteater.jihoon.Lazzzy;

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

    /////////////////////////////////////////////////////////////
    /*               **** CHANGE THIS VALUE ****               */
    /////////////////////////////////////////////////////////////
    final String ipAddress = "192.0.0.1";   //  IP Address  //
    final int portNumber = 333;              //  Port Number //
    /////////////////////////////////////////////////////////////


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
                    arduinoSocket = new Socket(ipAddress, portNumber);

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
