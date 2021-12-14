package ServertMarkin;

import java.sql.SQLException;




public class CTRCheckButton {
	public String markinmarkout()
	{
		LoginServlet f=new LoginServlet();
		String status1="";
		String username=f.username3;
		
		
		if(username.equalsIgnoreCase("no"))
		{
			
			
			
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
