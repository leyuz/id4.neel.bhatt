using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [Route ("[controller]")]
    [Authorize]
    public class IdentityController : Controller {
        [HttpGet]
        public IActionResult Get () {
            return new JsonResult (from c in User.Claims select new { c.Type, c.Value });
        }
    }
}