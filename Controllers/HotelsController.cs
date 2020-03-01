using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelBeds.Controllers
{
    [Route("hotels")]
    public class HotelsController : Controller
    {
        const string Api_Key = "956sxnzbe36anayk6bafmywq";
        const string Secret = "9Tuvb5X9cR";
        const string Endpoint = "https://api.test.hotelbeds.com/hotel-content-api/1.0/hotels?fields=all&language=ENG&from=1&to=100&useSecondaryLanguage=false";
        
        public ContentResult Get()
        {
            
            var client = new RestClient(Endpoint);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Api-key", Api_Key);
            request.AddHeader("X-Signature", GetSignature());
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Encoding", "gzip");
            IRestResponse response = client.Execute(request);
            return Content( response.Content);
        }  
        
        [Route("{id}")]
        public ContentResult Get(int id)
        {
            
            var client = new RestClient($"https://api.test.hotelbeds.com/hotel-content-api/1.0/hotels/{id}/details?language=ENG&useSecondaryLanguage=False");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Api-key", Api_Key);
            request.AddHeader("X-Signature", GetSignature());
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Encoding", "gzip");
            IRestResponse response = client.Execute(request);
            return Content( response.Content);
        }
        private string GetSignature()
        {
            
            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;
            using (var sha = SHA256.Create()) {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                   var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(Api_Key + Secret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }

           return signature;
        }
    }
}
