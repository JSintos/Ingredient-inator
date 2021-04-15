using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Ingredient_inator.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage = "A category is required")]
        public int Category { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Serving Size")]
        [Required(ErrorMessage = "Serving size is required")]
        public string ServingSize { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Portion List")]
        [Required(ErrorMessage = "A list of portions is required")]
        public string PortionList { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Ingredient List")]
        [Required(ErrorMessage = "A list of ingredients is required")]
        public string IngredientList { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "A list of steps is required")]
        public string Steps { get; set; }
        [Display(Name = "Photo link")]
        [Required(ErrorMessage = "A link to a photo is required")]
        public string PhotoLink { get; set; }
        [Display(Name = "Video link")]
        public string? VideoLink { get; set; }
    }
}