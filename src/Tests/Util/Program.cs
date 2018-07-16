using CommandDotNet;
using Microsoft.Extensions.Configuration;
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

            var appRunner = new AppRunner<SearchApiCommands>();
            appRunner.Run(args);
        }
    }
}
