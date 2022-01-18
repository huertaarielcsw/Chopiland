using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Identity;
using Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chopiland.Models;
using Microsoft.AspNetCore.Authorization;

namespace Chopiland.Controllers
{
    [Authorize(Roles = "Admin, Normal")]
    public class AuctionController : Controller
    {
        private readonly IAnouncementService anouncementService;
        private readonly IAuctionService auctionService;
        private readonly IAuctionUserService auctionUserService;
        private readonly UserManager<User> userManager;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;


        public AuctionController(
            IAnouncementService anouncementService,
            IAuctionService auctionService,
            IAuctionUserService auctionUserService,
            UserManager<User> userManager,
            IProductService productService,
            ICategoryService categoryService)
        {
            this.anouncementService = anouncementService;
            this.auctionService = auctionService;
            this.auctionUserService = auctionUserService;
            this.userManager = userManager;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        // GET: Auction
        public ActionResult Index(string SearchString, Int64 categoryId, Int64 productId)
        {
            List<Auction> auctionsExpired = new List<Auction>();
            foreach (var item in auctionService.GetAuctions())
            {
                if (item.FinalDate.CompareTo(DateTime.UtcNow) <= 0)
                {
                    auctionsExpired.Add(item);
                }
            }
            foreach (var item in auctionsExpired)
            {
                auctionService.DeleteAuction(item.Id);
            }
            var auctions = auctionService.GetAuctions();
            var anouncements = anouncementService.GetAnouncements();
            if (!string.IsNullOrEmpty(SearchString))
            {
                auctions = auctions.Where(s => anouncementService.GetAnouncement(s.AnouncementId).AnouncementName.Contains(SearchString));
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
            List<Int64> announcementsId = new List<Int64>();
            foreach (var item in anouncements)
            {
                announcementsId.Add(item.Id);
            }
            auctions = auctions.Where(s => announcementsId.Contains(s.AnouncementId));
            List<AuctionViewModel> model = new List<AuctionViewModel>();
            auctions.ToList().ForEach(u =>
            {
                Anouncement announcement = anouncementService.GetAnouncement(u.AnouncementId);
                Product product = productService.GetProduct(announcement.ProductId);
                var auctionUser = auctionUserService.GetAll().ToList().Where(s => s.AuctionId == u.Id).OrderByDescending(t => t.BidPrice);
                decimal lastBid = 0;
                if (auctionUser.Count() != 0)
                {
                    lastBid = auctionUser.First().BidPrice;
                }
                if (categoryId != 0)
                {
                    if (product.CategoryId == categoryId)
                    {
                        AuctionViewModel auction = new AuctionViewModel
                        {
                            Id = u.Id,
                            AnouncementName = $"{announcement.AnouncementName}",
                            Description = announcement.Description,
                            InitialPrice = u.InitialPrice,
                            ImageUrl = announcement.ImageUrl,
                            FinalDate = u.FinalDate,
                            LastBid = lastBid
                        };
                        model.Add(auction);
                    }
                }
                else
                {
                    AuctionViewModel auction = new AuctionViewModel
                    {
                        Id = u.Id,
                        AnouncementName = $"{announcement.AnouncementName}",
                        Description = announcement.Description,
                        InitialPrice = u.InitialPrice,
                        ImageUrl = announcement.ImageUrl,
                        FinalDate = u.FinalDate,
                        LastBid = lastBid
                    };
                    model.Add(auction);
                }              
            });
            var auctionCPVM = new AuctionCPViewModel
            {
                items = model,
                Categories = categories,
                Products = products
            };

            return View(auctionCPVM);
        }

        [HttpGet]
        public ActionResult Bid(int? id)
        {
            if (id.HasValue && id != 0)
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                if (user == null)
                {
                    return View("Error");
                }
                var auctionUser = auctionUserService.GetAll().ToList().Where(s => s.AuctionId == id).OrderByDescending(t => t.BidPrice);
                List<BidsUsersViewModel> items = new List<BidsUsersViewModel>();
                foreach (var item in auctionUser)
                {
                    User _user = userManager.FindByIdAsync(item.UserId).Result;
                    BidsUsersViewModel bidsUser = new BidsUsersViewModel { User = _user.UserName, Bid = item.BidPrice };
                    items.Add(bidsUser);
                }
                BiddingViewModel biddingVM;
                if (items.Count() > 0)
                {
                   biddingVM = new BiddingViewModel { BidsUsers = items, LastBid = items.First().Bid};
                }
                else
                {
                    var auction = auctionService.GetAuction((long)id);
                   biddingVM = new BiddingViewModel { BidsUsers = items, LastBid = auction.InitialPrice };
                }
                
                return View("Bid",biddingVM);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(BiddingViewModel model)
        {
            Anouncement anouncement = anouncementService.GetAnouncement(model.Id);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                return View("Error");
            }
            if(model.LastBid >= model.NewBid)
            {
                return RedirectToAction("Bid");
            }
            AuctionUser auctionUser = auctionUserService.GetAuctionUser(user.Id, model.Id);
            if (auctionUser == null)
            {
                auctionUser = new AuctionUser { UserId = user.Id, AuctionId = model.Id, BidPrice = model.NewBid };
                auctionUserService.InsertAuctionUser(auctionUser);
            }
            else
            {
                auctionUser.BidPrice = model.NewBid;
                auctionUserService.UpdateAuctionUser(auctionUser);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddAuction()
        {
            AuctionViewModel model = new AuctionViewModel();
            model.AnouncementNameSL = PopulateAnouncementsDropDownList();

            return View("AddAuction", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuction(AuctionViewModel model)
        {
            Anouncement announcement = anouncementService.GetAnouncement(model.AnouncementId);
            Auction auction = new Auction
            {
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                OwnerID = userManager.GetUserAsync(HttpContext.User).Result.Id,
                AnouncementId = announcement.Id,
                FinalDate = model.FinalDate,
                InitialPrice = model.InitialPrice
            };
            auctionService.InsertAuction(auction);
            if (auction.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult DeleteAuction(int id)
        {
            Auction auction = auctionService.GetAuction(id);
            string name = $"{anouncementService.GetAnouncement(auction.AnouncementId).AnouncementName}";
            return PartialView("DeleteAuction", name);
        }

        [HttpPost]
        public ActionResult DeleteAuction(long id, IFormCollection form)
        {
            auctionService.DeleteAuction(id);
            return RedirectToAction("Index");
        }

        public SelectList PopulateAnouncementsDropDownList(object selectedAnnouncement = null)
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var announcementsQuery = anouncementService.GetAnouncements().ToList().Where(x => x.OwnerID == user.Id);
            var AnnouncementNameSL = new SelectList(announcementsQuery, "Id", "AnouncementName", selectedAnnouncement);
            return AnnouncementNameSL;
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