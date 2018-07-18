using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Declarations;
using Declarations.DomainModel;
using Declarations.Interfaces.Query;
using GiHubGrapthQlDataRetriever.Docs;
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
        private readonly string _user;
        private readonly Func<string> _tokenFunc;

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
                    DefaultRequestHeaders = {Authorization = new AuthenticationHeaderValue("Bearer", token)}
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
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue("Bearer", _tokenFunc.Invoke())
                    }
                };
                _graphQlClient.DefaultRequestHeaders.Add("User-Agent", _user);
            }

            var graphQlResponse = await _graphQlClient.PostAsync(request);
            return graphQlResponse;

        }

        public async Task<QueryResult<IList<GitRepository>>> GetRepositoriesByCity(string city)
        {
            var ret = new QueryResult<IList<GitRepository>>();
            var list = new List<GitRepository>();
            var q = new SearchReposByCityInDescriptionQuery(city);
            var graphQlResponse = await RunQuery(q.Query, null);

            if (ErrorExists(graphQlResponse, ret))
                return ret;

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
                list.Add(graphQlResponse.Data["search"]["edges"][i]["node"].ToObject<GitRepository>());
            ret.Value = list;
            return ret;
        }

        public async Task<QueryResult<IList<GitRepository>>> GetRepositoryByUser(string user)
        {
            var ret = new QueryResult<IList<GitRepository>>();
            var list = new List<GitRepository>();
            var q = new SearchReposByUserQuery(user);
            var graphQlResponse = await RunQuery(q.Query, null);

            if (ErrorExists(graphQlResponse, ret))
                return ret;

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; i++)
                list.Add(graphQlResponse.Data["search"]["edges"][i]["node"].ToObject<GitRepository>());

            ret.Value = list;
            return ret;
        }

        public async Task<QueryResult<GitUser>> GetUserGraphByLogin(string user, int depth, int amount)
        {
            var ret = new QueryResult<GitUser>();
            var q = new SearchUserByLoginQuery(user, depth, amount);
            var graphQlResponse = await RunQuery(q.Query, null);

            if (ErrorExists(graphQlResponse, ret))
                return ret;

            for (var i = 0; i < graphQlResponse.Data["search"]["edges"].Count; ++i)
            {
                ret.Value = BuildUser(graphQlResponse.Data["search"]["edges"][i]["node"]);
                return ret;
            }

            return ret;
        }

        #region helpers

        private static GitUser BuildUser(dynamic currentNode)
        {
            UserDoc doc = currentNode.ToObject<UserDoc>();
            var ret = Mapper.Map(doc, new GitUser());

            if (currentNode["followers"] == null)
                return ret;
            for (var i = 0; i < currentNode["followers"]["edges"].Count; ++i)
                ret.Followers.Add(BuildUser(currentNode["followers"]["edges"][i]["node"]));

            return ret;
        }

        private static bool ErrorExists<T>(GraphQLResponse graphQlResponse, QueryResult<T> ret)
        {
            if (graphQlResponse.Errors != null && 
                (graphQlResponse.Errors.Length > 0 && 
                 !graphQlResponse.Errors.ToList().All(x => x.Message == "Must have push access to view repository collaborators.")))
            {
                ret.Error = string.Join($"{Environment.NewLine}", graphQlResponse.Errors.ToList().Select(x => x.Message));
                return true;
            }

            return false;
        }

        #endregion
    }
}
