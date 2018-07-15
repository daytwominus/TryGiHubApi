using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Util
{
    public class RestHelper
    {
	    private string _address;
	    public RestHelper(string address)
	    {
		    _address = address;
	    }

		public void CallApi()
	    {
		    var request = WebRequest.Create(_address);
		    var response = request.GetResponse();
			using (var dataStream = response.GetResponseStream())
				using (var reader = new StreamReader(dataStream ?? throw new InvalidOperationException()))
				{
					string responseFromServer = reader.ReadToEnd();
				}
		}
    }
}
