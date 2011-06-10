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
		
		override public string ToString() {
			DataMaster user = new User();
			user.id = Convert.ToInt32(this.read("usid"));
			user.get();
			DataMaster show = new Show();
			show.id = Convert.ToInt32(this.read("shid"));
			show.get();			
			return this.id.ToString() + " " + user.ToString() + " - " + show;
		}
	}
}