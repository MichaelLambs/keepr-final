using System;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr_final.Models;
using keepr_final.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_final.Controllers
{
    [Route("[controller]")] // = /auth
    public class AuthController : Controller
    {
        private readonly UserRepository _repo;

        public AuthController(UserRepository repo) // ALLOWS TO USE METHODS IN USERREPOSITORY
        {
            _repo = repo;
        }

        [HttpPost("register")] // = auth/register
        public async Task<UserReturnModel> Register([FromBody] UserCreateModel userData) // ASYNC AND TASK GOES TOGETHER
        {
            if (!ModelState.IsValid) { return null; } // IF NOT A VALID USERCREATEMODEL

            try
            {
                UserReturnModel user = _repo.Register(userData);
                ClaimsPrincipal principal = user.SetClaims();
                // METHOD BELOW LOGS USER INTO APPICATION
                await HttpContext.SignInAsync(principal); // await = .then
                return user;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        [HttpPost("login")] // = auth/login
        public async Task<UserReturnModel> Login([FromBody] UserLoginModel userData)
        {
            if (!ModelState.IsValid) { return null; }

            try
            {
                UserReturnModel user = _repo.Login(userData);
                var principal = user.SetClaims();
                await HttpContext.SignInAsync(principal);
                return user;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        [HttpDelete("logout")] // auth/logout
        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync();
            return "successfully logged out";
        }

        [Authorize]
        [HttpGet("authenticate")] // auth/authenticate
        public UserReturnModel Authenticate()
        {
            var user = HttpContext.User; // LIKE SESSION
            var id = user.Identity.Name;
            return _repo.GetUserById(id);
        }

    }
}