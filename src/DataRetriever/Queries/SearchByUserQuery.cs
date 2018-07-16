using System;
using System.Collections.Generic;
using System.Text;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchByUserQuery : BaseQuery
    {
        public SearchByUserQuery(string user)
        {
            Variables.Add(new KeyValuePair<string, string>(nameof(user), user));
        }

        protected override string QueryTemplate { get; } = @"
{
  search(query: ""user:{user} sort:stars-desc"", type: REPOSITORY, first: 10) {
        edges {
            node {
                ... on Repository
                {
                    name
                        resourcePath
                    assignableUsers(first:10) {
                        edges {
                            node {
                                name
                            }
                        }
                    }
                    collaborators(first: 20)
                    {
                        totalCount
                        edges {
                            node {
                                websiteUrl
                            }
                        }
                    }
                    primaryLanguage {
                        name
                    }
                    collaborators(first: 20)
                    {
                        edges {
                            node {
                                name
                            }
                        }
                    }
                    owner {
                        login
                    }
                    assignableUsers(first: 10)
                    {
                        edges {
                            node {
                                resourcePath
                                    name
                            }
                        }
                    }
                    stargazers {
                        totalCount
                    }
                }
            }
        }
    }
}

";
    }
}
