using AppliAuth.Models;
using AppliAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AppliAuth.Models.AuthModels;

namespace AppliAuth.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {

        private readonly UserService UserService;

        public AuthController(UserService userService)
        {
            UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                ApiResult Result = UserService.Login(model);

                if(!Result.success)
                {
                    return Unauthorized(Result);
                }

                return Ok(UserService.Login(model));
            }
            catch(Exception e)
            {
                return BadRequest(new ApiResult { success = false, message = e.Message });
            }

          
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                UserService.Register(model);
                return Ok(new ApiResult { success = true });
            }
            catch(Exception e)
            {
                return BadRequest(new ApiResult { success = false, message = e.Message });
            }

           
        }

    }
}
