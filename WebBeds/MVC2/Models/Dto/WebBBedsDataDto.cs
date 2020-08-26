using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBeds.UI.Models.Dto
{
    public class WebBBedsDataDto
    {
        public IEnumerable<Hotel> HotelDto { get; set; }

        public IEnumerable<Error> Errors { get; set; }
    }

    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GeoId { get; set; }
        public int Rating { get; set; }
        public List<Rate> Rates { get; set; }

    }
    public class Rate
    {
        public string RateType { get; set; }
        public string BoardType { get; set; }
        public double Value { get; set; }

    }

    public class Error
    {
        public string Message { get; set; }
    }
}
