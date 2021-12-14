package connection;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;



public class dbConnection 
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
	public String login(String username,String password) throws SQLException, ClassNotFoundException
	{
		Connection con=dbConnection.connections();
		String sql="select username,password from tblLogin where username='"+username+"' AND password='"+password+"'";
		PreparedStatement psm=con.prepareStatement(sql);
		ResultSet rs=psm.executeQuery();
		String status="no";
		username="";
		
		try {
			while(rs.next())
			{
				status="yes";
				username=rs.getString(1);
				password=rs.getString(2);
				
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return username;
	}
	

}
