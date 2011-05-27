
using System;

namespace cinemaXXX
{
	public class Movie : DataMaster
	{
		
		//what is the main table behind this dataclass
		new private static string _dbTable = "movies";
		//what is the primary key for the above table, this could have been derived from the database, but might be too expensive without some sort of cache
		new private static string _primaryKey = "moid";
		
		public Movie ()
		{
			base._dbTable = _dbTable;
			base._primaryKey = _primaryKey;
		}
		
		override protected DataMaster spawn() {
			return new Movie();
		}
	}
}