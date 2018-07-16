using System.Threading.Tasks;
using Common;
using NUnit.Framework;

namespace GiHubGrapthQlDataRetrieverTests
{
	[TestFixture]
    public class Tests
    {
	    private readonly GiHubGrapthQlDataRetriever.GiHubGrapthQlDataRetriever _dataRetriever;

		public Tests()
	    {
		    _dataRetriever = new GiHubGrapthQlDataRetriever.GiHubGrapthQlDataRetriever("panasiux", TokenProvider.Token);
	    }

        [Test]
        public void SmokeTest()
        {
	        var res = _dataRetriever.GetRepositoriesByCity("stockholm").Result;
			Assert.NotNull(res);
		}
    }
}
