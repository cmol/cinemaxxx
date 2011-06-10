using System;
using System.Data;
using System.Collections.Generic;

namespace cinemaXXX
{
	public class Genre : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "genres";
		
		public Genre ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Genre();
		}
	}
	
	public class MovieGenre : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "movie_genre";
		
		public MovieGenre ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new MovieGenre();
		}
		
		override public string ToString() {
			DataMaster movie = new Movie();
			movie.id = Convert.ToInt32(this.read("moid"));
			movie.get();
			DataMaster genre = new Genre();
			genre.id = Convert.ToInt32(this.read("gid"));
			genre.get();
			return movie + " - " + genre;
		}
	}
	
	public class PeopleMovies : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "people_movies";
		
		public PeopleMovies ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new PeopleMovies();
		}
		
		override public string ToString() {
			DataMaster movie = new Movie();
			movie.id = Convert.ToInt32(this.read("moid"));
			movie.get();
			DataMaster person = new People();
			person.id = Convert.ToInt32(this.read("peid"));
			person.get();
			DataMaster role = new Role();
			role.id = Convert.ToInt32(this.read("rid"));
			role.get();
			return movie + " - " + person + " - " + role;
		}
		
	}
	
	public class PGSystems : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "pg_systems";
		
		public PGSystems ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new PGSystems();
		}
	}
	
	public class Price : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "prices";
		
		public Price ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Price();
		}
		
		override public string ToString() {
			return this.read("title") + " " + this.read("price");
		}
		
	}	
	
	public class Role : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "role";
		
		public Role ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Role();
		}
	}
	
	public class Theater : DataMaster
	{
		//what is the main table behind this dataclass
		new private static string _dbTable = "theaters";
		
		public Theater ()
		{
			base._dbTable = _dbTable;
			base._prepare();
		}
		
		override protected DataMaster spawn() {
			return new Theater();
		}
	}
}
