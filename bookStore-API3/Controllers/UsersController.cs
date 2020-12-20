using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore_API3.Contracts;
using bookStore_API3.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookStore_API3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;

        public UsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
             ILoggerService logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// User login Endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {

            try
            {
                var location = GetControllerActionNames();
                var username = userDTO.Username;
                var password = userDTO.Password;
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

                _logger.LogInfo($"{location}: Login Attempt from  user: {username} ");
                if (result.Succeeded)
                {
                    _logger.LogInfo($"{location}: {username} Sucessfully Authenticated");
                    var user = await _userManager.FindByNameAsync(username);
                    var tokenString = await GenerateJSONWebToken(user);
                    return Ok(user);
                }
                _logger.LogInfo($"{location}: {username} Not Authenticated");
                return Unauthorized(userDTO);
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            return "";
        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "something went wrong. Please contact the Administrator");
        }
    }
}
