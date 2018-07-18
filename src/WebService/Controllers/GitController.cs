using System.Threading.Tasks;
using Common;
using Declarations.Interfaces.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        #region private fields

        private readonly IDataRetriever _dataRetriever;
        private readonly ILogger<GitController> _logger;

        #endregion

        #region Init

        public GitController(IDataRetriever dataRetriever, ILogger<GitController> logger)
        {
            _logger = logger;
            _dataRetriever = dataRetriever;
        }

        #endregion

        #region API

        [HttpPost("token")]
        public void Post([FromBody]string token)
        {
            _logger.LogInformation("updating token");
            TokenProvider.Token = token;
        }

        [HttpGet("repos/{city}")]
        public async Task<IActionResult> GetReposByCityAsync(string city)
        {
            _logger.LogInformation($"Search repos by city {city}");
            var data = await _dataRetriever.GetRepositoriesByCity(city);
            if (!string.IsNullOrEmpty(data.Error))
                return StatusCode(500, data.Error);

            return Ok(data);
        }

        [HttpGet("repos/info/{user}")]
        public async Task<IActionResult> GetReposByUser(string user)
        {
            _logger.LogInformation($"Search repos by user {user}");
            var data = await _dataRetriever.GetRepositoryByUser(user);
            if (!string.IsNullOrEmpty(data.Error))
                return StatusCode(500, data.Error);

            return Ok(data);
        }

        [HttpGet("users/{user}")]
        public async Task<IActionResult> GetUserByLogin(string user, int depth, int amount)
        {
            if (depth < 0)
                return BadRequest($"{nameof(depth)} must be >= 0");
            var res = await _dataRetriever.GetUserGraphByLogin(user, depth, amount);
            if (!string.IsNullOrEmpty(res.Error))
                return StatusCode(500, res.Error);

            _logger.LogInformation($"Search users by user {user}");
            return Ok(res);
        }

        #endregion
    }
}
