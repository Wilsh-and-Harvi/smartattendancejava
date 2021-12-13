package connection;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.nio.charset.Charset;
import java.util.Random;

public class test {
	public String GetMacid()
	{
//		StringBuilder sb = new StringBuilder();
//		InetAddress ip=null;
//        try {
//
//            ip = InetAddress.getLocalHost();
//            System.out.println("Current IP address : " + ip.getHostAddress());
//
//            NetworkInterface network = NetworkInterface.getByInetAddress(ip);
//
//            byte[] mac = network.getHardwareAddress();
//
//            System.out.print("Current MAC address : ");
//
//            
//            for (int i = 0; i < mac.length; i++) {
//                sb.append(String.format("%02X%s", mac[i], (i < mac.length - 1) ? ":" : ""));
//            }
//           
//            System.out.println(sb.toString());
//
//        } catch (Exception e) {
//
//            e.printStackTrace();
//
//        }
//		 
//        return ip.getHostAddress();
		String ip = "";
		try (java.util.Scanner s = new java.util.Scanner(new java.net.URL("http://eth0.me/").openStream(), "UTF-8")
			.useDelimiter("\\A")) {
		    ip = s.next();
		} catch (java.io.IOException e) {
		    e.printStackTrace();
		}
		return ip;
	}
	

	
	
	

}
