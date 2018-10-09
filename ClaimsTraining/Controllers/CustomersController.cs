using ClaimsTraining.Services;
using ClaimsTraining.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsTraining.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private ICustomerService _ICustomerService;

        public CustomersController(ICustomerService _ICustomerService)
        {
            this._ICustomerService = _ICustomerService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]CustomerViewModel _CustomerParams)
        {
            var _Customer = _ICustomerService.Authenticate(_CustomerParams.UserName, _CustomerParams.UserPass);

            if (_Customer == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(_Customer);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var _Customers = _ICustomerService.GetAll();
            return Ok(_Customers);
        }
    }
}