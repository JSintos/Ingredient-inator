using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Ingredient_inator.Models
{
    public class CategoryRecipeViewModel
    {
        public Category Category { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
