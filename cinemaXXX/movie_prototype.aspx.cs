
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cinemaXXX
{


	public partial class movie_prototype : System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			if (Page.IsPostBack == false) {
				
				if (!LoginHelper.redirectByAuthenticated("admin")) {
					//Environment.Exit(0);
					Content2 = null;
				}
				
			}
				
				//Movie stuff = new Movie();
				//stuff.getSchema();
				//stuff.id = 2;
				//stuff.getSchema();
				//stuff.get();
				//stuff._dbData["moid"] = "abc";
				/*var movies = stuff.getAll ();
				movies[2].createPlaceHolderTable(placeholder1);
				movies[1].createPlaceHolderTable(placeholder1);
				*/
				//stuff.createPlaceHolderTable(placeholder1);
				
				/*People dims = new People();
				dims.id = 1;
				dims.get();
				dims.createPlaceHolderTable(placeholder1);*/
				
				//stuff.createPlaceHolderTable(placeholder1);
				
	
				//stuff._dbData["title"] = "en ny titel";
				//stuff.update();
				
				
				
				
				Movie filmA = new Movie();
				filmA.id = 1;
				filmA.get();
				
				//Movie filmB = new Movie();
				//filmB.id = 2;
				//filmB.get();
				
				filmA.createPlaceHolderTable(placeholder1);
				//filmB.createPlaceHolderTable(placeholder1);
				
				
				
				People personA = new People();
				personA.id = 1;
				personA.get();
				/*
				
				
				filmA.createPlaceHolderTable(placeholder1);
				//personA.createPlaceHolderTable(placeholder1);
				filmB.createPlaceHolderTable(placeholder1);*/
			
			
		}
		
		protected void clickmig(object sender, System.EventArgs e)
        {
			Movie dims = new Movie();
			dims.fillObjectFromPlaceHolder(placeholder1);
			placeholder1.Controls.Clear();
			dims.createPlaceHolderTable(placeholder1);
			//dims.(placeholder1);
			dims.update();
        }
		
		
		
		
		
		
	}
}
