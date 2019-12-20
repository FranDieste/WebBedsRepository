using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Domain.Base.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        DateTime ModificationDate { get; set; }
        string UserCode { get; set; }
    }
}
