using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Application.Dto
{
    public class WebBedsGetHotelDto
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int HotelGeoId { get; set; }
        public int HotelRating { get; set; }
        public List<WebBedsRateDto> WebBedsRates { get; set; }
    }

    public class WebBedsRateDto
    {
        public string RateType { get; set; }
        public string BoardType { get; set; }
        public double Value { get; set; }
    }
}
