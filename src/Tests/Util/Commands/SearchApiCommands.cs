using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Declarations.Exceptions;
using Tectil.NCommand.Contract;

namespace Util.Commands
{
	public class SearchApiCommands
	{
		[Command(description: "Searching by connecting to web service")]
		public bool Search([Argument(description: "Search by argument.")] string by)
		{
			try
			{
				var token = TokenProvider.Token;

				var h = new RestHelper("http://localhost:5000/api/values");
				h.CallApi();
			}
			catch (NoTokenException e)
			{
				Console.WriteLine("No token found. Please add it to environment variables or by using this util");
			}
			return true;
		}

		[Command(description: "Providing git token")]
		public bool Token([Argument(description: "Provide token")] string token)
		{
			Console.WriteLine("Updating token");
			
			TokenProvider.Token = token;
			return true;
		}
	}
}

