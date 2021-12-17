using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.DTOs;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.DTOs;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChamealeonApp.Controllers
{
    //Authors: Amir and Burhan (Implemented update a user)
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        private readonly DataContext _context;
        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            TokenService tokenService,
            DataContext context
            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _tokenService = tokenService;

            _context = context;
        }

        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            //TODO: check if inputs are valid, ex ints are ints
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Age = registerDto.Age,
                Gender = registerDto.Gender.Trim(),
                Diet = registerDto.Diet.Trim(),
                Weight = registerDto.Weight,
                Height = registerDto.Height,
                PersonalNutritionalInformationGoal = registerDto.PersonalNutritionalInformationGoal
            };
            // user.PersonalNutritionalInformationGoal.Calories *= 7;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                return Ok(result);

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
            {
                var token = _tokenService.CreateToken(user);
                return Ok(token);
            }

            return Unauthorized("Not a good password");
        }


        //Author: Burhan
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDetails([FromBody] UserInformationDTO userModel)
        {
            try
            {
                //find the user
                var user = await _userManager.Users.Include(u => u.PersonalNutritionalInformationGoal).FirstOrDefaultAsync(us => us.NormalizedEmail
                    .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //only change properties if they have been changed by the user
                user.Age = int.Equals(userModel.Age, null) ? user.Age : userModel.Age;
                user.Diet = string.IsNullOrEmpty(userModel.Diet) ? user.Diet : userModel.Diet;
                user.Weight = double.Equals(userModel.Weight, null) ? user.Weight : userModel.Weight;
                user.Height = double.Equals(userModel.Height, null) ? user.Height : userModel.Height;
                user.PersonalNutritionalInformationGoal.Calories = double.Equals(userModel.Calories, null) ? user.PersonalNutritionalInformationGoal.Calories : userModel.Calories;

                await _userManager.UpdateAsync(user);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest(new ErrorDTO { Title = "An error has occured updating the user's details." });
            }
        }

        //delete user
        //mike
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            //TODO: delete associated meal plan

            //await _context.SaveChangesAsync();
            return Ok();
        }

        //Amir
        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("userDetails")]
        public async Task<IActionResult> GetUserDetailsAsyncAsync()
        {
            var user = await _userManager.Users.Include(u => u.PersonalNutritionalInformationGoal).FirstOrDefaultAsync(us => us.NormalizedEmail
                    .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            return Ok(new
            {
                Name = user.UserName,
                Diet = user.Diet,
                Height = user.Height,
                Weight = user.Weight,
                Calories = user.PersonalNutritionalInformationGoal.Calories,
                Age = user.Age
            });
        }

    }
}