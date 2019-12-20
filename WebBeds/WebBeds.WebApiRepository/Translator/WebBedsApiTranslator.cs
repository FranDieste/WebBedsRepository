using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;
using WebBeds.Domain.Services;
using WebBeds.WebApiRepository.Dto;
using WebBeds.WebApiRepository.Interfaces;

namespace WebBeds.WebApiRepository.Translator
{
    public class WebBedsApiTranslator : ITranslator<IEnumerable<WebBedsDto>, IEnumerable<HotelAggregate>>
    {

        private WebBedsApiTranslator() { }

        private static WebBedsApiTranslator _instance;

        public static WebBedsApiTranslator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebBedsApiTranslator();

                return _instance;
            }
        }


        //Throw a dto and return a object domain
        public IEnumerable<HotelAggregate> Translate(IEnumerable<WebBedsDto> dto,int numNights)
        {
            if (dto == null) return null;

            const string C_RateType = "PerNight";

            List<HotelAggregate> hotelAggregateList = new List<HotelAggregate>();
            dto.ToList().ForEach(i =>
            {

                var CurrentDomainHotel = DomainService.Instance.CreateHotelAggregate(i.hotel.propertyID, 
                                                                                     i.hotel.name,
                                                                                     i.hotel.geoId,
                                                                                     i.hotel.rating);
                //Hijos a validar
                if (i.rates != null && i.rates.Count() > 0)
                {
                    i.rates.ToList().ForEach(r =>
                    {

                        var finalValue = (r.rateType.ToUpper()==C_RateType.ToUpper())?(r.value * numNights):r.value;
                        CurrentDomainHotel.AddRate(r.rateType,r.boardType, finalValue);

                    });
                }

                var currentElement = hotelAggregateList.FirstOrDefault(item => item.Id == i.hotel.propertyID);
                if (currentElement == null)
                    hotelAggregateList.Add(CurrentDomainHotel);

            });

            return hotelAggregateList;

        }
    }
}
