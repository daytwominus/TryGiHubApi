using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Common;
using Declarations.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Util.Commands
{
	public class SearchApiCommands
	{
	    private IConfiguration _configuration;

	    public SearchApiCommands()
	    {
	        _configuration = new ConfigurationBuilder()
	            .AddJsonFile("appsettings.json", true, true)
	            .Build();
        }

        public bool Search(string by)
        {
            try
            {
                //var token = TokenProvider.Token;

                var h = new RestHelper("http://localhost:5000/api/values");
                h.CallApi();
            }
            catch (NoTokenException e)
            {
                Console.WriteLine("No token found. Please add it to environment variables or by using this util");
            }
            return true;
        }

        public bool Token(string token)
        {
            Console.WriteLine("Updating token");

            //todo: send token to service
            TokenProvider.Token = token;
            return true;
        }
    }
}

