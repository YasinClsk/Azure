﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptographyController : ControllerBase
    {
        public IActionResult CurrentDateTime()
        {
            var dateTime = DateTime.Now;
            return Ok(dateTime);
        }
    }
}