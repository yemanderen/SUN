using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Webapi.Controllers
{
    public class PublishController : Controller
    {
        private readonly ICapPublisher _capBus;

        public PublishController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }

        [Route("~/send")]
        public IActionResult SendMessage()
        {
            var header = new Dictionary<string, string>()
            {
                ["my.header.first"] = "first",
                ["my.header.second"] = "second"
            };
            //capBus.Publish("test.show.time", DateTime.Now);
            _capBus.Publish("test.show.time", DateTime.Now, header);

            return Ok();
        }
    }
}
