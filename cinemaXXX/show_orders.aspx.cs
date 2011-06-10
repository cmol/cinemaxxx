
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cinemaXXX
{
	public partial class show_orders : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (!LoginHelper.redirectByAuthenticated("user")) {
					ContentPlaceHolderContent = null;
				}
			
			if (Request["orid"] != null) {
				Order order = new Order();
				order.id = int.Parse(Request["orid"]);
				order.delete();
			}
			
			drawOrders(orderList);		
		}
		
		private void drawOrders(PlaceHolder _orderList)
		{
			var temp = new Order();
			User user = (User) Session["UserToken"];
			var orders = temp.getAllWhere("usid = '"+user.id.ToString()+"'");
			
			foreach (var orid in orders.Keys)
			{
				_orderList.Controls.Add(new LiteralControl("<p>"+orders[orid].read("shid", true)+" " +
					"<a href=\"show_orders.aspx?orid="+orders[orid].id.ToString()+"\">Delete Order</a></p>"));
			}
			
		}
	}
}

