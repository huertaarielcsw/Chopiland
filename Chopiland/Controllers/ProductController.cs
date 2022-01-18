using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Chopiland.Models;
using Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Chopiland.Controllers
{
    [Authorize(Roles = "Admin, Normal")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<User> userManager;

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }
        // GET: Product
        [HttpGet]
        public ActionResult Index(Int64 productCategory, string searchString)
        {
            var products = productService.GetProducts();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString));
            }
            SelectList categories;
            if (productCategory != 0)
            {
                products = products.Where(s => s.CategoryId == productCategory);
                categories = PopulateCategoriesDropDownList(productCategory);
            }
            else
            {
                categories = PopulateCategoriesDropDownList();
            }
            List<ProductViewModel> model = new List<ProductViewModel>();
            products.ToList().ForEach(u =>
            {
                var category = categoryService.GetCategory(u.CategoryId);

                ProductViewModel product = new ProductViewModel
                {
                    Id = u.Id,
                    ProductName = $"{u.ProductName}",
                    CategoryName = $"{category.CategoryName}"
                };
                model.Add(product);
            });
            var productCategoriesVM = new ProductCategoriesViewModel
            {
                Products = model,
                Categories = categories
            };
            return View(productCategoriesVM);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductViewModel model = new ProductViewModel();
            model.CategoryNameSL = PopulateCategoriesDropDownList();

            return View("AddProduct", model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            var category = categoryService.GetCategory(model.CategoryId);
            Product productEntity = new Product
            {
                ProductName = model.ProductName,
                CategoryId = model.CategoryId,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                OwnerID = userManager.GetUserAsync(HttpContext.User).Result.Id
            };
            productService.InsertProduct(productEntity);
            if (productEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            ProductViewModel model = new ProductViewModel();
            if (id.HasValue && id != 0)
            {
                Product productEntity = productService.GetProduct(id.Value);
                model.ProductName = productEntity.ProductName;
                Category category = categoryService.GetCategory(productEntity.CategoryId);
                model.CategoryName = category.CategoryName;
                model.CategoryId = category.Id;
                model.CategoryNameSL = PopulateCategoriesDropDownList(category.Id);
            }
            return View("EditProduct", model);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            Product productEntity = productService.GetProduct(model.Id);
            productEntity.ProductName = model.ProductName;
            productEntity.ModifiedDate = DateTime.UtcNow;
            productEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            productEntity.CategoryId = model.CategoryId;
            productService.UpdateProduct(productEntity);
            if (productEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult DeleteProduct(int id)
        {
            Product product = productService.GetProduct(id);
            string name = $"{product.ProductName}";
            return PartialView("DeleteProduct", name);
        }

        [HttpPost]
        public ActionResult DeleteProduct(long id, IFormCollection form)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public SelectList PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var productsQuery = categoryService.GetCategories().ToList();
            var CategoryNameSL = new SelectList(productsQuery, "Id", "CategoryName", selectedCategory);
            return CategoryNameSL;
        }
    }
}