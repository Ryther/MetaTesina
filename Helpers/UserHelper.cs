using System;
using MetaTesina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MetaTesina.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Security.Claims;

namespace MetaTesina.Helpers
{
    public class UserHelper
    {
        private readonly HttpContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserHelper(UserManager<ApplicationUser> userManager, HttpContext context) 
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> GetUserId()
        {
            return _userManager.GetUserId(_context.User);
        }
            
        public async Task<string> GetUserFirstNameAsync()
        {
            var user = await _userManager.GetUserAsync(_context.User);
            return user.ApplicationUserFirstName;
        }
    }
}