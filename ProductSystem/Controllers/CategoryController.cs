using Microsoft.AspNetCore.Mvc;
using ProductSystem.DAL;
using ProductSystem.Models;

namespace ProductSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();

            if (category != null)

            {
                foreach (var category1 in category)
                {
                    var CategoryViewModel = new CategoryViewModel()
                    {
                        CategoryId = category1.CategoryId,
                        CategoryName = category1.CategoryName,
                        CategoryDescription = category1.CategoryDescription,

                    };
                    categoryList.Add(CategoryViewModel);
                }
                return View(categoryList);
            }
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isCategoryExists = _context.Categories
                                           .Any(c => c.CategoryName == categoryData.CategoryName);
                    if (isCategoryExists)
                    {
                        TempData["errorMessage"] = "Category name already exists. Please enter a unique name.";
                        return View(categoryData);
                    }
                    var category = new Category()
                    {
                        CategoryName = categoryData.CategoryName,
                        CategoryDescription = categoryData.CategoryDescription,
                    };
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Category Created Successfully";
                    return RedirectToAction("Create", "Product");


                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }

            }
            catch (Exception e)
            {

                TempData["errorMessage"] = e.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = new Category()
                    {
                        CategoryId = model.CategoryId,
                        CategoryName = model.CategoryName,
                        CategoryDescription = model.CategoryDescription,
                    };
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                    TempData["successMessage"] = "category Details updated..!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(p => p.CategoryId == Id);
                if (category != null)
                {
                    var categoryView = new CategoryViewModel()
                    {
                        CategoryId = Id,
                        CategoryName = category.CategoryName,
                        CategoryDescription = category.CategoryDescription
                    };
                    return View(categoryView);
                }
                else
                {
                    TempData["errorMessage"] = $"Category Details not available with the Id:{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return RedirectToAction("Index");

            }

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(p => p.CategoryId == Id);
                if (category != null)
                {
                    var categoryView = new CategoryViewModel()
                    {
                        CategoryId = Id,
                        CategoryName = category.CategoryName,
                        CategoryDescription = category.CategoryDescription
                    };
                    return View(categoryView);
                }
                else
                {
                    TempData["errorMessage"] = $"Category Details not available with the Id:{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public IActionResult Delete(CategoryViewModel model)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(x => x.CategoryId == model.CategoryId);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    TempData["successMessage"] = "category is successfully deleted..!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"category Details not available with the Id:{model.CategoryId}";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {

                TempData["errorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}