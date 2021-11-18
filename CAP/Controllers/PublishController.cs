using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAP.Controllers
{
    public class PublishController : Controller
    {
        private readonly ICapPublisher _capBus;

        public PublishController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }

        //[Route("~/adonet/transaction")]
        //public IActionResult AdonetWithTransaction()
        //{
        //    using (var connection = new MySqlConnection(ConnectionString))
        //    {
        //        using (var transaction = connection.BeginTransaction(_capBus, autoCommit: true))
        //        {
        //            //your business logic code

        //            _capBus.Publish("xxx.services.show.time", DateTime.Now);
        //        }
        //    }

        //    return Ok();
        //}

        //[Route("~/ef/transaction")]
        //public IActionResult EntityFrameworkWithTransaction([FromServices] AppDbContext dbContext)
        //{
        //    using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: true))
        //    {
        //        //your business logic code

        //        _capBus.Publish("xxx.services.show.time", DateTime.Now);
        //    }

        //    return Ok();
        //}

        //[CapSubscribe("xxx.services.show.time")]
        //public void CheckReceivedMessage(DateTime datetime)
        //{
        //    Console.WriteLine(datetime);
        //}

        [Route("~/send")]
        public IActionResult SendMessage([FromServices] ICapPublisher capBus)
        {
            var header = new Dictionary<string, string>()
            {
                ["my.header.first"] = "first",
                ["my.header.second"] = "second"
            };
            //capBus.Publish("test.show.time", DateTime.Now);
            capBus.Publish("test.show.time", DateTime.Now, header);

            return Ok();
        }
    }
}
