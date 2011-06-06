using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	// See DataMaster instructions
	public class PG : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "pgs";
		
		public PG ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new PG();
		}
	}
}
