using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Declarations.DomainModel;
using Declarations.Interfaces.Query;
using GiHubGrapthQlDataRetriever.Queries;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;

namespace GiHubGrapthQlDataRetriever
{
    public class GiHubGrapthQlDataRetriever : IDataRetriever
    {
        private const string GitHubApiPath = "https://api.github.com/graphql";
        private GraphQLClient _graphQlClient;

        public GiHubGrapthQlDataRetriever(string user, string token)
        {
            _graphQlClient = new GraphQLClient(GitHubApiPath)
            {
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
            };
            _graphQlClient.DefaultRequestHeaders.Add("User-Agent", user);
        }

        public async Task<GraphQLResponse> RunQuery(string query, IList<KeyValuePair<string, string>> variables)
        {
            var request = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };

            var graphQLResponse = await _graphQlClient.PostAsync(request);
	        return graphQLResponse;

        }

        public async Task<IList<GitRepository>> GetRepositoryByCity(string city)
        {
	        var ret = new List<GitRepository>();
	        var q = new SearchByCityInDescriptionQuery(city);
	        var graphQlResponse = await RunQuery(q.Query, null);

	        for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
	        {
		        ret.Add(graphQlResponse.Data["search"]["edges"][i]["node"].ToObject<GitRepository>());
			}

	        return ret;
        }
    }
}
