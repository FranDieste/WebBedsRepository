using System;
using System.Collections.Generic;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;

namespace WebBeds.Application.Dto
{
    public class WebBedsGetDataRequestDto
    {
        public IEnumerable<HotelAggregate> HotelList { get; set; }

        public IEnumerable<Exception> Exceptions { get; set; }
    }
}
