using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MetaTesina.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int ApplicationUserID { get; set; }

        [Required]
        [Display(Name="Nome Utente")]
        [MaxLength(20)]
        public string ApplicationUserFirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name="Cognome Utente")]
        public string ApplicationUserLastName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name="Nickname Utente")]
        public string ApplicationUserNickname { get; set; }
    }
}
