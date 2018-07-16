using System;
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

        public bool Search(string by, string q)
        {
            try
            {
                //var token = TokenProvider.Token;

                var h = new RestHelper(_configuration["serviceEndpoint"]);
                h.Get($"git/city/{q}");
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
            
            var h = new RestHelper($"{_configuration["serviceEndpoint"]}/git/token");
            h.Post(token);
            TokenProvider.Token = token;
            return true;
        }
    }
}

