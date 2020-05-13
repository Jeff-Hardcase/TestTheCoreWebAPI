using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TestTheCoreWebAPI.Models;
using Microsoft.Extensions.Options;

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
        [Route("demo")]
        public string GetValues()
        {
            var globals = __settings.Value.GlobalSettings;
            var locals = __settings.Value.LocalSettings;

            var displayResult = "ServiceName = {0} \nGetCodes = {1} \nServiceBaseURL = {2} \nTraceLogging = {3}";
            var result = string.Format(displayResult, globals.ServiceName, globals.GetCodes, locals.ServiceBaseURL, locals.LoggingConfig.TraceLogging);

            return result;
        }
    }
}