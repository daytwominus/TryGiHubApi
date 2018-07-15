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

        [HttpGet("{city}")]
        public string Get(string city)
        {
            return "value";
        }

	    [HttpPut("{token}")]
	    public void Put([FromBody]string value)
	    {
	    }
	}
}
