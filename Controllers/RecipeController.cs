using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Ingredient_inator.Data;
using Ingredient_inator.Models;

namespace Ingredient_inator.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var list = _context.Recipes.ToList();

            return View(list);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Recipe Recipe)
        {
            var NewRecipe = new Recipe()
            {
                Author = _userManager.GetUserId(User),
                Name = Recipe.Name,
                Category = Recipe.Category,
                DateCreated = DateTime.Now,
                ServingSize = Recipe.ServingSize,
                PortionList = Recipe.PortionList,
                IngredientList = Recipe.IngredientList,
                Steps = Recipe.Steps,
                PhotoLink = Recipe.PhotoLink,
                VideoLink = Recipe.VideoLink
            };

            _context.Recipes.Add(NewRecipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundRecipe = _context.Recipes.Where(R => R.RecipeId == Id).SingleOrDefault();
            if (FoundRecipe == null)
            {
                return RedirectToAction("Index");
            }

            return View(FoundRecipe);
        }

        [HttpPost]
        public IActionResult Edit(int? RecipeId, Recipe Recipe)
        {
            var FoundRecipe = _context.Recipes.Where(R => R.RecipeId == RecipeId).SingleOrDefault();
            FoundRecipe.Author = Recipe.Author;
            FoundRecipe.Name = Recipe.Name;
            FoundRecipe.Category = Recipe.Category;
            FoundRecipe.DateCreated = DateTime.Now;
            FoundRecipe.ServingSize = Recipe.ServingSize;
            FoundRecipe.PortionList = Recipe.PortionList;
            //FoundRecipe.PortionList = Recipe.PortionList.Replace(",", "<br />");
            FoundRecipe.IngredientList = Recipe.IngredientList;
            FoundRecipe.Steps = Recipe.Steps;
            FoundRecipe.PhotoLink = Recipe.PhotoLink;
            FoundRecipe.VideoLink = Recipe.VideoLink;

            _context.Recipes.Update(FoundRecipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundRecipe = _context.Recipes.Where(R => R.RecipeId == Id).SingleOrDefault();
            if (FoundRecipe == null)
            {
                return RedirectToAction("Index");
            }

            _context.Recipes.Remove(FoundRecipe);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ViewRecipe(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundRecipe = _context.Recipes.Where(R => R.RecipeId == Id).SingleOrDefault();
            if (FoundRecipe == null)
            {
                return RedirectToAction("Index");
            }

            RecipeReviewViewModel RRVM = new RecipeReviewViewModel();

            RRVM.Recipe = FoundRecipe;
            
            var FoundReviews = _context.Reviews.Where(R => R.RecipeId == Id).ToList();
            RRVM.Reviews = FoundReviews;

            ViewBag.UserId = _userManager.GetUserId(User);

            // portion list
            //Recipe.PortionList.Replace(",", "<br/>");
            //FoundRecipe.PortionList = Recipe.PortionList.Replace(",", "<br/>");
            //_context.Recipes.Update(FoundRecipe);
            //_context.SaveChanges();
            //string[] list = Recipe.PortionList.Replace(",", "<br />");

            // remove later
            //FoundRecipe.PortionList = Recipe.PortionList.Replace(",", "<br />");
            //_context.Recipes.Update(FoundRecipe);
            //_context.SaveChanges();

            return View(RRVM);
        }
    }
}
