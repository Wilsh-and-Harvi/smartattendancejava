package connection;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.Calendar;



public class DeviceMacidCheck 
{
	static Connection connect = null;
	static Statement statement = null;
	PreparedStatement preparedStatement = null;
	ResultSet resultSet = null;
	static String host = "pulse-hmsawsdb.cxvfz81wqwim.ap-south-1.rds.amazonaws.com";
	static String user = "admin";
	static String passwd = "Pulseadmin2021";
	public static Connection connections() throws ClassNotFoundException, SQLException
	{
		 Class.forName("com.mysql.jdbc.Driver");
	    
	    return connect = DriverManager
	        .getConnection("jdbc:mysql://"+ host +"/RemSys?"
	            + "user=" + user + "&password=" + passwd);
	   
	}
	public String checkstatus() throws ClassNotFoundException, SQLException
	{
		System.out.println("Start");
		
		Connection con=DeviceMacidCheck.connections();
	test f=new test();
	 String macid=f.GetMacid();
		String sql="select username from tblLogin where macid='"+macid+"'";
		String username="";
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		int i=0;
		while(rs.next())
		{
			
			
			username = rs.getString(1);
			
            i=i+1;  
              
              
		}
		rs.close();
		if(i==0)
		{
			username="no";
		}
		return username;
		
	}

}
