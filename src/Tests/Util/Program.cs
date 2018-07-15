using System;
using Microsoft.Extensions.Configuration;
using Tectil.NCommand;
using Util.Commands;

namespace Util
{
    class Program
    {
		static void Main(string[] args)
        {
	        IConfiguration config = new ConfigurationBuilder()
		        .AddJsonFile("appsettings.json", true, true)
		        .Build();
	        var endpoint = config["serviceEndpoint"];

			var commands = new NCommands();
	        commands.RunConsole(args);
		}
    }
}
