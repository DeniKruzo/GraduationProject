using GraduationProject.Domains;

namespace GraduationProject.Models.ViewModel
{
    public class OrdersListViewModel
    {
        public IEnumerable<openOrder> getAllOrders { get; set; }

        public string orderCategory { get; set; }
    }
}
