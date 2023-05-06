using GraduationProject.Abstract;
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

        public IActionResult OrdersList()
        {
            OrdersListViewModel obj = new OrdersListViewModel();
            obj.getAllOrders = _allOrders.Orders;
            obj.orderCategory = "Заказы";
            return View(obj);
        }
    }
}
