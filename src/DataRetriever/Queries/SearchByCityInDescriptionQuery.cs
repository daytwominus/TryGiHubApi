using System;
using System.Collections.Generic;
using System.Text;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchByCityInDescriptionQuery : BaseQuery
	{
		public SearchByCityInDescriptionQuery(string location)
		{
			Variables.Add(new KeyValuePair<string, string>(nameof(location), location));
		}

		protected override string QueryTemplate { get; } = @"{
		  search(query: ""description:{location}"", type: REPOSITORY, first: 10) {
				repositoryCount
					edges
				{
					node
					{
						... on Repository {
							name
								description

							stargazers {
								totalCount

							}
							forks {
								totalCount
		  
							}
							updatedAt

						}
					}
				}
			}
		}";
	}
}
