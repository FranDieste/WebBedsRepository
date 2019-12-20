using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBeds.Application.Dto;
using WebBeds.Application.Interfaces;
using WebBeds.Application.Translator;
using WebBeds.CrossCutting.Cache;
using WebBeds.CrossCutting.Log;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Repositories;

namespace WebBeds.Application.Models.Queries.WebBedsQuery
{
    public class WebBedsGetDataQueryHandler : IQueryHandler<WebBedsGetDataQuery, WebBedsGetDataQueryResult>
    {
        private IWebBedsWebApiRepository _webBedsWebApiRepository;
        //________________________________________________________________________________________________
        //This constructor injects the repository class. There are no references to the repository layer
        //This is done by Unity
        //________________________________________________________________________________________________
        public WebBedsGetDataQueryHandler(IWebBedsWebApiRepository webBedsWebApiRepository)
        {
            _webBedsWebApiRepository = webBedsWebApiRepository;
        }

        //_______________________________________________________________________________________
        //This method realice the following actions:
        //  -Validate empthy params or not informed.
        //  -We search  the information in cache.If not is in cache we call to server repository
        //  -We tranform the information receibed
        //_______________________________________________________________________________________
        public WebBedsGetDataQueryResult ExecuteQuery(WebBedsGetDataQuery dto)
        {
            try
            {
                if (dto == null || dto.DestinationId == 0) return null;
               
                if (dto.NumNights <= 0)
                    dto.NumNights = 1;

                var webBedDataRequestList = new WebBedsGetDataRequestDto();

                //We find an information first in cache memory 
                var key = dto.DestinationId.ToString() + "_" + dto.NumNights.ToString();

                webBedDataRequestList = GetInformationFromMemoryCache(key);
                
                if (IsNullOrEmptyCacheData(webBedDataRequestList))
                    webBedDataRequestList = GetDataWebBedFromWebApiService(dto);

                if (webBedDataRequestList == null)
                    return null;
                else if (webBedDataRequestList.HotelList != null && webBedDataRequestList.HotelList.Count()>0)
                     ApplicationCache.Cache.Set(key, webBedDataRequestList);

                //We call the translate function  to return our dto from domain objects list
                return WebBedsGetDataAppTranslator.Instance.Translate(webBedDataRequestList);
            }
            catch (Exception ex)
            {
                LogEnvironment.Instance.Add("Error WebBedsGetDataQueryHandler:" + GetExceptionString(ex));
                throw;
            }
        }
        //______________________________
        //We Get information of cache
        //______________________________
        private WebBedsGetDataRequestDto GetInformationFromMemoryCache(string key)
        {

            var webBedDataRequestDto = new WebBedsGetDataRequestDto();
            var exceptions = new List<Exception>();
            try
            {
                webBedDataRequestDto = ApplicationCache.Cache.Get<WebBedsGetDataRequestDto>(key);
            }
            catch (Exception ex)
            {
                LogEnvironment.Instance.Add("Error GetInformationFromMemoryCache:" + GetExceptionString(ex));
                exceptions.Add(ex);
                webBedDataRequestDto.Exceptions = exceptions;
            }

            return webBedDataRequestDto;

        }

        private string GetExceptionString(Exception ex)
        {
            var currentString = String.Format("Source:{0} Message:{1} + InnerException:{2}", ex.TargetSite.ToString(), ex.Message.ToString(), (ex.InnerException == null) ? "Null" : ex.InnerException.ToString());

            return currentString;
        }

        //__________________________________________________
        //We get the information from web api repository
        //__________________________________________________
        private WebBedsGetDataRequestDto GetDataWebBedFromWebApiService(WebBedsGetDataQuery dto)
        {

            var resultDataList = new List<HotelAggregate>();
            var exceptionList = new List<Exception>();

            var webBedDataRequestDto = new WebBedsGetDataRequestDto();

            try
            {
                var result = _webBedsWebApiRepository.GetData(dto.DestinationId,dto.NumNights);
                //resultDataList.AddRange(result);
                resultDataList = result.ToList();
            }
            catch (Exception ex)
            {

                exceptionList.Add(ex);
            }

            webBedDataRequestDto.HotelList = resultDataList;
            webBedDataRequestDto.Exceptions = exceptionList;
           
            return webBedDataRequestDto;
        }

        private bool IsNullOrEmptyCacheData(WebBedsGetDataRequestDto dto)
        {

            if (dto == null || dto.HotelList == null || dto.Exceptions == null)
                return true;
            else
                return false;

        }

    }
}
