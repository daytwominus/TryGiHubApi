using System.Collections.Generic;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchUserByLoginQuery : BaseQuery
    {
        public SearchUserByLoginQuery(string user)
        {
            Variables.Add(new KeyValuePair<string, string>(nameof(user), user));//todo: add recursion for specifying depth
        }
        protected override string QueryTemplate { get; } = @"
{
  search (query: ""{user}"", type: USER, first: 1){
    edges {
      node {
        ... on User {
          name
          followers (first:20){
            edges {
              node {
                name
                location
                followers(first:10) {
                  edges {
                    node {
                      id
                      name
                      location
                    }
                  }
                }
              }
            }
          }
          login
        }
      }
    }
  }
}

";
    }
}
