using System;
using System.Collections.Generic;
using System.Text;

namespace WebBeds.CrossCutting.Interfaces
{
    public  interface ICache
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? timeSpan = null);
        bool Exists(string key);
    }
}
