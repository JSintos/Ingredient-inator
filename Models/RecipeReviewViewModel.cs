using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Ingredient_inator.Models
{
    public class RecipeReviewViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Review> Reviews { get; set; }
        public int RecipeId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}
