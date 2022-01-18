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
using System.IO;
using Microsoft.AspNetCore.Identity;
using Chopiland.Authorization;

namespace Chopiland.Controllers
{
    public class AnouncementController : Controller
    {
        private readonly IAnouncementService anouncementService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ICartAnouncementService cartAnouncementService;
        private readonly UserManager<User> userManager;
        private readonly IAuthorizationService authorizationService;

        public AnouncementController(
            IAnouncementService anouncementService,
            IProductService productService,
            ICategoryService categoryService,
            ICartAnouncementService cartAnouncementService,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
        {
            this.anouncementService = anouncementService;
            this.productService = productService;
            this.categoryService = categoryService;
            this.cartAnouncementService = cartAnouncementService;
            this.userManager = userManager;
            this.authorizationService = authorizationService;
        }
        [AllowAnonymous]
        // GET: Anouncement
        [HttpGet]
        public ActionResult Index(string SearchString, Int64 categoryId, Int64 productId)
        {
            List<Anouncement> offersExpired = new List<Anouncement>();
            foreach (var item in anouncementService.GetAnouncements().Where(u => u.Offer == true))
            {
                if (item.ExpirationDate.CompareTo(DateTime.UtcNow) <= 0) 
                {
                    item.Offer = false;
                    item.OfferPrice = 0;
                    item.ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
                    offersExpired.Add(item);
                }
            }
            foreach (var item in offersExpired)
            {
                anouncementService.UpdateAnouncement(item);
            }
            var anouncements = anouncementService.GetAnouncements();
            if (!string.IsNullOrEmpty(SearchString))
            {
                anouncements = anouncements.Where(s => s.AnouncementName.Contains(SearchString));
            }
            SelectList products;
            if (productId != 0)
            {
                anouncements = anouncements.Where(s => s.ProductId == productId);
                products = PopulateProductsDropDownList(productId);
            }
            else
            {
                products = PopulateProductsDropDownList();
            }
            SelectList categories;
            if (categoryId != 0)
            {
                categories = PopulateCategoriesDropDownList(categoryId);
            }
            else
            {
                categories = PopulateCategoriesDropDownList();
            }
            List<AnouncementViewModel> model = new List<AnouncementViewModel>();
            anouncements = anouncements.OrderByDescending(t => t.Offer == true);
            
            anouncements.ToList().ForEach(u =>
            {
                if (categoryId != 0)
                {
                    Product _product = productService.GetProduct(u.ProductId);
                    if (_product.CategoryId == categoryId)
                    {
                        AnouncementViewModel anouncement = new AnouncementViewModel
                        {
                            Id = u.Id,
                            AnouncementName = $"{u.AnouncementName}",
                            ProductName = _product.ProductName,
                            Description = u.Description,
                            Price = u.Price,
                            Amount = u.Amount,
                            ImageUrl = u.ImageUrl,
                            IsOffer = u.Offer,
                            OfferPrice = u.OfferPrice,
                            ExpirationDate = u.ExpirationDate
                        };
                        model.Add(anouncement);
                    }
                }
                else
                {
                    Product _product = productService.GetProduct(u.ProductId);
                    AnouncementViewModel anouncement = new AnouncementViewModel
                    {
                        Id = u.Id,
                        AnouncementName = $"{u.AnouncementName}",
                        ProductName = _product.ProductName,
                        Description = u.Description,
                        Price = u.Price,
                        Amount = u.Amount,
                        ImageUrl = u.ImageUrl,
                        IsOffer = u.Offer,
                        OfferPrice = u.OfferPrice,
                        ExpirationDate = u.ExpirationDate
                    };
                    model.Add(anouncement);
                }
                
            });
            var anouncementProductCategoryVM = new AnouncementProductCategoryViewModel
            {
                Categories = categories,
                Products = products,
                Anuncios = model
            };
            return View(anouncementProductCategoryVM);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpGet]
        public ActionResult AddAnouncement()
        {
            AnouncementViewModel model = new AnouncementViewModel();
            model.ProductNameSL = PopulateProductsDropDownList();

            return View("AddAnouncement", model);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpPost]
        public ActionResult AddAnouncement(AnouncementViewModel model)
        {
            string storePath = "~/images/";
            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                storePath = $"{storePath}índice1.png";
            }
            else
            {
                storePath = $"{storePath}{model.ImageUrl}";
            }
            Product _product = productService.GetProduct(model.ProductId);
            Anouncement anouncementEntity = new Anouncement
            {
                AnouncementName = model.AnouncementName,
                Product = _product,
                Description = model.Description,
                Price = model.Price,
                Amount = model.Amount,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                ProductId = _product.Id,
                ImageUrl = storePath,
                OwnerID = userManager.GetUserAsync(HttpContext.User).Result.Id
            };
            anouncementService.InsertAnouncement(anouncementEntity);
            if (anouncementEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpGet]
        public ActionResult EditAnouncement(int? id)
        {
            AnouncementViewModel model = new AnouncementViewModel();
            if (id.HasValue && id != 0)
            {
                Anouncement anouncementEntity = anouncementService.GetAnouncement(id.Value);
                var isAuthorized = authorizationService.AuthorizeAsync(User,anouncementEntity,Operations.Update);
                if (!isAuthorized.Result)
                {
                    return RedirectToAction("Index");
                }
                Product _product = productService.GetProduct(anouncementEntity.ProductId);
                model.AnouncementName = anouncementEntity.AnouncementName;
                model.ProductName = _product.ProductName;
                model.ProductId = _product.Id;
                model.Description = anouncementEntity.Description;
                model.Price = anouncementEntity.Price;
                model.Amount = anouncementEntity.Amount;
                model.ImageUrl = anouncementEntity.ImageUrl;
                model.ProductNameSL = PopulateProductsDropDownList(_product.Id);
            }
            return View("EditAnouncement", model);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpPost]
        public ActionResult EditAnouncement(AnouncementViewModel model)
        {
            string storePath = "~/images/";
            
            var product = productService.GetProduct(model.ProductId);
            Anouncement anouncementEntity = anouncementService.GetAnouncement(model.Id);
            var isAuthorized = authorizationService.AuthorizeAsync(User, anouncementEntity, Operations.Update);
            if (!isAuthorized.Result)
            {
                return RedirectToAction("Index");
            }
            anouncementEntity.AnouncementName = model.AnouncementName;
            anouncementEntity.Product = product;
            anouncementEntity.ProductId = model.ProductId;
            anouncementEntity.Description = model.Description;
            anouncementEntity.Price = model.Price;
            anouncementEntity.Amount = model.Amount;
            anouncementEntity.ModifiedDate = DateTime.UtcNow;
            anouncementEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                anouncementEntity.ImageUrl = $"{storePath}{model.ImageUrl}";
            }
            anouncementService.UpdateAnouncement(anouncementEntity);
            if (anouncementEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpGet]
        public ActionResult Offer(int? id)
        {
            AnouncementViewModel model = new AnouncementViewModel();
            if (id.HasValue && id != 0)
            {
                Anouncement anouncementEntity = anouncementService.GetAnouncement(id.Value);
                var isAuthorized = authorizationService.AuthorizeAsync(User, anouncementEntity, Operations.Update);
                if (!isAuthorized.Result)
                {
                    return RedirectToAction("Index");
                }
                Product _product = productService.GetProduct(anouncementEntity.ProductId);
                model.AnouncementName = anouncementEntity.AnouncementName;
                model.Price = anouncementEntity.Price;
                model.ImageUrl = anouncementEntity.ImageUrl;
                model.IsOffer = anouncementEntity.Offer;
                model.OfferPrice = anouncementEntity.OfferPrice;
                model.ExpirationDate = anouncementEntity.ExpirationDate;
            }
            return View("Offer", model);
        }

        [Authorize(Roles = "Admin, Normal")]
        [HttpPost]
        public ActionResult Offer(AnouncementViewModel model)
        {
            var product = productService.GetProduct(model.ProductId);
            Anouncement anouncementEntity = anouncementService.GetAnouncement(model.Id);
            var isAuthorized = authorizationService.AuthorizeAsync(User, anouncementEntity, Operations.Update);
            if (!isAuthorized.Result)
            {
                return RedirectToAction("Index");
            }
            if (model.OfferPrice > 0)
            {
                anouncementEntity.ModifiedDate = DateTime.UtcNow;
                anouncementEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                anouncementEntity.Offer = true;
                anouncementEntity.OfferPrice = model.OfferPrice;
                anouncementEntity.ExpirationDate = model.ExpirationDate;
                anouncementService.UpdateAnouncement(anouncementEntity);
            }           
            
            if (anouncementEntity.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Normal")]
        [HttpGet]
        public ActionResult DeleteAnouncement(long id)
        {
            Anouncement anouncement = anouncementService.GetAnouncement(id);
            var isAuthorized = authorizationService.AuthorizeAsync(User, anouncement, Operations.Delete);
            if (!isAuthorized.Result)
            {
                return RedirectToAction("Index");
            }
            string name = $"{anouncement.AnouncementName}";
            return PartialView("DeleteAnouncement", name);
        }
        [Authorize(Roles = "Admin, Normal")]
        [HttpPost]
        public ActionResult DeleteAnouncement(long id, IFormCollection form)
        {
            Anouncement anouncement = anouncementService.GetAnouncement(id);
            var isAuthorized = authorizationService.AuthorizeAsync(User, anouncement, Operations.Delete);
            if (isAuthorized.Result)
            {
                anouncementService.DeleteAnouncement(id);
            }            
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult DetailsAnouncement(int? id)
        {
            AnouncementViewModel model = new AnouncementViewModel();
            if (id.HasValue && id != 0)
            {
                Anouncement anouncementEntity = anouncementService.GetAnouncement(id.Value);
                Product _product = productService.GetProduct(anouncementEntity.ProductId);
                model.Id = anouncementEntity.Id;
                model.AnouncementName = anouncementEntity.AnouncementName;
                model.ProductName = _product.ProductName;
                model.ProductId = _product.Id;
                model.Description = anouncementEntity.Description;
                model.Price = anouncementEntity.Price;
                model.Amount = anouncementEntity.Amount;
                model.ImageUrl = anouncementEntity.ImageUrl;
                model.IsOffer = anouncementEntity.Offer;
                model.OfferPrice = anouncementEntity.OfferPrice;
                model.ExpirationDate = anouncementEntity.ExpirationDate;
            }
            return View("Details", model);
        }

        [Authorize(Roles = "Admin, Normal")]
        [HttpGet]
        public ActionResult AddToCart(long id , int amount)
        {
            Anouncement anouncement = anouncementService.GetAnouncement(id);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                return View("Error");
            }
            ShoppingCartAnouncement shoppingCartAnouncement = new ShoppingCartAnouncement { ShoppingCartId = user.Id, AnouncementId = id, Quantity = amount };
            cartAnouncementService.InsertShoppingCartAnouncement(shoppingCartAnouncement);
            return RedirectToAction("Index");

        }
        public SelectList PopulateProductsDropDownList(object selectedProduct = null)
        {
            var productsQuery = productService.GetProducts().ToList();
            var ProductNameSL = new SelectList(productsQuery, "Id", "ProductName", selectedProduct);
            return ProductNameSL;
        }

        public SelectList PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var productsQuery = categoryService.GetCategories().ToList();
            var CategoryNameSL = new SelectList(productsQuery, "Id", "CategoryName", selectedCategory);
            return CategoryNameSL;
        }
    }
}