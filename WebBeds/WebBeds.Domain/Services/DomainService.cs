using System;
using System.Collections.Generic;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Entities;

namespace WebBeds.Domain.Services
{
    //____________________________________________________________________________________________________________
    //I use singleton patron for instance a class DomainService.
    //I select this way, because I leave a class more open than I use a "static" access modifier
    //If I use a static access modifier in this class, I can`t Inherit or implemented other classes or interfaces
    //____________________________________________________________________________________________________________
    public class DomainService
    {

        //Singleton Patron
        private DomainService() { }

        private static DomainService _instance;

        public static DomainService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DomainService();

                return _instance;
            }
        }

        public HotelAggregate CreateHotelAggregate(int propertyId,string name, int geoId,int rating)
        {

            var currentHotelAggregate = new HotelAggregate()
            {
                Id =propertyId,
                Name = name,
                GeoId = geoId,
                Rating = rating,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                UserCode = Environment.UserName,
                Version = 1

            };

            return currentHotelAggregate;

        }
    }
}
