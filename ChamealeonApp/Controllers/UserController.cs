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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return Ok(new UserDTO
                {
                    Token = _tokenService.CreateToken(user)
                });

            return Unauthorized("Not a good password");
        }

        //update user details
        //burhan
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDetails([FromBody] UserInformationDTO userModel)
        {
            //find the user
            var user = await _userManager.Users.Include(u => u.PersonalNutritionalInformationGoal).FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //Reference:
            // TODO: find what properties need to be updated
            user.Age = int.Equals(userModel.Age, null) ? user.Age : userModel.Age;
            user.Diet = string.IsNullOrEmpty(userModel.Diet) ? user.Diet : userModel.Diet;
            user.Weight = double.Equals(userModel.Weight, null) ? user.Weight : userModel.Weight;
            user.Height = double.Equals(userModel.Height, null) ? user.Height : userModel.Height;

            user.PersonalNutritionalInformationGoal.Calories = double.Equals(userModel.Calories, null) ? user.PersonalNutritionalInformationGoal.Calories : userModel.Calories;
            // user.PersonalNutritionalInformationGoal.Fat = double.Equals(userModel.Fat, null) ? user.PersonalNutritionalInformationGoal.Fat : userModel.Fat;
            // user.PersonalNutritionalInformationGoal.Protein = double.Equals(userModel.Protein, null) ? user.PersonalNutritionalInformationGoal.Protein : userModel.Protein;
            // user.PersonalNutritionalInformationGoal.Carbs = double.Equals(userModel.Carbs, null) ? user.PersonalNutritionalInformationGoal.Carbs : userModel.Carbs;
            // user.PersonalNutritionalInformationGoal.Sodium = double.Equals(userModel.Sodium, null) ? user.PersonalNutritionalInformationGoal.Sodium : userModel.Sodium;
            // user.PersonalNutritionalInformationGoal.Sugar = double.Equals(userModel.Sugar, null) ? user.PersonalNutritionalInformationGoal.Sugar : userModel.Sugar;


            await _userManager.UpdateAsync(user);

            //TODO: not sure if it reflects the change
            await _context.SaveChangesAsync();
            return Created("", null);
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
    }
}