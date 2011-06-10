using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	// See DataMaster instructions
	public class Show : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "shows";
		
		public Show ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Show();
		}
		
		override public string ToString() {
			DataMaster movie = new Movie();
			movie.id = Convert.ToInt32(this.read("moid"));
			movie.get();
			return movie.ToString() + " " + this.read("show_start") + " - " + this.read("show_end");
		}
	}
}