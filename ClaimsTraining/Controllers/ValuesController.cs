using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsTraining.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult Admin()
        {
            return Ok(new { LinkText = "Admin" });
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("User")]
        public IActionResult CUser()
        {
            return Ok(new { LinkText = "Users" });
        }
    }
}