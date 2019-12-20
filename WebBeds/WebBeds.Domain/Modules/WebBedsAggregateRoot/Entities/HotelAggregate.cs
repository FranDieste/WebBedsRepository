using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBeds.Domain.Base.Interfaces;

namespace WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities
{
    
    public class HotelAggregate : IAggregateRoot
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int GeoId { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string UserCode { get; set; }
        public int Version { get; set; }

        private List<RateEntity> _rateEntity = new List<RateEntity>();
        public IEnumerable<RateEntity> RatesEntity { get { return _rateEntity; }}

        public void AddRate(string rateType, string boardType,double value)
        {

            var currentElement = RatesEntity.FirstOrDefault(r => r.RateType == rateType && r.BoardType == boardType);
            if (currentElement == null)
            {
                var rate = new RateEntity()
                {

                    Id = _rateEntity.Count() + 1,
                    HotelAggregateId = this.Id,
                    RateType = rateType,
                    BoardType = boardType,
                    Value = value,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    UserCode = Environment.UserName  //Loggin User Code.

                };

                _rateEntity.Add(rate);

            }

        }

    }
}
