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

        [HttpGet("repos/{city}")]
        public List<GitRepository> GetReposByCity(string city)
        {
            var repors = _dataRetriever.GetRepositoriesByCity(city).Result.ToList();

            _logger.LogInformation($"Search repos by city {city}");
            return repors;
        }

        [HttpGet("repos/{user}")]
        public List<GitRepository> GetReposByUser(string user)
        {
            var repors = _dataRetriever.GetRepositoryByUser(user).Result.ToList();

            _logger.LogInformation($"Search repos by user {user}");
            return repors;
        }

        [HttpGet("users/{user}")]
        public GitUser GetUserByLogin(string user)
        {
            var repors = _dataRetriever.GetUserByName(user).Result;

            _logger.LogInformation($"Search users by user {user}");
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
