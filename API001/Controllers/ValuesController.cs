using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API001.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ValuesController> _logger; 
        public ValuesController(IConfiguration configuration,ILogger<ValuesController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var message = new
            {
                currentDateTime =DateTime.Now,
                level = "info",
                hostName = Environment.MachineName,
                messageInfo="hello world"
            };
            _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(message));
        }
        [HttpGet]
        public string Get()
        {
            var desc = _configuration["desc"];
            return $"API001:{DateTime.Now.ToString()}  { Environment.MachineName + " OS:" + Environment.OSVersion.VersionString} DESC: { desc }";
        }
    }
}
