using System;
using System.Collections.Generic;
using Csi.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Csi.LookMeUp.Api
{
    public class Credentials
    {
        public string Username {get;set;}
        public string Password {get;set;}
    }

    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        JwtAuthenticator auth = null;

        public JwtController()
        {
            auth = new JwtAuthenticator();
        }

        // // GET api/values
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2", auth.GetToken("asd", "asd"),
        //     Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("TODO:ToReplaceLater123"))
        //      };
        // }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
            
            
        //     return "value";
        // }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] Credentials value)
        {
            // Request.CreateErrorResponse(HttpStatusCode.BadRequest, myCustomError);

            
            //System.Net.Http.HttpResponseMessage message = Response.cre

            BadRequestResult r = BadRequest();
            //r.StatusCode = 

            return "ASA OK";
        }

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