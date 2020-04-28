using System;
using System.Web.Http;
using Umbraco.Core.Logging;
using Umbraco.Web.WebApi;

namespace LondonMeetup.Demo.V8Way.CodeSamples.Controllers
{
    public class TestAPIController : UmbracoApiController
    {
         [HttpPost]
        public string SayHello()
        {
            var random = new Random();
            var newRandom = random.Next();
            //this is stored as a message template with property new random, so searchable
            Logger.Info<TestAPIController>("We are saying hello to {newRandom}", newRandom);

            // message template stored with the value of newrandom as a part of message template, not useful 
            Logger.Info<TestAPIController>($"We are saying hello to {newRandom}");
            Logger.Info<TestAPIController>($"I am saying hello!");
           
            return "Hello there";
        }
    }
}
