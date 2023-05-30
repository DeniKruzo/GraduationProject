using GraduationProject.Abstract;
using GraduationProject.Areas.Identity.Data;
using GraduationProject.BussinesLogic;
using GraduationProject.Data;
using GraduationProject.Domains;
using GraduationProject.Models;
using GraduationProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data;
using System.Drawing;

namespace GraduationProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IAllOrders _allOrders;
        private readonly IOrderCategory _orderCategory;
        private GraduationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _appEnvironment;
        public SelectList category { get; set; }

        public OrdersController(IAllOrders allOrders, IOrderCategory orderCategory,GraduationDbContext context,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment appEnvironment)
        {
            _allOrders = allOrders;
            _orderCategory = orderCategory;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            category = new SelectList(_context.CategoryOrder.Select(n => n.Name).ToList());
            
        }

        [Route("Orders/ordersList")]
        [Route("Orders/ordersList/{category}")]
        [Authorize(Roles = "specialist")]
        public IActionResult OrdersList(string category)
        {
            string _category = category;
            IQueryable<openOrder> openOrders = null;
            string currCategory = "";

            //рефакторинг тернарным оператором
            //openOrders = category == null ? openOrders = _allOrders.Orders : openOrders = _allOrders.Orders.Where(i => i.CategoryOrder.Name.Equals(category.ToString()));

            if (string.IsNullOrEmpty(category))
            {
                openOrders = _allOrders.Orders;
            }
            else
            {
                if(string.Equals(category.ToString(),category,StringComparison.OrdinalIgnoreCase))
                {
                    openOrders = _allOrders
                        .Orders
                        .Where(i => i.CategoryOrder.Name.Equals(category.ToString()));
                }

                currCategory = _category;
            }
            var modelForOrders = new OrdersListViewModel
            {
                getAllOrders = openOrders,
                orderCategory = currCategory,
            };

            return View(modelForOrders);
        }

        [Route("Orders/MyOrdersList")]
        [Authorize(Roles = "customer")]
        public IActionResult MyOrdersList()
        {
            var userId = _userManager.GetUserId(User);
            var myList = _allOrders.Orders.Where(o => o.CustomerId == userId);
            var model = new OrdersListViewModel
            {
                getAllOrders = myList,
            };
            return View(model);
        }

        [Route("Orders/getMoreInfo/{id:int}")]
        public IActionResult GetMoreInfo(int id)
        {
            var moreInfo = _allOrders.Orders
                .Where(i => i.OrderId == id);

            var modelForSelectOrder = new OrdersListViewModel
            {
                getAllOrders = moreInfo,
                
            };
            return View(modelForSelectOrder);
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
        public IActionResult AddNewOrder()
        {
            var upModel = new UpdateOrdersModel
            {
                SelectedCat = category,
                DeadLine = DateTime.UtcNow,
                
            };
            return View(upModel);
        }
         
        [HttpPost]
        [Authorize(Roles = "customer")]
        public IActionResult AddNewOrder(UpdateOrdersModel model)
        {
            var order = new openOrder
            {
                Name = model.Name,
                ShortDesc = model.ShortDesc,
                LongDesc = model.LongDesc,
                Img = ImageMethods.AddFile(_appEnvironment ,model.formFile),
                Price = model.Price,
                DeadLine = model.DeadLine,
                CustomerId = _userManager.GetUserId(User),
                isOpen = true,
                CategoryOrder = _context.CategoryOrder.First(c=>c.Name == model.GetOrderName)
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            return RedirectToAction("MyOrdersList", "Orders");
        }

        [Authorize(Roles = "customer")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderForEdit = _context.Orders
                .FirstOrDefault(i => i.OrderId == id);

            if (orderForEdit == null) return View("Error");

            var model = new UpdateOrdersModel(orderForEdit)
            {
                SelectedCat = category
            };

            return View(model);
        }

        [Authorize(Roles = "customer")]
        [HttpPost]
        public IActionResult Edit(UpdateOrdersModel model)
        {
            model.CustomerId = _userManager.GetUserId(User);
            model.SelectedCat = category;
            model.Img = ImageMethods.AddFile(_appEnvironment, model.formFile);
            model.CategoryOrder = _context.CategoryOrder.First(c => c.Name == model.GetOrderName);
            model.isOpen = true;
          
            _context.Orders.Update(model);

            _context.SaveChanges();

            return RedirectToAction("MyOrdersList", "Orders");
        }
    }
}
