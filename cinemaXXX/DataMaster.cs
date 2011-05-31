using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace cinemaXXX
{
	public class DataMaster
	{

/* 
 * ----------------------------------------------------
 * Beginning of constructors and initialisers
 */
		
		public DataMaster ()
		{
			this._dbData = new Dictionary<string, object>();
			if (_dbReferences.Count == 0) {
			 	//TABLE_NAME,COLUMN_NAME,REFERENCED_TABLE_NAME,REFERENCED_COLUMN_NAME from information_schema.KEY_COLUMN_USAGE where constraint_schema='cinemaxxx' AND REFERENCED_COLUMN_NAME is not NULL;
				string sql = "SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE constraint_schema = '" + ConfigurationSettings.AppSettings["DataBaseName"] + "' AND REFERENCED_COLUMN_NAME IS NOT NULL";
			}
		}
		
		/* things we need to do to get things rolling, but can't do in the DataMasters constructor.
		 * call this in the childs constructor
		 */
		protected bool _prepare() {
			this.getSchema();
			foreach (DataRow row in _dbSchemas[this._dbTable].Rows){
				if ((bool) row["IsKey"]) {
					this._primaryKey = row["ColumnName"].ToString();
				}
				//neccesary?
				this._dbData.Add(row["ColumnName"].ToString(), new object());
			}
			return true;
		}
		
		
/* End of constructors and initialisers
 * ----------------------------------------------------
 * Beginning of definitions
 */
		
		static private MySqlConnection _dbcon;
		static private MySqlConnection dbConnection() {
			if (!(_dbcon is MySqlConnection)) {
				string ConnectionInfo = "Server=" + ConfigurationSettings.AppSettings["DataBaseServer"] + ";Database=" + ConfigurationSettings.AppSettings["DataBaseName"] + ";User ID=" + ConfigurationSettings.AppSettings["DataBaseUid"] + ";Password=" + ConfigurationSettings.AppSettings["DataBasePasswd"] + ";Pooling=" + ConfigurationSettings.AppSettings["DataBasePooling"];
			
				_dbcon = new MySqlConnection(ConnectionInfo);
				
				//_dbcon = new MySqlConnection(ConfigurationManager.AppSettings["ConnectionInfo"]);
				///mnt/con-rw/home/heigren/Projects/cinemaxxx/cinemaXXX/DataMaster.cs(84,84): Warning CS0618: `System.Configuration.ConfigurationSettings.AppSettings' is obsolete: `This property is obsolete.  Please use System.Configuration.ConfigurationManager.AppSettings' (CS0618) (cinemaXXX)
				_dbcon.Open();
			}
			return _dbcon;
		}
		
		/* DataTable able to containg Schema info for all the tables
		 */
		static private Dictionary<string, DataTable> _dbSchemas = new Dictionary<string, DataTable>();
		
		/* used for multidimensional reference storage [tablename][columnnamewithref][reftargettable], columnname in source and target should be the same according to our db structure */
		static private Dictionary<string, object> _dbReferences = new Dictionary<string, object>();
		
		/* Dictionary for our DB data, string is the column */
		protected Dictionary<string, object> _dbData;
		
		/* what is the main table behind this dataclass */
		protected string _dbTable;
		
		/* What is the primary key for this table */
		private string _primaryKey;

		/* get/set the id of the object, derived from _primaryKey */
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
		
/* End of definitions
 * ----------------------------------------------------
 * Beginning of Database I/O
 */
		
		/* fill the object with data for the current ID */
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
		
		/* Get all records from the current table 
		 * this and getAllWhere needs to be non-static for proper polymorphism, or we won't be able to call spawn from the child
		 */
		public Dictionary<int, DataMaster> getAll() {
			return getAllWhere("1=1");
		}
		
		/* Get all records from the current table matching the supplied where clause
		 * this needs to be non-static for proper polymorphism, or we won't be able to call spawn from the child
		 */
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
		
		/* Insert (not update) the current object into the database
		 * add is reserved :(
		 */
		public bool insert() {
			string[] columns = new string[this._dbData.Count];
			string[] values = new string[columns.Length];
			int i = 0;
			foreach (var column in this._dbData.Keys) {
				columns[i] = column;
				values[i] = this._dbEncapsulate(column);
				i++;
			}
			
			string sql = "INSERT INTO " + this._dbTable + " (" + string.Join(", ",columns) + ") VALUES (" + string.Join(", ",values) + ")";
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			cmd.ExecuteNonQuery();
			MySqlCommand incId = new MySqlCommand("select last_insert_id()", dbConnection());
			//fix this
			this.id = Convert.ToInt16(incId.ExecuteScalar());
			return true;
		}
		
		/* Update the current object in the Database 
		 */
		public bool update() {
			string sql = "UPDATE " + this._dbTable + " SET \n";
			int runs = this._dbData.Keys.Count;
			int i = 1;
			foreach (var field in this._dbData.Keys) {
				sql += field + " = " + this._dbEncapsulate(field);
				if (i++ < runs) {
					sql += ",\n";
				} else {
					sql += "\n";
				}
			}
			sql += "WHERE " + this._primaryKey + " = " + this._dbEncapsulate(this._primaryKey);
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			return (cmd.ExecuteNonQuery() == 1 ? true : false);
		}
		
		/* Delete the current object from the Database */
		public bool delete() {
			string sql = "SELECT * FROM " + this._dbTable + " WHERE " + this._primaryKey + " = " + this.id;
			MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
			return (cmd.ExecuteNonQuery() == 1 ? true : false);
		}

/* End of Database I/O
 * ----------------------------------------------------
 * Beginning of DB helper functions
 */	
		
		/* Make sure we have schema info for the current table
		 */
		public bool getSchema() {
			if (!(_dbSchemas.ContainsKey(this._dbTable))) {
				string sql = "SELECT * FROM " + this._dbTable + " LIMIT 1";
				MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
				using (MySqlDataReader reader = cmd.ExecuteReader()) {
					while(reader.Read()) {
						_dbSchemas[this._dbTable] = reader.GetSchemaTable();
					}
				}
			}
			return true;
		}
		
		/* Return a string for an SQL statement for the specified key
		 * String and datatypes should be encapsulated with ''
		 * Empty key values should return as string NULL
		 */
		private string _dbEncapsulate(string key) {
			string returnString;
			Type type = typeof(int);
			
			foreach (DataRow row in _dbSchemas[this._dbTable].Rows){
				if ((string) row["ColumnName"] == key) {
					type = Type.GetType(row["DataType"].ToString());
					break;
				}
			}
			
			if (type == typeof(string) || type == typeof(DateTime)) {
				returnString = "'" + this._dbData[key].ToString() + "'";
			} else {
				returnString = ((this._dbData[key].ToString().Length > 0) ? this._dbData[key].ToString() : "NULL");
			}
			return returnString;
		}
		
		/*
		 * fill _dbData from the specified MySqlDataReader position
		 * Feth the schema for the current table if we don't already have it
		 */
		private bool _readerFill (MySqlDataReader reader)
		{
			if (!(_dbSchemas.ContainsKey(this._dbTable))) {
				_dbSchemas[this._dbTable] = reader.GetSchemaTable();
			}
			this._dbData.Clear ();
			for (int i = 0; i < reader.FieldCount; i++) {
				this._dbData.Add (reader.GetName (i), reader[reader.GetName (i)]);
			}
			return true;
		}
		
/* End of DB helper functions
 * ----------------------------------------------------
 * Beginning of I/O
 * Use these to read/write to and from _dbTable
 * To start locking variable names, create new similar functions in the child
 */		
 
 	public object read(string key) {
 		return this._dbData[key];
 	}
 	
 	public bool write(string key, object input) {
 		this._dbData[key] = input;
 		return true;
 	}
 
 /* End of IO
 * ----------------------------------------------------
 * Beginning of stuff
 */		
		
		/* Virtual spawn method, overridden in children, so children can create new objects the same type as itself */
		virtual protected DataMaster spawn() {
			return new DataMaster();
		}

		/* Create and return a dynamic HTML input control, it's up to the programmer to make sure the ID's are unique */
		static public WebControl createHtmlInputControl(object dataObject, string id) {
			WebControl webControl;
			if (dataObject is bool) {
				CheckBox control = new CheckBox ();
				control.Checked = (bool)dataObject;
				webControl = control;
			} else {
				TextBox control = new TextBox ();
				control.Text = dataObject.ToString ();
				webControl = control;
			}
			webControl.ID = id;
			return webControl;
		}
		
		/* Have we already made a conrol with this ID? */
		public bool webControlExists(WebControl container, string controlID) {
        if (container != null && container.HasControls()) {
            if (container.FindControl(controlID) != null){
                return true;
			}
		}
        return false;
    	}
		
		/* Add a generated table to the specified placeholder */
		public bool createPlaceHolderTable(PlaceHolder placeholder)
		{
			placeholder.Controls.Add(this.createTable());
			return true;
		}
		
		/* Create a table with input controls for the current database table */
		public Table createTable()
		{
			Table table = new Table ();
			foreach (var key in this._dbData.Keys)
			{
				TableRow row = new TableRow ();
				
				TableCell cellA = new TableCell ();
				cellA.Text = key;
				row.Cells.Add (cellA);
				
				TableCell cellB = new TableCell ();
				cellB.Controls.Add (createHtmlInputControl(this._dbData[key], key));
				row.Cells.Add (cellB);
				table.Rows.Add (row);
			}
			return table;
		}
		
		//work in progress
		//create the opposite of createHtmlInputControl to handle control types when read
		public bool fillObjectFromPlaceHolder(PlaceHolder placeholder) {
			Control currentControl;
			foreach(DataRow row in _dbSchemas[this._dbTable].Rows) {
				currentControl = placeholder.FindControl((row["ColumnName"]).ToString());
				if (currentControl is CheckBox) {
					this._dbData[row["ColumnName"].ToString()] = ((CheckBox)currentControl).Checked;
				} else if (currentControl is TextBox) {
					this._dbData[row["ColumnName"].ToString()] = ((TextBox)currentControl).Text;
				}
			}
			return true;
		}
		
/* End of stuff
 * ----------------------------------------------------
 */		
 
	}
}