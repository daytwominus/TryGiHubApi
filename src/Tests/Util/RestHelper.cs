using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Declarations.DomainModel;

namespace Util
{
    public class RestHelper
    {
	    private readonly string _address;
	    public RestHelper(string address)
	    {
		    _address = address;
	    }

		public void Get(string spec)
	    {
	        using (var client = new HttpClient())
	        {
	            var res = client.GetAsync($"{_address}/{spec}").Result;
	            var responseBody = res.Content.ReadAsStringAsync().Result;

                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GitRepository>>(responseBody);

                Console.WriteLine($"Update token response: {res.StatusCode}");
	        }
        }

        public void Post(string content)
        {
            using (var client = new HttpClient())
            {
                var res = client.PostAsync(_address, new StringContent(content, Encoding.UTF8, "application/json")).Result;
                
                Console.WriteLine($"Update token response: {res.StatusCode}");
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
