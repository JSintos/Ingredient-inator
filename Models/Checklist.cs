using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Ingredient_inator.Models
{
    public class Checklist
    {
        [Key]
        public int ChecklistId { get; set; }
        public int Owner { get; set; }
        public int RecipeId { get; set; }
    }
}