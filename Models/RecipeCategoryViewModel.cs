using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient_inator.Models
{
    public class RecipeCategoryViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }
    }
}
