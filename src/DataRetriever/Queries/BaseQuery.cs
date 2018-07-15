using System;
using System.Collections.Generic;
using System.Text;

namespace GiHubGrapthQlDataRetriever.Queries
{
    public abstract class BaseQuery
    {
		protected abstract string QueryTemplate { get; }

	    public string Query
	    {
		    get
		    {
			    var q = QueryTemplate;
				Variables.ForEach(v => q = q.Replace("{" + v.Key + "}", v.Value));
			    return q;
		    }
	    }

	    public List<KeyValuePair<string, string>> Variables { get; } = new List<KeyValuePair<string, string>>();
    }
}
