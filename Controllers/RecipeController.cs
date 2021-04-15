﻿using Microsoft.AspNetCore.Mvc;
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
                // _userManager.GetUserId(User),
                Author = Recipe.Author,
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
            FoundRecipe.Category = Recipe.Category;
            FoundRecipe.DateCreated = DateTime.Now;
            FoundRecipe.ServingSize = Recipe.ServingSize;
            FoundRecipe.PortionList = Recipe.PortionList;
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
    }
}