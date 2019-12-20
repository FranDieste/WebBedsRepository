using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebBeds.WebApiRepository.Interfaces;

namespace WebBeds.WebApiRepository.Base
{
    public class Request : IRequest
    {
        //_________________________________________________________________________
        //I launch a request to the endpoint and return the serialized information
        //_________________________________________________________________________
        public string Get(string endPoint, int timeOut)
        {

            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(timeOut);
                var response = client.GetAsync(endPoint).Result;

                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public string Post<T>(string endPoint, int timeOut, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
