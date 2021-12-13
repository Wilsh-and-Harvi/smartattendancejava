package connection;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Date;

import ServertMarkin.demo;



public class newEmployee {
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
	public void newEmp(String username) throws SQLException, ClassNotFoundException
	{
		test d=new test();
		String macid=d.GetMacid();
		Connection con=newEmployee.connections();
		
		String sql="update tblLogin set macid=? where username=?";
		PreparedStatement psm=con.prepareStatement(sql);
		 
		psm.setString(1,macid);
		psm.setString(2,username);
		psm.executeUpdate();
		
		
		
			}
}
