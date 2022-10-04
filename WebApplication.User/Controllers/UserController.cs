using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.CRUDUser.Constants;
using WebApplication.CRUDUser.Controllers;
using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Filters;
using WebApplication.CRUDUser.Services;
//nếu không build được error 500 thì có thể là tên hàm và tên Interface ở Controller không đúng
//hoặc có thể là tên ở cái HttpGet bị trùng giữa 2 controller

namespace WebApplication3.CRUDUser.Controllers
{
    [Authorize]
    [AuthorizationFilter(UserTypes.Admin)]
    public class UserController : ApiControllerBase
    {
        private readonly IUserServices _UserServices;
        public UserController(
            IUserServices UserServices,
            ILogger<UserController> logger) : base(logger)
        {
            _UserServices = UserServices;
        }

        [HttpGet("get-all-pagesize-User")]
        public IActionResult GetAll([FromQuery] CustomerFilterDto input)
        {
            try
            {
                return Ok(_UserServices.GetAllUser(input));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("GetByIdUser")]
        public IActionResult GetByIdUser([FromQuery] int id)
        {
            try
            {
                return Ok(_UserServices.GetByIdUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("Post User")]
        public IActionResult CreateUser([FromForm] CreateUserDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                _UserServices.CreateUser(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("Update User")]
        public IActionResult UpdateByIdUser(int id, [FromForm] UpdateUserDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                return Ok(_UserServices.UpdateByIdUser(id, input));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPut("Update Status For User")]
        public IActionResult UpdateByIdUserStatus(int id, bool Status)
        {
            try
            {
                //de model validation
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                _UserServices.UpdateByIdUserStatus(id, Status);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("Delete User")]
        public IActionResult DeleteByIdUser([FromForm] int id)
        {
            try
            {
                _UserServices.DeleteByIdUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        //[HttpPost("Login User")]
        //public IActionResult Login(LoginDto input)
        //{
        //    try
        //    {
        //        string token = _UserServices.Login(input);
        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        //    }
        //}
    }
}
