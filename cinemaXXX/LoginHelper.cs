using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace cinemaXXX
{
	public static class LoginHelper
	{
		//----------------- Start methods for drawing loginBox
		
		private static void drawLogin(PlaceHolder loginBox)
		{
			LiteralControl title = new LiteralControl("<span>Login</span>");
			
			Table table = new Table();
			TableRow rowA = new TableRow();
			TableRow rowB = new TableRow();
			TableRow rowC = new TableRow();
			TableCell cellA = new TableCell();
			TableCell cellB = new TableCell();
			TableCell cellC = new TableCell();
			TableCell cellD = new TableCell();
			TableCell cellE = new TableCell();
			TableCell cellF = new TableCell();
			
			TextBox email = new TextBox();
			email.ID = "loginEmail";
						
			TextBox passwd = new TextBox();
			passwd.ID = "loginPasswd";
			passwd.TextMode = TextBoxMode.Password;
			
			Button login = new Button();
			login.Text = "Login";
			login.ID = "loginButton";
			
			cellA.Controls.Add(new LiteralControl("Email:"));
			cellB.Controls.Add(email);
			rowA.Cells.Add(cellA);
			rowA.Cells.Add(cellB);
			
			cellC.Controls.Add(new LiteralControl("Password:"));
			cellD.Controls.Add(passwd);
			rowB.Cells.Add(cellC);
			rowB.Cells.Add(cellD);
			
			cellF.Controls.Add(login);
			rowC.Cells.Add(cellE);
			rowC.Cells.Add(cellF);
			
			table.Rows.Add(rowA);
			table.Rows.Add(rowB);
			table.Rows.Add(rowC);
			
			loginBox.Controls.Add(title);
			loginBox.Controls.Add(table);
			
		}
		
		private static void drawLogout(PlaceHolder loginBox)
		{
			User user = (User) HttpContext.Current.Session["UserToken"];
			LiteralControl title = new LiteralControl("<span>Hi, "+user.ToString()+"</span><br />");
			
			Button logout = new Button();
			logout.Text = "Logout";
			logout.ID = "loginButton";
			
			loginBox.Controls.Add(title);
			loginBox.Controls.Add(logout);
		}
		
		public static void drawByAuthenticated(PlaceHolder loginBox)
		{
			loginBox.Controls.Clear();
			if (HttpContext.Current.Session["UserToken"] is User) {
				drawLogout(loginBox);
			}
			else {
				drawLogin(loginBox);
			}
		}
		
		//----------------- End methods for drawing loginBox
		
		//----------------- Start methods for authenticating
		
		public static bool loggedIn()
		{
			return (HttpContext.Current.Session["UserToken"] != null ? true : false);
		}
		
		public static bool isAdmin()
		{
			if (loggedIn()) {
				User user = (User) HttpContext.Current.Session["UserToken"];
				if (user.id == 1) {
					return true;
				}
			}
			return false;
		}
		
		public static bool redirectByAuthenticated(string auth)
		{
			switch (auth) {
			case "admin":
				if (!isAdmin()) {
					System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
					return false;
				}
				break;
			case "user":
				if (!loggedIn()) {
					System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
					return false;
				}
				break;
			default:
			break;
			}
			return true;
		}
		
		//----------------- End methods for authenticating
	}
}