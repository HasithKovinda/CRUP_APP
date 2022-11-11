using BuilkyBookWeb.Data;
using BuilkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuilkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.name == obj.displayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("index");
            }

            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _db.Categories.Find(id);
            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.name == obj.displayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("index");
            }

            return View(obj);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _db.Categories.Find(id);
            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("index");

        }
    }
}
