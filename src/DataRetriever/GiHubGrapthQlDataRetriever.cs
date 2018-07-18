using System;
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
        private string _user;
        private Func<string> _tokenFunc;

        public GiHubGrapthQlDataRetriever(string user, string token)
        {
            _user = user;

            CheckClient(token);
        }

        private void CheckClient(string token)
        {
            if (_graphQlClient == null)
            {
                _graphQlClient = new GraphQLClient(GitHubApiPath)
                {
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
                };
                _graphQlClient.DefaultRequestHeaders.Add("User-Agent", _user);
            }
        }

        public GiHubGrapthQlDataRetriever(string user, Func<string> tokenFunc)
        {
            _user = user;
            _tokenFunc = tokenFunc;
        }

        public async Task<GraphQLResponse> RunQuery(string query, IList<KeyValuePair<string, string>> variables)
        {
            var request = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };

            if (_graphQlClient == null)
            {
                _graphQlClient = new GraphQLClient(GitHubApiPath)
                {
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", _tokenFunc.Invoke()) }
                };
                _graphQlClient.DefaultRequestHeaders.Add("User-Agent", _user);
            }

            var graphQlResponse = await _graphQlClient.PostAsync(request);
            return graphQlResponse;

        }

        public async Task<IList<GitRepository>> GetRepositoriesByCity(string city)
        {
            var ret = new List<GitRepository>();
            var q = new SearchReposByCityInDescriptionQuery(city);
            var graphQlResponse = await RunQuery(q.Query, null);

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
            {
                ret.Add(graphQlResponse.Data["search"]["edges"][i]["node"].ToObject<GitRepository>());
            }

            return ret;
        }

        public async Task<IList<GitRepository>> GetRepositoryByUser(string user)
        {
            var ret = new List<GitRepository>();
            var q = new SearchReposByUserQuery(user);
            var graphQlResponse = await RunQuery(q.Query, null);

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
            {
                ret.Add(graphQlResponse.Data["search"]["edges"][i]["node"].ToObject<GitRepository>());
            }

            return ret;
        }

        public async Task<GitUser> GetUserByName(string user, int depth)
        {
            var ret = new GitUser();
            var q = new SearchUserByLoginQuery(user, depth);
            var graphQlResponse = await RunQuery(q.Query, null);

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
            {
                ret = graphQlResponse.Data["search"]["edges"][i].ToObject<GitUser>();
                return ret;
            }

            return ret;
        }
    }
}
