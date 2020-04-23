using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csi.AuthenticationGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Csi.AuthenticationGateway.Security;

namespace Csi.AuthenticationGateway.Controllers
{

    [Route("reg")]
    [Route("api/reg")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly EventId CONSTRUCTION_EVENT = new EventId(1000, "asd");

        private readonly ILogger<RegistrationController> logger;

        private TextHasher textHasher;



        public RegistrationController(ILogger<RegistrationController> logger)
        {
            this.logger = logger;
            this.logger.LogDebug(CONSTRUCTION_EVENT, null);

            this.textHasher = new TextHasher(logger);
        }


        // POST api/values
        [HttpPost]
        public IHttpActionResult Post([FromBody] CredentialRegistration value)
        {
            this.logger.LogDebug("In post for CredentialRegistration");

            if (value.HasEmptyFields())
            {
                return BadRequest(value);
            }

            byte[] hashBytes = textHasher.Hash(8, value.Password);
        

            

        }

        // POST api/values
        // [HttpPost]
        // public void Post([FromBody] IXCredential value)
        // {
        //     this.logger.LogDebug("In post for IXCredential value");
        // }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            return new string[] { "reg", "con" };
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
