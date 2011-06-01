using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace cinemaXXX
{
	public class User : DataMaster
	{
		//what is the main table behind this dataclass
		new private string _dbTable = "users";
		
		public User ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		public User Login(string _email, string _password)
		{
			var users = this.getAllWhere("email = '"+_email+"' AND passwd = '"+_password+"'");
			if (users.Count == 1) {
				foreach (int key in users.Keys) {
					HttpContext.Current.Session["UserToken"] = (User) users[key];
					return (User) users[key];
				}
				return null;
			}
			else {
				return null;
			}
		}
		
		override protected DataMaster spawn() {
			return new User();
		}
	}
}