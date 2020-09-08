using System.Text.Json;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using TestTheCoreWebAPI.Models;
using Microsoft.Extensions.Options;
using TestTheCoreWebAPI.Models.Repos;

namespace TestTheCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IOptions<SettingsModel> __settings { get; set; }

        public TestController(IOptions<SettingsModel> settings)
        {
            __settings = settings;
        }

        [HttpGet]
        public string Get()
        {
            var result = JsonSerializer.Serialize(__settings.Value, new JsonSerializerOptions { WriteIndented = true });

            return result;
        }

        [HttpGet]
        [Route("response")]
        public string GetResponse()
        {
            var wsURL = "https://api.gemini.com/v1/pubticker/btcusd";
            var result = WebServiceRepo.CallService<object>(wsURL, HttpVerb.Get);

            return result.ToString();
        }


        [HttpPost]
        [Route("response")]
        public string PostWithHeader([FromHeader] string AuthToken, [FromBody] LogItem jsonBody, string wsURL)
        {
            NameValueCollection header = null;

            if (!string.IsNullOrEmpty(AuthToken))
            {
                header = new NameValueCollection
                {
                    { "Authorization", AuthToken }
                };
            }

            var result = WebServiceRepo.CallService<object>(wsURL, HttpVerb.Post, jsonBody, header);

            return result.ToString();
        }

        [HttpPost]
        [Route("json")]
        public string PostJSON([FromHeader] string AuthToken, [FromBody] object jsonBody, string wsURL)
        {
            NameValueCollection header = null;

            if (!string.IsNullOrEmpty(AuthToken))
            {
                header = new NameValueCollection
                {
                    { "Authorization", AuthToken }
                };
            }

            var result = WebServiceRepo.SubmitJSON<object>(wsURL, HttpVerb.Post, jsonBody.ToString(), header);

            return result.ToString();
        }

        [HttpGet]
        [Route("demo/mileagePlus/{mileagePlus}")]
        public string GetValues(string mileagePlus)
        {
            var globals = __settings.Value.GlobalSettings;
            var locals = __settings.Value.LocalSettings;

            var displayResult = "ServiceName = {0} \nGetCodes = {1} \nServiceBaseURL = {2} \nTraceLogging = {3}";
            var result = string.Format(displayResult, mileagePlus, globals.GetCodes, locals.ServiceBaseURL, locals.LoggingConfig.TraceLogging);

            return result;
        }

    }
}