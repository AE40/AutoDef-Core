using System;
using System.IO;

namespace AutoDef
{
	class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args">
		/// Takes in the search term as the first parameter, 
		/// the string to replace it with as the second parameter, 
		/// the file to search as the third parameter and
		/// an optional bool for weather to be caps sensetive (default true) as the fourth parameter 
		/// </param>
		static void Main( string [] args )
		{
			if( args.Length > 4 || args.Length < 3 )
			{
				throw new ArgumentOutOfRangeException();
			}

			string lastTriedPath = "";

			string search = args [ 0 ];
			string replace = args [ 1 ];
			string content = "";
			try
			{
				lastTriedPath = args [ 2 ];
				content = File.ReadAllText( lastTriedPath );
			}
			catch( FileNotFoundException )
			{
				try
				{
					lastTriedPath = Path.Combine( Environment.CurrentDirectory, args [ 2 ] );
					content = File.ReadAllText( lastTriedPath );
				}
				catch( FileNotFoundException )
				{
					Console.WriteLine( "The specified file path could not be found" );
					return;
				}
			}
			bool capsSensitive = true;

			if( args.Length > 3 )
			{
				capsSensitive = Boolean.Parse( args [ 3 ] );
			}

			if( !capsSensitive )
			{
				search = search.ToLower();
				content = content.ToLower();
			}

			string newContent = content.Replace( search, replace );
			if( newContent == content )
			{
				if( capsSensitive )
				{
					Console.WriteLine( "The search term could not be foun within the file" );
				}
				else
				{
					Console.WriteLine( "The search term could not be foun within the file. Maby it is capitalized?" );
				}
				return;
			}

			File.WriteAllText( lastTriedPath, newContent );
			Console.WriteLine("Search term replaced successfully");

		}

	}
}
