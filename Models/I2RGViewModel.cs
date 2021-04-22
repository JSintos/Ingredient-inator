using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient_inator.Models
{
    public class I2RGViewModel
    {
        // Recipes that have a complete match
        public List<Recipe> FoundRecipes { get; set; }
        // Recipes that have an complete match
        public List<Recipe> IncompleteRecipes { get; set; }
    }
}
