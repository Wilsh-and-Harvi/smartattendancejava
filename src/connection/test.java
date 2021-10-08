package connection;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;

public class test {
	public String GetMacid()
	{
		StringBuilder sb = new StringBuilder();
		InetAddress ip;
        try {

            ip = InetAddress.getLocalHost();
            System.out.println("Current IP address : " + ip.getHostAddress());

            NetworkInterface network = NetworkInterface.getByInetAddress(ip);

            byte[] mac = network.getHardwareAddress();

            System.out.print("Current MAC address : ");

            
            for (int i = 0; i < mac.length; i++) {
                sb.append(String.format("%02X%s", mac[i], (i < mac.length - 1) ? ":" : ""));
            }
           
            System.out.println(sb.toString());

        } catch (Exception e) {

            e.printStackTrace();

        }
        return sb.toString();
	}
	

	
	
	

}
