using Declarations.Interfaces;
using Declarations.Interfaces.Query;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class GitController : Controller
    {
        private IDataRetriever _dataRetriever;

        public GitController(IDataRetriever dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        // GET api/values/5
        [HttpGet("{city}")]
        public string Get(string city)
        {
            return "value";
        }
    }
}
