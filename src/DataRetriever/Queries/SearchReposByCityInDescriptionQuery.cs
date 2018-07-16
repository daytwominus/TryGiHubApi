using System.Collections.Generic;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchReposByCityInDescriptionQuery : BaseQuery
	{
		public SearchReposByCityInDescriptionQuery(string location)
		{
			Variables.Add(new KeyValuePair<string, string>(nameof(location), location));
		}

		protected override string QueryTemplate { get; } = @"
{
search(query: ""location:{location} sort:stars-desc"", type: REPOSITORY, first: 10) {
	    edges{
	        node {
	            ... on Repository
	            {
	                name
	                    description
	                primaryLanguage
	                {
	                    name
	                }
	                collaborators (first:20) {
	                    edges {
	                        node {
	                            name
	                        }
	                    }
	                }
	                owner {
	                    id
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
