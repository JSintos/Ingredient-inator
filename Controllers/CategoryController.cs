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
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var Categories = _context.Categories.ToList();

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

            _context.Categories.Remove(FoundCategory);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}