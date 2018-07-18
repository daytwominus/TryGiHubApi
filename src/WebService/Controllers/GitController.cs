using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetReposByUser(string user)
        {   
            var data = await _dataRetriever.GetRepositoryByUser(user);

            _logger.LogInformation($"Search repos by user {user}");
            return Ok(data);
        }

        [HttpGet("users/{user}")]
        public async Task<IActionResult> GetUserByLogin(string user, int depth)
        {
            if (depth < 0)
                return BadRequest($"{nameof(depth)} must be >= 0");
            var userInfo = await _dataRetriever.GetUserGraphByLogin(user, depth);

            _logger.LogInformation($"Search users by user {user}");
            return Ok(userInfo);
        }

        [HttpPost("token")]
        public void Post([FromBody]string token)
        {
            _logger.LogInformation("updating token");
            TokenProvider.Token = token;
        }
    }
}
