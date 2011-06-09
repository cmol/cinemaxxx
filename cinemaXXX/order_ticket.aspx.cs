
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{


	public partial class order_ticket : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (Request["shid"] != null) {
				displayShow(ticketOrder, int.Parse(Request["shid"]));
			}
			else if (Request["login"] != null) {
				displayLogin(ticketOrder);
			}
			else {
				displayShows(ticketOrder);
			}
			
		}
		
		private void displayShow(PlaceHolder placeholder, int shid)
		{
			placeholder.Controls.Clear();
			Show show = new Show();
			show.id = Convert.ToInt32(Request["shid"]);
			show.get();
			
			TextBox tickets = new TextBox();
			tickets.ID = "numOfTickets";
			
			Button order = new Button();
			order.Text = "Order!";
			order.Click += new System.EventHandler(this.orderClick);
			
			HiddenField hiddenShid = new HiddenField();
			hiddenShid.Value = shid.ToString();
			hiddenShid.ID = "hiddenShid";
			
			placeholder.Controls.Add(new LiteralControl("<h1>"+show.read("moid", true)+" @ "+show.read("show_start")+"</h1>"));
			placeholder.Controls.Add(new LiteralControl("<p>### tickets left</p>"));
			placeholder.Controls.Add(new LiteralControl("<p>Please input # of tickets you want to order</p>"));
			placeholder.Controls.Add(tickets);
			placeholder.Controls.Add(order);
			placeholder.Controls.Add(new LiteralControl("<br /><br /><a href=\"order_ticket.aspx\">Back to show list</a>"));
			placeholder.Controls.Add(hiddenShid);
		}
		
		private void displayShows(PlaceHolder placeholder)
		{
			placeholder.Controls.Clear();
			
			if (Request["success"] != null && Request["success"] == "true") {
				placeholder.Controls.Add(new LiteralControl("<p class=\"success\">Your order has been processed successfully!</p>"));
			}
			
			DataMaster fetch = new Show();
			var shows = fetch.getAll();
			if (shows.Count > 0) {
				foreach (var shid in shows.Keys) {
					placeholder.Controls.Add(new LiteralControl("<p>"+shows[shid].read("moid", true)+" @ "+shows[shid].read("show_start")+" <a href=\"order_ticket.aspx?shid="+shid+"\">Order tickets</a></p>"));
				}
			} else {
				throw new Exception("No shows found");
			}
		}
		
		private void orderClick(object sender, System.EventArgs e)
		{
			Order order = new Order();
			order.write("tickets", int.Parse(((TextBox)ticketOrder.FindControl("numOfTickets")).Text));
			order.write("price", 55.0);
			order.write("shid", int.Parse(((HiddenField)ticketOrder.FindControl("hiddenShid")).Value));
			if (LoginHelper.loggedIn()) {
				doOrder(order);
			}
			else {
				Session["tmpOrder"] = order;
				Response.Redirect("order_ticket.aspx?login=true");
			}
		}
		
		private void doOrder(Order order)
		{
			User user = (User) Session["UserToken"];
			order.write("usid", user.id);
			order.insert();
			Response.Redirect("order_ticket.aspx?success=true");
		}
		
		protected void loginDo(object sender, System.EventArgs e)
		{
			User currentUser = new User();
			User tmp = new User();
			currentUser = tmp.Login(((TextBox)ticketOrder.FindControl("loginEmail")).Text, ((TextBox)ticketOrder.FindControl("loginPasswd")).Text);
			LoginHelper.drawByAuthenticated((PlaceHolder)Master.FindControl("loginBox"));
			if (currentUser != null) {
				Order order = (Order) Session["tmpOrder"];
				doOrder(order);
			}
			else {
				ticketOrder.Controls.Add(new LiteralControl("Login failed"));
			}
		}
		
		protected void displayLogin(PlaceHolder placeholder)
		{
			LoginHelper.drawByAuthenticated(placeholder);
			Button bt = ((Button)placeholder.FindControl("loginButton"));
			bt.Click += new System.EventHandler(loginDo);
		}
	}
}

