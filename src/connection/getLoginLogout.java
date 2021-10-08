package connection;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.Calendar;


import ServertMarkin.demo;

public class getLoginLogout {
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
	public String Login() throws ClassNotFoundException, SQLException
	{
		demo d=new demo();
		String employeeid=d.getEmployeeid();
		Connection con=getLoginLogout.connections();
		String date1 = new SimpleDateFormat("yyyy-MM-dd").format(Calendar.getInstance().getTime());
		
	 String markin="";
		String sql="select intime from tblEmployeeattaindance where employeeid='"+employeeid+"' AND date='"+date1+"' ORDER BY id LIMIT 1";
		
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		int i=0;
		while(rs.next())
		{
			
			
			  markin = rs.getString(1);
			  markin="You logged in at "+markin;
			
              
              
		}
		if(markin.equalsIgnoreCase("You logged in at null"))
		{
			markin="";
		}
		rs.close();
		return markin;
	}
	public String Logout() throws ClassNotFoundException, SQLException
	{
		demo d=new demo();
		String employeeid=d.getEmployeeid();
		Connection con=getLoginLogout.connections();
		String date1 = new SimpleDateFormat("yyyy-MM-dd").format(Calendar.getInstance().getTime());
		
	 String markin="";
		String sql="select outtime from tblEmployeeattaindance where employeeid='"+employeeid+"' AND date='"+date1+"' ORDER BY id DESC LIMIT 1";
		
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		int i=0;
		while(rs.next())
		{
			
			
			  markin = rs.getString(1);
			  markin="You logged out at "+markin;
			
              
              
		}
		if(markin.equalsIgnoreCase("You logged out at null"))
		{
			markin="";
		}
		rs.close();
		return markin;
	}

}
