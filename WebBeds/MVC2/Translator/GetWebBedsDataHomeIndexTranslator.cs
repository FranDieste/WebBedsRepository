using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBeds.Application.Dto;
using WebBeds.Application.Interfaces;
using WebBeds.UI.Models.Dto;

namespace WebBeds.UI.Translator
{
    public class GetWebBedsDataHomeIndexTranslator : IAppTranslator<WebBedsGetDataQueryResult, WebBBedsDataDto>
    {

        private GetWebBedsDataHomeIndexTranslator() { }

        private static GetWebBedsDataHomeIndexTranslator _instance;

        public static GetWebBedsDataHomeIndexTranslator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GetWebBedsDataHomeIndexTranslator();

                return _instance;
            }
        }

        public WebBBedsDataDto Translate(WebBedsGetDataQueryResult dto)
        {
            //Implementar el dto.

            if (dto == null) return null;

            var resultData = new WebBBedsDataDto();

            var webBedsHotels = new List<Hotel>();

            if (dto.DataResult != null && dto.DataResult.Count() > 0)
            {
                dto.DataResult.ToList().ForEach(item =>
                {
                    var currentHotel = new Hotel()
                    {

                        Id = item.HotelId,
                        Name = item.HotelName,
                        GeoId = item.HotelGeoId,
                        Rating = item.HotelRating
                    };

                    if (item.WebBedsRates != null && item.WebBedsRates.Count() > 0)
                    {
                        currentHotel.Rates = new List<Rate>();

                        item.WebBedsRates.ForEach(r =>
                        {
                            var rate = new Rate()
                            {
                                BoardType = r.BoardType,
                                RateType = r.RateType,
                                Value = r.Value

                            };

                            currentHotel.Rates.Add(rate);

                        });
                    }


                    webBedsHotels.Add(currentHotel);

                });

                resultData.HotelDto = webBedsHotels;
            }


            //Obtenemos los datos de los errores
            if (dto.ErrorMessage != null && dto.ErrorMessage.Count() > 0)
                resultData.Errors = dto.ErrorMessage.Select(e => new Error() { Message = e });


            return resultData;

        }

    }
}

