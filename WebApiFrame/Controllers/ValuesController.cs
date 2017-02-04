using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiFrame.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment e)
        {
            this._logger = logger;
            this._logger.LogInformation("ValuesController ===========");
            this._logger.LogDebug($"debug: ApplicationName: {e.ApplicationName}  e.WebRootPath: {e.WebRootPath}");
            this._logger.LogInformation($"info: ApplicationName: {e.ApplicationName}  e.WebRootPath: {e.WebRootPath}");
            this._logger.LogError($"error: ApplicationName: {e.ApplicationName}  e.WebRootPath: {e.WebRootPath}");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
