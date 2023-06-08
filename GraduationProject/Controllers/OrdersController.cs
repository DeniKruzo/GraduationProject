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
using System.Linq;

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
        private int _pageSize = 2;
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

        [Route("OrdersList")]
        [Route("OrdersList/{gate}")]
        [Route("OrdersList/{bug}/{gate:int}")]
        [Authorize(Roles = "specialist")]
        public IActionResult OrdersList(string bug, int gate = 1)
        {
            var ordersWithCategory = bug == null ? _allOrders.Orders 
                : _allOrders.Orders
                    .Where(i => i.CategoryOrder.Name.Equals(bug.ToString())); 
       
            var orders = ordersWithCategory
               .Skip((gate - 1) * _pageSize)
               .Take(_pageSize);

            var pagesQuantity = (int)(
                Math.Ceiling(ordersWithCategory.Count() / (float)_pageSize));

            var modelForOrders = new OrdersListViewModel
            {
                getAllOrders = orders,
                orderCategory = bug,
                CurrentPage = gate,
                PagesQuantity = pagesQuantity
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
