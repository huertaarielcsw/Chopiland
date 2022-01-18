using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Data;
using Chopiland.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Chopiland.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            categoryService.GetCategories().ToList().ForEach(u =>
            {
                string majorCategoryName = "";
                if(u.CategoryId.HasValue)
                {
                    var majorCategory = categoryService.GetCategory(u.CategoryId.Value);
                    majorCategoryName = majorCategory.CategoryName;
                }
                
                CategoryViewModel Category = new CategoryViewModel
                {
                    Id = u.Id,
                    Category = $"{u.CategoryName}",
                    MajorCategory = $"{majorCategoryName}"
                };
                model.Add(Category);
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            CategoryViewModel model = new CategoryViewModel();
            model.CategoryNameSL = PopulateCategoriesDropDownList();

            return View("AddCategory", model);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel model)
        {
            Category majorCategory = null;
            Int64 ? categoryId = null;
            if (model.CategoryId.HasValue)
            {
                majorCategory = categoryService.GetCategory(model.CategoryId.Value);
                categoryId = majorCategory.Id;
            }
            Category categoryEntity = new Category
            {
                CategoryId = categoryId,
                CategoryName = model.Category,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            categoryService.InsertCategory(categoryEntity);
            if (categoryEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            CategoryViewModel model = new CategoryViewModel();
            if (id.HasValue && id != 0)
            {
                Category categoryEntity = categoryService.GetCategory(id.Value);
                model.Category = categoryEntity.CategoryName;
                Int64? majorCategoryId = null;
                string majorCategoryName = "";
                if (categoryEntity.CategoryId.HasValue)
                {
                    var majorCategory = categoryService.GetCategory(categoryEntity.CategoryId.Value);
                    majorCategoryId = majorCategory.Id;
                    majorCategoryName = majorCategory.CategoryName;
                }
                model.CategoryId = majorCategoryId;
                model.MajorCategory = majorCategoryName;
                if (majorCategoryId.HasValue)
                {
                    model.CategoryNameSL = PopulateCategoriesDropDownList(majorCategoryId.Value, model.Category);
                }
                else
                {
                    model.CategoryNameSL = PopulateCategoriesDropDownList(null, model.Category);
                }            
            }
            return View("EditCategory", model);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel model)
        {
            Category categoryEntity = categoryService.GetCategory(model.Id);
            categoryEntity.CategoryName = model.Category;
            categoryEntity.ModifiedDate = DateTime.UtcNow;//ver q pasa cuando quito padre y lo dejo null
            categoryEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            categoryEntity.CategoryId = model.CategoryId;
            categoryService.UpdateCategory(categoryEntity);
            if (categoryEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult DeleteCategory(int id)
        {
            Category category = categoryService.GetCategory(id);
            string name = $"{category.CategoryName}";
            return PartialView("DeleteCategory", name);
        }

        [HttpPost]
        public ActionResult DeleteCategory(long id, IFormCollection form)
        {
            Category category = categoryService.GetCategory(id);
            var children = categoryService.GetCategories().ToList().Where(x => x.CategoryId.HasValue && x.CategoryId.Value == category.Id);
            foreach (var item in children)
            {
                categoryService.DeleteCategory(item.Id);
            }
            categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        public SelectList PopulateCategoriesDropDownList(object selectedCategory = null,string categoryName = "")
        {
            var productsQuery = categoryService.GetCategories().ToList().Where(x => x.CategoryName != categoryName);
            var CategoryNameSL = new SelectList(productsQuery, "Id", "CategoryName", selectedCategory);
            return CategoryNameSL;
        }
    }
}