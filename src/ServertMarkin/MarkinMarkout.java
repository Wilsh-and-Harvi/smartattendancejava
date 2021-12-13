package ServertMarkin;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Calendar;
import java.util.Date;

import connection.getTime;





public class MarkinMarkout 
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
	public String checkstatus(String employeeid) throws ClassNotFoundException, SQLException
	{
		System.out.println("Start");
		String Status="";
		Connection con=MarkinMarkout.connections();
		String date1 = new SimpleDateFormat("yyyy-MM-dd").format(Calendar.getInstance().getTime());
		System.out.println(date1);
	    String markin = "";
        String markout = "";
        System.out.println("First1");
	 
		String sql="select markinstatus,markoutstatus from tblEmployeeattaindance where employeeid='"+employeeid+"' AND date='"+date1+"' ORDER BY id DESC LIMIT 1";
		
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		int i=0;
		while(rs.next())
		{
			
			
			  markin = rs.getString(1);
			 
				 markout = rs.getString(2);
			try
			{
				if(markout.equalsIgnoreCase(""))
	             {
	            	 markout="abc";
	             }
			}
			catch(Exception ee)
			{
				markout="abc";
			}
             
             
            
              i=i+1;
              if (markin.equalsIgnoreCase("Success") && markout.equalsIgnoreCase("Success"))
              {
            	  System.out.println("Markin");
            	  Status=markin(employeeid, employeeid);
              }
              else
              {
            	  Status="Mark out Success";
            	  //Status=markout(employeeid, employeeid);
              }
              
              
              
		}
		rs.close();
		if(i==0)
		{
			
			Status=markin(employeeid, employeeid);
			
		}
		return Status;
		
	}
	public String checkstatus1(String employeeid) throws ClassNotFoundException, SQLException
	{
		System.out.println("Start");
		String Status="";
		Connection con=MarkinMarkout.connections();
		String date1 = new SimpleDateFormat("yyyy-MM-dd").format(Calendar.getInstance().getTime());
		System.out.println(date1);
	    String markin = "";
        String markout = "";
        System.out.println("First1");
	 
		String sql="select markinstatus,markoutstatus from tblEmployeeattaindance where employeeid='"+employeeid+"' AND date='"+date1+"' ORDER BY id DESC LIMIT 1";
		
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		int i=0;
		while(rs.next())
		{
			
			
			  markin = rs.getString(1);
			 
				 markout = rs.getString(2);
			try
			{
				if(markout.equalsIgnoreCase(""))
	             {
	            	 markout="abc";
	             }
			}
			catch(Exception ee)
			{
				markout="abc";
			}
             
             
            
              i=i+1;
              if (markin.equalsIgnoreCase("Success") && markout.equalsIgnoreCase("Success"))
              {
            	  System.out.println("Markin");
            	  Status="markin";
              }
              else
              {
            	  Status="markout";
            	  //Status=markout(employeeid, employeeid);
              }
              
              
              
		}
		rs.close();
		if(i==0)
		{
			
			Status="markin";
			
		}
		return Status;
		
	}
	  
	public String markin(String employeeid, String empname) throws SQLException, ClassNotFoundException
	{
		Connection con=MarkinMarkout.connections();
		String sql="insert into tblEmployeeattaindance(intime,employeeid, employeename, markinstatus,date) values(?,?,?,?,?)";
		PreparedStatement psm=con.prepareStatement(sql);
		getTime time =new getTime();
		   String intime=time.GetTime();
		   SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");  
		    Date date = new Date();  
		    String date1=formatter.format(date);
		    
		psm.setString(1,intime);
		psm.setString(2,employeeid);
		psm.setString(3,empname);
		psm.setString(4,"Success");
		psm.setString(5, date1);
		
		
		int i=psm.executeUpdate();
		if(i>0)
		{
			return employeeid+" Mark in Success "+intime;
		}
		else
		{
			return employeeid+" Mark in Failed";
		}
			
		
	}
	public String markout(String employeeid, String empname) throws SQLException, ClassNotFoundException
	{
		Connection con=MarkinMarkout.connections();
		String sql="update tblEmployeeattaindance set outtime=?, markoutstatus=? where employeeid=? AND date=? ORDER BY id DESC LIMIT 1 ";
		PreparedStatement psm=con.prepareStatement(sql);
		getTime time =new getTime();
		   String outtime=time.GetTime();
		   SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");  
		    Date date = new Date();  
		    String date1=formatter.format(date);
		    System.out.println();  
		psm.setString(1,outtime);
		psm.setString(2,"Success");
		psm.setString(3,employeeid);
		psm.setString(4,date1);
		
		
		
		int i=psm.executeUpdate();
		if(i>0)
		{
			return employeeid+" Mark out Success "+outtime;
		}
		else
		{
			return employeeid+" Mark out Failed";
		}
			
		
	}
	
	 

}
