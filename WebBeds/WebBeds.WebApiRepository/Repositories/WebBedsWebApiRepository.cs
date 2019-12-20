using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Repositories;
using WebBeds.WebApiRepository.Dto;
using WebBeds.WebApiRepository.Interfaces;
using WebBeds.WebApiRepository.Translator;

namespace WebBeds.WebApiRepository.Repositories
{
    public class WebBedsWebApiRepository : IWebBedsWebApiRepository
    {
        private IRequest _request;
        //____________________________
        //.Net Framework. config file
        //_____________________________
        //private readonly string endPoint = ConfigurationManager.AppSettings["WebBedsEndPoint"];
        //private readonly string timeOutString = ConfigurationManager.AppSettings["WebBedsTimeOut"];;
        int timeOut = 1;

      

        public WebBedsWebApiRepository(IRequest request)
        {
            _request = request;
        }

        //Devuelve un objeto de dominio
        public IEnumerable<HotelAggregate> GetData(int destinationId, int numNights)
        {
            //_____________________________________________________________________________________________
            //This not is a good practice, but I don´t know how I can acceded to a config file in .Net Cor
            //The files .config in .Net Core not is  use
            //_____________________________________________________________________________________________
            const string url = "https://webbedsdevtest.azurewebsites.net/api/findBargain?";
            const string code = "aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==";

            try
            {
                var endPoint = GetEndPoint(url,destinationId,numNights,code);

                //int.TryParse(timeOutString, out timeOut);
                var responseString = _request.Get(endPoint, timeOut);
                //Parseamos el string a un dto nuestro.
                var webBedsDto = JsonConvert.DeserializeObject<List<WebBedsDto>>(responseString);

                //_______________________________________________________
                //we transform a external dto in a list of  domain model
                //_______________________________________________________
                return WebBedsApiTranslator.Instance.Translate(webBedsDto, numNights);

            }
            catch (Exception)
            {
                throw;
            }

        }

        private string GetEndPoint(string url,int destinationId,int numNights,string securCode)
        {
            var finalEndPoint = url + "destinationId=" + destinationId + "&nights=" + numNights + "&code=" + securCode;
            return finalEndPoint;
        }
    }
}
