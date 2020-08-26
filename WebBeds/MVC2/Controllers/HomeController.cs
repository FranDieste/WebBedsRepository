using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.Unity;
using WebBeds.Application.Dto;
using WebBeds.Application.Interfaces;
using WebBeds.CrossCutting.IocRegistration;
using WebBeds.UI.Models.Dto;
using WebBeds.UI.Translator;




namespace WebBeds.Controllers
{
    public class HomeController : Controller
    {
        //____________________________________________________________________
        //This action realice the following tasks:
        //  -Get params from our view page
        //  -Get an information and return this information to a our page 
        //____________________________________________________________________
        public ActionResult Index()
        {

            var result = new WebBBedsDataDto();

            if (Request.HasFormContentType && Request.Form != null && Request.Form.Count > 0)
            {

                var destinationId = Request.Form["DestinationId"];
                var numNights = Request.Form["NumNights"];
                if (!String.IsNullOrEmpty(destinationId))
                    result = GetWebBedsData(destinationId, numNights);
            }

            //Pasar result a dto de la applicacion
            return View(result);
        }


        private int ConvertNumeric(string strNumber)
        {
            int number;
            var isNumeric = int.TryParse(strNumber, out number);

            if (isNumeric)
                return number;
            else
                return 0;
        }

        //___________________________________________________________________________
        //This function call the method that return an information
        //We use an "Unity" from create instances of classes.
        //Params:
        //   - IN:WebBedsGetDataQuery
        //   -OUT:WebBBedsDataDto 
        //___________________________________________________________________________
        private WebBBedsDataDto GetWebBedsData(string destinationId, string numNights)
        {
            var myApplication = MyUnityContainer().Resolve<IQueryHandler<WebBedsGetDataQuery, WebBedsGetDataQueryResult>>();
            var queryDto = new WebBedsGetDataQuery();
            queryDto.DestinationId = ConvertNumeric(destinationId);
            queryDto.NumNights = ConvertNumeric(numNights);

            var resultData = myApplication.ExecuteQuery(queryDto);

            WebBBedsDataDto resultDto;

            resultDto = GetWebBedsDataHomeIndexTranslator.Instance.Translate(resultData);

            return resultDto;

        }

        //____________________________
        //Get UnityContainer
        //____________________________
        private UnityContainer MyUnityContainer()
        {
             return UnityRegistration.UnityRegistrationInstance.GetUnityContainer();

            
        }
    }
}
