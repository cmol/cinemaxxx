
using System;
using System.Web;
using System.Web.UI;

namespace cinemaXXX
{


	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (Page.IsPostBack == false) {

			}
			Page.Header.Title += " - Moviez!";
			
			DataMaster movieFetch = new Movie();
			var movies = movieFetch.getAllWhere("1=1 ORDER BY 'moid' DESC LIMIT 1");
			if (movies.Count == 1) {
				foreach (var key in movies.Keys) {
					showMovieStats.InnerHtml = "Spilletid " + movies[key].read("runtime").ToString() + "<br />";
					showMovieStats.InnerHtml += "Premiere " + ((DateTime)movies[key].read("launch")).ToShortDateString() + "<br />";
					showMovieDesc.InnerHtml = "<h1>" + movies[key].read("title").ToString() + "</h1>";
					showMovieDesc.InnerHtml += movies[key].read("description").ToString();
					showMovieImg.ImageUrl = movies[key].read("imgurl").ToString();
					Page.Header.Title += " - " + movies[key].read("title");
				}
			} else {
				throw new Exception("Movie not found");
			}
		}
	}
}

