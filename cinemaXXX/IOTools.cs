using System;
using System.Text.RegularExpressions;
namespace cinemaXXX
{
	public static class IOTools
	{
		// Takes a DateTime object and converts it into an UnixTime long
		public static long toUnixTime(DateTime dt)
		{
			DateTime unixEpoch = new DateTime(1970,1,1,0,0,0);
			return ((dt.Ticks - unixEpoch.Ticks)/10000000);
		}
		
		// Takes an UnixTime long and converts it into a DateTime object
		public static DateTime fromUnixTime(long dt)
		{
			DateTime unixEpoch = new DateTime(1970,1,1,0,0,0);
			return unixEpoch.AddSeconds(dt);
		}
		
		// Takes strings in the form of "YYYY/MM/DD", "YYYY/MM/DD HH:MM:SS" or "DD/MM/YYYY", "DD/MM/YYYY HH:MM:SS" and returns a DateTime
		public static DateTime stringToDateTime(string date_str)
		{
			Regex r = new Regex("^[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]");
			
			// If the string date format is without hours, minutes, seconds
			if (date_str.IndexOf(" ") == -1) {
				string[] date_arr = new string[3];
				date_arr = date_str.Split('/');
				//int[] ints = date_arr.Select(x => int.Parse(x)).ToArray();
				return (r.Match(date_str).Success) ? new DateTime(int.Parse(date_arr[0]), int.Parse(date_arr[1]), int.Parse(date_arr[2]), 0, 0, 0) : new DateTime(int.Parse(date_arr[0]), int.Parse(date_arr[1]), int.Parse(date_arr[2]), 0, 0, 0);
			}
			// If the string date format is with hours, minutes, seconds
			else {
				string[] date_arr = new string[6];
				date_arr = date_str.Replace(':', '/').Replace(' ', '/').Split('/');
				return (r.Match(date_str).Success) ? new DateTime(int.Parse(date_arr[0]), int.Parse(date_arr[1]), int.Parse(date_arr[2]), int.Parse(date_arr[3]), int.Parse(date_arr[4]), int.Parse(date_arr[5])) : new DateTime(int.Parse(date_arr[0]), int.Parse(date_arr[1]), int.Parse(date_arr[2]), int.Parse(date_arr[3]), int.Parse(date_arr[4]), int.Parse(date_arr[5]));
			}
		}
		
		// Escapes single and double quotes. This might not be the proper solution, but it works for now,
		// however there still need to be som int validation of it being an int.
		public static string cleanString(string str)
		{
			return str.Replace("'", "\'").Replace("\"", "\\\"");
		}
	}
}

