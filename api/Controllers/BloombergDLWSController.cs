using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [Route ("[controller]")]
    // [Authorize (Roles = "BloombergAdmin")]
    [Authorize]
    public class BloombergDLWSController : Controller {
        [HttpGet]
        public IActionResult Get () {
            return new JsonResult (new { source = "Bloomberg", ticker = "USDJPY" });

        }
    }
}