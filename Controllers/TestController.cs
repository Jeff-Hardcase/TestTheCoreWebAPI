using System;
using System.Text.Json;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using TestTheCoreWebAPI.Models;
using TestTheCoreWebAPI.Models.Domain;
using TestTheCoreWebAPI.Models.Repos;
using Microsoft.Extensions.Options;


namespace TestTheCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private SettingsModel Settings { get; set; }

        public TestController(IOptions<SettingsModel> settings)
        {
            Settings = settings.Value;
        }

        [HttpGet]
        public string Get()
        {
            var result = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });

            return result;
        }

        [HttpGet]
        [Route("btc")]
        public GeminiAssetData GetBTC()
        {
            var wsURI = new Uri(new Uri(Settings.LocalSettings.GeminiHostURL), Settings.GlobalSettings.GeminiPriceBTC);
            var result = WebServiceRepo.CallService<GeminiAssetData>(wsURI.AbsoluteUri, HttpVerb.Get);

            return result;
        }

        [HttpGet]
        [Route("eth")]
        public GeminiAssetData GetETH()
        {
            var wsURI = new Uri(new Uri(Settings.LocalSettings.GeminiHostURL), Settings.GlobalSettings.GeminiPriceETH);
            var result = WebServiceRepo.CallService<GeminiAssetData>(wsURI.AbsoluteUri, HttpVerb.Get);

            return result;
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
            var globals = Settings.GlobalSettings;
            var locals = Settings.LocalSettings;

            var values = $"MileagePlus = {mileagePlus}\nGetCodes = {globals.GetCodes}\nServiceBaseURL = {locals.ServiceBaseURL}\nTraceLogging = {locals.LoggingConfig.TraceLogging}";

            return values;
        }

    }
}