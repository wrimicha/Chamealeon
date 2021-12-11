using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.DTOs;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Helpers;
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
    public class SwapMealController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public SwapMealController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }


        


    }
}