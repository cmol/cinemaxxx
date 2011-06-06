
using System;
using System.Web;
using System.Web.UI;

namespace cinemaXXX
{


	public partial class show_movie : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (Page.IsPostBack == false) {

			}
			Page.Header.Title += " - Moviez!";
			
			DataMaster movie = new Movie();
			movie.id = Convert.ToInt32(Request["moid"]);
			movie.get();
			
			showMovieStats.InnerHtml = "Spilletid " + movie.read("runtime").ToString() + "<br />";
			showMovieStats.InnerHtml += "Premiere " + ((DateTime)movie.read("launch")).ToShortDateString() + "<br />";
			
			showMovieDesc.InnerHtml = "<h1>" + movie.read("title").ToString() + "</h1>";
			showMovieDesc.InnerHtml += movie.read("description").ToString();
			
			showMovieImg.ImageUrl = movie.read("imgurl").ToString();
		}
		
	}
}
