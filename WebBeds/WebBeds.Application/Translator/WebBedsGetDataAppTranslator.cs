using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBeds.Application.Dto;
using WebBeds.Application.Interfaces;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;

namespace WebBeds.Application.Translator
{
    public class WebBedsGetDataAppTranslator : IAppTranslator<WebBedsGetDataRequestDto, WebBedsGetDataQueryResult>
    {
        private WebBedsGetDataAppTranslator() { }

        private static WebBedsGetDataAppTranslator _instance;
        //_______________________________________________________________________________
        //Singleton patron.
        //I think is better than a static class,because we can implemented interfaces or
        //inherit classes(is more open)
        //________________________________________________________________________________
        public static WebBedsGetDataAppTranslator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebBedsGetDataAppTranslator();

                return _instance;
            }
        }

        //_______________________________________________________________________
        //We tranform information to return the dto "WebBedsGetDataQueryResult"
        //________________________________________________________________________
        public WebBedsGetDataQueryResult Translate(WebBedsGetDataRequestDto dto)
        {
            if (dto == null || dto.HotelList.Count() == 0 && dto.Exceptions.Count() == 0)
                return null;

            var result = new WebBedsGetDataQueryResult();

            //Get a data of webBeds
            if (dto.HotelList.Count()!=0)
            {
                var webBedsGetDataDtos = GetWebBedsData(dto.HotelList);
                result.DataResult = webBedsGetDataDtos;
            }

            if(dto.Exceptions.Count() != 0)
            {
                var exceptionMessages = GetWebBedsExceptions(dto.Exceptions);
                result.ErrorMessage = exceptionMessages;
            }
            
            return result;

        }

        //____________________________________________________
        //We transform a list of HotelAggregate in a our dto
        //____________________________________________________
        private IEnumerable<WebBedsGetHotelDto> GetWebBedsData(IEnumerable<HotelAggregate> webBedHotelAggregateList)
        {

            var webBedsGetDataDto = new List<WebBedsGetHotelDto>();

            webBedHotelAggregateList.ToList().ForEach(h =>
            {
                var hotelDto = new WebBedsGetHotelDto()
                {

                    HotelId = h.Id,
                    HotelName = h.Name,
                    HotelGeoId = h.GeoId,
                    HotelRating = h.Rating
                };


                if (h.RatesEntity != null && h.RatesEntity.Count() > 0)
                {
                    hotelDto.WebBedsRates = new List<WebBedsRateDto>();
                    h.RatesEntity.ToList().ForEach(r =>
                    {
                        var currentRate = new WebBedsRateDto()
                        {

                             RateType = r.RateType,
                             BoardType = r.BoardType,
                             Value = r.Value
                        };
                        hotelDto.WebBedsRates.Add(currentRate);
                    });
                }

                webBedsGetDataDto.Add(hotelDto);

            });

            return webBedsGetDataDto;
        }

        //____________________________________________________
        //We transform a list of Exception in a our dto
        //____________________________________________________
        private IEnumerable<string> GetWebBedsExceptions(IEnumerable<Exception> exc)
        {

            var expetionMessages = new List<string>();

            if (exc.Count() > 0)
            {
                exc.ToList().ForEach(d =>
                {
                    expetionMessages.Add(d.Message);
                });
            }

            return expetionMessages;


        }

    }
}
