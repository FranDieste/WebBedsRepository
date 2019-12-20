using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.WebApiRepository.Interfaces
{
   public interface IRequest
    {
        string Get(string endPoint, int timeOut);
        string Post<T>(string endPoint, int timeOut, T obj);
    }
}
