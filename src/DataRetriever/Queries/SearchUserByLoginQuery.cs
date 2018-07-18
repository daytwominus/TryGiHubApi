using System;
using System.Collections.Generic;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchUserByLoginQuery : BaseQuery
    {
        #region private fields

        private const string RecursionPlaceholder = "{recursionPlaceholder}";
        private readonly int _depth;

        #endregion

        public SearchUserByLoginQuery(string user, int depth)
        {
            Variables.Add(new KeyValuePair<string, string>(nameof(user), user));

            if (_depth < 0)
                throw new ApplicationException($"depth should be >= 0");
            _depth = depth;
        }

        public override string Query
        {
            get
            {
                var q = base.Query;
                var counter = _depth;
                while (counter-- != 0)
                {
                    q = q.Replace(RecursionPlaceholder, SubQuery);
                }

                q = q.Replace(RecursionPlaceholder, string.Empty);

                return q;
            }
        }

        private const string SubQuery = @"
followers(first: 10) {
                  edges {
                    node {
                      id
                      name
                      location
                      {recursionPlaceholder}
                    }
                  }
                }
";
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
                {recursionPlaceholder}
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
