using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Domain.Base.Interfaces
{
    public interface IAggregateRoot : IEntity
    {
        int Version { get; set; }
    }
}
