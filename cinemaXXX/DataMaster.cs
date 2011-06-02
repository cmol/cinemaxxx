using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;

/* cinemaXXX
 * 
 * Write clever stuff here
 * 
 * Database structure rules
 * *. Every table must have a primary key, this primary key can only cover a single column
 * *. This primary key is the row identity for that particular row.
 * *. The name of that field must be used in other tables, when that field is a reference. references must use this name.
 * 
 * NameSpace structure
 * *. Class names must be uppercase CamelCase
 * *. Members must be lowercase camelCase
 * *. Protected/private stuff must begin with an underscore
 * *. All methods must have a return type. If no object is to be returned, the method should return true/false, indicating success/failiure
 * 
 */



 /*
 * DataMaster class, all classess accessing the main database directly must inherit from DataMaster
 * Every table in the Database should have an associated class
 * 
 * Be mindful when using/creating public static methods, as quite a bit of the functionality here expects certain preparations to have already been made
 * 
 * This is the basic, and mandatory structure/content of an inherited class
 * -----------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	// See DataMaster instructions
	public class CLASSNAME : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "TABLENAMEFORTHISCLASS";
		
		public CLASSNAME ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new CLASSNAME();
		}
	}
}
 * ----------------------------------------------------- 
 */

namespace cinemaXXX
{
	public partial class DataMaster
	{

/* 
 * ----------------------------------------------------
 * Beginning of constructors and initialisers
 */
		
		public DataMaster ()
		{
			this._dbData = new Dictionary<string, object>();
			/* get the foreign key references in all the tables, quick and easy */
			if (_dbReferences.Count == 0) {
				string sql = "SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE constraint_schema = '" + ConfigurationSettings.AppSettings["DataBaseName"] + "' AND REFERENCED_COLUMN_NAME IS NOT NULL";
				MySqlCommand cmd = new MySqlCommand(sql, dbConnection());
				using (MySqlDataReader reader = cmd.ExecuteReader()) {
					while(reader.Read()) {
						if (_dbReferences.ContainsKey(reader["TABLE_NAME"].ToString())) {
							((Dictionary<string, string>)_dbReferences[reader["TABLE_NAME"].ToString()]).Add(
								reader["REFERENCED_COLUMN_NAME"].ToString(), 
								reader["REFERENCED_TABLE_NAME"].ToString()
							);
						} else {
							Dictionary<string, string> reference = new Dictionary<string, string>();
							reference.Add(reader["REFERENCED_COLUMN_NAME"].ToString(), reader["REFERENCED_TABLE_NAME"].ToString());
							_dbReferences.Add(reader["TABLE_NAME"].ToString(), reference);
						}
					}
				}
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
		
		/* Standard MySql connection */
		static private MySqlConnection _dbcon;
		static private MySqlConnection dbConnection() {
			if (!(_dbcon is MySqlConnection)) {
				string ConnectionInfo = "Server=" + ConfigurationSettings.AppSettings["DataBaseServer"] + ";Database=" + ConfigurationSettings.AppSettings["DataBaseName"] + ";User ID=" + ConfigurationSettings.AppSettings["DataBaseUid"] + ";Password=" + ConfigurationSettings.AppSettings["DataBasePasswd"] + ";Pooling=" + ConfigurationSettings.AppSettings["DataBasePooling"];
				_dbcon = new MySqlConnection(ConnectionInfo);
				_dbcon.Open();
			}
			return _dbcon;
		}
		
		/* DataTable able to contain Schema info for all the tables
		 * It is important that the schema be present on the table key for the current object, so _prepare makes sure it's there
		 * Dictionary key is the table name
		 */
		//static private Dictionary<string, DataTable> _dbSchemas = new Dictionary<string, DataTable>();
		static public Dictionary<string, DataTable> _dbSchemas = new Dictionary<string, DataTable>();
		
		/* used for multidimensional reference storage [tablename][columnnamewithref][reftargettable], columnname in source and target should be the same according to our db structure */
		static private Dictionary<string, Dictionary<string,string>> _dbReferences = new Dictionary<string, Dictionary<string, string>>();
		
		/* Dictionary for our DB data 
		 * Data in this object shouldn't be accessed hardcoded by key reference from outside the corrosponding class, but through the read an write functions, so database column names can be changed without affecting the application source/binaries
		 * Dictionary key is the column name
		 */
		protected Dictionary<string, object> _dbData;
		
		/* What is the main table behind this dataclass
		 * Will be set by the inhereted class when it is instantiated
		 */
		protected string _dbTable;
		
		/* What is the primary key for this table 
		 * Will be set when the inherited class is instantiated
		 */
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
		
		/* Make sure we have schema info for the current table. It is important that this is present, so this is called from _prepare
		 */
		public bool getSchema() {
			if (!(_dbSchemas.ContainsKey(this._dbTable))) {
				string sql = "SELECT * FROM " + this._dbTable + " LIMIT 1";
				//throw new Exception(sql);
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
		 * No db input data is ever validated for injection etc. this would be a good place to do it
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
			
			if (type == typeof(string)) {
				returnString = "'" + this._dbData[key].ToString() + "'";
			} else if (type == typeof(DateTime)) {
				returnString = "'" + IOTools.stringToDateTime(this._dbData[key].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "'";
			} else {
				returnString = ((this._dbData[key].ToString().Length > 0) ? this._dbData[key].ToString() : "NULL");
			}
			return returnString;
		}
		
		/*
		 * fill _dbData from the specified MySqlDataReader position
		 * Fetch the schema for the current table if we don't already have it (we should have)
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
		
		/* Is this key a foreign key? meening there is a primary key in som table there is a reference to this one? 
		 */
		private bool _isForeignKey(string key) {
			return DataMaster._dbReferences[this._dbTable].ContainsKey(key);
		}
		
		/* If this key is a foreign key, then in which table is the "master" for this reference? 
		 */
		private string _foreignKeyTable(string key) {
			if (!DataMaster._dbReferences[this._dbTable].ContainsKey(key)) return null;
			return DataMaster._dbReferences[this._dbTable][key];
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
		
		/* Virtual spawn method, overridden in children, so children can create new objects the same type as itself
		 * This could be replaced by the newer spawnTableClass, and spawn() could subsequently be removed from inherited classes, although spawnTableClass is a bit heavier than this one 
		  */
		virtual protected DataMaster spawn() {
			return new DataMaster();
		}

		/* Create a dynamic HTML input control for the provided key, it's up to the programmer to make sure the ID's on a page are unique */
		public WebControl createHtmlInputControl(string key, string id) {
			Type type = typeof(int);
			int length = 0;
			WebControl webControl;
			foreach (DataRow row in _dbSchemas[this._dbTable].Rows){
				if ((string) row["ColumnName"] == key) {
					type = Type.GetType(row["DataType"].ToString());
					length = (int) row["ColumnSize"];
					break;
				}
			}
			
			if (type ==typeof(bool)) {
				CheckBox control = new CheckBox ();
				control.Checked = (bool)this._dbData[key];
				webControl = control;
			} else {
				if (this._isForeignKey(key)) {
					/* SELECTS! */
				    DropDownList control = new DropDownList();

					DataMaster tmpObj = DataMaster.spawnTableClass(this._foreignKeyTable(key));

					var elements = tmpObj.getAll();
					foreach (var elementkey in elements.Keys){
						ListItem li = new ListItem();
						li.Value = elementkey.ToString();
						li.Text = elements[elementkey].read("title").ToString();
						control.Items.Add(li);
					}
				    webControl = control;
				} else {
					TextBox control = new TextBox ();
					control.Text = this._dbData[key].ToString();
					if (length > 255) {
						control.TextMode = TextBoxMode.MultiLine;
					}
					webControl = control;
				}
			}
			webControl.ID = id;
			return webControl;
		}
	
		/* Have we already made a control with this ID? */
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
				cellB.Controls.Add (this.createHtmlInputControl(key, key));
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
		
		/* Find the class belonging to the provided table, and return an object of that class
		 * Usefull when you want the data that is being referenced, and not just the id
		 * A bit heavy, it needs to instantiate the classes one by one, until it reaches the matching class
		 */
		public static DataMaster spawnTableClass(string table) {
			Assembly assembly = Assembly.GetExecutingAssembly();
			var types = assembly.GetTypes();
			foreach (Type type in types) {
				if (type.IsSubclassOf(typeof(DataMaster))) {
					DataMaster tmpObject = (DataMaster)Activator.CreateInstance(type);
					if (tmpObject._dbTable == table) {
						return tmpObject;
					}
				}
			}
			return null;
		}
		
/* End of stuff
 * ----------------------------------------------------
 */		
 
	}
}
