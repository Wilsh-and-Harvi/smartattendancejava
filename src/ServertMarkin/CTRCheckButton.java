package ServertMarkin;

import java.sql.SQLException;

import connection.DeviceMacidCheck;
import connection.test;

public class CTRCheckButton {
	public String markinmarkout()
	{
		String status1="";
		String username="";
		DeviceMacidCheck f=new DeviceMacidCheck();
		try {
			username=f.checkstatus();
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		if(username.equalsIgnoreCase("no"))
		{
			test f1=new test();
			status1=f1.GetMacid();
			
		}
		else
		{
			demo d=new demo();
			
			MarkinMarkout f2=new MarkinMarkout();
			try {
				status1=f2.checkstatus1(username);
			} catch (ClassNotFoundException | SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			if(status1.equalsIgnoreCase("markin"))
			{
				d.setEmployeeid(username);
				status1="markin";
			}
			else if(status1.equalsIgnoreCase("markout"))
			{
				d.setEmployeeid(username);
				status1="markout";
			}
		}
		return status1;
	}

}
