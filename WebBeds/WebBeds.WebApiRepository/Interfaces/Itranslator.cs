using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.WebApiRepository.Interfaces
{
    public interface ITranslator<Tin, Tout>
    {
        Tout Translate(Tin dto,int numNights);
    }
}
