using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductSystem.DAL;
using ProductSystem.Models;

namespace ProductSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            this._context = context;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            
            var skip = (pageNumber - 1) * pageSize;

           
            var products = await _context.Products
                                       .Include(p => p.Category)
                                       .Skip(skip)  
                                       .Take(pageSize)  
                                       .ToListAsync();  

            List<ProductViewModel> productList = new List<ProductViewModel>();

            if (products != null)
            {
                foreach (var product in products)
                {
                    var productViewModel = new ProductViewModel()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category?.CategoryName
                    };
                    productList.Add(productViewModel);
                }
            }

         
            var totalProducts = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

           
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(productList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductModel productData)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Product product = new Product();

                    product.ProductName = productData.ProductName;
                    product.ProductDescription = productData.ProductDescription;
                    product.CategoryId = productData.CategoryId;             
                        
                  
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Product Created Successfully";
                    return RedirectToAction("Index");

                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                    TempData["errorMessage"] = "Model data is not valid";
                   // ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", producyData.CategoryId);
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productData.CategoryId);

                    return View(productData);
                }

            }
            catch (Exception e)
            {

                TempData["errorMessage"] = e.Message;
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productData.CategoryId);

                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(ProductEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product()
                    {
                        ProductId = model.ProductId,
                        ProductName = model.ProductName,
                        ProductDescription = model.ProductDescription,
                        CategoryId = model.CategoryId
                    };
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Product Details updated..!";
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
                var product = _context.Products.SingleOrDefault(p => p.ProductId == Id);
                if (product != null)
                {
                    var productView = new ProductEditModel()
                    {
                        ProductId = Id,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        CategoryId = product.CategoryId
                    };
                    return View(productView);
                }
                else
                {
                    TempData["errorMessage"] = $"Product Details not available with the Id:{Id}";
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
                var product = _context.Products.SingleOrDefault(p => p.ProductId == Id);
                if (product != null)
                {
                    var productView = new ProductViewModel()
                    {
                        ProductId = Id,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription
                    };
                    return View(productView);
                }
                else
                {
                    TempData["errorMessage"] = $"Product Details not available with the Id:{Id}";
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
        public IActionResult Delete(ProductViewModel model)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(x => x.ProductId == model.ProductId);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Product is successfully deleted..!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Product Details not available with the Id:{model.ProductId}";
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
