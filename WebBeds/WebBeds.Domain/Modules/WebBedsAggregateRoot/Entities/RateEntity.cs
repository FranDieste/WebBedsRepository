using System;
using System.Collections.Generic;
using System.Text;
using WebBeds.Domain.Base.Interfaces;

namespace WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities
{
    public class RateEntity : IEntity
    {
        public int Id { get; set; }
        public int HotelAggregateId { get; set; }
        public string RateType { get; set; }
        public string BoardType { get; set; }
        public double Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string UserCode { get; set; }
    }
}
