using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebBeds.Application.Dto;
using WebBeds.Application.Models.Queries.WebBedsQuery;
using WebBeds.WebApiRepository.Base;
using WebBeds.WebApiRepository.Repositories;

namespace WebBeds.UnitTest
{
    [TestClass]
    public class UnitWebBeds
    {
        [TestMethod]
        public void GetWebBedsData()
        {
            try
            {
                var requestWebApiRepository = new Request();
                var webBedsApiRepository = new WebBedsWebApiRepository(requestWebApiRepository);


                var query = new WebBedsGetDataQueryHandler(webBedsApiRepository);
                var queryDto = new WebBedsGetDataQuery()
                {
                    DestinationId = 279,
                    NumNights = 2
                };

                var result = query.ExecuteQuery(queryDto);

                Assert.IsNotNull(result);
            }
            catch (System.Exception)
            {

                Assert.Fail("Error in method ExecuteQuery of the WebBedsGetDataQueryHandler class");
            }

        }
    }
}
