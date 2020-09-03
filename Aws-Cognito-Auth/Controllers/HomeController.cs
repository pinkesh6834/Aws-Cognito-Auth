using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aws_Cognito_Auth.Controllers
{
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("api/getdetails")]
        public ActionResult GetDetails()
        {
            return Ok("Response from GetDetails");
        }
    }
}