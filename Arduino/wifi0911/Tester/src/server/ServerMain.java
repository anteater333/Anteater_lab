/*****************************
     ******* 코멘트 *******
ESP8266 java listener.
0911. 일단은 모듈을 서버로, 자바를 클라이언트로 해서 통신은 성공.
******************************/

package server;

import java.io.*;
import java.net.*;

public class ServerMain {
	public static void main(String[] args) {
		try {
			ServerSocket serverSocket = new ServerSocket(333);
			
			//System.out.println("Waiting for Arduino...");
			//Socket clientSocket = serverSocket.accept();
			//System.out.println("Connected! " + clientSocket.getInetAddress());
			
			Socket clientSocket = new Socket("192.168.0.6", 333);
			
			BufferedReader readBuf = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			PrintWriter writeBuf = new PrintWriter(clientSocket.getOutputStream());
			
			String recvStr;
			
			while(true){
				writeBuf.println("OK");
				writeBuf.flush();
				System.out.println("TTT");
				recvStr = readBuf.readLine();
				System.out.println("TTT");
				if(recvStr == null){
					System.out.println("연결 끊어짐.");
					break;
				}
				else{
					System.out.println("Arduino : "+ recvStr);
					writeBuf.println("OK");
					writeBuf.flush();
				}
			}
		} catch (IOException e) {
			e.printStackTrace();
		}

	}

}
