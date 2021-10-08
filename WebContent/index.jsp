
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@ page import="ServertMarkin.CTRCheckButton"%>
<%@ page import="ServertMarkin.demo"%>
<%@ page import="connection.getLoginLogout"%>
<!DOCTYPE html>
<html>
<head>
<style>
.cen {
	align-content: center;
	padding-left: 90px;
}

.button1 {
	width: 250px;
	margin-left: 65px
}
</style>
<meta charset="utf-8">
<meta name="viewport"
	content="width=device-width, initial-scale=1, shrink-to-fit=no">
<title>Stylish Login Form Design | Smarteyeapps.com</title>
<link href="https://fonts.googleapis.com/css?family=Ubuntu&display=swap"
	rel="stylesheet">
<link rel="shortcut icon" href="assets/images/fav.png">
<link rel="stylesheet" href="assets/css/bootstrap.min.css">
<link rel="stylesheet" href="assets/css/style.css">
</head>
<body>
	<div class="container-fluid bg-login">
		<div class="container">
			<div class="row">
				<div class="col-lg-9 col-md-12 login-card">
					<div class="row">
						<div class="col-md-5 detail-part">
							<h1>Smart Attendance</h1>
							<p>Geo-fencing Mobile App to mark attendance automatically
								and link with RemSys.</p>
							<%! demo f2=new demo(); %>
							<%! CTRCheckButton f=new CTRCheckButton(); %>
							<%String status=f.markinmarkout(); %>
						</div>
						<div class="col-md-7 logn-part">
							<div class="row">
								<div class="col-lg-10 col-md-12 mx-auto">
									<div class="logo-cover">

										<% if(status.equals("markin") || status.equals("markout")){%>
										<table>
											<tr>
												<td><img height="100" width="120"
													src="assets/images/SALOGO.png" alt=""></td>
												<td align="top">
													<h3 align="top">
														Welcome to
														<%= f2.getEmployeeid() %></h3>
												</td>
											</tr>
										</table>
										<%} else{%>
										<% %>
										<img height="100" width="120" src="assets/images/SALOGO.png"
											alt="">
										<%}%>



									</div>

									<% if(status.equals("markin")){%>
									<form action="MarkinServlet" method="POST">
										<button class="btn btn-primary button1">Login</button>
										<br>
										<%! getLoginLogout f3=new getLoginLogout(); %>
										<%String markin=f3.Login(); %><br>
										<%String markout=f3.Logout(); %>
										<p class="cen"><%= markin %></p>
										<p class="cen"><%= markout %></p>
									</form>
									<%}else if(status.equals("markout")){ %>
									<form action="Markout" method="POST">
										<button class="btn btn-primary button1">Logout</button>
										<br>
										<%! getLoginLogout f4=new getLoginLogout(); %>
										<%String markin=f4.Login(); %><br>
										<%String markout=f4.Logout(); %>
										<p class="cen"><%= markin %></p>
										<p class="cen"><%= markout %></p>
									</form>
									<%}else {%>
									<div class="form-cover">

										<h6>Login Here</h6>
										<form action="LoginServlet" method="POST">
											<input placeholder="Enter Username" type="text"
												class="form-control" name="username"> <input
												Placeholder="Enter PAssword" type="password"
												class="form-control" name="password">
											<div class="row form-footer">

												<div class="col-md-6 button-div">
													<button class="btn btn-primary">Login</button>
												</div>
											</div>
										</form>
									</div>
									<%}%>




								</div>
							</div>



						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

</body>
<script src="assets/js/jquery-3.2.1.min.js"></script>
<script src="assets/js/popper.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
</html>