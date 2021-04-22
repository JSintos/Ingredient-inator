using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient_inator.Models
{
    public class RecipeCreateViewModel
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public string ServingSize { get; set; }
        public string PortionList { get; set; }
        public string IngredientList { get; set; }
        public string Steps { get; set; }
        public string PhotoLink { get; set; }
        public string VideoLink { get; set; }
        public List<Category> Categories { get; set; }

    }
}
