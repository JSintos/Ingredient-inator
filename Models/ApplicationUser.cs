using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Ingredient_inator.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        // [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        // [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
    }
}