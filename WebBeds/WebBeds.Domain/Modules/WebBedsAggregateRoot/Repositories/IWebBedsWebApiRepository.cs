using System;
using System.Collections.Generic;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;

namespace WebBeds.Domain.Modules.WebBedsAggregateRoot.Repositories
{
    public interface IWebBedsWebApiRepository
    {
        IEnumerable<HotelAggregate> GetData(int destinationId,int numNights);
    }
}
