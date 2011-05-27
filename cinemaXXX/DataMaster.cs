using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace cinemaXXX
{
	public class DataMaster
	{
		
		//quick and dirty to get things rolling, connection options should be stored in config later
		static protected MySqlConnection dbcon;
		static public MySqlConnection dbConnection() {	
			if (!(dbcon is MySqlConnection)) {
				dbcon = new MySqlConnection("Server=localhost;Database=cinemaxxx;User ID=root;Password=;Pooling=false");
				dbcon.Open();
			}
			return dbcon;
		}
		
		//protected Dictionary<string, object> _dbData;
		//for easy debug access
		public Dictionary<string, object> _dbData;
		
		//what is the main table behind this dataclass
		protected string _dbTable;
		
		protected string _primaryKey;
		
		public DataMaster ()
		{
			this._dbData = new Dictionary<string, object>();
		}
		
		public int id{
			set {
				if (this._dbData.ContainsKey(this._primaryKey)) {
					this._dbData[this._primaryKey] = value;
				} else {
					this._dbData.Add(this._primaryKey, value);
				}
			}
			get {
				//lets hope it's there
				return (int) this._dbData[this._primaryKey];
			}
		}
		
		public bool get() {
			string sql = "SELECT * FROM " + this._dbTable + " WHERE " + this._primaryKey + " = " + this._dbData[this._primaryKey];
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			using (MySqlDataReader reader = cmd.ExecuteReader()) {
				while(reader.Read()) {
					this._readerFill(reader);
				}
			}
			return true;
		}
		
		//this and getAllWhere needs to be non-static for proper polymorphism, or we won't be able to call spawn from the child
		public Dictionary<int, DataMaster> getAll() {
			return getAllWhere("1=1");
		}
		
		virtual protected DataMaster spawn() {
			return new DataMaster();
		}
		
		//this needs to be non-static for proper polymorphism, or we won't be able to call spawn from the child
		protected Dictionary<int, DataMaster> getAllWhere(string sqlWhere) {
			string sql = "SELECT * FROM " + this._dbTable + " WHERE "+sqlWhere;
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			Dictionary<int, DataMaster> dictionary = new Dictionary<int, DataMaster>();
			using (MySqlDataReader reader = cmd.ExecuteReader()) {
				while(reader.Read()) {
					DataMaster tmpObject = this.spawn();
					tmpObject._readerFill(reader);
					dictionary.Add((int) tmpObject._dbData[tmpObject._primaryKey], tmpObject);
				}
			}
			return dictionary;
		}
		
		//add is reserved :(
		//this one needs some serious beautification
		// the whole datatype thing needs to be seperated
		public bool insert() {
			string[] columns = new string[this._dbData.Count];
			string[] values = new string[columns.Length];
			//string cell;
			int i = 0;
			foreach (var column in this._dbData.Keys) {
				columns[i] = column;
				if (this._dbData[column] is string || this._dbData[column] is DateTime) {
					values[i] = "'" + this._dbData[column] + "'";
				} else {
					if (this._dbData[column].ToString().Length == 0) {
						values[i] = "NULL";
					} else {
						values[i] = this._dbData[column].ToString();
					}
				}
				i++;
			}
			
			string sql = "INSERT INTO " + this._dbTable + " (" + string.Join(", ",columns) + ") VALUES (" + string.Join(", ",values) + ")";
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			//return (cmd.ExecuteNonQuery() == 1 ? true : false);
			cmd.ExecuteNonQuery();
			MySqlCommand incId = new MySqlCommand("select last_insert_id()", dbConnection());
			this.id = Convert.ToInt16(incId.ExecuteScalar());
			return true;
		}
		
		//work in progress, wait until there is a datatype function in place (look at insert())
		public bool update() {
			
			return true;
		}
		
		public bool delete() {
			string sql = "SELECT * FROM " + this._dbTable + " WHERE " + this._primaryKey + " = " + this.id;
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			return (cmd.ExecuteNonQuery() == 1 ? true : false);
		}
		
		private bool _readerFill(MySqlDataReader reader) {
			this._dbData.Clear();
			for(int i=0;i<reader.FieldCount;i++) {
				this._dbData.Add(reader.GetName(i), reader[reader.GetName(i)]);
			}
			return true;
		}
		
	}
}
