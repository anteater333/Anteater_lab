/*****************************
     ******* �ڸ�Ʈ *******
ESP8266 java massage transmitter.
0911. �ϴ��� ����� ������, �ڹٸ� Ŭ���̾�Ʈ�� �ؼ� ����� ����.
0912. ����� ������ �ϴ°� �´°Ͱ���.
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
					System.out.println("���� ������.");
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
