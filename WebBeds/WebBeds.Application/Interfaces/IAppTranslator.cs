using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.Application.Interfaces
{
    public interface IAppTranslator<Tin, Tout>
    {
        Tout Translate(Tin dto);
    }
}
