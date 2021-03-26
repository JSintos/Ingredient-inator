using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Ingredient_inator.Data;
using Ingredient_inator.Models;

namespace Ingredient_inator.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext Context;

        public CategoryController(ApplicationDbContext Context)
        {
            this.Context = Context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var Categories = Context.Categories.ToList();

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
                Name = Category.Name
            };

            Context.Categories.Add(NewCategory);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundCategory = Context.Categories.Where(C => C.CategoryId == Id).SingleOrDefault();
            if (FoundCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(FoundCategory);
        }

        [HttpPost]
        public IActionResult Update(int? CategoryId, Category Category)
        {
            var FoundCategory = Context.Categories.Where(C => C.CategoryId == CategoryId).SingleOrDefault();
            FoundCategory.Name = Category.Name;

            Context.Categories.Update(FoundCategory);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var FoundCategory = Context.Categories.Where(C => C.CategoryId == Id).SingleOrDefault();
            if (FoundCategory == null)
            {
                return RedirectToAction("Index");
            }

            Context.Categories.Remove(FoundCategory);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}