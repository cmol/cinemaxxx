
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cinemaXXX
{


	public partial class masterpage : System.Web.UI.MasterPage
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			LoginHelper.drawByAuthenticated(loginBox);
			((Button)loginBox.FindControl("loginButton")).Click += new System.EventHandler(loginDo);
			
			if (Page.IsPostBack == false) {
				//
			}
			
			Menu.drawMenu(menu);
			
		}
		
		protected void loginDo(object sender, System.EventArgs e)
        {
			if (Session["UserToken"] != null) {
				Session["UserToken"] = null;
				LoginHelper.drawByAuthenticated(loginBox);
			}
			else {
				User currentUser = new User();
				User tmp = new User();
				currentUser = tmp.Login(((TextBox)loginBox.FindControl("loginEmail")).Text, ((TextBox)loginBox.FindControl("loginPasswd")).Text);
				if (currentUser != null) {
					LoginHelper.drawByAuthenticated(loginBox);
				}
				else {
					loginBox.Controls.Add(new LiteralControl("Login failed!"));
				}
			}
        }
		
	}
}

