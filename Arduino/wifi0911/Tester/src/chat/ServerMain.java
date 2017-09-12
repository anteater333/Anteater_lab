/*****************************
     ******* 코멘트 *******
ESP8266 java massage transmitter.
0911. 일단은 모듈을 서버로, 자바를 클라이언트로 해서 통신은 성공.
0912. 모듈을 서버로 하는게 맞는것같다.
******************************/

package chat;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Scanner;

public class ServerMain {
	public static void main(String[] args) {
		try {
			Scanner sc = new Scanner(System.in);
			
			Socket clientSocket = new Socket("192.168.0.6", 333);
			System.out.println("Connected to ESP8266 module @ arduino");
			
			BufferedReader readBuf = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			PrintWriter writeBuf = new PrintWriter(clientSocket.getOutputStream());
			
			String recvStr;
			String sendStr;
			
			while(true){
				System.out.print("msg:");
				sendStr = sc.nextLine();
				writeBuf.println("msg:" + sendStr);
				writeBuf.flush();
				recvStr = readBuf.readLine();
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
