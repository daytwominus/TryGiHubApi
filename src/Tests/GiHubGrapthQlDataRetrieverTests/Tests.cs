using System.Threading.Tasks;
using Common;
using Xunit;

namespace GiHubGrapthQlDataRetrieverTests
{
    public class Tests
    {
	    private readonly GiHubGrapthQlDataRetriever.GiHubGrapthQlDataRetriever _dataRetriever;

		public Tests()
	    {
		    _dataRetriever = new GiHubGrapthQlDataRetriever.GiHubGrapthQlDataRetriever("panasiux", TokenProvider.Token);
		    
	    }

        [Fact]
        public async Task SmokeTest()
        {
	        var res = await _dataRetriever.GetRepositoryByCity("stockholm");
		}
    }
}
