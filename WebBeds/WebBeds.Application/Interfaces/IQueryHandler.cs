using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Application.Interfaces
{
    public interface IQueryHandler<Tin, Tout>
    {
        Tout ExecuteQuery(Tin dto);
    }
}
