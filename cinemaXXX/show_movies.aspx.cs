
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace cinemaXXX
{


	public partial class show_movies : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var temp = new Movie();
			var movies = temp.getAll();
			
			Table table = new Table();
			foreach (int moid in movies.Keys) {
				TableRow row = new TableRow();
				
				TableCell imageCell = new TableCell();
				Image image = new Image();
				image.ImageUrl = movies[moid].read("imgurl").ToString();
				
				HtmlAnchor anchor = new HtmlAnchor();
				anchor.HRef = "/show_movie.aspx?moid=" + movies[moid].id;
				anchor.Controls.Add(image);
				
				imageCell.Controls.Add(anchor);
				row.Controls.Add(imageCell);
				
				TableCell cell = new TableCell();
				cell.VerticalAlign = VerticalAlign.Top;

				cell.Text = "<h2>" + movies[moid].read("title").ToString() + "</h2>";
				row.Controls.Add(cell);
				cell.Text += (movies[moid].read("description").ToString().Length > 1024) ? movies[moid].read("description").ToString().Substring(0, 1024) + "..." : movies[moid].read("description").ToString();
				row.Controls.Add(cell);
				
				table.Controls.Add(row);
			}
			PlaceHolder1.Controls.Add(table);
			
		}
	
	

	
	
	}
}
