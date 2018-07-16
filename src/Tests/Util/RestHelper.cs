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
		    var response = GetResponse(request);
            if(response == null)
                return;
			using (var dataStream = response.GetResponseStream())
				using (var reader = new StreamReader(dataStream ?? throw new InvalidOperationException()))
				{
					string responseFromServer = reader.ReadToEnd();
				}
		}

        private WebResponse GetResponse(WebRequest request)
        {
            try
            {
                return request.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine($"Request to service failed. Probably address ({_address}) is wrong or service stopped.");
            }

            return null;
        }
    }
}
