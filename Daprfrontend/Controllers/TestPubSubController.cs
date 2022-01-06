﻿using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Daprfrontend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestPubSubController : ControllerBase
    {
        private readonly ILogger<TestPubSubController> _logger;
        private readonly DaprClient _daprClient;
        public TestPubSubController(ILogger<TestPubSubController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpPost]
        public ActionResult Post()
        {
            Stream stream = Request.Body;
            byte[] buffer = new byte[Request.ContentLength.Value];
            stream.Position = 0L;
            stream.ReadAsync(buffer, 0, buffer.Length);
            string content = Encoding.UTF8.GetString(buffer);
            return Ok(content);
        }

        [HttpGet("pub")]
        public async Task<ActionResult> PubAsync()
        {
            var data = new WeatherForecast();
            await _daprClient.PublishEventAsync<WeatherForecast>("pubsub", "test_topic", data);
            return Ok();
        }
    }
}
