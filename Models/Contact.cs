using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient_inator.Models
{
    public class Contact
    {
        [Key]
        public int MessageId { get; set; }

        [Display(Name = "Sender")]
        [Required(ErrorMessage = "Please enter your name.")]
        public string SenderName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid format.")]
        [Required(ErrorMessage ="Please enter your email address.")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please write a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
