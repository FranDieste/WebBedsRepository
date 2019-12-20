using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Application.Dto
{
    public class WebBedsGetDataQueryResult
    {
        public IEnumerable<WebBedsGetHotelDto> DataResult { get; set; }

        public IEnumerable<string> ErrorMessage { get; set; }

    }
}
