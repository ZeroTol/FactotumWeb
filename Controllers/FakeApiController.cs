using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactotumModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactotumWeb.Controllers
{
    /// <summary>
    /// This one is used just for testing purposes, should be removed before release.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FakeApiController : ControllerBase
    {
        // GET: api/Libraries/GetAllAuthor  
        [HttpGet]
        [Route("GetAllAuthor")]
        [EnableCors("AllowOrigin")]
        public IActionResult GetAllAuthor()
        {
            return Ok(true);
        }

        [HttpPost]
        [Route("AllAuthor")]
        public IActionResult AllAuthor([FromBody] string fact_id)
        {
            return Ok(fact_id);
        }

        [HttpPost]
        [Route("LikeFact")]
        public IActionResult LikeFact([FromBody] string fact_id)
        {
            return Ok(fact_id);
        }

        [HttpPost]
        [Route("DislikeFact")]
        public IActionResult DislikeFact([FromBody] string fact_id)
        {
            return Ok(fact_id);
        }

        [HttpPost]
        [Route("AppendFact")]
        public IActionResult AppendFact([FromBody] MDashBoardFact fact)
        {
            //Core.Instance.MessageService.GetDashBoardFacts(order).ToArray();
            return Ok(fact);
        }
    }
}