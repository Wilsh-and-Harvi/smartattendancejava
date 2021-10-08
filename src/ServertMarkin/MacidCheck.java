package ServertMarkin;

import java.io.IOException;
import java.sql.SQLException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import connection.DeviceMacidCheck;
import connection.test;

/**
 * Servlet implementation class MacidCheck
 */
public class MacidCheck extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public MacidCheck() {
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
		doGet(request, response);
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
			
			request.setAttribute("myname",f1.GetMacid());
			
			request.getRequestDispatcher("index.jsp").forward(request, response); 
			response.sendRedirect("index.jsp");
		}
		else
		{
			String status1="";
			MarkinMarkout f2=new MarkinMarkout();
			try {
				status1=f2.checkstatus1(username);
			} catch (ClassNotFoundException | SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			if(status1.equalsIgnoreCase("markin"))
			{
				request.setAttribute("button","markin");
				
				request.getRequestDispatcher("index.jsp").forward(request, response); 
				response.sendRedirect("index.jsp");
			}
			else if(status1.equalsIgnoreCase("markout"))
			{
				request.setAttribute("button","markout");
				
				request.getRequestDispatcher("index.jsp").forward(request, response); 
				response.sendRedirect("index.jsp");
			}
		}
	}

}
