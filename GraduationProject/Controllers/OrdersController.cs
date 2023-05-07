using GraduationProject.Abstract;
using GraduationProject.Domains;
using GraduationProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IAllOrders _allOrders;
        private readonly IOrderCategory _orderCategory;

        public OrdersController(IAllOrders allOrders, IOrderCategory orderCategory)
        {
            _allOrders = allOrders;
            _orderCategory = orderCategory;
        }

        [Route("Orders/ordersList")]
        [Route("Orders/ordersList/{category}")]
        public IActionResult OrdersList(string category)
        {
            string _category = category;
            IEnumerable<openOrder> openOrders = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category))
            {
                openOrders = _allOrders.Orders;
            }
            else
            {
                if(string.Equals(category.ToString(),category,StringComparison.OrdinalIgnoreCase))
                {
                    openOrders = _allOrders.Orders.Where(i => i.CategoryOrder.Name.Equals(category.ToString()));
                }

                currCategory = _category;

              
            }
            var ordObj = new OrdersListViewModel
            {
                getAllOrders = openOrders,
                orderCategory = currCategory
            };


            return View(ordObj);
        }

        [Route("Orders/getMoreInfo/{id:int}")]
        public IActionResult getMoreInfo(int id)
        {
            var moreInfo = _allOrders.Orders.Where(i => i.OrderId == id);
            var ordObj = new OrdersListViewModel
            {
                getAllOrders = moreInfo,
                
            };
            return View(ordObj);
        }
    }

    
}
