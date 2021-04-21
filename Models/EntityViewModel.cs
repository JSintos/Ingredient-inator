using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredient_inator.Models
{
    public class EntityViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
