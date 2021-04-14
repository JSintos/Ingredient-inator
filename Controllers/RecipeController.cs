using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ingredient_inator.Data;
using Ingredient_inator.Models;

namespace Ingredient_inator.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Recipes.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Recipe record)
        {
            var recipe = new Recipe()
            {
                Author = record.Author,
                Category = record.Category,
                DateCreated = DateTime.Now,
                ServingSize = record.ServingSize,
                PortionList = record.PortionList,
                IngredientList = record.IngredientList,
                Steps = record.Steps,
                PhotoLink = record.PhotoLink,
                VideoLink = record.VideoLink
            };

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var recipe = _context.Recipes.Where(i => i.RecipeId == id).SingleOrDefault();
            if (recipe == null)
            {
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Recipe record)
        {
            var recipe = _context.Recipes.Where(i => i.RecipeId == id).SingleOrDefault();
            recipe.Author = record.Author;
            recipe.Category = record.Category;
            recipe.DateCreated = DateTime.Now;
            recipe.ServingSize = record.ServingSize;
            recipe.PortionList = record.PortionList;
            recipe.IngredientList = record.IngredientList;
            recipe.Steps = record.Steps;
            recipe.PhotoLink = record.PhotoLink;
            recipe.VideoLink = record.VideoLink;

            _context.Recipes.Update(recipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var recipe = _context.Recipes.Where(i => i.RecipeId == id).SingleOrDefault();
            if (recipe == null)
            {
                return RedirectToAction("Index");
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
