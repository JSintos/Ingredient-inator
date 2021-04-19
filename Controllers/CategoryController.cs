using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Ingredient_inator.Data;
using Ingredient_inator.Models;

namespace Ingredient_inator.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var Categories = _context.Categories.ToList();

            ViewBag.UserId = _userManager.GetUserId(User);

            return View(Categories);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category Category)
        {
            var NewCategory = new Category()
            {
                Name = Category.Name,
                Author = _userManager.GetUserId(User)
            };

            _context.Categories.Add(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundCategory = _context.Categories.Where(C => C.CategoryId == Id).SingleOrDefault();
            if (FoundCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(FoundCategory);
        }

        [HttpPost]
        public IActionResult Update(int? CategoryId, Category Category)
        {
            var FoundCategory = _context.Categories.Where(C => C.CategoryId == CategoryId).SingleOrDefault();
            FoundCategory.Name = Category.Name;

            _context.Categories.Update(FoundCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundCategory = _context.Categories.Where(C => C.CategoryId == Id).SingleOrDefault();
            if (FoundCategory == null)
            {
                return RedirectToAction("Index");
            }

            var FoundRecipes = _context.Recipes.Where(R => R.Category == Id).ToList();
            foreach (Recipe Recipe in FoundRecipes)
            {
                Recipe.Category = null;
            }
            _context.SaveChanges();

            _context.Categories.Remove(FoundCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ViewCategory(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundCategory = _context.Categories.Where(C => C.CategoryId == Id).SingleOrDefault();
            if (FoundCategory == null)
            {
                return RedirectToAction("Index");
            }

            CategoryRecipeViewModel CRVM = new CategoryRecipeViewModel();

            CRVM.Category = FoundCategory;

            var FoundRecipes = _context.Recipes.Where(R => R.Category == Id).ToList();
            CRVM.Recipes = FoundRecipes;

            ViewBag.UserId = _userManager.GetUserId(User);

            return View(CRVM);
        }
    }
}