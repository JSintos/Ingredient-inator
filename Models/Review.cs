using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Ingredient_inator.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int RecipeId { get; set; }
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You need to input a review")]
        public string Content { get; set; }
        [Required(ErrorMessage = "You need to set a rating")]
        public int Rating { get; set; }

    }
}
