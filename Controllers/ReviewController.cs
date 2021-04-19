﻿using System;
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
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(RecipeReviewViewModel RRVM)
        {
            var NewReview = new Review()
            {
                RecipeId = RRVM.RecipeId,
                Author = _userManager.GetUserId(User),
                DatePosted = DateTime.Now,
                Content = RRVM.Content,
                Rating = RRVM.Rating
            };

            _context.Reviews.Add(NewReview);
            _context.SaveChanges();

            return Redirect("~/Recipe/Index");
        }

        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    var FoundRecipe = _context.Recipes.Where(R => R.RecipeId == Id).SingleOrDefault();
        //    if (FoundRecipe == null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View(FoundRecipe);
        //}

        [HttpPost]
        public IActionResult Edit(int? ReviewId, Review Review)
        {
            var FoundReview = _context.Reviews.Where(R => R.ReviewId == ReviewId).SingleOrDefault();
            FoundReview.Content = Review.Content;
            FoundReview.Rating = Review.Rating;
            
            _context.Reviews.Update(FoundReview);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundReview = _context.Reviews.Where(R => R.ReviewId == Id).SingleOrDefault();
            if (FoundReview == null)
            {
                return RedirectToAction("Index");
            }

            _context.Reviews.Remove(FoundReview);
            _context.SaveChanges();

            return Redirect("~/Recipe/Index");
        }       
    }
}