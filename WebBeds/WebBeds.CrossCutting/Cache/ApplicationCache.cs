
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using WebBeds.CrossCutting.Interfaces;

namespace WebBeds.CrossCutting.Cache
{
    public class ApplicationCache : ICache
    {

        private static ApplicationCache applicationCache;

        private IMemoryCache _cache;

        private ApplicationCache()
        {
            

        }
        private ApplicationCache(IMemoryCache cache)
        {
            _cache = cache;

        }

        //__________________________________________
        //Singleton patron.Return the same instance
        //__________________________________________
        public static ApplicationCache Cache
        {
            get
            {
                if (applicationCache == null)
                    applicationCache = new ApplicationCache(new MemoryCache(new MemoryCacheOptions()));

                return applicationCache;
            }

        }

        //__________________________________________________________
        //This function validate if a key exist in the memory cache
        //__________________________________________________________
        public bool Exists(string key)
        {

            object existItem;


            if (_cache.TryGetValue(key, out existItem))
                return true;
            else
                return false;
           
        }

        //________________________________
        //returns an item from the cache
        //________________________________
        public T Get<T>(string key)
        {

            object existItem;

             _cache.TryGetValue(key, out existItem);
                       
            return (T)existItem;
        }

        //________________________________
        //Add an item to the memory cache
        //________________________________
        public void Set<T>(string key, T value, TimeSpan? timeSpan = null)
        {
            if (Exists(key))
                _cache.Remove(key);

            _cache.Set(key, value);
                        
        }


        public void AssignListToCache<T>(IEnumerable<T> value)
        {
            var key = typeof(T).Name;


            Set(key, value);
        }

    }
}
