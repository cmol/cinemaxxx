
using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	public class People : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "people";
		
		public People ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new People();
		}
	}
}

