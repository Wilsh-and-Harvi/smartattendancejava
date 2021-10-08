package ServertMarkin;
import ServertMarkin.MarkinMarkout;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import connection.dbConnection;
import connection.newEmployee;

/**
 * Servlet implementation class LoginServlet
 */
public class LoginServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public LoginServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		response.getWriter().append("Served at: ").append(request.getContextPath());
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		response.setContentType("text/html");
		doGet(request, response);
		String username=request.getParameter("username");
		String password=request.getParameter("password");
		 dbConnection co=new dbConnection();
		String status="";
		
		try {
			status = co.login(username, password);
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		String Status="";
		response.setContentType("text/html");
		if(status=="yes")
		{
			newEmployee ee=new newEmployee();
			try {
				ee.newEmp(username);
			} catch (ClassNotFoundException | SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			response.sendRedirect("index.jsp");
			
			//response.sendRedirect("adminHomePage.html");
		}
		else
		{
			status="Login Failed";
			request.setAttribute("myname",status);
			request.getRequestDispatcher("index.jsp").forward(request, response); 
			response.sendRedirect("index.jsp");
		}
	}

}
