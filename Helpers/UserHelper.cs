using System;
using MetaTesina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MetaTesina.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;

namespace MetaTesina.Helpers
{
    public class UserHelper
    {
        /*private readonly HttpContext _context;
        private readonly UserManager<ApplicationUser> _userManager;*/
        
        /*public UserHelper(UserManager<ApplicationUser> userManager, HttpContext context) 
        {
            _context = context;
            _userManager = userManager;
        }*/

        public static async Task<string> GetUserId(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.GetUserAsync(context.User);
            return user.Id;
        }
            
        public static async Task<string> GetUserFirstNameAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.GetUserAsync(context.User);
            return user.ApplicationUserFirstName;
        }

        public static async Task<string> GetUserRoleId(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.GetUserAsync(context.User);
            var userRoles = user.Roles;
            string userRoleId = null;

            foreach (var role in userRoles)
            {
                userRoleId = role.RoleId;
            }
            return userRoleId;
        }

        public static async Task<string> GetUserRoleName(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.GetUserAsync(context.User);
            var userRoles = await userManager.GetRolesAsync(user);
            string userRoleName = null;

            foreach (var role in userRoles)
            {
                userRoleName = role.ToString();
            }
            return userRoleName;
        }

        public static List<KeyValuePair<string, int>> GetRolesLevelsList() 
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }

        public static int GetUserLevel(string role) 
        {
            var list = new List<string>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) 
            {
                if (e.ToString().Equals(role))
                {
                    return (int)e;
                }
            }
            return -1;
        }
        
        public static List<string> GetRolesByMinLevel(int level) 
        {
            var list = new List<string>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) 
            {
                if ((int)e >= level)
                {
                    list.Add((e.ToString()));
                }
            }
            return list;
        }

        public static List<string> GetRolesByMaxLevel(int level) 
        {
            var list = new List<string>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) 
            {
                if ((int)e <= level)
                {
                    list.Add((e.ToString()));
                }
            }
            return list;
        }

        public static List<string> GetRolesIdByMinLevel(int level, ApplicationDbContext context) 
        {
            
            var exclusionList = new List<string>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) 
            {
                if ((int)e <= level)
                {
                    exclusionList.Add((e.ToString()));
                }
            }
            
            var roleList = new List<string>();
            foreach (var role in context.Roles)
                {
                    if (!exclusionList.Contains(role.Name))
                        roleList.Add(role.Id);
                }

            return roleList;
        }

        public static List<string> GetRolesIdByMaxLevel(int level, ApplicationDbContext context) 
        {
            var exclusionList = new List<string>();
            foreach (var e in Enum.GetValues(typeof(UsrRoles))) 
            {
                if ((int)e >= level)
                {
                    exclusionList.Add((e.ToString()));
                }
            }
            
            var roleList = new List<string>();
            foreach (var role in context.Roles)
                {
                    if (!exclusionList.Contains(role.Name))
                        roleList.Add(role.Id);
                }
                
            return roleList;
        }

        private enum UsrRoles
        {
            SuperAdmin = 150,
            Admin = 100,
            Moderator = 50,
            Writer = 25,
            User = 10,
        }
    }
}