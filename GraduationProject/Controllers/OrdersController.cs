using GraduationProject.Abstract;
using GraduationProject.Areas.Identity.Data;
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

namespace GraduationProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IAllOrders _allOrders;
        private readonly IOrderCategory _orderCategory;
        private GraduationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IWebHostEnvironment _appEnvironment;


        public OrdersController(IAllOrders allOrders, IOrderCategory orderCategory,GraduationDbContext context,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment appEnvironment)
        {
            _allOrders = allOrders;
            _orderCategory = orderCategory;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
        }

        [Route("Orders/ordersList")]
        [Route("Orders/ordersList/{category}")]
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
                orderCategory = currCategory
            };

            return View(modelForOrders);
        }

        [Route("Orders/getMoreInfo/{id:int}")]
        public IActionResult getMoreInfo(int id)
        {
            var moreInfo = _allOrders.Orders
                .Where(i => i.OrderId == id);

            var modelForSelectOrder = new OrdersListViewModel
            {
                getAllOrders = moreInfo,
                
            };
            return View(modelForSelectOrder);
        }

        public SelectList category { get; set; }

        [HttpGet]
        public IActionResult AddNewOrder()
        {
            category = new SelectList(_context.CategoryOrder.Select(n => n.Name).ToList());
            var upModel = new UpdateOrdersModel
            {
                SelectedCat = category,
                DeadLine = DateTime.UtcNow,
                
            };
            return View(upModel);
        }
         
        [HttpPost]
        [Authorize(Roles = "customer")]
        public IActionResult AddNewOrder(UpdateOrdersModel model, IFormFile uploadedFile)
        {

            var order = new openOrder
            {
                Name = model.Name,
                ShortDesc = model.ShortDesc,
                LongDesc = model.LongDesc,
                Img = AddFile(model.formFile),
                Price = model.Price,
                DeadLine = model.DeadLine,
                CustomerId = _userManager.GetUserId(User),
                isOpen = true,
                CategoryOrder = _context.CategoryOrder.First(c=>c.Name == model.GetOrderName)
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            return RedirectToAction("OrdersList", "Orders");
        }

        [HttpPost]
        public string AddFile(IFormFile uploadedFile)
        {
            string fileName = uploadedFile.FileName;

            return fileName;
        }

    }

    
}
