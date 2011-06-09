using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	// See DataMaster instructions
	public class Order : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "orders";
		
		public Order ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Order();
		}
	}
}