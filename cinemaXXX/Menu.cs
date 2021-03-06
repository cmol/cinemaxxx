using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cinemaXXX
{
	public static class Menu
	{
		public static void drawMenu(System.Web.UI.HtmlControls.HtmlGenericControl menu)
		{
			// Begin menu declaration for all
			Dictionary<string, string> menuItems = new Dictionary<string, string>();
			if (HttpContext.Current.Session["UserToken"] == null) menuItems.Add("Create User", "create_user.aspx");
			menuItems.Add("Show Movies", "show_movies.aspx");
			menuItems.Add("Order Tickets", "order_ticket.aspx");
			
			// Begin user menu declaration
			if (LoginHelper.loggedIn()) {
				menuItems.Add("Show Orders", "show_orders.aspx");
			}
			
			// Begin admin menu declaration
			if (LoginHelper.isAdmin()) {
				menuItems.Add("Show Admins", "show_admin.aspx");
				menuItems.Add("Show Adv. Admin", "admin_advanced.aspx");
			}
						
			// Draw menu
			foreach (var key in menuItems.Keys) {
				menu.Controls.Add(new LiteralControl("<li onclick=\"location.href='"+menuItems[key]+"'\">"+key+"</li>"));
			}
		}
	}
}


