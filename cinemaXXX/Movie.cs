
using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	public class Movie : DataMaster
	{
		//what is the main table behind this dataclass
		new private string _dbTable = "movies";
		
		public Movie ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Movie();
		}
	}
}