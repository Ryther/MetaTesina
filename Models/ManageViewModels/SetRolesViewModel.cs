using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MetaTesina.Models.ManageViewModels
{
    public class SetRolesViewModel
    {
        [Display(Name="Nome Utente")]
        public string Id { get; set; }
        public string UserName { get; set; }

        [Display(Name="Ruolo")] 
        public string RoleName { get; set; }

        static public async Task<List<SetRolesViewModel>> GetRolesViewModel(UserManager<ApplicationUser> userManager)
        {
            var userList = new List<SetRolesViewModel>();
            var users = userManager.Users.ToList();

            foreach (var user in users)
            {           
                var role = await userManager.GetRolesAsync(user);
                userList.Add(new SetRolesViewModel() {
                    Id = user.Id,
                    UserName = user.UserName,
                    RoleName = role.First()
                });
            }

            return userList;
        }

        static public async Task<List<SetRolesViewModel>> GetRolesViewModel(UserManager<ApplicationUser> userManager, string[] excludedRoles)
        {
            var userList = new List<SetRolesViewModel>();
            var users = userManager.Users.ToList();

            foreach (var user in users)
            {           
                var role = await userManager.GetRolesAsync(user);

                foreach (var testRole in excludedRoles)
                {
                    if (role.First() != testRole)
                    {
                        userList.Add(new SetRolesViewModel() {
                            Id = user.Id,
                            UserName = user.UserName,
                            RoleName = role.First()
                        });
                    }
                }
            }

            return userList;
        }
    }
}