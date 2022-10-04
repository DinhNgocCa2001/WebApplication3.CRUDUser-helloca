using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.CRUDUser.Constants;
using WebApplication.CRUDUser.Controllers;
using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Filters;
using WebApplication.CRUDUser.Services;
//gặp lỗi TypeError: Failed to execute 'fetch' on 'Window': Request with GET/HEAD method cannot have body.
//thì xem lại mấy cái [FromQuery]
namespace WebApplication3.CRUDUser.Controllers
{

    //[Authorize]
    //[AuthorizationFilter(UserTypes.Admin)]
    //để có thể thực hiện thao tác thì cần : Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ey
    //JzdWIiOiI3IiwibmFtZSI6ImRpbmhuZ29jY2EiLCJ1c2VyX3R5cGUiOiIxIiwiZXhwIjoxNjYzOTcxNjY4LCJpc3MiOiJkZW1vLmlzc3Vl
    //ciIsImF1ZCI6ImRlbW8uYXVkaWVuY2UifQ.kc5UUO89KziC7X8Tu0zSjw9zywW-m-CogTazY6C9RKc
    //eyJ... chính là token được sinh ra khi login tài khoản
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerServices _CustomerServices;
        private readonly IUserServices _UserServices;

        //public CustomerController(ICustomerServices CustomerServices)
        //{
        //    _CustomerServices = CustomerServices;
        //}

        public CustomerController(
            ICustomerServices CustomerServices,
            IUserServices UserServices,
            ILogger<CustomerController> logger) : base(logger)
        {
            _CustomerServices = CustomerServices;
            _UserServices = UserServices;
        }

        [HttpGet("get-all-pagesize")]
        public IActionResult GetAll([FromQuery] CustomerFilterDto input)
        {
            try
            {
                return Ok(_CustomerServices.GetAllCustomer(input));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("Get by Id")]
        public IActionResult GetByIdCustomer([FromQuery] int id)
        {
            try
            {
                return Ok(_CustomerServices.GetByIdCustomer(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("Post Customer")]
        public IActionResult CreateSudent([FromForm] CreateCustomerDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                _CustomerServices.CreateCustomer(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("Update Customer")]
        public IActionResult UpdateByIdCustomer(int id, [FromForm] UpdateCustomerDto Customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                return Ok(_CustomerServices.UpdateByIdCustomer(id, Customer));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("Delete Customer")]
        public IActionResult DeleteByIdCustomer([FromForm] int id)
        {
            try
            {
                _CustomerServices.DeleteByIdCustomer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("Login User")]
        public IActionResult Login(LoginDto input)
        {
            try
            {
                string token = _UserServices.Login(input);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        //ListUserOfCustomer
        [HttpGet("List User Of Customer")]
        public IActionResult ListUserOfCustomer(int id)
        {
            try
            {
                return Ok(_CustomerServices.ListUserOfCustomer(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
