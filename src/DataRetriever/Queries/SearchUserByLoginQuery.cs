using System;
using System.Collections.Generic;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public class SearchUserByLoginQuery : BaseQuery
    {
        #region private fields

        private const string RecursionPlaceholder = "{recursionPlaceholder}";
        private const string AmountPlaceholder = "{amount}";
        private readonly int _depth;
        private readonly int _amount;

        #endregion

        public SearchUserByLoginQuery(string user, int depth, int amount)
        {
            Variables.Add(new KeyValuePair<string, string>(nameof(user), user));

            if (_depth < 0)
                throw new ApplicationException($"depth should be >= 0");
            _depth = depth;
            _amount = amount;
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

                q = q.Replace(RecursionPlaceholder, string.Empty).Replace(AmountPlaceholder, _amount.ToString());

                return q;
            }
        }

        private const string SubQuery = @"
            followers(first: {amount}) {
                  edges {
                    node {
                      name
                      location
                      login
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
                      location
                      login
                      {recursionPlaceholder}
                    }
                  }
                }
              }
            }
";
    }
}
