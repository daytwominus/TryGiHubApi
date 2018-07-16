using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Declarations.DomainModel;
using Declarations.Interfaces;
using Declarations.Interfaces.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        private IDataRetriever _dataRetriever;
        private IConfiguration _configuration;

        public GitController(IDataRetriever dataRetriever, IConfiguration configuration)
        {
            _dataRetriever = dataRetriever;
            _configuration = configuration;
        }

        [HttpGet("city/{city}")]
        public List<GitRepository> Get(string city)
        {
            var repors = _dataRetriever.GetRepositoryByCity(city).Result.ToList();
            Console.WriteLine($"Search by city {city}");
            return repors;
            //return new List<GitRepository>(){ new GitRepository() { Description = "test" } };
        }

	    //[HttpPost("{token}")]
	    [HttpPost("token")]
        //[Route("token")]
	    public void Post([FromBody]string token)
	    {
            Console.WriteLine("updating token");
	        TokenProvider.Token = token;
	    }
	}
}
