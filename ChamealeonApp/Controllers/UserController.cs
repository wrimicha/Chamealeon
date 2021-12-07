using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Models.DTOs;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TokenService tokenService
            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _tokenService = tokenService;
        }

        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }


        // login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("email doesn't exist");



            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
                return Ok(new UserDTO
                {
                    Token = _tokenService.CreateToken(user)
                });

            return Unauthorized("Not a good password");
        }

        //update user details
        //burhan
        [HttpPost]
        public async Task<IActionResult> UpdateDetails([FromBody] User userModel)
        {

            var user = await _userManager.FindByIdAsync(userModel.Id);
            // TODO: find what properties need to be updated
            // Let's assume email only changed
            user.Email = userModel.Email;
            await _userManager.UpdateAsync(user);
            return Created("", null);
        }

        //delete user
        //mike
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return Ok();
        }
    }
}