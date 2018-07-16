using System.Collections.Generic;
using System.Linq;
using Common;
using Declarations.DomainModel;
using Declarations.Interfaces.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        private readonly IDataRetriever _dataRetriever;
        private readonly ILogger<GitController> _logger;

        public GitController(IDataRetriever dataRetriever, ILogger<GitController> logger)
        {
            _logger = logger;
            _dataRetriever = dataRetriever;
        }

        [HttpGet("city/{city}")]
        public List<GitRepository> Get(string city)
        {
            var repors = _dataRetriever.GetRepositoryByCity(city).Result.ToList();

            _logger.LogInformation($"Search by city {city}");
            return repors;
        }

	    [HttpPost("token")]
	    public void Post([FromBody]string token)
	    {
	        _logger.LogInformation("updating token");
	        TokenProvider.Token = token;
	    }
	}
}
