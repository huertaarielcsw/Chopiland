using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Data;
using Microsoft.AspNetCore.Identity;
using Chopiland.Models;
using Microsoft.AspNetCore.Authorization;

namespace Chopiland.Controllers
{
    [Authorize(Roles = "Admin, Normal")]
    public class ShoppingCartController : Controller
    {
        private readonly IAnouncementService anouncementService;
        private readonly ICartAnouncementService cartAnouncementService;
        private readonly UserManager<User> userManager;
        private static Random random;
 
        public ShoppingCartController(
            IAnouncementService anouncementService,
            ICartAnouncementService cartAnouncementService,
            UserManager<User> userManager)
        {
            this.anouncementService = anouncementService;
            this.cartAnouncementService = cartAnouncementService;
            this.userManager = userManager;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                return View("Error");
            }
            var shoppingCartAnouncements = cartAnouncementService.GetAll().ToList().Where(s => s.ShoppingCartId == user.Id);
            List<ItemCartViewModel> items = new List<ItemCartViewModel>();
            decimal totalPrice = 0;
            decimal price = 0;
            foreach (var item in shoppingCartAnouncements)
            {                
                var anouncement = anouncementService.GetAnouncement(item.AnouncementId);
                if(anouncement.Offer)
                {
                    price = anouncement.OfferPrice;
                }
                else
                {
                    price = anouncement.Price;
                }
                var itemCart = new ItemCartViewModel
                {
                    Id = anouncement.Id,
                    ImageUrl = anouncement.ImageUrl,
                    Price = price,
                    AnouncementName = anouncement.AnouncementName,
                    Amount = item.Quantity
                };
                items.Add(itemCart);
                totalPrice += price * item.Quantity;
            }
            var shoppingCartVm = new ShoppingCartViewModel { TotalPrice = totalPrice , Items = items};
            return View(shoppingCartVm);
        }

        public ActionResult Delete(int id)
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                return View("Error");
            }
            cartAnouncementService.DeleteShoppingCartAnouncement(user.Id, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Checkout ()
        {
            random = new Random();
            int x = random.Next(0, 10);
            if (x > 0)
            {
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                if (user == null)
                {
                    return View("Error");
                }
                var shoppingCartAnouncements = cartAnouncementService.GetAll().ToList().Where(s => s.ShoppingCartId == user.Id);
                decimal pricetopay = 0;
                decimal totalPrice = 0;
                decimal price = 0;
                List<ItemCartViewModel> items = new List<ItemCartViewModel>();
                foreach (var item in shoppingCartAnouncements)
                {
                   
                    var anouncement = anouncementService.GetAnouncement(item.AnouncementId);
                    if (anouncement.Offer)
                    {
                        price = anouncement.OfferPrice;
                    }
                    else
                    {
                        price = anouncement.Price;
                    }
                    totalPrice += price * item.Quantity;
                    if (anouncement.Amount >= item.Quantity)
                    {
                        anouncement.Amount -= item.Quantity;
                        pricetopay += price * item.Quantity;
                        anouncementService.UpdateAnouncement(anouncement);
                        cartAnouncementService.DeleteShoppingCartAnouncement(user.Id, item.AnouncementId);
                        var itemCart = new ItemCartViewModel
                        {
                            Price = price,
                            AnouncementName = anouncement.AnouncementName,
                            Amount = item.Quantity
                        };
                        items.Add(itemCart);
                    }
                    else if (anouncement.Amount > 0)
                    {
                        pricetopay += anouncement.Amount * price;
                        item.Quantity = item.Quantity - anouncement.Amount;
                        anouncement.Amount = 0;
                        anouncementService.UpdateAnouncement(anouncement);
                        cartAnouncementService.UpdateShoppingCartAnouncement(item);
                        var itemCart = new ItemCartViewModel
                        {
                            Price = price,
                            AnouncementName = anouncement.AnouncementName,
                            Amount = anouncement.Amount
                        };
                        items.Add(itemCart);
                    }
                    
                }
                decimal cash = (decimal)(random.Next((int)totalPrice, (int)totalPrice + 1000));
                OrderCompleteViewModel orderCompleteVm = new OrderCompleteViewModel { Cash = cash, Change = cash - pricetopay, TotalAmount = pricetopay, Items = items };
                return View("OrderComplete", orderCompleteVm);
            }
            return RedirectToAction("Index");
            
        }
        
    }
}